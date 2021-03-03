
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Occupancy Sensing cluster implementation (Cluster ID 0x0406).
    ///
    /// The cluster provides an interface to occupancy sensing functionality, including
    /// configuration and provision of notifications of occupancy status.
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
        public const string CLUSTER_NAME = "Occupancy Sensing";

        // Attribute constants

        /// <summary>
        /// The Occupancy attribute is a bitmap.
        /// Bit 0 specifies the sensed occupancy as follows: 1 = occupied, 0 = unoccupied. All
        /// other bits are reserved.
        /// </summary>
        public const ushort ATTR_OCCUPANCY = 0x0000;

        /// <summary>
        /// The OccupancySensorType attribute specifies the type of the occupancy sensor.
        /// </summary>
        public const ushort ATTR_OCCUPANCYSENSORTYPE = 0x0001;

        /// <summary>
        /// The PIROccupiedToUnoccupiedDelay attribute is 8-bits in length and specifies
        /// the time delay, in seconds, before the PIR sensor changes to its occupied state when
        /// the sensed area becomes unoccupied. This attribute, along with
        /// PIRUnoccupiedToOccupiedTime, may be used to reduce sensor 'chatter' when used in
        /// an area where occupation changes frequently.
        /// </summary>
        public const ushort ATTR_PIROCCUPIEDTOUNOCCUPIEDDELAY = 0x0010;

        /// <summary>
        /// The PIRUnoccupiedToOccupiedDelay attribute is 8-bits in length and specifies
        /// the time delay, in seconds, before the PIR sensor changes to its unoccupied state
        /// when the sensed area becomes occupied.
        /// </summary>
        public const ushort ATTR_PIRUNOCCUPIEDTOOCCUPIEDDELAY = 0x0011;

        /// <summary>
        /// The PIRUnoccupiedToOccupiedThreshold attribute is 8 bits in length and
        /// specifies the number of movement detection events that must occur in the period
        /// PIRUnoccupiedToOccupiedDelay, before the PIR sensor changes to its occupied
        /// state. This attribute is mandatory if the PIRUnoccupiedToOccupiedDelay
        /// attribute is implemented.
        /// </summary>
        public const ushort ATTR_PIRUNOCCUPIEDTOOCCUPIEDTHRESHOLD = 0x0012;

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
        /// The UltrasonicUnoccupiedToOccupiedThreshold attribute is 8 bits in length and
        /// specifies the number of movement detection events that must occur in the period
        /// UltrasonicUnoccupiedToOccupiedDelay, before the Ultrasonic sensor changes to
        /// its occupied state. This attribute is mandatory if the
        /// UltrasonicUnoccupiedToOccupiedDelay attribute is implemented.
        /// </summary>
        public const ushort ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDTHRESHOLD = 0x0022;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(8);

            attributeMap.Add(ATTR_OCCUPANCY, new ZclAttribute(this, ATTR_OCCUPANCY, "Occupancy", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, true));
            attributeMap.Add(ATTR_OCCUPANCYSENSORTYPE, new ZclAttribute(this, ATTR_OCCUPANCYSENSORTYPE, "Occupancy Sensor Type", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_PIROCCUPIEDTOUNOCCUPIEDDELAY, new ZclAttribute(this, ATTR_PIROCCUPIEDTOUNOCCUPIEDDELAY, "PIR Occupied To Unoccupied Delay", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_PIRUNOCCUPIEDTOOCCUPIEDDELAY, new ZclAttribute(this, ATTR_PIRUNOCCUPIEDTOOCCUPIEDDELAY, "PIR Unoccupied To Occupied Delay", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_PIRUNOCCUPIEDTOOCCUPIEDTHRESHOLD, new ZclAttribute(this, ATTR_PIRUNOCCUPIEDTOOCCUPIEDTHRESHOLD, "PIR Unoccupied To Occupied Threshold", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_ULTRASONICOCCUPIEDTOUNOCCUPIEDDELAY, new ZclAttribute(this, ATTR_ULTRASONICOCCUPIEDTOUNOCCUPIEDDELAY, "Ultra Sonic Occupied To Unoccupied Delay", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDDELAY, new ZclAttribute(this, ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDDELAY, "Ultra Sonic Unoccupied To Occupied Delay", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDTHRESHOLD, new ZclAttribute(this, ATTR_ULTRASONICUNOCCUPIEDTOOCCUPIEDTHRESHOLD, "Ultrasonic Unoccupied To Occupied Threshold", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Occupancy Sensing cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclOccupancySensingCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
