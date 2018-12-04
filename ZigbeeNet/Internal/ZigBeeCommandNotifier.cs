using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZigbeeNet.Internal
{
    public class ZigBeeCommandNotifier
    {
        public List<IZigbeeCommandListener> CommandListeners { get; set; }

        public void AddCommandListener(IZigbeeCommandListener commandListener)
        {
            lock (CommandListeners)
            {
                CommandListeners.Add(commandListener);
            }
        }

        public void RemoveCommandListener(IZigbeeCommandListener commandListener)
        {
            lock (CommandListeners)
            {
                CommandListeners.Remove(commandListener);
            }
        }

        public void NotifyCommandListeners(ZigBeeCommand command)
        {
            foreach (IZigbeeCommandListener commandListener in CommandListeners)
            {
                Task.Run(() => commandListener.CommandReceived(command));
            }
        }
    }
}
