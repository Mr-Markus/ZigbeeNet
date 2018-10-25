using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigbeeNet.CC;

namespace ZigbeeNet
{
    public class ZigbeeService : IDisposable
    {
        private bool _disposed;
        private bool _isRunning;

        private EventBridge _eventBridge;

        private ConcurrentDictionary<ulong, Device> _deviceInfoList = new ConcurrentDictionary<ulong, Device>();

        public ZigbeeController Controller { get; set; }

        public event EventHandler OnReady;
        public event EventHandler PermitJoining;

        public ZigbeeService(Options options)
        {
            Controller = new ZigbeeController(this, options);

            _eventBridge = new EventBridge(Controller);

            Controller.Started += Controller_Started;
        }

        private void Controller_Started(object sender, EventArgs e)
        {
            Controller.PermitJoin(255);

            OnReady?.Invoke(this, EventArgs.Empty);
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

        public void Ready()
        {
            OnReady?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Starts the zigbee service
        /// </summary>
        public void Start()
        {
            Controller.Start();
        }

        /// <summary>
        /// Stops the zigbee service
        /// </summary>
        public void Stop()
        {
            if (_isRunning)
            {
                Controller.Stop();

                _isRunning = false;
            }
        }

        /// <summary>
        /// Resets the zigbee service
        /// </summary>
        public void Reset()
        {
            throw new NotImplementedException();
        }

        private void Read(byte[] data)
        {
            throw new NotImplementedException();
        }        
    }
}
