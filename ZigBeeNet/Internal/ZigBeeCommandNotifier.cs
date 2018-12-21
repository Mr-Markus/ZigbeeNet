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


        // TODO: REMOVE THIS AFTER FIX MULTI THREADING ISSUES
        public bool HasObject(IZigBeeCommandListener listener)
        {
            foreach (var item in _commandListeners)
            {
                if (item == listener)
                    return true;
            }

            return false;
        }

        public void AddCommandListener(IZigBeeCommandListener commandListener)
        {
            if (commandListener is ZigBeeTransaction)
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
            // Enumeration is thread-safe in ConcurrentBag http://dotnetpattern.com/csharp-concurrentbag
            foreach (IZigBeeCommandListener commandListener in _commandListeners)
            {
                Task.Run(() =>
                {
                    //try
                    //{
                        commandListener.CommandReceived(command);
                    //}
                    //catch (Exception ex)
                    //{
                    //    var x = "";
                    //}
                });
            }
        }
    }
}
