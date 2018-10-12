using System;
using System.Collections.Generic;
using System.IO;
using ZigbeeNet;
using UnpiNet;
using System.Diagnostics;
using ZigbeeNet.CC.Commands;
using System.Threading;
using ZigbeeNet.ZCL;
using BinarySerialization;

namespace ZigbeeNet.CC
{
    public class CCZnp
    {
        //TODO: get and set to data destination e.g. database or textfile
        //public Dictionary<ulong, Device> Devices { get; set; }

        public bool Enabled { get; set; }
        public Unpi Unpi { get; set; }

        public event EventHandler Ready;
        public event EventHandler Closed;
        public event EventHandler ResetDone;

        public event EventHandler<ZpiObject> AsyncResponse;
        
        private List<ZpiObject> _txQueue;
        private bool _spinLock;

        private bool _reset;
        private bool _resetting {
            get
            {
                return _reset;
            }
            set
            {
                int dueTime = Timeout.Infinite;
                int period = Timeout.Infinite;

                if (value == true)
                {
                    dueTime = 30000;
                    period = 30000;
                }

                _resetTimeout = new Timer((object state) =>
                    {
                        if(_resetting)
                        {
                            // if AREQ:SYS:RESET does not return in 30 sec
                            // release the lock to avoid the requests from enqueuing
                            _spinLock = false;
                        }
                    }, null, dueTime, period);
                
                
                _reset = value;
            }
        }
        private Timer _resetTimeout;
        private Timer _timeout;
        private ZpiObject _sreqRunning;

        public CCZnp()
        {
            _txQueue = new List<ZpiObject>();
        }

        public void Init(string port, int baudrate = 115200)
        {
            ZpiMeta.Init();
            ZdoMeta.Init();

            Unpi = new Unpi(port, baudrate, 1);
            Unpi.DataReceived += Unpi_DataReceived;
            Unpi.Opened += Unpi_Opened;
            Unpi.Closed += Unpi_Closed;

            Unpi.Open();
        }

        private void Unpi_Closed(object sender, EventArgs e)
        {
            Stop();
        }

        private void Unpi_Opened(object sender, EventArgs e)
        {
            Ready?.Invoke(this, EventArgs.Empty);
        }

        private void Unpi_DataReceived(object sender, Packet e)
        {
            if ((byte)e.Type == (byte)MessageType.AREQ)
            {
                if ((e.SubSystem == UnpiNet.SubSystem.RPC_SYS_SYS && e.Cmd1 == 0)
                || (e.SubSystem == UnpiNet.SubSystem.RPC_SYS_SAPI && e.Cmd1 == 9))
                {
                    //Reset done
                    _resetting = false;
                    _spinLock = false;

                    ResetDone?.Invoke(this, EventArgs.Empty);

                    return;
                }

                if (_sreqRunning != null && _sreqRunning.IndObject != null 
                    && (byte)_sreqRunning.IndObject.SubSystem == (byte)e.SubSystem 
                    && _sreqRunning.IndObject.CommandId == e.Cmd1)
                {
                    _timeout.Change(Timeout.Infinite, Timeout.Infinite);

                    _resetting = false;

                    ZpiObject zpiObject = new ZpiObject((SubSystem)e.SubSystem, (MessageType)e.Type, e.Cmd1);
                    zpiObject.Parsed += (object s, ZpiObject result) =>
                    {
                        AsyncResponse?.Invoke(this, result);

                        // schedule next transmission if something in txQueue
                        ScheduleNextSend(zpiObject);
                    };
                }
                else
                {
                    ZpiObject zpiObject = new ZpiObject((SubSystem)e.SubSystem, (MessageType)e.Type, e.Cmd1);
                    zpiObject.Parsed += (object s, ZpiObject result) =>
                    {
                        AsyncResponse?.Invoke(this, result);
                    };
                    zpiObject.Parse((MessageType)e.Type, e.Length, e.Payload);
                }
            }
            else if((byte)e.Type == (byte)MessageType.SRSP)
            {
                //TODO: Check if _sreqRunning has a response object. if not then clear all flags and schedule next. else check if srsp is sreq object

                if(_sreqRunning != null &&_sreqRunning.IndObject == null) {
                    _timeout.Change(Timeout.Infinite, Timeout.Infinite);

                    _resetting = false;

                    ZpiObject zpiObject = new ZpiObject((SubSystem)e.SubSystem, (MessageType)e.Type, e.Cmd1);
                    zpiObject.Parsed += (object s, ZpiObject result) =>
                    {
                        if (_sreqRunning != null)
                        {
                            _sreqRunning.Response(result);
                        }

                        // schedule next transmission if something in txQueue
                        ScheduleNextSend(result);
                    };
                    zpiObject.Parse((MessageType)e.Type, e.Length, e.Payload);
                }
            }
        }

