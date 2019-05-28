using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

namespace ZigBeeNet.Internal
{
    public class ZigBeeCommandNotifier
    {
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
            /*
             * https://stackoverflow.com/questions/24172232/is-list-copy-thread-safe
             * 
             * List() with the following ctor calls internally CopyTo() which is not threadsafe
             * so either we have to lock the instantiation of the List or the enumeration
             */

            var tmp = new List<IZigBeeCommandListener>(0);
            lock (_lock)
            {
                tmp = new List<IZigBeeCommandListener>(_commandListeners);
            }

            // TODO: Consider using a .net build in Concurrent Collection
            // TODO: Consider removing Tas.Run()
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
                        Log.Error("Error during the notification of commandListeners.", ex);
                    }
                });
            }
        }
    }
}
