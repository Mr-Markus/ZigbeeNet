
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Diagnostics cluster implementation (Cluster ID 0x0B05).
    ///
    /// The diagnostics cluster provides access to information regarding the operation of the
    /// ZigBee stack over time. This information is useful to installers and other network
    /// administrators who wish to know how a particular device is functioning on the network.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclDiagnosticsCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0B05;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Diagnostics";

        // Attribute constants

        /// <summary>
        /// An attribute that is incremented each time the device resets. A reset is defined as
        /// any time the device restarts. This is not the same as a reset to factory defaults,
        /// which should clear this and all values.
        /// </summary>
        public const ushort ATTR_NUMBEROFRESETS = 0x0000;

        /// <summary>
        /// This attribute keeps track of the number of writes to persistent memory. Each time
        /// that the device stores a token in persistent memory it will increment this value.
        /// </summary>
        public const ushort ATTR_PERSISTENTMEMORYWRITES = 0x0001;

        /// <summary>
        /// A counter that is incremented each time the MAC layer receives a broadcast.
        /// </summary>
        public const ushort ATTR_MACRXBCAST = 0x0100;

        /// <summary>
        /// A counter that is incremented each time the MAC layer transmits a broadcast.
        /// </summary>
        public const ushort ATTR_MACTXBCAST = 0x0101;

        /// <summary>
        /// A counter that is incremented each time the MAC layer receives a unicast.
        /// </summary>
        public const ushort ATTR_MACRXUCAST = 0x0102;

        /// <summary>
        /// A counter that is incremented each time the MAC layer transmits a unicast.
        /// </summary>
        public const ushort ATTR_MACTXUCAST = 0x0103;
        public const ushort ATTR_MACTXUCASTRETRY = 0x0104;
        public const ushort ATTR_MACTXUCASTFAIL = 0x0105;
        public const ushort ATTR_APSRXBCAST = 0x0106;
        public const ushort ATTR_APSTXBCAST = 0x0107;
        public const ushort ATTR_APSRXUCAST = 0x0108;
        public const ushort ATTR_APSTXUCASTSUCCESS = 0x0109;
        public const ushort ATTR_APSTXUCASTRETRY = 0x010A;
        public const ushort ATTR_APSTXUCASTFAIL = 0x010B;
        public const ushort ATTR_ROUTEDISCINITIATED = 0x010C;
        public const ushort ATTR_NEIGHBORADDED = 0x010D;
        public const ushort ATTR_NEIGHBORREMOVED = 0x010E;
        public const ushort ATTR_NEIGHBORSTALE = 0x010F;
        public const ushort ATTR_JOININDICATION = 0x0110;
        public const ushort ATTR_CHILDMOVED = 0x0111;
        public const ushort ATTR_NWKFCFAILURE = 0x0112;
        public const ushort ATTR_APSFCFAILURE = 0x0113;
        public const ushort ATTR_APSUNAUTHORIZEDKEY = 0x0114;
        public const ushort ATTR_NWKDECRYPTFAILURES = 0x0115;
        public const ushort ATTR_APSDECRYPTFAILURES = 0x0116;
        public const ushort ATTR_PACKETBUFFERALLOCATEFAILURES = 0x0117;
        public const ushort ATTR_RELAYEDUCAST = 0x0118;
        public const ushort ATTR_PHYTOMACQUEUELIMITREACHED = 0x0119;
        public const ushort ATTR_PACKETVALIDATEDROPCOUNT = 0x011A;
        public const ushort ATTR_AVERAGEMACRETRYPERAPSMESSAGESENT = 0x011B;
        public const ushort ATTR_LASTMESSAGELQI = 0x011C;
        public const ushort ATTR_LASTMESSAGERSSI = 0x011D;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(32);

            attributeMap.Add(ATTR_NUMBEROFRESETS, new ZclAttribute(this, ATTR_NUMBEROFRESETS, "Number Of Resets", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PERSISTENTMEMORYWRITES, new ZclAttribute(this, ATTR_PERSISTENTMEMORYWRITES, "Persistent Memory Writes", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MACRXBCAST, new ZclAttribute(this, ATTR_MACRXBCAST, "MAC Rx Bcast", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MACTXBCAST, new ZclAttribute(this, ATTR_MACTXBCAST, "MAC Tx Bcast", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MACRXUCAST, new ZclAttribute(this, ATTR_MACRXUCAST, "MAC Rx Ucast", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MACTXUCAST, new ZclAttribute(this, ATTR_MACTXUCAST, "MAC Tx Ucast", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MACTXUCASTRETRY, new ZclAttribute(this, ATTR_MACTXUCASTRETRY, "MAC Tx Ucast Retry", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MACTXUCASTFAIL, new ZclAttribute(this, ATTR_MACTXUCASTFAIL, "MAC Tx Ucast Fail", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSRXBCAST, new ZclAttribute(this, ATTR_APSRXBCAST, "APS Rx Bcast", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSTXBCAST, new ZclAttribute(this, ATTR_APSTXBCAST, "APS Tx Bcast", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSRXUCAST, new ZclAttribute(this, ATTR_APSRXUCAST, "APS Rx Ucast", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSTXUCASTSUCCESS, new ZclAttribute(this, ATTR_APSTXUCASTSUCCESS, "APS Tx Ucast Success", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSTXUCASTRETRY, new ZclAttribute(this, ATTR_APSTXUCASTRETRY, "APS Tx Ucast Retry", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSTXUCASTFAIL, new ZclAttribute(this, ATTR_APSTXUCASTFAIL, "APS Tx Ucast Fail", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ROUTEDISCINITIATED, new ZclAttribute(this, ATTR_ROUTEDISCINITIATED, "Route Disc Initiated", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NEIGHBORADDED, new ZclAttribute(this, ATTR_NEIGHBORADDED, "Neighbor Added", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NEIGHBORREMOVED, new ZclAttribute(this, ATTR_NEIGHBORREMOVED, "Neighbor Removed", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NEIGHBORSTALE, new ZclAttribute(this, ATTR_NEIGHBORSTALE, "Neighbor Stale", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_JOININDICATION, new ZclAttribute(this, ATTR_JOININDICATION, "Join Indication", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CHILDMOVED, new ZclAttribute(this, ATTR_CHILDMOVED, "Child Moved", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NWKFCFAILURE, new ZclAttribute(this, ATTR_NWKFCFAILURE, "NWK FC Failure", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSFCFAILURE, new ZclAttribute(this, ATTR_APSFCFAILURE, "APS FC Failure", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSUNAUTHORIZEDKEY, new ZclAttribute(this, ATTR_APSUNAUTHORIZEDKEY, "APS Unauthorized Key", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NWKDECRYPTFAILURES, new ZclAttribute(this, ATTR_NWKDECRYPTFAILURES, "NWK Decrypt Failures", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSDECRYPTFAILURES, new ZclAttribute(this, ATTR_APSDECRYPTFAILURES, "APS Decrypt Failures", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PACKETBUFFERALLOCATEFAILURES, new ZclAttribute(this, ATTR_PACKETBUFFERALLOCATEFAILURES, "Packet Buffer Allocate Failures", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RELAYEDUCAST, new ZclAttribute(this, ATTR_RELAYEDUCAST, "Relayed Ucast", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PHYTOMACQUEUELIMITREACHED, new ZclAttribute(this, ATTR_PHYTOMACQUEUELIMITREACHED, "Phy To MAC Queue Limit Reached", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PACKETVALIDATEDROPCOUNT, new ZclAttribute(this, ATTR_PACKETVALIDATEDROPCOUNT, "Packet Validate Drop Count", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_AVERAGEMACRETRYPERAPSMESSAGESENT, new ZclAttribute(this, ATTR_AVERAGEMACRETRYPERAPSMESSAGESENT, "Average MAC Retry Per APS Message Sent", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_LASTMESSAGELQI, new ZclAttribute(this, ATTR_LASTMESSAGELQI, "Last Message LQI", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_LASTMESSAGERSSI, new ZclAttribute(this, ATTR_LASTMESSAGERSSI, "Last Message RSSI", ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER), true, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Diagnostics cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclDiagnosticsCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
