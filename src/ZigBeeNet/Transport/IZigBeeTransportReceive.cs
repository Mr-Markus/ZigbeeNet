using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Transport
{
    /// <summary>
    /// Defines the interface for data passed from the transport layer(ie dongle) to the ZigBee stack framework.
    /// 
    /// ZigBee transport interface implemented by different hardware drivers.This could support for example serial
    /// interfaces for dongles, or IP connections to remote interfaces.
    ///
    /// The ZCL interface allows the stack to specify the NWK(Network) header, the APS(Application Support Sublayer) and
    /// the payload.The headers are provided separately to allow the framework to specify the configuration in some detail,
    /// while allowing the transport implementation(eg dongle) to format the data as per its needs.The payload is
    /// serialised by the framework using the ZigBeeSerializer and ZigBeeDeserializer interfaces, thus
    /// allowing the format to be set for different hardware implementations.
    /// 
    /// The ZDO interface exchanges only command classes.This is different to the ZCL interface since different sticks tend
    /// to implement ZDO functionality as individual commands rather than allowing a binary ZDO packet to be sent and
    /// received.
    /// </summary>
    public interface IZigBeeTransportReceive
    {
        /// <summary>
        /// A callback called by the <see cref="ZigBeeTransportTransmit">
        /// when a ZigBee Cluster Library command is received.
        /// 
        /// The method allows the transport layer to specify the NWK(Network) header, the APS(Application Support Sublayer)
        /// and the payload.The headers are provided separately to allow the framework to specify the configuration in some
        /// detail, while allowing the transport implementation (eg dongle) to format the data as per its needs.The payload
        /// is deserialised by the framework using the ZigBeeDeserializer
        /// interface, thus allowing the format to be set for different hardware implementations.
        /// </summary>
        /// <param name="apsFrame">the ZigBeeApsFrame for this command</param>
        void ReceiveCommand(ZigBeeApsFrame apsFrame);

        /// <summary>
        /// Set the network state.
        /// 
        /// This is a callback from the <see cref="ZigBeeTransportTransmit"> when the state of the transport changes
        /// </summary>
        /// <param name="state"></param>
        void SetNetworkState(ZigBeeTransportState state);

        /// <summary>
        /// Announce a node has joined or left the network.
        /// 
        /// It should be assumed that this interface provides a authoritative statement about a devices status.
        /// This should come from higher level information provided by the coordinator/transport layer.
        /// </summary>
        /// <param name="deviceStatus">The ZigBeeNodeStatus of the node</param>
        /// <param name="networkAddress">The network address of the new node</param>
        /// <param name="ieeeAddress">The IeeeAddress address of the new node</param>
        void NodeStatusUpdate(ZigBeeNodeStatus deviceStatus, ushort networkAddress, IeeeAddress ieeeAddress);
    }
}
