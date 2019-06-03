// License text here

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;

namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Diagnosticscluster implementation (Cluster ID 0x0B05).
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

        /* Attribute constants */

        /// <summary>
        /// </summary>
        public const ushort ATTR_MACRXBCAST = 0x0100;

        /// <summary>
        /// </summary>
        public const ushort ATTR_MACTXBCAST = 0x0101;

        /// <summary>
        /// </summary>
        public const ushort ATTR_MACRXUCAST = 0x0102;

        /// <summary>
        /// </summary>
        public const ushort ATTR_MACTXUCAST = 0x0103;

        /// <summary>
        /// </summary>
        public const ushort ATTR_MACTXUCASTRETRY = 0x0104;

        /// <summary>
        /// </summary>
        public const ushort ATTR_MACTXUCASTFAIL = 0x0105;

        /// <summary>
        /// </summary>
        public const ushort ATTR_APSRXBCAST = 0x0106;

        /// <summary>
        /// </summary>
        public const ushort ATTR_APSTXBCAST = 0x0107;

        /// <summary>
        /// </summary>
        public const ushort ATTR_APSRXUCAST = 0x0108;

        /// <summary>
        /// </summary>
        public const ushort ATTR_APSTXUCASTSUCCESS = 0x0109;

        /// <summary>
        /// </summary>
        public const ushort ATTR_APSTXUCASTRETRY = 0x010A;

        /// <summary>
        /// </summary>
        public const ushort ATTR_APSTXUCASTFAIL = 0x010B;

        /// <summary>
        /// </summary>
        public const ushort ATTR_ROUTEDISCINITIATED = 0x010C;

        /// <summary>
        /// </summary>
        public const ushort ATTR_NEIGHBORADDED = 0x010D;

        /// <summary>
        /// </summary>
        public const ushort ATTR_NEIGHBORREMOVED = 0x010E;

        /// <summary>
        /// </summary>
        public const ushort ATTR_NEIGHBORSTALE = 0x010F;

        /// <summary>
        /// </summary>
        public const ushort ATTR_JOININDICATION = 0x0110;

        /// <summary>
        /// </summary>
        public const ushort ATTR_CHILDMOVED = 0x0111;

        /// <summary>
        /// </summary>
        public const ushort ATTR_NWKFCFAILURE = 0x0112;

        /// <summary>
        /// </summary>
        public const ushort ATTR_APSFCFAILURE = 0x0113;

        /// <summary>
        /// </summary>
        public const ushort ATTR_APSUNAUTHORIZEDKEY = 0x0114;

        /// <summary>
        /// </summary>
        public const ushort ATTR_NWKDECRYPTFAILURES = 0x0115;

        /// <summary>
        /// </summary>
        public const ushort ATTR_APSDECRYPTFAILURES = 0x0116;

        /// <summary>
        /// </summary>
        public const ushort ATTR_PACKETBUFFERALLOCATEFAILURES = 0x0117;

        /// <summary>
        /// </summary>
        public const ushort ATTR_RELAYEDUCAST = 0x0118;

        /// <summary>
        /// </summary>
        public const ushort ATTR_PHYTOMACQUEUELIMITREACHED = 0x0119;

        /// <summary>
        /// </summary>
        public const ushort ATTR_PACKETVALIDATEDROPCOUNT = 0x011A;

        /// <summary>
        /// </summary>
        public const ushort ATTR_AVERAGEMACRETRYPERAPSMESSAGESENT = 0x011B;

        /// <summary>
        /// </summary>
        public const ushort ATTR_LASTMESSAGELQI = 0x011C;

        /// <summary>
        /// </summary>
        public const ushort ATTR_LASTMESSAGERSSI = 0x011D;


        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(30);

            ZclClusterType diagnostics = ZclClusterType.GetValueById(ClusterType.DIAGNOSTICS);

            attributeMap.Add(ATTR_MACRXBCAST, new ZclAttribute(diagnostics, ATTR_MACRXBCAST, "MacRxBcast", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MACTXBCAST, new ZclAttribute(diagnostics, ATTR_MACTXBCAST, "MacTxBcast", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MACRXUCAST, new ZclAttribute(diagnostics, ATTR_MACRXUCAST, "MacRxUcast", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MACTXUCAST, new ZclAttribute(diagnostics, ATTR_MACTXUCAST, "MacTxUcast", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MACTXUCASTRETRY, new ZclAttribute(diagnostics, ATTR_MACTXUCASTRETRY, "MacTxUcastRetry", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MACTXUCASTFAIL, new ZclAttribute(diagnostics, ATTR_MACTXUCASTFAIL, "MacTxUcastFail", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSRXBCAST, new ZclAttribute(diagnostics, ATTR_APSRXBCAST, "APSRxBcast", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSTXBCAST, new ZclAttribute(diagnostics, ATTR_APSTXBCAST, "APSTxBcast", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSRXUCAST, new ZclAttribute(diagnostics, ATTR_APSRXUCAST, "APSRxUcast", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSTXUCASTSUCCESS, new ZclAttribute(diagnostics, ATTR_APSTXUCASTSUCCESS, "APSTxUcastSuccess", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSTXUCASTRETRY, new ZclAttribute(diagnostics, ATTR_APSTXUCASTRETRY, "APSTxUcastRetry", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSTXUCASTFAIL, new ZclAttribute(diagnostics, ATTR_APSTXUCASTFAIL, "APSTxUcastFail", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ROUTEDISCINITIATED, new ZclAttribute(diagnostics, ATTR_ROUTEDISCINITIATED, "RouteDiscInitiated", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NEIGHBORADDED, new ZclAttribute(diagnostics, ATTR_NEIGHBORADDED, "NeighborAdded", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NEIGHBORREMOVED, new ZclAttribute(diagnostics, ATTR_NEIGHBORREMOVED, "NeighborRemoved", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NEIGHBORSTALE, new ZclAttribute(diagnostics, ATTR_NEIGHBORSTALE, "NeighborStale", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_JOININDICATION, new ZclAttribute(diagnostics, ATTR_JOININDICATION, "JoinIndication", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CHILDMOVED, new ZclAttribute(diagnostics, ATTR_CHILDMOVED, "ChildMoved", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NWKFCFAILURE, new ZclAttribute(diagnostics, ATTR_NWKFCFAILURE, "NWKFCFailure", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSFCFAILURE, new ZclAttribute(diagnostics, ATTR_APSFCFAILURE, "APSFCFailure", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSUNAUTHORIZEDKEY, new ZclAttribute(diagnostics, ATTR_APSUNAUTHORIZEDKEY, "APSUnauthorizedKey", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NWKDECRYPTFAILURES, new ZclAttribute(diagnostics, ATTR_NWKDECRYPTFAILURES, "NWKDecryptFailures", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APSDECRYPTFAILURES, new ZclAttribute(diagnostics, ATTR_APSDECRYPTFAILURES, "APSDecryptFailures", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PACKETBUFFERALLOCATEFAILURES, new ZclAttribute(diagnostics, ATTR_PACKETBUFFERALLOCATEFAILURES, "PacketBufferAllocateFailures", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RELAYEDUCAST, new ZclAttribute(diagnostics, ATTR_RELAYEDUCAST, "RelayedUcast", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PHYTOMACQUEUELIMITREACHED, new ZclAttribute(diagnostics, ATTR_PHYTOMACQUEUELIMITREACHED, "PhytoMACqueuelimitreached", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PACKETVALIDATEDROPCOUNT, new ZclAttribute(diagnostics, ATTR_PACKETVALIDATEDROPCOUNT, "PacketValidatedropcount", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_AVERAGEMACRETRYPERAPSMESSAGESENT, new ZclAttribute(diagnostics, ATTR_AVERAGEMACRETRYPERAPSMESSAGESENT, "AverageMACRetryPerAPSMessageSent", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_LASTMESSAGELQI, new ZclAttribute(diagnostics, ATTR_LASTMESSAGELQI, "LastMessageLQI", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_LASTMESSAGERSSI, new ZclAttribute(diagnostics, ATTR_LASTMESSAGERSSI, "LastMessageRSSI", ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER), true, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Diagnostics cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclDiagnosticsCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// Get the MacRxBcast attribute [attribute ID256].
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetMacRxBcastAsync()
        {
            return Read(_attributes[ATTR_MACRXBCAST]);
        }

        /// <summary>
        /// Synchronously Get the MacRxBcast attribute [attribute ID256].
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public uint GetMacRxBcast(long refreshPeriod)
        {
            if (_attributes[ATTR_MACRXBCAST].IsLastValueCurrent(refreshPeriod))
            {
                return (uint)_attributes[ATTR_MACRXBCAST].LastValue;
            }

            return (uint)ReadSync(_attributes[ATTR_MACRXBCAST]);
        }


        /// <summary>
        /// Get the MacTxBcast attribute [attribute ID257].
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetMacTxBcastAsync()
        {
            return Read(_attributes[ATTR_MACTXBCAST]);
        }

        /// <summary>
        /// Synchronously Get the MacTxBcast attribute [attribute ID257].
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public uint GetMacTxBcast(long refreshPeriod)
        {
            if (_attributes[ATTR_MACTXBCAST].IsLastValueCurrent(refreshPeriod))
            {
                return (uint)_attributes[ATTR_MACTXBCAST].LastValue;
            }

            return (uint)ReadSync(_attributes[ATTR_MACTXBCAST]);
        }


        /// <summary>
        /// Get the MacRxUcast attribute [attribute ID258].
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetMacRxUcastAsync()
        {
            return Read(_attributes[ATTR_MACRXUCAST]);
        }

        /// <summary>
        /// Synchronously Get the MacRxUcast attribute [attribute ID258].
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public uint GetMacRxUcast(long refreshPeriod)
        {
            if (_attributes[ATTR_MACRXUCAST].IsLastValueCurrent(refreshPeriod))
            {
                return (uint)_attributes[ATTR_MACRXUCAST].LastValue;
            }

            return (uint)ReadSync(_attributes[ATTR_MACRXUCAST]);
        }


        /// <summary>
        /// Get the MacTxUcast attribute [attribute ID259].
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetMacTxUcastAsync()
        {
            return Read(_attributes[ATTR_MACTXUCAST]);
        }

        /// <summary>
        /// Synchronously Get the MacTxUcast attribute [attribute ID259].
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public uint GetMacTxUcast(long refreshPeriod)
        {
            if (_attributes[ATTR_MACTXUCAST].IsLastValueCurrent(refreshPeriod))
            {
                return (uint)_attributes[ATTR_MACTXUCAST].LastValue;
            }

            return (uint)ReadSync(_attributes[ATTR_MACTXUCAST]);
        }


        /// <summary>
        /// Get the MacTxUcastRetry attribute [attribute ID260].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetMacTxUcastRetryAsync()
        {
            return Read(_attributes[ATTR_MACTXUCASTRETRY]);
        }

        /// <summary>
        /// Synchronously Get the MacTxUcastRetry attribute [attribute ID260].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetMacTxUcastRetry(long refreshPeriod)
        {
            if (_attributes[ATTR_MACTXUCASTRETRY].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_MACTXUCASTRETRY].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_MACTXUCASTRETRY]);
        }


        /// <summary>
        /// Get the MacTxUcastFail attribute [attribute ID261].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetMacTxUcastFailAsync()
        {
            return Read(_attributes[ATTR_MACTXUCASTFAIL]);
        }

        /// <summary>
        /// Synchronously Get the MacTxUcastFail attribute [attribute ID261].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetMacTxUcastFail(long refreshPeriod)
        {
            if (_attributes[ATTR_MACTXUCASTFAIL].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_MACTXUCASTFAIL].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_MACTXUCASTFAIL]);
        }


        /// <summary>
        /// Get the APSRxBcast attribute [attribute ID262].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetAPSRxBcastAsync()
        {
            return Read(_attributes[ATTR_APSRXBCAST]);
        }

        /// <summary>
        /// Synchronously Get the APSRxBcast attribute [attribute ID262].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetAPSRxBcast(long refreshPeriod)
        {
            if (_attributes[ATTR_APSRXBCAST].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_APSRXBCAST].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_APSRXBCAST]);
        }


        /// <summary>
        /// Get the APSTxBcast attribute [attribute ID263].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetAPSTxBcastAsync()
        {
            return Read(_attributes[ATTR_APSTXBCAST]);
        }

        /// <summary>
        /// Synchronously Get the APSTxBcast attribute [attribute ID263].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetAPSTxBcast(long refreshPeriod)
        {
            if (_attributes[ATTR_APSTXBCAST].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_APSTXBCAST].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_APSTXBCAST]);
        }


        /// <summary>
        /// Get the APSRxUcast attribute [attribute ID264].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetAPSRxUcastAsync()
        {
            return Read(_attributes[ATTR_APSRXUCAST]);
        }

        /// <summary>
        /// Synchronously Get the APSRxUcast attribute [attribute ID264].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetAPSRxUcast(long refreshPeriod)
        {
            if (_attributes[ATTR_APSRXUCAST].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_APSRXUCAST].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_APSRXUCAST]);
        }


        /// <summary>
        /// Get the APSTxUcastSuccess attribute [attribute ID265].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetAPSTxUcastSuccessAsync()
        {
            return Read(_attributes[ATTR_APSTXUCASTSUCCESS]);
        }

        /// <summary>
        /// Synchronously Get the APSTxUcastSuccess attribute [attribute ID265].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetAPSTxUcastSuccess(long refreshPeriod)
        {
            if (_attributes[ATTR_APSTXUCASTSUCCESS].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_APSTXUCASTSUCCESS].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_APSTXUCASTSUCCESS]);
        }


        /// <summary>
        /// Get the APSTxUcastRetry attribute [attribute ID266].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetAPSTxUcastRetryAsync()
        {
            return Read(_attributes[ATTR_APSTXUCASTRETRY]);
        }

        /// <summary>
        /// Synchronously Get the APSTxUcastRetry attribute [attribute ID266].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetAPSTxUcastRetry(long refreshPeriod)
        {
            if (_attributes[ATTR_APSTXUCASTRETRY].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_APSTXUCASTRETRY].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_APSTXUCASTRETRY]);
        }


        /// <summary>
        /// Get the APSTxUcastFail attribute [attribute ID267].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetAPSTxUcastFailAsync()
        {
            return Read(_attributes[ATTR_APSTXUCASTFAIL]);
        }

        /// <summary>
        /// Synchronously Get the APSTxUcastFail attribute [attribute ID267].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetAPSTxUcastFail(long refreshPeriod)
        {
            if (_attributes[ATTR_APSTXUCASTFAIL].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_APSTXUCASTFAIL].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_APSTXUCASTFAIL]);
        }


        /// <summary>
        /// Get the RouteDiscInitiated attribute [attribute ID268].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetRouteDiscInitiatedAsync()
        {
            return Read(_attributes[ATTR_ROUTEDISCINITIATED]);
        }

        /// <summary>
        /// Synchronously Get the RouteDiscInitiated attribute [attribute ID268].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetRouteDiscInitiated(long refreshPeriod)
        {
            if (_attributes[ATTR_ROUTEDISCINITIATED].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ROUTEDISCINITIATED].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ROUTEDISCINITIATED]);
        }


        /// <summary>
        /// Get the NeighborAdded attribute [attribute ID269].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetNeighborAddedAsync()
        {
            return Read(_attributes[ATTR_NEIGHBORADDED]);
        }

        /// <summary>
        /// Synchronously Get the NeighborAdded attribute [attribute ID269].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetNeighborAdded(long refreshPeriod)
        {
            if (_attributes[ATTR_NEIGHBORADDED].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_NEIGHBORADDED].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_NEIGHBORADDED]);
        }


        /// <summary>
        /// Get the NeighborRemoved attribute [attribute ID270].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetNeighborRemovedAsync()
        {
            return Read(_attributes[ATTR_NEIGHBORREMOVED]);
        }

        /// <summary>
        /// Synchronously Get the NeighborRemoved attribute [attribute ID270].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetNeighborRemoved(long refreshPeriod)
        {
            if (_attributes[ATTR_NEIGHBORREMOVED].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_NEIGHBORREMOVED].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_NEIGHBORREMOVED]);
        }


        /// <summary>
        /// Get the NeighborStale attribute [attribute ID271].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetNeighborStaleAsync()
        {
            return Read(_attributes[ATTR_NEIGHBORSTALE]);
        }

        /// <summary>
        /// Synchronously Get the NeighborStale attribute [attribute ID271].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetNeighborStale(long refreshPeriod)
        {
            if (_attributes[ATTR_NEIGHBORSTALE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_NEIGHBORSTALE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_NEIGHBORSTALE]);
        }


        /// <summary>
        /// Get the JoinIndication attribute [attribute ID272].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetJoinIndicationAsync()
        {
            return Read(_attributes[ATTR_JOININDICATION]);
        }

        /// <summary>
        /// Synchronously Get the JoinIndication attribute [attribute ID272].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetJoinIndication(long refreshPeriod)
        {
            if (_attributes[ATTR_JOININDICATION].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_JOININDICATION].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_JOININDICATION]);
        }


        /// <summary>
        /// Get the ChildMoved attribute [attribute ID273].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetChildMovedAsync()
        {
            return Read(_attributes[ATTR_CHILDMOVED]);
        }

        /// <summary>
        /// Synchronously Get the ChildMoved attribute [attribute ID273].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetChildMoved(long refreshPeriod)
        {
            if (_attributes[ATTR_CHILDMOVED].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_CHILDMOVED].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_CHILDMOVED]);
        }


        /// <summary>
        /// Get the NWKFCFailure attribute [attribute ID274].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetNWKFCFailureAsync()
        {
            return Read(_attributes[ATTR_NWKFCFAILURE]);
        }

        /// <summary>
        /// Synchronously Get the NWKFCFailure attribute [attribute ID274].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetNWKFCFailure(long refreshPeriod)
        {
            if (_attributes[ATTR_NWKFCFAILURE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_NWKFCFAILURE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_NWKFCFAILURE]);
        }


        /// <summary>
        /// Get the APSFCFailure attribute [attribute ID275].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetAPSFCFailureAsync()
        {
            return Read(_attributes[ATTR_APSFCFAILURE]);
        }

        /// <summary>
        /// Synchronously Get the APSFCFailure attribute [attribute ID275].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetAPSFCFailure(long refreshPeriod)
        {
            if (_attributes[ATTR_APSFCFAILURE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_APSFCFAILURE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_APSFCFAILURE]);
        }


        /// <summary>
        /// Get the APSUnauthorizedKey attribute [attribute ID276].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetAPSUnauthorizedKeyAsync()
        {
            return Read(_attributes[ATTR_APSUNAUTHORIZEDKEY]);
        }

        /// <summary>
        /// Synchronously Get the APSUnauthorizedKey attribute [attribute ID276].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetAPSUnauthorizedKey(long refreshPeriod)
        {
            if (_attributes[ATTR_APSUNAUTHORIZEDKEY].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_APSUNAUTHORIZEDKEY].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_APSUNAUTHORIZEDKEY]);
        }


        /// <summary>
        /// Get the NWKDecryptFailures attribute [attribute ID277].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetNWKDecryptFailuresAsync()
        {
            return Read(_attributes[ATTR_NWKDECRYPTFAILURES]);
        }

        /// <summary>
        /// Synchronously Get the NWKDecryptFailures attribute [attribute ID277].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetNWKDecryptFailures(long refreshPeriod)
        {
            if (_attributes[ATTR_NWKDECRYPTFAILURES].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_NWKDECRYPTFAILURES].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_NWKDECRYPTFAILURES]);
        }


        /// <summary>
        /// Get the APSDecryptFailures attribute [attribute ID278].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetAPSDecryptFailuresAsync()
        {
            return Read(_attributes[ATTR_APSDECRYPTFAILURES]);
        }

        /// <summary>
        /// Synchronously Get the APSDecryptFailures attribute [attribute ID278].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetAPSDecryptFailures(long refreshPeriod)
        {
            if (_attributes[ATTR_APSDECRYPTFAILURES].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_APSDECRYPTFAILURES].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_APSDECRYPTFAILURES]);
        }


        /// <summary>
        /// Get the PacketBufferAllocateFailures attribute [attribute ID279].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetPacketBufferAllocateFailuresAsync()
        {
            return Read(_attributes[ATTR_PACKETBUFFERALLOCATEFAILURES]);
        }

        /// <summary>
        /// Synchronously Get the PacketBufferAllocateFailures attribute [attribute ID279].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetPacketBufferAllocateFailures(long refreshPeriod)
        {
            if (_attributes[ATTR_PACKETBUFFERALLOCATEFAILURES].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_PACKETBUFFERALLOCATEFAILURES].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_PACKETBUFFERALLOCATEFAILURES]);
        }


        /// <summary>
        /// Get the RelayedUcast attribute [attribute ID280].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetRelayedUcastAsync()
        {
            return Read(_attributes[ATTR_RELAYEDUCAST]);
        }

        /// <summary>
        /// Synchronously Get the RelayedUcast attribute [attribute ID280].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetRelayedUcast(long refreshPeriod)
        {
            if (_attributes[ATTR_RELAYEDUCAST].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_RELAYEDUCAST].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_RELAYEDUCAST]);
        }


        /// <summary>
        /// Get the PhytoMACqueuelimitreached attribute [attribute ID281].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetPhytoMACqueuelimitreachedAsync()
        {
            return Read(_attributes[ATTR_PHYTOMACQUEUELIMITREACHED]);
        }

        /// <summary>
        /// Synchronously Get the PhytoMACqueuelimitreached attribute [attribute ID281].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetPhytoMACqueuelimitreached(long refreshPeriod)
        {
            if (_attributes[ATTR_PHYTOMACQUEUELIMITREACHED].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_PHYTOMACQUEUELIMITREACHED].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_PHYTOMACQUEUELIMITREACHED]);
        }


        /// <summary>
        /// Get the PacketValidatedropcount attribute [attribute ID282].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetPacketValidatedropcountAsync()
        {
            return Read(_attributes[ATTR_PACKETVALIDATEDROPCOUNT]);
        }

        /// <summary>
        /// Synchronously Get the PacketValidatedropcount attribute [attribute ID282].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetPacketValidatedropcount(long refreshPeriod)
        {
            if (_attributes[ATTR_PACKETVALIDATEDROPCOUNT].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_PACKETVALIDATEDROPCOUNT].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_PACKETVALIDATEDROPCOUNT]);
        }


        /// <summary>
        /// Get the AverageMACRetryPerAPSMessageSent attribute [attribute ID283].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetAverageMACRetryPerAPSMessageSentAsync()
        {
            return Read(_attributes[ATTR_AVERAGEMACRETRYPERAPSMESSAGESENT]);
        }

        /// <summary>
        /// Synchronously Get the AverageMACRetryPerAPSMessageSent attribute [attribute ID283].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetAverageMACRetryPerAPSMessageSent(long refreshPeriod)
        {
            if (_attributes[ATTR_AVERAGEMACRETRYPERAPSMESSAGESENT].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_AVERAGEMACRETRYPERAPSMESSAGESENT].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_AVERAGEMACRETRYPERAPSMESSAGESENT]);
        }


        /// <summary>
        /// Get the LastMessageLQI attribute [attribute ID284].
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetLastMessageLQIAsync()
        {
            return Read(_attributes[ATTR_LASTMESSAGELQI]);
        }

        /// <summary>
        /// Synchronously Get the LastMessageLQI attribute [attribute ID284].
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetLastMessageLQI(long refreshPeriod)
        {
            if (_attributes[ATTR_LASTMESSAGELQI].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_LASTMESSAGELQI].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_LASTMESSAGELQI]);
        }


        /// <summary>
        /// Get the LastMessageRSSI attribute [attribute ID285].
        ///
        /// The attribute is of type sbyte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetLastMessageRSSIAsync()
        {
            return Read(_attributes[ATTR_LASTMESSAGERSSI]);
        }

        /// <summary>
        /// Synchronously Get the LastMessageRSSI attribute [attribute ID285].
        ///
        /// The attribute is of type sbyte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public sbyte GetLastMessageRSSI(long refreshPeriod)
        {
            if (_attributes[ATTR_LASTMESSAGERSSI].IsLastValueCurrent(refreshPeriod))
            {
                return (sbyte)_attributes[ATTR_LASTMESSAGERSSI].LastValue;
            }

            return (sbyte)ReadSync(_attributes[ATTR_LASTMESSAGERSSI]);
        }

    }
}
