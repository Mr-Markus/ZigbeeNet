using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public interface IZigBeeNetworkNodeListener
    {
        /// <summary>
        /// Node was added
        /// </summary>
        /// <param name="node"></param>
        void NodeAdded(ZigbeeNode node);

        /// <summary>
        /// Node was updated
        /// </summary>
        /// <param name="node"></param>
        void NodeUpdated(ZigbeeNode node);

        /// <summary>
        /// Node was removed
        /// </summary>
        /// <param name="node"></param>
        void NodeRemoved(ZigbeeNode node);
    }
}
