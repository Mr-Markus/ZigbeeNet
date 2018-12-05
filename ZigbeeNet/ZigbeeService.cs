using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigBeeNet.CC;

namespace ZigBeeNet
{
    public class ZigBeeService : IDisposable
    {
        private bool _disposed;
        private bool _isRunning;
        
        private ConcurrentDictionary<ulong, ZigBeeNode> _deviceInfoList = new ConcurrentDictionary<ulong, ZigBeeNode>();

        public IHardwareChannel Controller { get; set; }

        public event EventHandler OnReady;

        public ZigBeeService(Options options)
        {
            Controller = new CCZnp(options);

            Controller.Started += Controller_Started;
        }

        private void Controller_Started(object sender, EventArgs e)
        {
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

        public void PermitJoining(int time)
        {
            Controller.PermitJoinAsync(time);
        }
    }
}
