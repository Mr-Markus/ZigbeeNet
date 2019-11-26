
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
    /// Time cluster implementation (Cluster ID 0x000A).
    ///
    /// This cluster provides a basic interface to a real-time clock. The clock time may be read
    /// and also written, in order to synchronize the clock (as close as practical) to a time
    /// standard. This time standard is the number of seconds since 0 hrs 0 mins 0 sec on 1st
    /// January 2000 UTC (Universal Coordinated Time).
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclTimeCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x000A;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Time";

        // Attribute constants

        /// <summary>
        /// The Time attribute is 32-bits in length and holds the time value of a real time clock.
        /// This attribute has data type UTCTime, but note that it may not actually be
        /// synchronised to UTC - see discussion of the TimeStatus attribute below.
        /// If the Master bit of the TimeStatus attribute has a value of 0, writing to this
        /// attribute shall set the real time clock to the written value, otherwise it cannot be
        /// written. The value 0xffffffff indicates an invalid time.
        /// </summary>
        public const ushort ATTR_TIME = 0x0000;

        /// <summary>
        /// The TimeStatus attribute holds a number of bit fields.
        /// </summary>
        public const ushort ATTR_TIMESTATUS = 0x0001;

        /// <summary>
        /// The TimeZone attribute indicates the local time zone, as a signed offset in seconds
        /// from the Time attribute value. The value 0xffffffff indicates an invalid time
        /// zone.
        /// </summary>
        public const ushort ATTR_TIMEZONE = 0x0002;

        /// <summary>
        /// The DstStart attribute indicates the DST start time in seconds. The value
        /// 0xffffffff indicates an invalid DST start time.
        /// </summary>
        public const ushort ATTR_DSTSTART = 0x0003;

        /// <summary>
        /// The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff
        /// indicates an invalid DST end time.
        /// Note that the three attributes DstStart, DstEnd and DstShift are optional, but if
        /// any one of them is implemented the other two must also be implemented. Note that this
        /// attribute should be set to a new value once every year.
        /// Note that this attribute should be set to a new value once every year, and should be
        /// written synchronously with the DstStart attribute. The DstEnd attribute
        /// indicates the DST end time in seconds. The value 0xffffffff indicates an invalid
        /// DST end time.
        /// Note that this attribute should be set to a new value once every year, and should be
        /// written synchronously with the DstStart attribute
        /// </summary>
        public const ushort ATTR_DSTEND = 0x0004;

        /// <summary>
        /// The DstShift attribute represents a signed offset in seconds from the standard
        /// time, to be applied between the times DstStart and DstEnd to calculate the Local
        /// Time. The value 0xffffffff indicates an invalid DST shift.
        /// The range of this attribute is +/- one day. Note that the actual range of DST values
        /// employed by countries is much smaller than this, so the manufacturer has the option
        /// to impose a smaller range.
        /// </summary>
        public const ushort ATTR_DSTSHIFT = 0x0005;

        /// <summary>
        /// A device may derive the time by reading the Time and TimeZone attributes and adding
        /// them together. If implemented however, the optional StandardTime attribute
        /// indicates this time directly. The value 0xffffffff indicates an invalid Standard
        /// Time.
        /// </summary>
        public const ushort ATTR_STANDARDTIME = 0x0006;

        /// <summary>
        /// A device may derive the time by reading the Time, TimeZone, DstStart, DstEnd and
        /// DstShift attributes and performing the calculation. If implemented however, the
        /// optional LocalTime attribute indicates this time directly. The value 0xffffffff
        /// indicates an invalid Local Time.
        /// </summary>
        public const ushort ATTR_LOCALTIME = 0x0007;

        /// <summary>
        /// The LastSetTime attribute indicates the most recent time that the Time attribute
        /// was set, either internally or over the ZigBee network (thus it holds a copy of the
        /// last value that Time was set to). This attribute is set automatically, so is Read
        /// Only. The value 0xffffffff indicates an invalid LastSetTime.
        /// </summary>
        public const ushort ATTR_LASTSETTIME = 0x0008;

        /// <summary>
        /// The ValidUntilTime attribute indicates a time, later than LastSetTime, up to
        /// which the Time attribute may be trusted. ‘Trusted’ means that the difference
        /// between the Time attribute and the true UTC time is less than an acceptable error.
        /// The acceptable error is not defined by this cluster specification, but may be
        /// defined by the application profile in which devices that use this cluster are
        /// specified.
        /// </summary>
        public const ushort ATTR_VALIDUNTILTIME = 0x0009;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(10);

            attributeMap.Add(ATTR_TIME, new ZclAttribute(this, ATTR_TIME, "Time", ZclDataType.Get(DataType.UTCTIME), true, true, true, false));
            attributeMap.Add(ATTR_TIMESTATUS, new ZclAttribute(this, ATTR_TIMESTATUS, "Time Status", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, true, false));
            attributeMap.Add(ATTR_TIMEZONE, new ZclAttribute(this, ATTR_TIMEZONE, "Time Zone", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_DSTSTART, new ZclAttribute(this, ATTR_DSTSTART, "DST Start", ZclDataType.Get(DataType.UTCTIME), false, true, true, false));
            attributeMap.Add(ATTR_DSTEND, new ZclAttribute(this, ATTR_DSTEND, "DST End", ZclDataType.Get(DataType.UTCTIME), false, true, true, false));
            attributeMap.Add(ATTR_DSTSHIFT, new ZclAttribute(this, ATTR_DSTSHIFT, "DST Shift", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_STANDARDTIME, new ZclAttribute(this, ATTR_STANDARDTIME, "Standard Time", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_LOCALTIME, new ZclAttribute(this, ATTR_LOCALTIME, "Local Time", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_LASTSETTIME, new ZclAttribute(this, ATTR_LASTSETTIME, "Last Set Time", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_VALIDUNTILTIME, new ZclAttribute(this, ATTR_VALIDUNTILTIME, "Valid Until Time", ZclDataType.Get(DataType.UTCTIME), false, true, true, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Time cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclTimeCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
