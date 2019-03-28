using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZigBeeNet.Logging;

namespace ZigBeeNet.Internal
{
    public class ZigBeeCommandNotifier
    {
        private readonly ILog _logger = LogProvider.For<ZigBeeCommandNotifier>();
        private readonly object _lock = new object();

        private List<IZigBeeCommandListener> _commandListeners;

        public ZigBeeCommandNotifier()
        {
            _commandListeners = new List<IZigBeeCommandListener>();
        }

        public void AddCommandListener(IZigBeeCommandListener commandListener)
        {
            lock (_lock)
            {
                _commandListeners.Add(commandListener);
            }
        }

        public void RemoveCommandListener(IZigBeeCommandListener commandListener)
        {
            lock (_lock)
            {
                _commandListeners.Remove(commandListener);
            }
        }

        public void NotifyCommandListeners(ZigBeeCommand command)
        {
            var tmp = new List<IZigBeeCommandListener>(_commandListeners);

            foreach (IZigBeeCommandListener commandListener in tmp)
            {
                Task.Run(() =>
                {
                    try
                    {
                        commandListener.CommandReceived(command);
                    }
                    catch (Exception ex)
                    {
                        _logger.ErrorException("Error during the notification of commandListeners.", ex);
                    }

                });
            }
        }
    }
}
