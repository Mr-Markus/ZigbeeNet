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

            if(commandListener == null)
            {
                // Hier stimmt gehörig was nicht :D .. in NotifyCommandListeners() wird nun eine NullRefEx geschmissen :D
                var x = "";
            }

            if(_commandListeners.TryPeek(out commandListener))
            {
                // Da im java proj mit HashSet gearbeitet wird -> keine duplicates
                return;
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

                    var countBefore = _commandListeners.Count;

                    commandListener.CommandReceived(command);

                    var countAfter = _commandListeners.Count;

                    if(commandListener is ZigBeeTransaction trans)
                    {
                        if (trans.IsTransactionMatch)
                        {
                            if(countBefore <= countAfter)
                            {
                                var THIS_BREAK_POINT_SHOULD_NEVER_GETTING_HIT = "";
                            }
                        }
                    }
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
