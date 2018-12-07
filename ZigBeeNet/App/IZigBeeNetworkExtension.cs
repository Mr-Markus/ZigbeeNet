using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.App
{
    /// <summary>
    /// Defines the interface for a ZigBee Extension.
    /// <p>
    /// Extensions provide specific functionality in the framework and can be instantiated and registered with the network
    /// manager. An extension is registered with the {@link ZigBeeNetworkManager}, and the manager will take care of
    /// starting and stopping the extension.
    /// <p>
    /// Extensions should register with the standard {@link ZigBeeNetworkManager} listeners to receive network notifications
    /// -:
    /// <ul>
    /// <li>ZigBeeNetworkStateListener for network state changes
    /// <li>ZigBeeNetworkNodeListener for updates to nodes
    /// <li>ZigBeeCommandListener to receive incoming commands
    /// <li>ZigBeeAnnounceListener to receive announcement messages
    /// </ul> 
    /// </summary>
    public interface IZigBeeNetworkExtension
    {
        /// <summary>
        /// Initializes an extension. The extension should perform any initialisation. This gets called when
        /// the extension is registered. The extension should not assume that the network is online, and should
        /// not attempt to communicate on the network until after {@link #extensionStartup()} is called.
        ///
        /// @param networkManager The {@link ZigBeeNetworkManager} of the network
        /// @return {@link ZigBeeStatus#SUCCESS} if the extension initialized successfully
        /// </summary>
        ZigBeeStatus ExtensionInitialize(ZigBeeNetworkManager networkManager);

        /// <summary>
        /// Starts an extension. The extension should perform any initialisation. This gets called when
        /// the extension is registered.
        /// </summary>
        /// <param name="networkManager">The ZigBeeNetworkManager of the network</param>
        /// <returns> true if the extension started successfully</returns>
        ZigBeeStatus ExtensionStartup(ZigBeeNetworkManager networkManager);

        /// <summary>
        /// Shuts down an extension. The extension should perform any shutdown and cleanup as required.
        /// </summary>
        void ExtensionShutdown();
    }
}
