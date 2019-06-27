using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Transport;
using Serilog;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ZigBeeNet.Tranport.SerialPort
{
    public class ZigBeeSerialPort : IZigBeePort
    {
        private System.IO.Ports.SerialPort _serialPort;

        private Task _reader;

        private CancellationTokenSource _cancellationToken;

        private BlockingCollection<byte> _fifoBuffer = new BlockingCollection<byte>(new ConcurrentQueue<byte>());

        public string PortName { get; set; }

        public int Baudrate { get; set; }

        public bool IsOpen { get => _serialPort != null && _serialPort.IsOpen ? true : false; }

        public ZigBeeSerialPort(string portName, int baudrate = 115200)
        {
            PortName = portName;
            Baudrate = baudrate;

            _serialPort = new System.IO.Ports.SerialPort(portName, baudrate);
            _cancellationToken = new CancellationTokenSource();
        }

        public void Close()
        {
            if (_cancellationToken != null)
            {
                _cancellationToken.Cancel();
            }

            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _serialPort.Dispose();
                _serialPort = null;
            }
        }

        public bool Open()
        {
            try
            {
                return Open(115200);
            }
            catch (Exception e)
            {
                Log.Warning("Unable to open serial port: " + e.Message);
                return false;
            }
        }

        public bool Open(int baudrate)
        {
            Baudrate = baudrate;

            bool success = false;

            Log.Debug("Opening port {Port} at {Baudrate} baud.", PortName, baudrate);

            _serialPort = new System.IO.Ports.SerialPort(PortName, baudrate);

            try
            {
                bool tryOpen = true;

                if (Environment.OSVersion.Platform.ToString().StartsWith("Win") == false)
                {
                    tryOpen = (tryOpen && File.Exists(PortName));
                }
                if (tryOpen)
                {
                    _serialPort.Open();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                Log.Debug("{Exception} - Error opening port {Port}\n{Port}", ex.GetType().Name, PortName, ex.Message);
            }

            if (_serialPort.IsOpen)
            {
                // Start Reader Task
                _reader = new Task(ReaderTask, _cancellationToken.Token);

                _reader.Start();

                // TODO: ConnectionStatusChanged event
            }

            return success;
        }

        public void PurgeRxBuffer()
        {
            /*
             *  The enumeration represents a moment-in-time snapshot of the contents of the queue. It does not reflect any updates to the collection after GetEnumerator was called.
             *  The enumerator is safe to use concurrently with reads from and writes to the queue.
             *  https://docs.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentqueue-1.getenumerator?view=netframework-4.8
             */
            while (_fifoBuffer.Count > 0)
            {
                _fifoBuffer.TryTake(out byte item);
            }
        }

        public byte? Read()
        {
            return Read(9999999);
        }

        public byte? Read(int timeout)
        {
            try
            {
                /* This blocks until data available (Producer Consumer pattern) */
                var notTimedOut = _fifoBuffer.TryTake(out byte value, timeout);

                if (notTimedOut)
                {
                    return value;
                }
                else
                {
                    return null; ;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Error while reading byte from serial port");
            }
            return null;
        }

        public void Write(byte[] value)
        {
            if (_serialPort == null)
                return;

            if (IsOpen)
            {
                try
                {
                    _serialPort.Write(value, 0, value.Length);

                    Log.Debug("Write data to serialport: {Data}", BitConverter.ToString(value));
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error while writing to serial port");
                }
            }
        }

        private void ReaderTask()
        {
            var message = new byte[512];

            while (IsOpen && _cancellationToken.IsCancellationRequested == false)
            {
                try
                {
                    var n = _serialPort.Read(message, 0, message.Length); // read may return anything from 0 - length , 0 = end of stream
                    if (n == 0) break;

                    for (int i = 0; i < n; i++)
                    {
                        _fifoBuffer.Add(message[i]);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error while reading from serial port");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
