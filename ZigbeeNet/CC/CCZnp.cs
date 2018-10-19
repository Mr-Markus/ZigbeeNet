using System;
using System.Collections.Generic;
using System.IO;
using ZigbeeNet;
using UnpiNet;
using System.Diagnostics;
using System.Threading;
using ZigbeeNet.ZCL;
using BinarySerialization;
using System.Collections.Concurrent;
using ZigbeeNet.CC.Logging;

namespace ZigbeeNet.CC
{
    public class CCZnp
    {
        private readonly ILog _logger = LogProvider.For<CCZnp>();


        public bool Enabled { get; set; }
        public Unpi Unpi { get; set; }

        public event EventHandler Ready;
        public event EventHandler Closed;
        public event EventHandler ResetDone;

        public event EventHandler<ZpiObject> AsyncResponse;
        
        private ConcurrentQueue<ZpiObject> _requestQueue;

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
                            _sreqRunning = null;
                            ZpiObject ignore = new ZpiObject();
                            _requestQueue.TryDequeue(out ignore);
                        }
                    }, null, dueTime, period);
                
                
                _reset = value;
            }
        }
        private Timer _resetTimeout;
        private Timer _timeout;
        private ZpiSREQ _sreqRunning;

        public CCZnp()
        {
            _requestQueue = new ConcurrentQueue<ZpiObject>();
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
            //Log.Information("{@Packet}", e);
            if (_sreqRunning != null && _sreqRunning.IndObject != null
                    && (byte)_sreqRunning.IndObject.SubSystem == (byte)e.SubSystem
                    && _sreqRunning.IndObject.CommandId == e.Cmd1)
            {
                _timeout.Change(Timeout.Infinite, Timeout.Infinite);

                _resetting = false;

                _sreqRunning.IndObject.OnParsed += (object s, ZpiObject result) =>
                {
                    _logger.Info("{Type} - {SubSystem} - {Name}", e.Type, result.SubSystem, result.Name);

                    ZpiObject current = _sreqRunning;
                    
                    // schedule next transmission if something in txQueue
                    ScheduleNextSend();

                    current.Response(result);
                };
                _sreqRunning.IndObject.Parse((MessageType)e.Type, e.Length, e.Payload);

                return;
            }
            
            if ((byte)e.Type == (byte)MessageType.AREQ)
            {
                if ((e.SubSystem == UnpiNet.SubSystem.RPC_SYS_SYS && e.Cmd1 == 0)
                || (e.SubSystem == UnpiNet.SubSystem.RPC_SYS_SAPI && e.Cmd1 == 9))
                {
                    //Reset done
                    _resetting = false;

                    ResetDone?.Invoke(this, EventArgs.Empty);

                    return;
                }
                ZpiObject zpiObject = new ZpiObject((SubSystem)e.SubSystem, (MessageType)e.Type, e.Cmd1);
                zpiObject.OnParsed += (object s, ZpiObject result) =>
                {
                    _logger.Info("{Type} - {SubSystem} - {Name}", e.Type, result.SubSystem, result.Name);

                    AsyncResponse?.Invoke(this, result);
                };
                zpiObject.Parse((MessageType)e.Type, e.Length, e.Payload);
            }
            else if ((byte)e.Type == (byte)MessageType.SRSP)
            {
                if (_sreqRunning != null)
                {
                    if ((byte)_sreqRunning.SubSystem == (byte)e.SubSystem && _sreqRunning.CommandId == e.Cmd1)
                    {
                        // Status Response
                        _timeout.Change(Timeout.Infinite, Timeout.Infinite);

                        _resetting = false;

                        _sreqRunning.OnParsed += (object s, ZpiObject result) =>
                        {
                            ZpiSREQ sREQ = (ZpiSREQ)result;

                            _logger.Info("{Type} - {SubSystem} - {Name} - {Status}", e.Type, sREQ.SubSystem, sREQ.Name, sREQ.Status);

                            if (_sreqRunning.IndObject == null)
                            {
                                ZpiObject current = _sreqRunning;

                                // schedule next transmission if something in txQueue
                                ScheduleNextSend();

                                current.Response(result);
                            }
                        };
                        _sreqRunning.Parse((MessageType)e.Type, e.Length, e.Payload);
                    }
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
            _requestQueue = new ConcurrentQueue<ZpiObject>();
            Unpi = null;

            Closed?.Invoke(this, EventArgs.Empty);
        }

        public byte[] SendCommand(MessageType type, SubSystem subSystem, byte commandId, byte[] payload)
        {
            return Unpi.Send((int)type, (int)subSystem, commandId, payload);
        }

        public void Request(SubSystem subSystem, byte cmdId, ArgumentCollection reqestArgs)
        {
            if (Unpi == null)
            {
                throw new NullReferenceException("CCZnp has not been initialized yet");
            }

            ZpiObject zpiObject = new ZpiObject(subSystem, cmdId);
            zpiObject.RequestArguments = reqestArgs;

            Request(zpiObject);
        }

        public void Request(ZpiObject zpiObject)
        {
            _logger.Info("{Type} - {SubSystem} - {Name}", zpiObject.Type, zpiObject.SubSystem, zpiObject.Name);

            if (_sreqRunning != null)
            {
                _requestQueue.Enqueue(zpiObject);

                return;
            }

            //prepare for transmission

            if (zpiObject.Type == MessageType.SREQ)
            {
                ZpiSREQ zpiSREQ = new ZpiSREQ(zpiObject);
                SendSREQ(zpiSREQ, true);
            }
            else if (zpiObject.Type == MessageType.AREQ)
            {
                SendAREQ(zpiObject);
            }
        }

        public void SendSREQ(ZpiSREQ zpiObject, bool queueDone = true)
        {
            //prepare for transmission
            if(queueDone == false)
            {
                _logger.Info("{Type} - {SubSystem} - {Name}", zpiObject.Type, zpiObject.SubSystem, zpiObject.Name);

                if (_sreqRunning != null)
                {
                    _requestQueue.Enqueue(zpiObject);

                    return;
                }
            }
            _sreqRunning = zpiObject;

            _timeout = new Timer((object state) =>
            {
                if(_sreqRunning != null)
                {
                    _sreqRunning = null;
                    _timeout.Change(Timeout.Infinite, Timeout.Infinite);

                    if (state is ZpiSREQ)
                    {
                        ZpiSREQ zpi = state as ZpiSREQ;
                        throw new TimeoutException($"Request timeout: {zpi.Type.ToString()}:{zpi.SubSystem.ToString()}:{zpi.Name}");
                    }
                    else
                    {
                        throw new TimeoutException("Request timeout");
                    }
                }
            }, zpiObject, 300000, 300000); //TODO: Get timeout by config            

            Unpi.Send((int)MessageType.SREQ, (int)zpiObject.SubSystem, zpiObject.CommandId, zpiObject.Frame);
        }

        public void SendAREQ(ZpiObject zpiObject)
        {
            if (_sreqRunning != null)
            {
                _requestQueue.Enqueue(zpiObject);

                return;
            }

            if ((zpiObject.SubSystem == SubSystem.SYS && zpiObject.CommandId == 0) 
                || (zpiObject.SubSystem == SubSystem.SAPI && zpiObject.CommandId == 9)) //resetReq or systemReset
            {
                _resetting = true;

                // clear all pending requests, since the system is reset
                _requestQueue = new ConcurrentQueue<ZpiObject>();

                this.AsyncResponse += (object sender, ZpiObject e) =>
                {
                    if (e.Type == MessageType.AREQ && e.SubSystem == SubSystem.SYS && e.CommandId == (byte)SYS.SysCommand.resetInd)
                    {
                        _resetting = false;
                    }
                };
            }

            Unpi.Send((int)MessageType.AREQ, (int)zpiObject.SubSystem, zpiObject.CommandId, zpiObject.Frame);
        }

        private void ScheduleNextSend()
        {
            _sreqRunning = null;

            ZpiObject next = new ZpiObject();
            if (_requestQueue.TryDequeue(out next)) { 
                Request(next);
            }
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
