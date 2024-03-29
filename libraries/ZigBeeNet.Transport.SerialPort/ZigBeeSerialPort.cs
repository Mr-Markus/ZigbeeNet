﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Transport;
using ZigBeeNet.Util;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace ZigBeeNet.Tranport.SerialPort
{
    public class ZigBeeSerialPort : IZigBeePort
    {
        private static ILogger _logger = LogManager.GetLog<ZigBeeSerialPort>();
        private System.IO.Ports.SerialPort _serialPort;

        private Task _reader;

        private CancellationTokenSource _cancellationToken;

        private BlockingCollection<byte> _fifoBuffer = new BlockingCollection<byte>(new ConcurrentQueue<byte>());

        public string PortName { get; private set; }

        public int Baudrate { get; private set; }

        public FlowControl FlowControl { get; private set; }


        public bool IsOpen { get => _serialPort != null && _serialPort.IsOpen ? true : false; }

        public ZigBeeSerialPort(string portName, int baudrate = 115200, FlowControl flowControl = FlowControl.FLOWCONTROL_OUT_NONE)
        {
            PortName = portName;
            Baudrate = baudrate;
            FlowControl = flowControl;

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
            return Open(Baudrate);
        }

        public bool Open(int baudrate)
        {
            return Open(baudrate, FlowControl);
        }

        public bool Open(int baudrate, FlowControl flowControl)
        {
            bool success = false;
            try
            {
                Baudrate = baudrate;
                FlowControl = flowControl;

                _logger.LogDebug("Opening port {Port} at {Baudrate} baud with {FlowControl}", PortName, Baudrate, FlowControl);

                _serialPort = new System.IO.Ports.SerialPort(PortName, baudrate);
                _serialPort.WriteTimeout = 3000;
                //_serialPort.Handshake = System.IO.Ports.Handshake.XOnXOff;

                try
                {
                    bool tryOpen = true;

                    if (!Environment.OSVersion.Platform.ToString().StartsWith("Win"))
                    {
                        tryOpen = (tryOpen && File.Exists(PortName));
                    }
                    if (tryOpen)
                    {
                        _serialPort.Open();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogDebug("Error opening port {Port}: {Exception}", PortName, ex.Message);
                }

                if (_serialPort.IsOpen)
                {
                    // Start Reader Task
                    _reader = Task.Factory.StartNew(ReaderTask, _cancellationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                    success = true;
                    // TODO: ConnectionStatusChanged event
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("Unable to open serial port: {Exception}", e.Message);
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
                _logger.LogError(e, "Error while reading byte from serial port: {Exception}", e.Message);
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

                    //Log.Debug("Write data to serialport: {Data}", BitConverter.ToString(value));
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error while writing to serial port: {Exception}", e.Message);
                }
            }
        }

        private void ReaderTask()
        {
            var message = new byte[512];

            while (IsOpen && !_cancellationToken.IsCancellationRequested)
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
                    if (!_cancellationToken.IsCancellationRequested)
                    {
                        _logger.LogError(e, "Error while reading from serial port: {Exception}", e.Message);
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}
