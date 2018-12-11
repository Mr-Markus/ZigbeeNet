using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZigBeeNet.Internal
{
    public class ZigBeeCommandNotifier
    {
        public List<IZigBeeCommandListener> CommandListeners { get; set; } = new List<IZigBeeCommandListener>();

        public void AddCommandListener(IZigBeeCommandListener commandListener)
        {
            lock (CommandListeners)
            {
                CommandListeners.Add(commandListener);
            }
        }

        public void RemoveCommandListener(IZigBeeCommandListener commandListener)
        {
            lock (CommandListeners)
            {
                CommandListeners.Remove(commandListener);
            }
        }

        public void NotifyCommandListeners(ZigBeeCommand command)
        {
            foreach (IZigBeeCommandListener commandListener in CommandListeners)
            {
                Task.Run(() => commandListener.CommandReceived(command));
            }
        }
    }
}
