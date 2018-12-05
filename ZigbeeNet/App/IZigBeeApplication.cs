using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;

namespace ZigBeeNet.App
{
    /**
     * Defines the interface for a ZigBee Application
     * <p>
     * Applications provide specific functionality in the framework and can be instantiated and registered with an endpoint.
     * An application is registered with the {@link ZigBeeEndpoint}, and the endpoint will take care of starting and
     * stopping the application, and passing any received commands to the application.
     * <p>
     * Normally, this will be managed through a {@link ZigBeeNetworkExtension} which will manage the addition of the
     * application to the endpoint when the node joins the network, along with responding to the service discovery requests.
     *
     * @author Chris Jackson
     *
     */
    public interface IZigBeeApplication
    {

        /**
         * Starts an application. The application should perform any initialisation. This gets called when
         * the application is registered.
         *
         * @param cluster The {@link ZclCluster} which is the client we are using
         * @return {@link ZigBeeStatus#SUCCESS} if the application started successfully
         */
        ZigBeeStatus AppStartup(ZclCluster cluster);

        /**
         * Shuts down an application. The application should perform any shutdown and cleanup as required.
         */
        void AppShutdown();

        /**
         * Gets the applicable cluster ID for this application
         *
         * @return the cluster ID
         */
        int GetClusterId();

        /**
         * Called when a command has been received. This is called by the {@link ZigBeeNode} when a command for this node is
         * received.
         *
         * @param command the received {@link ZigBeeCommand}
         */
        void CommandReceived(ZigBeeCommand command);
    }
}
