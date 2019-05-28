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
    /// Occupancy sensingcluster implementation (Cluster ID 0x0406).
    ///
    /// The cluster provides an interface to occupancy sensing functionality,
    /// including configuration and provision of notifications of occupancy status.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclOccupancySensingCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0406;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Occupancy sensing";

        /* Attribute constants */

        /// <summary>
        /// The Occupancy attribute is a bitmap.
        /// 
        /// Bit 0 specifies the sensed occupancy as follows: 1 = occupied, 0 = unoccupied.
        /// All other bits are reserved.
        /// </summary>
        public const ushort ATTR_OCCUPANCY = 0x0000;

        /// <summary>
        /// The OccupancySensorType attribute specifies the type of the occupancy sensor.
        /// </summary>
        public const ushort ATTR_OCCUPANCYSENSORTYPE = 0x0001;

        /// <summary>
        /// The PIROccupiedToUnoccupiedDelay attribute is 8-bits in length and specifies
        /// the time delay, in seconds, before the PIR sensor changes to its occupied state
        /// when the sensed area becomes unoccupied. This attribute, along with
        /// PIRUnoccupiedToOccupiedTime, may be used to reduce sensor 'chatter' when
        /// used in an area where occupation changes frequently.
        /// </summary>
        public const ushort ATTR_PIROCCUPIEDTOUNOCCUPIEDDELAY = 0x0010;

        /// <summary>
        /// The PIRUnoccupiedToOccupiedDelay attribute is 8-bits in length and specifies
        /// the time delay, in seconds, before the PIR sensor changes to its unoccupied state
        /// when the sensed area becomes occupied.
        /// </summary>
        public const ushort ATTR_PIRUNOCCUPIEDTOOCCUPIEDDELAY = 0x0011;

        /// <summary>
        /// The UltraSonicOccupiedToUnoccupiedTime attribute specifies the time delay, in
        /// seconds, before the ultrasonic sensor changes to its occupied state when the
        /// sensed area becomes unoccupied. This attribute, along with
        /// UltraSonicUnoccupiedToOccupiedTime, may be used to reduce sensor 'chatter'
        /// when used in an area where occupation changes frequently.
        /// </summary>
        public const ushort ATTR_ULTRASONICOCCUPIEDTOUNOCCUPIEDDELAY = 0x0020;

        /// <summary>
        /// The UltraSonicUnoccupiedToOccupiedTime attribute specifies the time delay, in
        /// seconds, before the ultrasonic sensor changes to its unoccupied state when the
        /// sensed area becomes occupied.
        /// </summary>
        public const ushort ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDDELAY = 0x0021;

        /// <summary>
        /// </summary>
        public const ushort ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDTHRESHOLD = 0x0022;


        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(7);

            ZclClusterType occupancysensing = ZclClusterType.GetValueById(ClusterType.OCCUPANCY_SENSING);

            attributeMap.Add(ATTR_OCCUPANCY, new ZclAttribute(occupancysensing, ATTR_OCCUPANCY, "Occupancy", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, true));
            attributeMap.Add(ATTR_OCCUPANCYSENSORTYPE, new ZclAttribute(occupancysensing, ATTR_OCCUPANCYSENSORTYPE, "OccupancySensorType", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_PIROCCUPIEDTOUNOCCUPIEDDELAY, new ZclAttribute(occupancysensing, ATTR_PIROCCUPIEDTOUNOCCUPIEDDELAY, "PIROccupiedToUnoccupiedDelay", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_PIRUNOCCUPIEDTOOCCUPIEDDELAY, new ZclAttribute(occupancysensing, ATTR_PIRUNOCCUPIEDTOOCCUPIEDDELAY, "PIRUnoccupiedToOccupiedDelay", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_ULTRASONICOCCUPIEDTOUNOCCUPIEDDELAY, new ZclAttribute(occupancysensing, ATTR_ULTRASONICOCCUPIEDTOUNOCCUPIEDDELAY, "UltraSonicOccupiedToUnoccupiedDelay", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDDELAY, new ZclAttribute(occupancysensing, ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDDELAY, "UltraSonicUnoccupiedToOccupiedDelay", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDTHRESHOLD, new ZclAttribute(occupancysensing, ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDTHRESHOLD, "UltrasonicUnoccupiedToOccupiedThreshold", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Occupancy sensing cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclOccupancySensingCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// Get the Occupancy attribute [attribute ID0].
        ///
        /// The Occupancy attribute is a bitmap.
        /// 
        /// Bit 0 specifies the sensed occupancy as follows: 1 = occupied, 0 = unoccupied.
        /// All other bits are reserved.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetOccupancyAsync()
        {
            return Read(_attributes[ATTR_OCCUPANCY]);
        }

        /// <summary>
        /// Synchronously Get the Occupancy attribute [attribute ID0].
        ///
        /// The Occupancy attribute is a bitmap.
        /// 
        /// Bit 0 specifies the sensed occupancy as follows: 1 = occupied, 0 = unoccupied.
        /// All other bits are reserved.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetOccupancy(long refreshPeriod)
        {
            if (_attributes[ATTR_OCCUPANCY].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_OCCUPANCY].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_OCCUPANCY]);
        }


        /// <summary>
        /// Set reporting for the Occupancy attribute [attribute ID0].
        ///
        /// The Occupancy attribute is a bitmap.
        /// 
        /// Bit 0 specifies the sensed occupancy as follows: 1 = occupied, 0 = unoccupied.
        /// All other bits are reserved.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <param name="minInterval">Minimum reporting period</param>
        /// <param name="maxInterval">Maximum reporting period</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetOccupancyReporting(ushort minInterval, ushort maxInterval)
        {
            return SetReporting(_attributes[ATTR_OCCUPANCY], minInterval, maxInterval);
        }


        /// <summary>
        /// Get the OccupancySensorType attribute [attribute ID1].
        ///
        /// The OccupancySensorType attribute specifies the type of the occupancy sensor.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetOccupancySensorTypeAsync()
        {
            return Read(_attributes[ATTR_OCCUPANCYSENSORTYPE]);
        }

        /// <summary>
        /// Synchronously Get the OccupancySensorType attribute [attribute ID1].
        ///
        /// The OccupancySensorType attribute specifies the type of the occupancy sensor.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetOccupancySensorType(long refreshPeriod)
        {
            if (_attributes[ATTR_OCCUPANCYSENSORTYPE].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_OCCUPANCYSENSORTYPE].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_OCCUPANCYSENSORTYPE]);
        }


        /// <summary>
        /// Set the PIROccupiedToUnoccupiedDelay attribute [attribute ID16].
        ///
        /// The PIROccupiedToUnoccupiedDelay attribute is 8-bits in length and specifies
        /// the time delay, in seconds, before the PIR sensor changes to its occupied state
        /// when the sensed area becomes unoccupied. This attribute, along with
        /// PIRUnoccupiedToOccupiedTime, may be used to reduce sensor 'chatter' when
        /// used in an area where occupation changes frequently.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="pIROccupiedToUnoccupiedDelay">The byte attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetPIROccupiedToUnoccupiedDelay(object value)
        {
            return Write(_attributes[ATTR_PIROCCUPIEDTOUNOCCUPIEDDELAY], value);
        }


        /// <summary>
        /// Get the PIROccupiedToUnoccupiedDelay attribute [attribute ID16].
        ///
        /// The PIROccupiedToUnoccupiedDelay attribute is 8-bits in length and specifies
        /// the time delay, in seconds, before the PIR sensor changes to its occupied state
        /// when the sensed area becomes unoccupied. This attribute, along with
        /// PIRUnoccupiedToOccupiedTime, may be used to reduce sensor 'chatter' when
        /// used in an area where occupation changes frequently.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetPIROccupiedToUnoccupiedDelayAsync()
        {
            return Read(_attributes[ATTR_PIROCCUPIEDTOUNOCCUPIEDDELAY]);
        }

        /// <summary>
        /// Synchronously Get the PIROccupiedToUnoccupiedDelay attribute [attribute ID16].
        ///
        /// The PIROccupiedToUnoccupiedDelay attribute is 8-bits in length and specifies
        /// the time delay, in seconds, before the PIR sensor changes to its occupied state
        /// when the sensed area becomes unoccupied. This attribute, along with
        /// PIRUnoccupiedToOccupiedTime, may be used to reduce sensor 'chatter' when
        /// used in an area where occupation changes frequently.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetPIROccupiedToUnoccupiedDelay(long refreshPeriod)
        {
            if (_attributes[ATTR_PIROCCUPIEDTOUNOCCUPIEDDELAY].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_PIROCCUPIEDTOUNOCCUPIEDDELAY].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_PIROCCUPIEDTOUNOCCUPIEDDELAY]);
        }


        /// <summary>
        /// Set the PIRUnoccupiedToOccupiedDelay attribute [attribute ID17].
        ///
        /// The PIRUnoccupiedToOccupiedDelay attribute is 8-bits in length and specifies
        /// the time delay, in seconds, before the PIR sensor changes to its unoccupied state
        /// when the sensed area becomes occupied.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="pIRUnoccupiedToOccupiedDelay">The byte attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetPIRUnoccupiedToOccupiedDelay(object value)
        {
            return Write(_attributes[ATTR_PIRUNOCCUPIEDTOOCCUPIEDDELAY], value);
        }


        /// <summary>
        /// Get the PIRUnoccupiedToOccupiedDelay attribute [attribute ID17].
        ///
        /// The PIRUnoccupiedToOccupiedDelay attribute is 8-bits in length and specifies
        /// the time delay, in seconds, before the PIR sensor changes to its unoccupied state
        /// when the sensed area becomes occupied.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetPIRUnoccupiedToOccupiedDelayAsync()
        {
            return Read(_attributes[ATTR_PIRUNOCCUPIEDTOOCCUPIEDDELAY]);
        }

        /// <summary>
        /// Synchronously Get the PIRUnoccupiedToOccupiedDelay attribute [attribute ID17].
        ///
        /// The PIRUnoccupiedToOccupiedDelay attribute is 8-bits in length and specifies
        /// the time delay, in seconds, before the PIR sensor changes to its unoccupied state
        /// when the sensed area becomes occupied.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetPIRUnoccupiedToOccupiedDelay(long refreshPeriod)
        {
            if (_attributes[ATTR_PIRUNOCCUPIEDTOOCCUPIEDDELAY].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_PIRUNOCCUPIEDTOOCCUPIEDDELAY].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_PIRUNOCCUPIEDTOOCCUPIEDDELAY]);
        }


        /// <summary>
        /// Set the UltraSonicOccupiedToUnoccupiedDelay attribute [attribute ID32].
        ///
        /// The UltraSonicOccupiedToUnoccupiedTime attribute specifies the time delay, in
        /// seconds, before the ultrasonic sensor changes to its occupied state when the
        /// sensed area becomes unoccupied. This attribute, along with
        /// UltraSonicUnoccupiedToOccupiedTime, may be used to reduce sensor 'chatter'
        /// when used in an area where occupation changes frequently.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="ultraSonicOccupiedToUnoccupiedDelay">The byte attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetUltraSonicOccupiedToUnoccupiedDelay(object value)
        {
            return Write(_attributes[ATTR_ULTRASONICOCCUPIEDTOUNOCCUPIEDDELAY], value);
        }


        /// <summary>
        /// Get the UltraSonicOccupiedToUnoccupiedDelay attribute [attribute ID32].
        ///
        /// The UltraSonicOccupiedToUnoccupiedTime attribute specifies the time delay, in
        /// seconds, before the ultrasonic sensor changes to its occupied state when the
        /// sensed area becomes unoccupied. This attribute, along with
        /// UltraSonicUnoccupiedToOccupiedTime, may be used to reduce sensor 'chatter'
        /// when used in an area where occupation changes frequently.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetUltraSonicOccupiedToUnoccupiedDelayAsync()
        {
            return Read(_attributes[ATTR_ULTRASONICOCCUPIEDTOUNOCCUPIEDDELAY]);
        }

        /// <summary>
        /// Synchronously Get the UltraSonicOccupiedToUnoccupiedDelay attribute [attribute ID32].
        ///
        /// The UltraSonicOccupiedToUnoccupiedTime attribute specifies the time delay, in
        /// seconds, before the ultrasonic sensor changes to its occupied state when the
        /// sensed area becomes unoccupied. This attribute, along with
        /// UltraSonicUnoccupiedToOccupiedTime, may be used to reduce sensor 'chatter'
        /// when used in an area where occupation changes frequently.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetUltraSonicOccupiedToUnoccupiedDelay(long refreshPeriod)
        {
            if (_attributes[ATTR_ULTRASONICOCCUPIEDTOUNOCCUPIEDDELAY].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_ULTRASONICOCCUPIEDTOUNOCCUPIEDDELAY].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_ULTRASONICOCCUPIEDTOUNOCCUPIEDDELAY]);
        }


        /// <summary>
        /// Set the UltraSonicUnoccupiedToOccupiedDelay attribute [attribute ID33].
        ///
        /// The UltraSonicUnoccupiedToOccupiedTime attribute specifies the time delay, in
        /// seconds, before the ultrasonic sensor changes to its unoccupied state when the
        /// sensed area becomes occupied.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="ultraSonicUnoccupiedToOccupiedDelay">The byte attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetUltraSonicUnoccupiedToOccupiedDelay(object value)
        {
            return Write(_attributes[ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDDELAY], value);
        }


        /// <summary>
        /// Get the UltraSonicUnoccupiedToOccupiedDelay attribute [attribute ID33].
        ///
        /// The UltraSonicUnoccupiedToOccupiedTime attribute specifies the time delay, in
        /// seconds, before the ultrasonic sensor changes to its unoccupied state when the
        /// sensed area becomes occupied.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetUltraSonicUnoccupiedToOccupiedDelayAsync()
        {
            return Read(_attributes[ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDDELAY]);
        }

        /// <summary>
        /// Synchronously Get the UltraSonicUnoccupiedToOccupiedDelay attribute [attribute ID33].
        ///
        /// The UltraSonicUnoccupiedToOccupiedTime attribute specifies the time delay, in
        /// seconds, before the ultrasonic sensor changes to its unoccupied state when the
        /// sensed area becomes occupied.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetUltraSonicUnoccupiedToOccupiedDelay(long refreshPeriod)
        {
            if (_attributes[ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDDELAY].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDDELAY].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDDELAY]);
        }


        /// <summary>
        /// Set the UltrasonicUnoccupiedToOccupiedThreshold attribute [attribute ID34].
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="ultrasonicUnoccupiedToOccupiedThreshold">The byte attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetUltrasonicUnoccupiedToOccupiedThreshold(object value)
        {
            return Write(_attributes[ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDTHRESHOLD], value);
        }


        /// <summary>
        /// Get the UltrasonicUnoccupiedToOccupiedThreshold attribute [attribute ID34].
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetUltrasonicUnoccupiedToOccupiedThresholdAsync()
        {
            return Read(_attributes[ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDTHRESHOLD]);
        }

        /// <summary>
        /// Synchronously Get the UltrasonicUnoccupiedToOccupiedThreshold attribute [attribute ID34].
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetUltrasonicUnoccupiedToOccupiedThreshold(long refreshPeriod)
        {
            if (_attributes[ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDTHRESHOLD].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDTHRESHOLD].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDTHRESHOLD]);
        }

    }
}
