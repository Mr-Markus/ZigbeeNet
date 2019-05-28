using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Transport
{
    /// <summary>
 /// Enumeration defining all possible configuration options for the <see cref="ZigBeeTransportTransmit">. Configuration is
 /// updated via the {@link ZigBeeTransportTransmit#updateTransportConfig(java.util.Dictionary)} method.
 ///
 /// @author Chris Jackson
 ///
 /// </summary>
    public enum TransportConfigOption
    {
        /// <summary>
         /// Defines the concentrator type.
         /// <p>
         /// Value must be one of <see cref="ConcentratorType">.
         ///
         /// @deprecated use CONCENTRATOR_CONFIG
         /// </summary>
        CONCENTRATOR_TYPE,
        /// <summary>
         /// Defines the concentrator type.
         /// <p>
         /// Value must be one of <see cref="ConcentratorConfigs">.
         /// </summary>
        CONCENTRATOR_CONFIG,
        /// <summary>
         /// Configures the trust centre join mode.
         /// <p>
         /// Value must be one of <see cref="TrustCentreJoinMode"> enumeration.
         /// </summary>
        TRUST_CENTRE_JOIN_MODE,
        /// <summary>
         /// Sets the trust centre link key.
         /// <p>
         /// Value must be a <see cref="ZigBeeKey">.
         /// </summary>
        TRUST_CENTRE_LINK_KEY,
        /// <summary>
         /// Sets a list of supported input clusters. This is primarily intended to allow the application to configure
         /// clusters that are matched in the MatchDescriptorRequest.
         /// <p>
         /// Value must be a <see cref="Collection"> of Integer defining the input clusters
         /// </summary>
        SUPPORTED_INPUT_CLUSTERS,
        /// <summary>
         /// Sets a list of supported output clusters. This is primarily intended to allow the application to configure
         /// clusters that are matched in the MatchDescriptorRequest.
         /// <p>
         /// Value must be a <see cref="Collection"> of Integer defining the output clusters
         /// </summary>
        SUPPORTED_OUTPUT_CLUSTERS,
        /// <summary>
         /// Sets an installation key for the specified address. Using a blank key (ie all zeros) may be used to remove
         /// the install key. This is dongle specific if the key can be removed or if it will time out.
         /// <p>
         /// Value must be a <see cref="ZigBeeNodeKey">
         /// </summary>
        INSTALL_KEY,
        /// <summary>
         /// Sets the device type used by the dongle. This allows to set the dongle as a coordinator or a router.
         /// <p>
         /// Value must be <see cref="DeviceType">
         /// </summary>
        DEVICE_TYPE,
        /// <summary>
         /// Sets the device radio power. Power level is defined in dBm.
         /// <p>
         /// Value must be <see cref="Integer">
         /// </summary>
        RADIO_TX_POWER
    }
}
