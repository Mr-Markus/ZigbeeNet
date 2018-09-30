using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigbeeNet
{
    public class ZigbeeService : IDisposable
    {
        private bool _disposed;
        private bool _isRunning;

        public ZigbeeController Controller { get; set; }

        public event EventHandler<byte[]> NewPacket;
        public event EventHandler Ready;

        public ZigbeeService(IZnp znp)
        {
            Controller = new ZigbeeController(this, znp);
        }

        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!_disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Note disposing has been done.
                _disposed = true;
            }
        }

        private void Setup(Action callback)
        {
            //Controller.st
        }

        /// <summary>
        /// Starts the zigbee service
        /// </summary>
        public void Start()
        {
            Setup(() =>
            {
                _isRunning = true;

                Ready(this, EventArgs.Empty);
            });
        }

        /// <summary>
        /// Stops the zigbee service
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
        }

        /// <summary>
        /// Resets the zigbee service
        /// </summary>
        public void Reset()
        {
            throw new NotImplementedException();
        }

        public Frame Send()
        {
            throw new NotImplementedException();
        }

        private void Send(byte[] data)
        {
            throw new NotImplementedException();
        }

        public void OnNewPacket(byte[] data)
        {
            NewPacket(this, data);
        }

        /// <summary>
        /// Permits devices to join the zigbee network
        /// </summary>
        /// <param name="time">Time in seconds</param>
        public void PermitJoining(int time)
        {
            if(time > 255 || time < 0)
            {
                throw new ArgumentOutOfRangeException("time", "Given value for 'time' have to be greater than 0 and less than 255");
            }
        }

        private void Read(byte[] data)
        {
            throw new NotImplementedException();
        }        
    }
}
