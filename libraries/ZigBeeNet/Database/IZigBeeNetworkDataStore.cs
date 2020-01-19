using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet;

namespace ZigBeeNet.Database
{
    /// <summary>
    /// Interface to be implemented by users providing a node database for persisting device data outside of the framework.
    ///
    /// ZigBee coordinators do not store any information about devices that have joined the network.In order to provide a
    /// continuous service, information about nodes that have joined needs to be persisted between restarts of the framework.
    /// The underlying data store implementation must be able to store and retrieve node data with the
    /// WriteNode(ZigBeeNodeDao) and ReadNode(IeeeAddress) methods, and also provide a list of all nodes
    /// currently in the store with the RreadNetworkNodes() method.
    /// </summary>
    public interface IZigBeeNetworkDataStore
    {
        /// <summary>
        /// Reads the list of nodes that are currently included in network. The underlying data store should return the list
        /// of all nodes that have been stored with <see cref="ZigBeeNodeDao">. If  <see cref="RemoveNode(IeeeAddress)">
        /// has subsequently been called, the node shall not be returned in this Set.
        /// </summary>
        /// <returns>the Set of IeeeAddress of all nodes currently included in the network. 
        /// Must not return null - if no nodes are currently in the network, return an empty Set.</returns>
        ISet<IeeeAddress> ReadNetworkNodes();

        /// <summary>
        /// Called when the library wants to restore the saved information about a node. This is normally only done on system startup
        /// </summary>
        /// <param name="address"> the IeeeAddress of the node to retrieve</param>
        /// <returns>the ZigBeeNodeDao containing the node data. May return null if the node is not found in the database.</returns>
        ZigBeeNodeDao ReadNode(IeeeAddress address);

        /// <summary>
        /// Called when information about a node has been updated, and the node must persist the node data to non-volatile storage.
        /// </summary>
        /// <param name="node"> the ZigBeeNodeDao to be persisted</param>
        void WriteNode(ZigBeeNodeDao node);

        /// <summary>
        /// Called when a node has been removed from the network. It is expected that the database implementation will remove this data from the storage.
        /// </summary>
        /// <param name="address">the IeeeAddress of the node to remove</param>
        void RemoveNode(IeeeAddress address);
    }
}
