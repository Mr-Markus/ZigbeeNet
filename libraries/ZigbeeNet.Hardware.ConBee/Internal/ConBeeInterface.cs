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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using ZigBeeNet.Transport;

namespace ZigbeeNet.Hardware.ConBee
{
    class ConBeeInterface : IDisposable
    {
        private IZigBeePort serialPort;
        private Slip slip;
        private Thread thread;
        private int sequenceNumber;
        private ConcurrentDictionary<byte, TaskCompletionSource<(StatusCodes status, byte[] data)>> responses = new ConcurrentDictionary<byte, TaskCompletionSource<(StatusCodes status, byte[] data)>>();
        private bool exit;

        public ConBeeInterface(IZigBeePort serialPort)
        {
            this.serialPort = serialPort;
        }

        public void Initialize()
        {
            serialPort.Open();
            var desc = new Slip.Descriptor()
            {
                buf = new byte[1024],
                buf_size = 1024,
                recv_message = SlipRecvMessage,
                write_byte = SlipWriteByte
            };
            slip = new Slip(desc);
            thread = new Thread(new ThreadStart(ReadLoop));
            thread.Name = "ConBee read loop";
            thread.IsBackground = true;
            thread.Start();
        }

        public async Task<byte[]> SendAsync(Commands command, params byte[] payload)
        {
            var packetLength = payload.Length + 5;
            var packetLengthWithCrc = packetLength + 2;
            var arr = new byte[packetLengthWithCrc];
            arr[0] = (byte)command;
            var seq = (byte)Interlocked.Increment(ref sequenceNumber);
            arr[1] = seq;
            arr[2] = 0;//Reserved
            arr[3] = (byte)(0xff & packetLength);
            arr[4] = (byte)(packetLength >> 8);
            Buffer.BlockCopy(payload, 0, arr, 5, payload.Length);
            Crc(arr, packetLength);
            var taskSource = new TaskCompletionSource<(StatusCodes status, byte[] data)>();
            responses[seq] = taskSource;
            slip.slip_send_message(arr, packetLengthWithCrc);
            Log.Debug("Slip Send:" + BitConverter.ToString(arr, 0, packetLengthWithCrc));
            var (status, data) = await taskSource.Task;
            if (status == StatusCodes.BUSY)
            {
                Log.Debug("Dongle is busy, resending");
                return await SendAsync(command, payload);
            }
            if (status == StatusCodes.SUCCESS)
            {
                return data;
            }
            throw new Exception(status.ToString());
        }

        private void ReadLoop()
        {
            try
            {
                while (!exit)
                {
                    var b = serialPort.Read();
                    if (b == null)
                        Log.Warning("SerialPort returned null");
                    slip.slip_read_byte(b.Value);
                }
                serialPort.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in ConBee interface ReadLoop: {Exception}");
            }
        }

        private void SlipWriteByte(byte[] data, int offset, int len)
        {
            serialPort.Write(data.Skip(offset).Take(len).ToArray());
        }

        private void SlipRecvMessage(byte[] data, int len)
        {
            Log.Debug("Slip Recv:" + BitConverter.ToString(data, 0, len));

            if (len < 7)
            {
                Log.Error("Packet too small");
                return;
            }
            if (!CheckCrc(data, len - 2))
            {
                Log.Warning("Wrong CRC");
                return;
            }
            var command = (Commands)data[0];
            byte? flags = null;
            if (command == Commands.DEVICE_STATE || command == Commands.DEVICE_STATE_CHANGED)
                flags = data[5];
            if (command == Commands.APS_DATA_INDICATION ||
                command == Commands.APS_DATA_REQUEST ||
                command == Commands.APS_DATA_CONFIRM)
                flags = data[7];
            if (flags.HasValue)
            {
                NetworkState = (NetworkStates)(flags & 0x3);
                ApsConfirmFlag = (flags & 0x04) > 0;
                ApsIndicationFlag = (flags & 0x08) > 0;
                ConfigurationFlag = (flags & 0x10) > 0;
                ApsRequestFreeSlotsFlag = (flags & 0x20) > 0;
            }
            if (Commands.DEVICE_STATE_CHANGED != command)
            {
                if (responses.TryRemove(data[1], out var taskSource))
                {
                    var status = (StatusCodes)data[2];
                    if (status == StatusCodes.SUCCESS)
                    {
                        taskSource.SetResult((status, data.Take(len).ToArray()));
                    }
                    else
                    {
                        taskSource.SetResult((status, data.Take(len).ToArray()));
                    }
                }
            }
            PacketRecieved?.Invoke();
        }

        static void Crc(byte[] buffer, int len)
        {
            ushort crc = 0;
            for (var i = 0; i < len; i++)
            {
                crc += buffer[i];
            }
            crc = (ushort)(~crc + 1);
            buffer[len] = (byte)crc;
            buffer[len + 1] = (byte)(crc >> 8);
        }

        static bool CheckCrc(byte[] buffer, int len)
        {
            ushort crc = 0;
            for (var i = 0; i < len; i++)
            {
                crc += buffer[i];
            }
            crc = (ushort)(~crc + 1);
            return buffer[len] == (byte)crc && buffer[len + 1] == (byte)(crc >> 8);
        }

