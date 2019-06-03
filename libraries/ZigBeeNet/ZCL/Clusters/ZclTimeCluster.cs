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
    /// Timecluster implementation (Cluster ID 0x000A).
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

        /* Attribute constants */

        /// <summary>
        /// The Time attribute is 32-bits in length and holds the time value of a real time
        /// clock. This attribute has data type UTCTime, but note that it may not actually be
        /// synchronised to UTC - see discussion of the TimeStatus attribute below.
        /// 
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
        /// from the Time attribute value. The value 0xffffffff indicates an invalid time zone.
        /// </summary>
        public const ushort ATTR_TIMEZONE = 0x0002;

        /// <summary>
        /// The DstStart attribute indicates the DST start time in seconds. The value 0xffffffff
        /// indicates an invalid DST start time.
        /// </summary>
        public const ushort ATTR_DSTSTART = 0x0003;

        /// <summary>
        /// The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff
        /// indicates an invalid DST end time.
        /// 
        /// Note that the three attributes DstStart, DstEnd and DstShift are optional, but if any
        /// one of them is implemented the other two must also be implemented.
        /// Note that this attribute should be set to a new value once every year.
        /// 
        /// Note that this attribute should be set to a new value once every year, and should be
        /// written synchronously with the DstStart attribute.
        /// The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff
        /// indicates an invalid DST end time.
        /// 
        /// Note that this attribute should be set to a new value once every year, and should be
        /// written synchronously with the DstStart attribute
        /// </summary>
        public const ushort ATTR_DSTEND = 0x0004;

        /// <summary>
        /// The DstShift attribute represents a signed offset in seconds from the standard time,
        /// to be applied between the times DstStart and DstEnd to calculate the Local Time.
        /// The value 0xffffffff indicates an invalid DST shift.
        /// 
        /// The range of this attribute is +/- one day. Note that the actual range of DST values
        /// employed by countries is much smaller than this, so the manufacturer has the
        /// option to impose a smaller range.
        /// </summary>
        public const ushort ATTR_DSTSHIFT = 0x0005;

        /// <summary>
        /// A device may derive the time by reading the Time and TimeZone attributes
        /// and adding them together. If implemented however, the optional StandardTime
        /// attribute indicates this time directly. The value 0xffffffff indicates an invalid
        /// Standard Time.
        /// </summary>
        public const ushort ATTR_STANDARDTIME = 0x0006;

        /// <summary>
        /// A device may derive the time by reading the Time, TimeZone, DstStart, DstEnd
        /// and DstShift attributes and performing the calculation. If implemented however,
        /// the optional LocalTime attribute indicates this time directly. The value 0xffffffff
        /// indicates an invalid Local Time.
        /// </summary>
        public const ushort ATTR_LOCALTIME = 0x0007;


        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(8);

            ZclClusterType time = ZclClusterType.GetValueById(ClusterType.TIME);

            attributeMap.Add(ATTR_TIME, new ZclAttribute(time, ATTR_TIME, "Time", ZclDataType.Get(DataType.UTCTIME), true, true, true, false));
            attributeMap.Add(ATTR_TIMESTATUS, new ZclAttribute(time, ATTR_TIMESTATUS, "TimeStatus", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_TIMEZONE, new ZclAttribute(time, ATTR_TIMEZONE, "TimeZone", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_DSTSTART, new ZclAttribute(time, ATTR_DSTSTART, "DstStart", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_DSTEND, new ZclAttribute(time, ATTR_DSTEND, "DstEnd", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_DSTSHIFT, new ZclAttribute(time, ATTR_DSTSHIFT, "DstShift", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_STANDARDTIME, new ZclAttribute(time, ATTR_STANDARDTIME, "StandardTime", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_LOCALTIME, new ZclAttribute(time, ATTR_LOCALTIME, "LocalTime", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Time cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclTimeCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// Set the Time attribute [attribute ID0].
        ///
        /// The Time attribute is 32-bits in length and holds the time value of a real time
        /// clock. This attribute has data type UTCTime, but note that it may not actually be
        /// synchronised to UTC - see discussion of the TimeStatus attribute below.
        /// 
        /// If the Master bit of the TimeStatus attribute has a value of 0, writing to this
        /// attribute shall set the real time clock to the written value, otherwise it cannot be
        /// written. The value 0xffffffff indicates an invalid time.
        ///
        /// The attribute is of type DateTime.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <param name="time">The DateTime attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetTime(object value)
        {
            return Write(_attributes[ATTR_TIME], value);
        }


        /// <summary>
        /// Get the Time attribute [attribute ID0].
        ///
        /// The Time attribute is 32-bits in length and holds the time value of a real time
        /// clock. This attribute has data type UTCTime, but note that it may not actually be
        /// synchronised to UTC - see discussion of the TimeStatus attribute below.
        /// 
        /// If the Master bit of the TimeStatus attribute has a value of 0, writing to this
        /// attribute shall set the real time clock to the written value, otherwise it cannot be
        /// written. The value 0xffffffff indicates an invalid time.
        ///
        /// The attribute is of type DateTime.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetTimeAsync()
        {
            return Read(_attributes[ATTR_TIME]);
        }

        /// <summary>
        /// Synchronously Get the Time attribute [attribute ID0].
        ///
        /// The Time attribute is 32-bits in length and holds the time value of a real time
        /// clock. This attribute has data type UTCTime, but note that it may not actually be
        /// synchronised to UTC - see discussion of the TimeStatus attribute below.
        /// 
        /// If the Master bit of the TimeStatus attribute has a value of 0, writing to this
        /// attribute shall set the real time clock to the written value, otherwise it cannot be
        /// written. The value 0xffffffff indicates an invalid time.
        ///
        /// The attribute is of type DateTime.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public DateTime GetTime(long refreshPeriod)
        {
            if (_attributes[ATTR_TIME].IsLastValueCurrent(refreshPeriod))
            {
                return (DateTime)_attributes[ATTR_TIME].LastValue;
            }

            return (DateTime)ReadSync(_attributes[ATTR_TIME]);
        }


        /// <summary>
        /// Set the TimeStatus attribute [attribute ID1].
        ///
        /// The TimeStatus attribute holds a number of bit fields.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="timeStatus">The ushort attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetTimeStatus(object value)
        {
            return Write(_attributes[ATTR_TIMESTATUS], value);
        }


        /// <summary>
        /// Get the TimeStatus attribute [attribute ID1].
        ///
        /// The TimeStatus attribute holds a number of bit fields.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetTimeStatusAsync()
        {
            return Read(_attributes[ATTR_TIMESTATUS]);
        }

        /// <summary>
        /// Synchronously Get the TimeStatus attribute [attribute ID1].
        ///
        /// The TimeStatus attribute holds a number of bit fields.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetTimeStatus(long refreshPeriod)
        {
            if (_attributes[ATTR_TIMESTATUS].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_TIMESTATUS].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_TIMESTATUS]);
        }


        /// <summary>
        /// Set the TimeZone attribute [attribute ID2].
        ///
        /// The TimeZone attribute indicates the local time zone, as a signed offset in seconds
        /// from the Time attribute value. The value 0xffffffff indicates an invalid time zone.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="timeZone">The int attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetTimeZone(object value)
        {
            return Write(_attributes[ATTR_TIMEZONE], value);
        }


        /// <summary>
        /// Get the TimeZone attribute [attribute ID2].
        ///
        /// The TimeZone attribute indicates the local time zone, as a signed offset in seconds
        /// from the Time attribute value. The value 0xffffffff indicates an invalid time zone.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetTimeZoneAsync()
        {
            return Read(_attributes[ATTR_TIMEZONE]);
        }

        /// <summary>
        /// Synchronously Get the TimeZone attribute [attribute ID2].
        ///
        /// The TimeZone attribute indicates the local time zone, as a signed offset in seconds
        /// from the Time attribute value. The value 0xffffffff indicates an invalid time zone.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public int GetTimeZone(long refreshPeriod)
        {
            if (_attributes[ATTR_TIMEZONE].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_TIMEZONE].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_TIMEZONE]);
        }


        /// <summary>
        /// Set the DstStart attribute [attribute ID3].
        ///
        /// The DstStart attribute indicates the DST start time in seconds. The value 0xffffffff
        /// indicates an invalid DST start time.
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="dstStart">The uint attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetDstStart(object value)
        {
            return Write(_attributes[ATTR_DSTSTART], value);
        }


        /// <summary>
        /// Get the DstStart attribute [attribute ID3].
        ///
        /// The DstStart attribute indicates the DST start time in seconds. The value 0xffffffff
        /// indicates an invalid DST start time.
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetDstStartAsync()
        {
            return Read(_attributes[ATTR_DSTSTART]);
        }

        /// <summary>
        /// Synchronously Get the DstStart attribute [attribute ID3].
        ///
        /// The DstStart attribute indicates the DST start time in seconds. The value 0xffffffff
        /// indicates an invalid DST start time.
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public uint GetDstStart(long refreshPeriod)
        {
            if (_attributes[ATTR_DSTSTART].IsLastValueCurrent(refreshPeriod))
            {
                return (uint)_attributes[ATTR_DSTSTART].LastValue;
            }

            return (uint)ReadSync(_attributes[ATTR_DSTSTART]);
        }


        /// <summary>
        /// Set the DstEnd attribute [attribute ID4].
        ///
        /// The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff
        /// indicates an invalid DST end time.
        /// 
        /// Note that the three attributes DstStart, DstEnd and DstShift are optional, but if any
        /// one of them is implemented the other two must also be implemented.
        /// Note that this attribute should be set to a new value once every year.
        /// 
        /// Note that this attribute should be set to a new value once every year, and should be
        /// written synchronously with the DstStart attribute.
        /// The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff
        /// indicates an invalid DST end time.
        /// 
        /// Note that this attribute should be set to a new value once every year, and should be
        /// written synchronously with the DstStart attribute
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="dstEnd">The uint attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetDstEnd(object value)
        {
            return Write(_attributes[ATTR_DSTEND], value);
        }


        /// <summary>
        /// Get the DstEnd attribute [attribute ID4].
        ///
        /// The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff
        /// indicates an invalid DST end time.
        /// 
        /// Note that the three attributes DstStart, DstEnd and DstShift are optional, but if any
        /// one of them is implemented the other two must also be implemented.
        /// Note that this attribute should be set to a new value once every year.
        /// 
        /// Note that this attribute should be set to a new value once every year, and should be
        /// written synchronously with the DstStart attribute.
        /// The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff
        /// indicates an invalid DST end time.
        /// 
        /// Note that this attribute should be set to a new value once every year, and should be
        /// written synchronously with the DstStart attribute
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetDstEndAsync()
        {
            return Read(_attributes[ATTR_DSTEND]);
        }

        /// <summary>
        /// Synchronously Get the DstEnd attribute [attribute ID4].
        ///
        /// The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff
        /// indicates an invalid DST end time.
        /// 
        /// Note that the three attributes DstStart, DstEnd and DstShift are optional, but if any
        /// one of them is implemented the other two must also be implemented.
        /// Note that this attribute should be set to a new value once every year.
        /// 
        /// Note that this attribute should be set to a new value once every year, and should be
        /// written synchronously with the DstStart attribute.
        /// The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff
        /// indicates an invalid DST end time.
        /// 
        /// Note that this attribute should be set to a new value once every year, and should be
        /// written synchronously with the DstStart attribute
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public uint GetDstEnd(long refreshPeriod)
        {
            if (_attributes[ATTR_DSTEND].IsLastValueCurrent(refreshPeriod))
            {
                return (uint)_attributes[ATTR_DSTEND].LastValue;
            }

            return (uint)ReadSync(_attributes[ATTR_DSTEND]);
        }


        /// <summary>
        /// Set the DstShift attribute [attribute ID5].
        ///
        /// The DstShift attribute represents a signed offset in seconds from the standard time,
        /// to be applied between the times DstStart and DstEnd to calculate the Local Time.
        /// The value 0xffffffff indicates an invalid DST shift.
        /// 
        /// The range of this attribute is +/- one day. Note that the actual range of DST values
        /// employed by countries is much smaller than this, so the manufacturer has the
        /// option to impose a smaller range.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="dstShift">The int attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetDstShift(object value)
        {
            return Write(_attributes[ATTR_DSTSHIFT], value);
        }


        /// <summary>
        /// Get the DstShift attribute [attribute ID5].
        ///
        /// The DstShift attribute represents a signed offset in seconds from the standard time,
        /// to be applied between the times DstStart and DstEnd to calculate the Local Time.
        /// The value 0xffffffff indicates an invalid DST shift.
        /// 
        /// The range of this attribute is +/- one day. Note that the actual range of DST values
        /// employed by countries is much smaller than this, so the manufacturer has the
        /// option to impose a smaller range.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetDstShiftAsync()
        {
            return Read(_attributes[ATTR_DSTSHIFT]);
        }

        /// <summary>
        /// Synchronously Get the DstShift attribute [attribute ID5].
        ///
        /// The DstShift attribute represents a signed offset in seconds from the standard time,
        /// to be applied between the times DstStart and DstEnd to calculate the Local Time.
        /// The value 0xffffffff indicates an invalid DST shift.
        /// 
        /// The range of this attribute is +/- one day. Note that the actual range of DST values
        /// employed by countries is much smaller than this, so the manufacturer has the
        /// option to impose a smaller range.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public int GetDstShift(long refreshPeriod)
        {
            if (_attributes[ATTR_DSTSHIFT].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_DSTSHIFT].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_DSTSHIFT]);
        }


        /// <summary>
        /// Get the StandardTime attribute [attribute ID6].
        ///
        /// A device may derive the time by reading the Time and TimeZone attributes
        /// and adding them together. If implemented however, the optional StandardTime
        /// attribute indicates this time directly. The value 0xffffffff indicates an invalid
        /// Standard Time.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetStandardTimeAsync()
        {
            return Read(_attributes[ATTR_STANDARDTIME]);
        }

        /// <summary>
        /// Synchronously Get the StandardTime attribute [attribute ID6].
        ///
        /// A device may derive the time by reading the Time and TimeZone attributes
        /// and adding them together. If implemented however, the optional StandardTime
        /// attribute indicates this time directly. The value 0xffffffff indicates an invalid
        /// Standard Time.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public int GetStandardTime(long refreshPeriod)
        {
            if (_attributes[ATTR_STANDARDTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_STANDARDTIME].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_STANDARDTIME]);
        }


        /// <summary>
        /// Get the LocalTime attribute [attribute ID7].
        ///
        /// A device may derive the time by reading the Time, TimeZone, DstStart, DstEnd
        /// and DstShift attributes and performing the calculation. If implemented however,
        /// the optional LocalTime attribute indicates this time directly. The value 0xffffffff
        /// indicates an invalid Local Time.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetLocalTimeAsync()
        {
            return Read(_attributes[ATTR_LOCALTIME]);
        }

        /// <summary>
        /// Synchronously Get the LocalTime attribute [attribute ID7].
        ///
        /// A device may derive the time by reading the Time, TimeZone, DstStart, DstEnd
        /// and DstShift attributes and performing the calculation. If implemented however,
        /// the optional LocalTime attribute indicates this time directly. The value 0xffffffff
        /// indicates an invalid Local Time.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public int GetLocalTime(long refreshPeriod)
        {
            if (_attributes[ATTR_LOCALTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_LOCALTIME].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_LOCALTIME]);
        }

    }
}