        public void Start()
        {
            Enabled = true;
            Unpi.Open();
        }

        public void Stop()
        {
            Enabled = false;
            _txQueue.Clear();
            Unpi = null;

            Closed?.Invoke(this, EventArgs.Empty);
        }

        public byte[] SendCommand(MessageType type, SubSystem subSystem, byte commandId, byte[] payload)
        {
            return Unpi.Send((int)type, (int)subSystem, commandId, payload);
        }

        public byte[] Request(SubSystem subSystem, byte cmdId, ArgumentCollection reqestArgs)
        {
            if (Unpi == null)
            {
                throw new NullReferenceException("CCZnp has not been initialized yet");
            }

            ZpiObject zpiObject = new ZpiObject(subSystem, cmdId);
            zpiObject.RequestArguments = reqestArgs;

            return Request(zpiObject);
        }

        public byte[] Request(ZpiObject zpiObject)
        {
            if(_txQueue.Count == 0 && _spinLock == true)
            {
                _spinLock = false;
            }
            if (_spinLock)
            {
                _txQueue.Add(zpiObject);

                return null;
            }

            //prepare for transmission
            _spinLock = true;

            if (zpiObject.Type == MessageType.SREQ)
            {
                SendSREQ(zpiObject);
            }
            else if (zpiObject.Type == MessageType.AREQ)
            {
                return SendAREQ(zpiObject);
            }

            return null;
        }

        private void SendSREQ(ZpiObject zpiObject)
        {
            _timeout = new Timer((object state) =>
            {
                if(_sreqRunning != null)
                {
                    _sreqRunning = null;
                    _timeout.Change(Timeout.Infinite, Timeout.Infinite);

                    if (state is ZpiObject)
                    {
                        ZpiObject zpi = state as ZpiObject;
                        throw new TimeoutException($"Request timeout: {zpi.Type.ToString()}:{zpi.SubSystem.ToString()}:{zpi.Name}");
                    }
                    else
                    {
                        throw new TimeoutException("Request timeout");
                    }
                }
            }, zpiObject, 300000, 300000); //TODO: Get timeout by config

            _sreqRunning = zpiObject;

            byte[] data = Unpi.Send((int)MessageType.SREQ, (int)zpiObject.SubSystem, zpiObject.CommandId, zpiObject.Frame);
                       
            _timeout.Change(Timeout.Infinite, Timeout.Infinite);

            // schedule next transmission if something in txQueue
            ScheduleNextSend();

            _resetting = false;
        }

        private byte[] SendAREQ(ZpiObject zpiObject)
        {
            if((zpiObject.SubSystem == SubSystem.SYS && zpiObject.CommandId == 0) 
                || (zpiObject.SubSystem == SubSystem.SAPI && zpiObject.CommandId == 9)) //resetReq or systemReset
            {
                _resetting = true;

                // clear all pending requests, since the system is reset
                _txQueue.Clear();

                this.AsyncResponse += (object sender, ZpiObject e) =>
                {
                    if (e.Type == MessageType.AREQ && e.SubSystem == SubSystem.SYS && e.CommandId == (byte)SYS.resetInd)
                    {
                        _spinLock = false;
                        _resetting = false;
                    }
                };
            } else
            {
                ScheduleNextSend();
            }

            return Unpi.Send((int)MessageType.AREQ, (int)zpiObject.SubSystem, zpiObject.CommandId, zpiObject.Frame);
        }

        private void ScheduleNextSend(ZpiObject current = null)
        {
            if (current != null && _sreqRunning != null)
            {
                _sreqRunning.Response(current);
            }

            _spinLock = false;

            if (_txQueue.Count > 0)
            {
                ZpiObject next = _txQueue[0];

                _txQueue.RemoveAt(0);
                
                Request(next);
            }

            _sreqRunning = null;
        }

        private void ParseIncomingData(ZpiObject request, byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
            {
                throw new NullReferenceException("Buffer is empty");
            }

            if (buffer[0] != 0xfe) //Fix SOF
            {
                throw new FormatException("Buffer is not a vailid frame");
            }

            var serializer = new BinarySerializer();

            List<Packet> packets = new List<Packet>();

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                packets.AddRange(serializer.Deserialize<List<Packet>>(stream));
            }
            foreach (Packet packet in packets)
            {
                if (packet.FrameCheckSequence.Equals(packet.Checksum) == false)
                {
                    throw new Exception("Received FCS is not equal with new packet");
                }

                ZpiObject result = new ZpiObject((SubSystem)packet.SubSystem, (MessageType)packet.Type, packet.Cmd1);
                result.Parse((MessageType)packet.Type, packet.Length, packet.Payload);

                request.Response(result);
            }
        }
    }
}
