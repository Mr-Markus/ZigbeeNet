/*
The MIT License (MIT)

Copyright (c) 2019 David Karlaš <david.karlas@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using ZigBeeNet;
using ZigBeeNet.Security;
using ZigBeeNet.Transport;

namespace ZigbeeNet.Hardware.ConBee
{
    public class ZigbeeDongleConBee : IZigBeeTransportTransmit
    {
        private ConBeeInterface _conbeeInterface;
        private IZigBeeTransportReceive _zigBeeTransportReceive;

        public ZigbeeDongleConBee(IZigBeePort serialPort)
        {
            _conbeeInterface = new ConBeeInterface(serialPort);
        }

        public string VersionString { get; set; }
        public IeeeAddress IeeeAddress { get; set; }
        public ushort NwkAddress { get; set; }

        public ZigBeeStatus Initialize()
        {
            _conbeeInterface.Initialize();

            _conbeeInterface.NetworkStateChanged += networkStateChanged;
            _conbeeInterface.ConfigurationFlagChanged += configurationChanged;
            _conbeeInterface.PacketRecieved += Process;

            VersionString = _conbeeInterface.GetVersionAsync().Result;
            IeeeAddress = new IeeeAddress(_conbeeInterface.MacAddress);
            NwkAddress = _conbeeInterface.NwkAddress;
            _conbeeInterface.SendAsync(Commands.DEVICE_STATE, 0, 0, 0);
            return ZigBeeStatus.SUCCESS;
        }

        SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        public async void Process()
        {
            await semaphore.WaitAsync();
            try
            {
                if (_conbeeInterface.ApsIndicationFlag)
                {
                    await ProcessDataIndication().ConfigureAwait(false);
                }
                else if (_conbeeInterface.ApsConfirmFlag)
                {
                    await _conbeeInterface.SendAsync(Commands.APS_DATA_CONFIRM, 0x00, 0x00).ConfigureAwait(false);
                }
                else if (_conbeeInterface.ApsRequestFreeSlotsFlag)
                {
                    if (ApsRequestsQueue.TryDequeue(out var buffer))
                        await _conbeeInterface.SendAsync(Commands.APS_DATA_REQUEST, buffer).ConfigureAwait(false);
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Exception while Process");
            }
            finally
            {
                semaphore.Release();
            }
        }

        private async Task ProcessDataIndication()
        {
            var data = await _conbeeInterface.SendAsync(Commands.APS_DATA_INDICATION, 0, 0).ConfigureAwait(false);
            var apsFrame = new ZigBeeApsFrame();
            int offset = 8;
            switch ((DestinationAddressMode)data[offset++])
            {
                case DestinationAddressMode.GroupAddress:
                case DestinationAddressMode.NwkAddress:
                    apsFrame.DestinationAddress = ParseUInt16(data, ref offset);
                    break;
                case DestinationAddressMode.IeeeAddress:
                    apsFrame.DestinationIeeeAddress = new IeeeAddress(BitConverter.ToUInt64(data, offset));
                    offset += 8;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            apsFrame.DestinationEndpoint = data[offset++];
            switch ((DestinationAddressMode)data[offset++])
            {
                case DestinationAddressMode.IeeeAddress:
                    //apsFrame.SourceIeeeAddress = new IeeeAddress(BitConverter.ToUInt64(data, offset));
                    offset += 8;
                    break;
                case DestinationAddressMode.NwkAddress:
                    apsFrame.SourceAddress = ParseUInt16(data, ref offset);
                    break;
                case DestinationAddressMode.NwkAddressAndIeeeAddress:
                    apsFrame.SourceAddress = ParseUInt16(data, ref offset);
                    //apsFrame.SourceIeeeAddress = new IeeeAddress(BitConverter.ToUInt64(data, offset));
                    offset += 8;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            apsFrame.SourceEndpoint = data[offset++];
            apsFrame.Profile = ParseUInt16(data, ref offset);
            apsFrame.Cluster = ParseUInt16(data, ref offset);
            apsFrame.Payload = new byte[ParseUInt16(data, ref offset)];
            Buffer.BlockCopy(data, offset, apsFrame.Payload, 0, apsFrame.Payload.Length);
            _zigBeeTransportReceive.ReceiveCommand(apsFrame);
        }

        ushort ParseUInt16(byte[] data, ref int offset)
        {
            var val = BitConverter.ToUInt16(data, offset);
            offset += 2;
            return val;
        }

        private void configurationChanged()
        {
            Log.Debug("ConfigurationChanged");
        }

        private void networkStateChanged()
        {
            Log.Debug($"NetworkStateChanged:{_conbeeInterface.NetworkState}");
        }

        ConcurrentQueue<byte[]> ApsRequestsQueue = new ConcurrentQueue<byte[]>();

        public void SendCommand(ZigBeeApsFrame apsFrame)
        {
            var offset = 0;
            var len = apsFrame.Payload.Length + 14;
            if (apsFrame.AddressMode == ZigBeeNwkAddressMode.Group)
            {
                len += 2;
            }
            else
            {
                if (apsFrame.DestinationIeeeAddress == null)
                {
                    len += 3;
                }
                else
                {
                    len += 9;
                }
            }
            var buffer = new byte[len];
            len -= 2;//Remove 2 lenght bytes
            buffer[offset++] = (byte)len;
            buffer[offset++] = (byte)(len >> 8);

            buffer[offset++] = apsFrame.ApsCounter;
            buffer[offset++] = 0;//Flags
            if (apsFrame.AddressMode == ZigBeeNwkAddressMode.Group)
            {
                buffer[offset++] = (byte)DestinationAddressMode.GroupAddress;
                buffer[offset++] = (byte)apsFrame.DestinationAddress;
                buffer[offset++] = (byte)(apsFrame.DestinationAddress >> 8);
            }
            else
            {
                if (apsFrame.DestinationIeeeAddress == null)
                {
                    buffer[offset++] = (byte)DestinationAddressMode.NwkAddress;
                    buffer[offset++] = (byte)apsFrame.DestinationAddress;
                    buffer[offset++] = (byte)(apsFrame.DestinationAddress >> 8);
                }
                else
                {
                    buffer[offset++] = (byte)DestinationAddressMode.IeeeAddress;
                    buffer[offset++] = (byte)apsFrame.DestinationIeeeAddress.Value;
                    buffer[offset++] = (byte)(apsFrame.DestinationIeeeAddress.Value >> 8);
                    buffer[offset++] = (byte)(apsFrame.DestinationIeeeAddress.Value >> 16);
                    buffer[offset++] = (byte)(apsFrame.DestinationIeeeAddress.Value >> 24);
                    buffer[offset++] = (byte)(apsFrame.DestinationIeeeAddress.Value >> 32);
                    buffer[offset++] = (byte)(apsFrame.DestinationIeeeAddress.Value >> 40);
                    buffer[offset++] = (byte)(apsFrame.DestinationIeeeAddress.Value >> 48);
                    buffer[offset++] = (byte)(apsFrame.DestinationIeeeAddress.Value >> 56);
                }
                buffer[offset++] = apsFrame.DestinationEndpoint;
            }

            buffer[offset++] = (byte)apsFrame.Profile;
            buffer[offset++] = (byte)(apsFrame.Profile >> 8);

            buffer[offset++] = (byte)apsFrame.Cluster;
            buffer[offset++] = (byte)(apsFrame.Cluster >> 8);

            buffer[offset++] = (byte)apsFrame.SourceEndpoint;

            buffer[offset++] = (byte)apsFrame.Payload.Length;
            buffer[offset++] = (byte)(apsFrame.Payload.Length >> 8);

            Buffer.BlockCopy(apsFrame.Payload, 0, buffer, offset, apsFrame.Payload.Length);
            offset += apsFrame.Payload.Length;

            buffer[offset++] = 0x04;

            buffer[offset] = (byte)apsFrame.Radius;

            ApsRequestsQueue.Enqueue(buffer);
            Process();
        }

        public ZigBeeKey TcLinkKey => throw new NotImplementedException();

        public ZigBeeStatus SetTcLinkKey(ZigBeeKey key)
        {
            throw new NotImplementedException();
        }

        public ZigBeeChannel ZigBeeChannel => (ZigBeeChannel)(1 << _conbeeInterface.CurrentChannel);

        public ZigBeeStatus SetZigBeeChannel(ZigBeeChannel channel)
        {
            _conbeeInterface.ChannelMask = (uint)channel;
            return ZigBeeStatus.SUCCESS;
        }

        public ExtendedPanId ExtendedPanId => new ExtendedPanId((long)_conbeeInterface.NwkExtendedPanId);

        public ZigBeeStatus SetZigBeeExtendedPanId(ExtendedPanId panId)
        {
            _conbeeInterface.ApsExtendedPanId = panId.Value;
            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeKey ZigBeeNetworkKey => new ZigBeeKey(_conbeeInterface.NetworkKey);

        public ZigBeeStatus SetZigBeeNetworkKey(ZigBeeKey key)
        {
            _conbeeInterface.NetworkKey = key.Key;
            return ZigBeeStatus.SUCCESS;
        }

        public ushort PanID => _conbeeInterface.NwkPanId;

        public ZigBeeStatus SetZigBeePanId(ushort panId)
        {
            return ZigBeeStatus.UNSUPPORTED;
        }

        public void SetZigBeeTransportReceive(IZigBeeTransportReceive zigBeeTransportReceive)
        {
            _zigBeeTransportReceive = zigBeeTransportReceive;
        }

        public void Shutdown()
        {
            _conbeeInterface.Dispose();
        }

        public ZigBeeStatus Startup(bool reinitialize)
        {
            return ZigBeeStatus.SUCCESS;
        }

        public void UpdateTransportConfig(TransportConfig configuration)
        {
            throw new NotImplementedException();
        }
    }
}