        public bool ApsRequestFreeSlotsFlag { get; private set; }
        public bool ApsConfirmFlag { get; private set; }
        public bool ApsIndicationFlag { get; private set; }

        private NetworkStates networkState;
        public NetworkStates NetworkState
        {
            get => networkState;
            private set
            {
                if (networkState == value)
                    return;
                networkState = value;
                NetworkStateChanged?.Invoke();
            }
        }

        private bool configurationFlag;
        public bool ConfigurationFlag
        {
            get => configurationFlag;
            private set
            {
                if (configurationFlag == value)
                    return;
                configurationFlag = value;
                ConfigurationFlagChanged?.Invoke();
            }
        }

        public event Action NetworkStateChanged;
        public event Action ConfigurationFlagChanged;
        public event Action PacketRecieved;

        public void Dispose()
        {
            exit = true;
        }

        private byte[] ReadParameter(Parameters parameter)
        {
            var data = SendAsync(Commands.READ_PARAMETER, 1, 0, (byte)parameter).Result;
            var len = BitConverter.ToUInt16(data, 5);
            return data.Skip(8).Take(len).ToArray();
        }

        private void WriteParameter(Parameters parameter, params byte[] data)
        {
            var payloadLength = 1 + data.Length;
            var buffer = new byte[data.Length + 3];
            buffer[0] = (byte)payloadLength;
            buffer[1] = (byte)(payloadLength >> 8);
            buffer[2] = (byte)parameter;
            Buffer.BlockCopy(data, 0, buffer, 3, data.Length);
            SendAsync(Commands.WRITE_PARAMETER, buffer).Wait();
        }

        public ulong MacAddress
        {
            get
            {
                return BitConverter.ToUInt64(ReadParameter(Parameters.MacAddress), 0);
            }
        }

        public ushort NwkPanId
        {
            get
            {
                return BitConverter.ToUInt16(ReadParameter(Parameters.NwkPanId), 0);
            }
        }

        public ushort NwkAddress
        {
            get
            {
                return BitConverter.ToUInt16(ReadParameter(Parameters.NwkAddress), 0);
            }
        }

        public ulong NwkExtendedPanId
        {
            get
            {
                return BitConverter.ToUInt64(ReadParameter(Parameters.NwkExtendedPanId), 0);
            }
        }

        public byte ApsDesignedCoordinator
        {
            get
            {
                return ReadParameter(Parameters.ApsDesignedCoordinator)[0];
            }
            set
            {
                WriteParameter(Parameters.ApsDesignedCoordinator, value);
            }
        }

        public uint ChannelMask
        {
            get
            {
                return BitConverter.ToUInt32(ReadParameter(Parameters.ChannelMask), 0);
            }
            set
            {
                WriteParameter(Parameters.ChannelMask, BitConverter.GetBytes(value));
            }
        }

        public ulong ApsExtendedPanId
        {
            get
            {
                return BitConverter.ToUInt64(ReadParameter(Parameters.ApsExtendedPanId), 0);
            }
            set
            {
                WriteParameter(Parameters.ApsExtendedPanId, BitConverter.GetBytes(value));
            }
        }

        public ulong TrustCenterAddress
        {
            get
            {
                return BitConverter.ToUInt64(ReadParameter(Parameters.TrustCenterAddress), 0);
            }
            set
            {
                WriteParameter(Parameters.TrustCenterAddress, BitConverter.GetBytes(value));
            }
        }

        public byte SecurityMode
        {
            get
            {
                return ReadParameter(Parameters.SecurityMode)[0];
            }
            set
            {
                WriteParameter(Parameters.SecurityMode, value);
            }
        }

        public byte[] NetworkKey
        {
            get
            {
                return ReadParameter(Parameters.NetworkKey);
            }
            set
            {

                WriteParameter(Parameters.NetworkKey, value);
            }
        }

        public byte CurrentChannel
        {
            get
            {
                return ReadParameter(Parameters.CurrentChannel)[0];
            }
        }

        public ushort ProtocolVersion
        {
            get
            {
                return BitConverter.ToUInt16(ReadParameter(Parameters.ProtocolVersion), 0);
            }
        }

        public byte NwkUpdateId
        {
            get
            {
                return ReadParameter(Parameters.NwkUpdateId)[0];
            }
            set
            {
                WriteParameter(Parameters.NwkUpdateId, value);
            }
        }

        public uint WatchdogTTL
        {
            get
            {
                return BitConverter.ToUInt32(ReadParameter(Parameters.WatchdogTTL), 0);
            }
            set
            {
                WriteParameter(Parameters.WatchdogTTL, BitConverter.GetBytes(value));
            }
        }

        public async Task<string> GetVersionAsync()
        {
            var response = await SendAsync(Commands.VERSION).ConfigureAwait(false);
            string result;
            switch (response[6])
            {
                case 0x05:
                    result = "ConBee or RaspBee";
                    break;
                case 0x07:
                    result = "ConBee II";
                    break;
                default:
                    result = $"Unknown({response[6]})";
                    break;
            }
            result += $" Major:{response[8]}";
            result += $" Minor:{response[7]}";
            return result;
        }
    }
}