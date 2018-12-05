﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Transport
{
    public interface IZigBeeTransportTransmit
    {
        /// <summary>
        /// Initialize the transport interface. Following the call to initialize the configuration methods may be used to
        /// configure the transport layer.
        /// 
        /// During the initialize() method, the provider must initialize the ports and perform any configuration required to
        /// get the stack ready for use. If the dongle has already joined a network, then this method will return ZigBeeStatus.SUCESS
        /// 
        /// At the completion of the initialize method, the IeeeAddress must return the valid address
        /// for the coordinator.
        /// </summary>
        /// <returns></returns>
        ZigBeeStatus Initialize();

        /// <summary>
        /// Starts the transport interface.
        /// 
        /// This call will optionally reconfigure the interface if the reinitialize parameter is true.
        /// </summary>
        /// <param name="reinitialize"></param>
        /// <returns>true if startup was success</returns>
        ZigBeeStatus Startup(bool reinitialize);

        /// <summary>
        /// Shuts down a transport interface.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Get the transport layer version string
        /// </summary>
        string VersionString { get; set; }

        /// <summary>
        /// Returns the IeeeAddress of the local device
        /// </summary>
        ZigBeeAddress64 IeeeAddress { get; set; }

        /// <summary>
        /// Gets the current ZigBee RF channel
        /// </summary>
        ZigBeeChannel ZigBeeChannel { get; set; }

        /// <summary>
        /// Gets the ZigBee PAN ID currently in use by the transport
        /// </summary>
        ZigbeeAddress16 PanID { get; set; }

        /// <summary>
        /// Gets the ZigBee Extended PAN ID currently in use by the transport
        /// </summary>
        ZigBeeAddress64 ExtendedPanId { get; set; }

        /// <summary>
        /// The ZigBee network security key
        /// </summary>
        ZigBeeKey ZigBeeNetworkKey { get; set; }

        /// <summary>
        /// The Trust Center link security key
        /// </summary>
        ZigBeeKey TcLinkKey { get; set; }

        ///  <summary>
        /// Sends ZigBee Cluster Library command without waiting for response. Responses are provided to the framework
        /// through the ZigBeeNetwork#receiveZclCommand callback.
        /// 
        /// This method must return instantly - it should NOT wait for a response.
        /// 
        /// The ZCL method allows the stack to specify the NWK (Network) header, the APS (Application Support Sublayer) and
        /// the payload. The headers are provided separately to allow the framework to specify the configuration in some
        /// detail, while allowing the transport implementation (eg dongle) to format the data as per its needs. The payload
        /// is serialised by the framework using the {@link ZigBeeSerializer} interface, thus allowing the format to be set
        /// for different hardware implementations.
        /// </summary>
        void SendCommand(ZigBeeApsFrame apsFrame);

        /// <summary>
        /// * Sets the transport configuration.
        /// 
        /// This method passes a Map 
        /// of TransportConfigOption s to the transport layer.Each option
        /// must be defined as a Object as defined by the option(see the documentation for
        /// TransportConfigOption. The transport layer should update its configuration as appropriate - if this will
        /// take any appreciable time to complete, the implementation should perform error checking and then return
        /// TransportConfigResult#SUCCESS.
        ///
        /// This method returns the result of each configuration in the calling TransportConfig.
        ///
        /// If configuration options are invalid, TransportConfigResult#ERROR_INVALID_VALUE is returned.
        ///
        /// If the transport is not in a mode where it can accept a specific configuration change
        ///
        /// TransportConfigResult#ERROR_INVALID_VALUE is returned in the value status
        /// </summary>
        /// <param name="configuration"></param>
        //void UpdateTransportConfig(TransportConfig configuration);
    }
}
