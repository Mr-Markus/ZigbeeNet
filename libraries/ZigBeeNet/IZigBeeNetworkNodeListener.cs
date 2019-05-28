using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public interface IZigBeeNetworkNodeListener
    {
        /// <summary>
        /// Node was added
        /// </summary>
        /// <param name="node"></param>
        void NodeAdded(ZigBeeNode node);

        /// <summary>
        /// Node was updated
        /// </summary>
        /// <param name="node"></param>
        void NodeUpdated(ZigBeeNode node);

        /// <summary>
        /// Node was removed
        /// </summary>
        /// <param name="node"></param>
        void NodeRemoved(ZigBeeNode node);
    }
}
