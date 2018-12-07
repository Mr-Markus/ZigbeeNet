using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    /// <summary>
    /// CCommand listeners provides a callback when commands are received
    /// </summary>
    public interface IZigBeeCommandListener
    {
        /// <summary>
        /// Called then a command has been received
        /// </summary>
        /// <param name="command"></param>
        void CommandReceived(ZigBeeCommand command);
    }
}
