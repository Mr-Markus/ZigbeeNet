using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using ZigBeeNet.Logging;

namespace ZigBeeNet.Internal
{
    public class ZigBeeCommandNotifier
    {
        private readonly ILog _logger = LogProvider.For<ZigBeeCommandNotifier>();

        private ConcurrentBag<IZigBeeCommandListener> _commandListeners;

        public ZigBeeCommandNotifier()
        {
            _commandListeners = new ConcurrentBag<IZigBeeCommandListener>();
        }

        public void AddCommandListener(IZigBeeCommandListener commandListener)
        {
            _commandListeners.Add(commandListener);
        }

        public void RemoveCommandListener(IZigBeeCommandListener commandListener)
        {
            _commandListeners.TryTake(out _);
        }

        public void NotifyCommandListeners(ZigBeeCommand command)
        {
            // Enumeration is thread-safe in ConcurrentBag http://dotnetpattern.com/csharp-concurrentbag
            foreach (IZigBeeCommandListener commandListener in _commandListeners)
            {
                Task.Run(() =>
                {
                    try
                    {
                        commandListener.CommandReceived(command);
                    }
                    catch(Exception ex)
                    {
                        _logger.ErrorException("Error during the notification of commandListeners.", ex);
                    }
                    
                });
            }
        }
    }
}
