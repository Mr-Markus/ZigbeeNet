using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;

namespace ZigBeeNet.App
{
    /// <summary>
    /// Defines the interface for a ZigBee Application
    /// 
    /// Applications provide specific functionality in the framework and can be instantiated and registered with an endpoint.
    /// An application is registered with the <see cref="ZigBeeEndpoint"/>, and the endpoint will take care of starting and
    /// stopping the application, and passing any received commands to the application.
    /// 
    /// Normally, this will be managed through a <see cref="ZigBeeNetworkExtension"/> which will manage the addition of the
    /// application to the endpoint when the node joins the network, along with responding to the service discovery requests.
    /// </summary>
    public interface IZigBeeApplication
    {

        /// <summary>
        /// Starts an application. The application should perform any initialisation. This gets called when
        /// the application is registered.
        ///
        /// <param name="cluster">The <see cref="ZclCluster"/> which is the client we are using</param>
        /// <returns><see cref="ZigBeeStatus.SUCCESS"/> if the application started successfully</returns>
        /// </summary>
        ZigBeeStatus AppStartup(ZclCluster cluster);

        /// <summary>
        /// Shuts down an application. The application should perform any shutdown and cleanup as required.
        /// </summary>
        void AppShutdown();

        /// <summary>
        /// Gets the applicable cluster ID for this application
        ///
        /// <returns>the cluster ID</returns>
        /// </summary>
        int GetClusterId();

        /// <summary>
        /// Called when a command has been received. This is called by the <see cref="ZigBeeNode"/> when a command for this node is
        /// received.
        ///
        /// <param name="command">the received <see cref="ZigBeeCommand"></param>
        /// </summary>
        void CommandReceived(ZigBeeCommand command);
    }
}
