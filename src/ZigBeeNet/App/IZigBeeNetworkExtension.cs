using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.App
{
    /// <summary>
    /// Defines the interface for a ZigBee Extension.
    /// 
    /// Extensions provide specific functionality in the framework and can be instantiated and registered with the network
    /// manager. An extension is registered with the <see cref="ZigBeeNetworkManager"/>, and the manager will take care of
    /// starting and stopping the extension.
    /// 
    /// Extensions should register with the standard <see cref="ZigBeeNetworkManager"/> listeners to receive network notifications
    ///
    /// ZigBeeNetworkStateListener for network state changes
    /// ZigBeeNetworkNodeListener for updates to nodes
    /// ZigBeeCommandListener to receive incoming commands
    /// ZigBeeAnnounceListener to receive announcement messages
    /// </summary>
    public interface IZigBeeNetworkExtension
    {
        /// <summary>
        /// Initializes an extension. The extension should perform any initialisation. This gets called when
        /// the extension is registered. The extension should not assume that the network is online, and should
        /// not attempt to communicate on the network until after {@link #extensionStartup()} is called.
        ///
        /// <param name="networkManager">The <see cref="ZigBeeNetworkManager"/> of the network</param>
        /// <returns>
        /// <see cref="ZigBeeStatus.SUCCESS"/> if the extension initialized successfully
        /// </returns>
        /// </summary>
        ZigBeeStatus ExtensionInitialize(ZigBeeNetworkManager networkManager);

        /// <summary>
        /// Starts an extension. The extension should perform any initialisation. This gets called when
        /// the extension is registered.
        /// </summary>
        /// <param name="ZigBeeNetworkManager"/>The ZigBeeNetworkManager of the network</param>
        /// <returns>
        /// <see cref="ZigBeeStatus.SUCCESS"/> if the extension initialized successfully, <see cref="ZigBeeStatus.INVALID_STATE"/>
        /// if the extension was already started.
        /// </returns>
        ZigBeeStatus ExtensionStartup();

        /// <summary>
        /// Shuts down an extension. The extension should perform any shutdown and cleanup as required.
        /// </summary>
        void ExtensionShutdown();
    }
}
