using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Logging;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Serial
{
    public class ZigBeeSerialPort : IZigBeePort
    {
        private readonly ILog _logger = LogProvider.For<ZigBeeSerialPort>();

        private SerialPort _serialPort;

        private Task _reader;

        private CancellationTokenSource _cancellationToken;

       // private ManualResetEventSlim _readResetEvent;

        /**
        * The circular fifo queue for receive data
        */
        private byte[] _buffer = new byte[512];

        /**
        * The receive buffer end pointer (where we put the newly received data)
        */
        private int _end = 0;

        /**
         * The receive buffer start pointer (where we take the data to pass to the application)
         */
        private int _start = 0;

        /**
         * The length of the receive buffer
         */
        private int _maxLength = 512;

        /**
         * Synchronisation object for buffer queue manipulation
         */
        private object _bufferSynchronisationObject = new object();

        public string PortName { get; set; }

        public int Baudrate { get; set; }

        public bool IsOpen { get => _serialPort != null && _serialPort.IsOpen ? true : false; }

        public ZigBeeSerialPort(string portName, int baudrate = 115200)
        {
            PortName = portName;
            Baudrate = baudrate;

            _serialPort = new SerialPort(portName, baudrate);
            _cancellationToken = new CancellationTokenSource();
            //_readResetEvent = new ManualResetEventSlim(false);
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

            //_readResetEvent.Set();
            //_readResetEvent.Dispose();
        }

        public bool Open()
        {
            try
            {
                return Open(115200);
            }
            catch (Exception e)
            {
                _logger.Warn("Unable to open serial port: " + e.Message);
                return false;
            }
        }

        public bool Open(int baudrate)
        {
            Baudrate = baudrate;

            bool success = false;

            _logger.Debug("Opening port {Port} at {Baudrate} baud.", PortName, baudrate);

            _serialPort = new SerialPort(PortName, baudrate);

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
                _logger.Debug("{Exception} - Error opening port {Port}\n{Port}", ex.GetType().Name, PortName, ex.Message);
            }

            if (_serialPort.IsOpen)
            {
                // serialPort.DataReceived event for receiving data is not working under Linux/Mono

                // Start Reader Task
                _reader = new Task(ReaderTask, _cancellationToken.Token);

                _reader.Start();

                //TODO: ConnectionStatusChanged event
            }

            return success;
        }

        public void PurgeRxBuffer()
        {
            _start = 0;
            _end = 0;
        }

        public byte? Read()
        {
            return Read(9999999);
        }

        public byte? Read(int timeout)
        {
            long endTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + timeout;

            try
            {
                while (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() < endTime)
                {
                    lock (_bufferSynchronisationObject)
                    {
                        if (_start != _end)
                        {
                            byte value = _buffer[_start++];

                            if (_start >= _maxLength)
                            {
                                _start = 0;
                            }

                            return value;
                        }
                    }

                    lock (_serialPort)
                    {
                        if (_serialPort == null)
                        {
                            return null;
                        }

                        //_readResetEvent.Wait(TimeSpan.FromMilliseconds(endTime - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()));
                        //Task.Delay(endTime - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error while reading byte from serial port");
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

                    _logger.Debug("Write data to serialport: {Data}", BitConverter.ToString(value));
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Error while writing to serial port");
                }
            }
        }

        private void ReaderTask()
        {
            while (IsOpen && _cancellationToken.IsCancellationRequested == false)
            {
                int len = 0;

                try
                {
                    len = _serialPort.BytesToRead;
                    if (len > 0)
                    {
                        byte[] message = new byte[len];

                        int readbytes = 0;
                        while (_serialPort.Read(message, readbytes, len - readbytes) <= 0) ;

                        lock (_bufferSynchronisationObject)
                        {
                            //Array.Copy(message, _buffer, message.Length);
                            foreach (byte recv in message)
                            {
                                _buffer[_end++] = recv;
                                if (_end >= _maxLength)
                                {
                                    _end = 0;
                                }
                            }
                        }

                        //lock (_serialPort)
                        //{
                        //    _readResetEvent.Set();
                        //}
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Error while reading from serial port");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
