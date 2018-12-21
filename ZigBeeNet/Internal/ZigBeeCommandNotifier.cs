using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.Transaction;

namespace ZigBeeNet.Internal
{
    public class ZigBeeCommandNotifier
    {
        private ConcurrentBag<IZigBeeCommandListener> _commandListeners;

        public ZigBeeCommandNotifier()
        {
            _commandListeners = new ConcurrentBag<IZigBeeCommandListener>();
        }

            
        public void AddCommandListener(IZigBeeCommandListener commandListener)
        {
            if(commandListener is ZigBeeTransaction)
            {
                var x = "";
            }

            _commandListeners.Add(commandListener);
        }

        public void RemoveCommandListener(IZigBeeCommandListener commandListener)
        {
            var removed = _commandListeners.TryTake(out commandListener);

            if (!removed)
            {
                var x = "";
            }
            else
            {
                var x = "";
            }
        }

        public void NotifyCommandListeners(ZigBeeCommand command)
        {
            lock (_commandListeners)
            {
                foreach (IZigBeeCommandListener commandListener in _commandListeners)
                {
                    Task.Run(() => commandListener.CommandReceived(command));
                }
            }
        }
    }
}
