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
using ZigBeeNet.ZCL.Clusters.IASWD;

namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// IAS WDcluster implementation (Cluster ID 0x0502).
    ///
    /// The IAS WD cluster provides an interface to the functionality of any Warning
    /// Device equipment of the IAS system. Using this cluster, a ZigBee enabled CIE
    /// device can access a ZigBee enabled IAS WD device and issue alarm warning
    /// indications (siren, strobe lighting, etc.) when a system alarm condition is detected.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclIASWDCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0502;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "IAS WD";

        /* Attribute constants */

        /// <summary>
        /// The MaxDuration attribute specifies the maximum time in seconds that the siren
        /// will sound continuously, regardless of start/stop commands.
        /// </summary>
        public const ushort ATTR_MAXDURATION = 0x0000;

        /// <summary>
        /// </summary>
        public const ushort ATTR_ZONETYPE = 0x0001;

        /// <summary>
        /// </summary>
        public const ushort ATTR_ZONESTATUS = 0x0002;

        /// <summary>
        /// </summary>
        public const ushort ATTR_IAS_CIE_ADDRESS = 0x0010;


        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(4);

            ZclClusterType iASWD = ZclClusterType.GetValueById(ClusterType.IAS_WD);

            attributeMap.Add(ATTR_MAXDURATION, new ZclAttribute(iASWD, ATTR_MAXDURATION, "MaxDuration", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, true, false));
            attributeMap.Add(ATTR_ZONETYPE, new ZclAttribute(iASWD, ATTR_ZONETYPE, "ZoneType", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_ZONESTATUS, new ZclAttribute(iASWD, ATTR_ZONESTATUS, "ZoneStatus", ZclDataType.Get(DataType.BITMAP_16_BIT), true, true, false, false));
            attributeMap.Add(ATTR_IAS_CIE_ADDRESS, new ZclAttribute(iASWD, ATTR_IAS_CIE_ADDRESS, "IAS_CIE_Address", ZclDataType.Get(DataType.IEEE_ADDRESS), true, true, true, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a IAS WD cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclIASWDCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// Set the MaxDuration attribute [attribute ID0].
        ///
        /// The MaxDuration attribute specifies the maximum time in seconds that the siren
        /// will sound continuously, regardless of start/stop commands.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <param name="maxDuration">The ushort attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetMaxDuration(object value)
        {
            return Write(_attributes[ATTR_MAXDURATION], value);
        }


        /// <summary>
        /// Get the MaxDuration attribute [attribute ID0].
        ///
        /// The MaxDuration attribute specifies the maximum time in seconds that the siren
        /// will sound continuously, regardless of start/stop commands.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetMaxDurationAsync()
        {
            return Read(_attributes[ATTR_MAXDURATION]);
        }

        /// <summary>
        /// Synchronously Get the MaxDuration attribute [attribute ID0].
        ///
        /// The MaxDuration attribute specifies the maximum time in seconds that the siren
        /// will sound continuously, regardless of start/stop commands.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetMaxDuration(long refreshPeriod)
        {
            if (_attributes[ATTR_MAXDURATION].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_MAXDURATION].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_MAXDURATION]);
        }


        /// <summary>
        /// Get the ZoneType attribute [attribute ID1].
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetZoneTypeAsync()
        {
            return Read(_attributes[ATTR_ZONETYPE]);
        }

        /// <summary>
        /// Synchronously Get the ZoneType attribute [attribute ID1].
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetZoneType(long refreshPeriod)
        {
            if (_attributes[ATTR_ZONETYPE].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_ZONETYPE].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_ZONETYPE]);
        }


        /// <summary>
        /// Get the ZoneStatus attribute [attribute ID2].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetZoneStatusAsync()
        {
            return Read(_attributes[ATTR_ZONESTATUS]);
        }

        /// <summary>
        /// Synchronously Get the ZoneStatus attribute [attribute ID2].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetZoneStatus(long refreshPeriod)
        {
            if (_attributes[ATTR_ZONESTATUS].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ZONESTATUS].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ZONESTATUS]);
        }


        /// <summary>
        /// Set the IAS_CIE_Address attribute [attribute ID16].
        ///
        /// The attribute is of type IeeeAddress.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <param name="iASCIEAddress">The IeeeAddress attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetIASCIEAddress(object value)
        {
            return Write(_attributes[ATTR_IAS_CIE_ADDRESS], value);
        }


        /// <summary>
        /// Get the IAS_CIE_Address attribute [attribute ID16].
        ///
        /// The attribute is of type IeeeAddress.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetIASCIEAddressAsync()
        {
            return Read(_attributes[ATTR_IAS_CIE_ADDRESS]);
        }

        /// <summary>
        /// Synchronously Get the IAS_CIE_Address attribute [attribute ID16].
        ///
        /// The attribute is of type IeeeAddress.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public IeeeAddress GetIASCIEAddress(long refreshPeriod)
        {
            if (_attributes[ATTR_IAS_CIE_ADDRESS].IsLastValueCurrent(refreshPeriod))
            {
                return (IeeeAddress)_attributes[ATTR_IAS_CIE_ADDRESS].LastValue;
            }

            return (IeeeAddress)ReadSync(_attributes[ATTR_IAS_CIE_ADDRESS]);
        }


        /// <summary>
        /// The Start Warning Command
        ///
        /// This command starts the WD operation. The WD alerts the surrounding area by
        /// audible (siren) and visual (strobe) signals.
        /// <br>
        /// A Start Warning command shall always terminate the effect of any previous
        /// command that is still current.
        ///
        /// <param name="header"><see cref="byte"/> Header</param>
        /// <param name="warningDuration"><see cref="ushort"/> Warning duration</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> StartWarningCommand(byte header, ushort warningDuration)
        {
            StartWarningCommand command = new StartWarningCommand();

            // Set the fields
            command.Header = header;
            command.WarningDuration = warningDuration;

            return Send(command);
        }

        /// <summary>
        /// The Squawk Command
        ///
        /// <param name="header"><see cref="byte"/> Header</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SquawkCommand(byte header)
        {
            SquawkCommand command = new SquawkCommand();

            // Set the fields
            command.Header = header;

            return Send(command);
        }

        public override ZclCommand GetCommandFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // START_WARNING_COMMAND
                    return new StartWarningCommand();
                case 2: // SQUAWK_COMMAND
                    return new SquawkCommand();
                    default:
                        return null;
            }
        }
    }
}
