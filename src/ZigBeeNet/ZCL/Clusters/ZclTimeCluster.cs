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

/**
 * Timecluster implementation (Cluster ID 0x000A).
 *
 * Code is auto-generated. Modifications may be overwritten!
 */
namespace ZigBeeNet.ZCL.Clusters
{
   public class ZclTimeCluster : ZclCluster
   {
       /**
       * The ZigBee Cluster Library Cluster ID
       */
       public static ushort CLUSTER_ID = 0x000A;

       /**
       * The ZigBee Cluster Library Cluster Name
       */
       public static string CLUSTER_NAME = "Time";

       /* Attribute constants */
       /**
        * The Time attribute is 32-bits in length and holds the time value of a real time        * clock. This attribute has data type UTCTime, but note that it may not actually be        * synchronised to UTC - see discussion of the TimeStatus attribute below.        * <p>        * If the Master bit of the TimeStatus attribute has a value of 0, writing to this        * attribute shall set the real time clock to the written value, otherwise it cannot be        * written. The value 0xffffffff indicates an invalid time.       */
       public static ushort ATTR_TIME = 0x0000;

       /**
        * The TimeStatus attribute holds a number of bit fields.       */
       public static ushort ATTR_TIMESTATUS = 0x0001;

       /**
        * The TimeZone attribute indicates the local time zone, as a signed offset in seconds        * from the Time attribute value. The value 0xffffffff indicates an invalid time zone.       */
       public static ushort ATTR_TIMEZONE = 0x0002;

       /**
        * The DstStart attribute indicates the DST start time in seconds. The value 0xffffffff        * indicates an invalid DST start time.       */
       public static ushort ATTR_DSTSTART = 0x0003;

       /**
        * The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff        * indicates an invalid DST end time.        * <p>        * Note that the three attributes DstStart, DstEnd and DstShift are optional, but if any        * one of them is implemented the other two must also be implemented.        * Note that this attribute should be set to a new value once every year.        * <p>        * Note that this attribute should be set to a new value once every year, and should be        * written synchronously with the DstStart attribute.        * The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff        * indicates an invalid DST end time.        * <p>        * Note that this attribute should be set to a new value once every year, and should be        * written synchronously with the DstStart attribute       */
       public static ushort ATTR_DSTEND = 0x0004;

       /**
        * The DstShift attribute represents a signed offset in seconds from the standard time,        * to be applied between the times DstStart and DstEnd to calculate the Local Time.        * The value 0xffffffff indicates an invalid DST shift.        * <p>        * The range of this attribute is +/- one day. Note that the actual range of DST values        * employed by countries is much smaller than this, so the manufacturer has the        * option to impose a smaller range.       */
       public static ushort ATTR_DSTSHIFT = 0x0005;

       /**
        * A device may derive the time by reading the Time and TimeZone attributes        * and adding them together. If implemented however, the optional StandardTime        * attribute indicates this time directly. The value 0xffffffff indicates an invalid        * Standard Time.       */
       public static ushort ATTR_STANDARDTIME = 0x0006;

       /**
        * A device may derive the time by reading the Time, TimeZone, DstStart, DstEnd        * and DstShift attributes and performing the calculation. If implemented however,        * the optional LocalTime attribute indicates this time directly. The value 0xffffffff        * indicates an invalid Local Time.       */
       public static ushort ATTR_LOCALTIME = 0x0007;


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

       /**
       * Default constructor to create a Time cluster.
       *
       * @param zigbeeEndpoint the {@link ZigBeeEndpoint}
       */
       public ZclTimeCluster(ZigBeeEndpoint zigbeeEndpoint)
           : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
       {
       }


       /**
       * Set the Time attribute [attribute ID0].
       *
       * The Time attribute is 32-bits in length and holds the time value of a real time       * clock. This attribute has data type UTCTime, but note that it may not actually be       * synchronised to UTC - see discussion of the TimeStatus attribute below.       * <p>       * If the Master bit of the TimeStatus attribute has a value of 0, writing to this       * attribute shall set the real time clock to the written value, otherwise it cannot be       * written. The value 0xffffffff indicates an invalid time.       *
       * The attribute is of type DateTime.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @param time the DateTime attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetTime(object value)
       {
           return Write(_attributes[ATTR_TIME], value);
       }


       /**
       * Get the Time attribute [attribute ID0].
       *
       * The Time attribute is 32-bits in length and holds the time value of a real time       * clock. This attribute has data type UTCTime, but note that it may not actually be       * synchronised to UTC - see discussion of the TimeStatus attribute below.       * <p>       * If the Master bit of the TimeStatus attribute has a value of 0, writing to this       * attribute shall set the real time clock to the written value, otherwise it cannot be       * written. The value 0xffffffff indicates an invalid time.       *
       * The attribute is of type DateTime.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetTimeAsync()
       {
           return Read(_attributes[ATTR_TIME]);
       }

       /**
       * Synchronously Get the Time attribute [attribute ID0].
       *
       * The Time attribute is 32-bits in length and holds the time value of a real time       * clock. This attribute has data type UTCTime, but note that it may not actually be       * synchronised to UTC - see discussion of the TimeStatus attribute below.       * <p>       * If the Master bit of the TimeStatus attribute has a value of 0, writing to this       * attribute shall set the real time clock to the written value, otherwise it cannot be       * written. The value 0xffffffff indicates an invalid time.       *
       * The attribute is of type DateTime.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public DateTime GetTime(long refreshPeriod)
       {
           if (_attributes[ATTR_TIME].IsLastValueCurrent(refreshPeriod))
           {
               return (DateTime)_attributes[ATTR_TIME].LastValue;
           }

           return (DateTime)ReadSync(_attributes[ATTR_TIME]);
       }


       /**
       * Set the TimeStatus attribute [attribute ID1].
       *
       * The TimeStatus attribute holds a number of bit fields.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param timeStatus the ushort attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetTimeStatus(object value)
       {
           return Write(_attributes[ATTR_TIMESTATUS], value);
       }


       /**
       * Get the TimeStatus attribute [attribute ID1].
       *
       * The TimeStatus attribute holds a number of bit fields.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetTimeStatusAsync()
       {
           return Read(_attributes[ATTR_TIMESTATUS]);
       }

       /**
       * Synchronously Get the TimeStatus attribute [attribute ID1].
       *
       * The TimeStatus attribute holds a number of bit fields.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public ushort GetTimeStatus(long refreshPeriod)
       {
           if (_attributes[ATTR_TIMESTATUS].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_TIMESTATUS].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_TIMESTATUS]);
       }


       /**
       * Set the TimeZone attribute [attribute ID2].
       *
       * The TimeZone attribute indicates the local time zone, as a signed offset in seconds       * from the Time attribute value. The value 0xffffffff indicates an invalid time zone.       *
       * The attribute is of type int.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param timeZone the int attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetTimeZone(object value)
       {
           return Write(_attributes[ATTR_TIMEZONE], value);
       }


       /**
       * Get the TimeZone attribute [attribute ID2].
       *
       * The TimeZone attribute indicates the local time zone, as a signed offset in seconds       * from the Time attribute value. The value 0xffffffff indicates an invalid time zone.       *
       * The attribute is of type int.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetTimeZoneAsync()
       {
           return Read(_attributes[ATTR_TIMEZONE]);
       }

       /**
       * Synchronously Get the TimeZone attribute [attribute ID2].
       *
       * The TimeZone attribute indicates the local time zone, as a signed offset in seconds       * from the Time attribute value. The value 0xffffffff indicates an invalid time zone.       *
       * The attribute is of type int.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public int GetTimeZone(long refreshPeriod)
       {
           if (_attributes[ATTR_TIMEZONE].IsLastValueCurrent(refreshPeriod))
           {
               return (int)_attributes[ATTR_TIMEZONE].LastValue;
           }

           return (int)ReadSync(_attributes[ATTR_TIMEZONE]);
       }


       /**
       * Set the DstStart attribute [attribute ID3].
       *
       * The DstStart attribute indicates the DST start time in seconds. The value 0xffffffff       * indicates an invalid DST start time.       *
       * The attribute is of type uint.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param dstStart the uint attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetDstStart(object value)
       {
           return Write(_attributes[ATTR_DSTSTART], value);
       }


       /**
       * Get the DstStart attribute [attribute ID3].
       *
       * The DstStart attribute indicates the DST start time in seconds. The value 0xffffffff       * indicates an invalid DST start time.       *
       * The attribute is of type uint.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetDstStartAsync()
       {
           return Read(_attributes[ATTR_DSTSTART]);
       }

       /**
       * Synchronously Get the DstStart attribute [attribute ID3].
       *
       * The DstStart attribute indicates the DST start time in seconds. The value 0xffffffff       * indicates an invalid DST start time.       *
       * The attribute is of type uint.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public uint GetDstStart(long refreshPeriod)
       {
           if (_attributes[ATTR_DSTSTART].IsLastValueCurrent(refreshPeriod))
           {
               return (uint)_attributes[ATTR_DSTSTART].LastValue;
           }

           return (uint)ReadSync(_attributes[ATTR_DSTSTART]);
       }


       /**
       * Set the DstEnd attribute [attribute ID4].
       *
       * The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff       * indicates an invalid DST end time.       * <p>       * Note that the three attributes DstStart, DstEnd and DstShift are optional, but if any       * one of them is implemented the other two must also be implemented.       * Note that this attribute should be set to a new value once every year.       * <p>       * Note that this attribute should be set to a new value once every year, and should be       * written synchronously with the DstStart attribute.       * The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff       * indicates an invalid DST end time.       * <p>       * Note that this attribute should be set to a new value once every year, and should be       * written synchronously with the DstStart attribute       *
       * The attribute is of type uint.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param dstEnd the uint attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetDstEnd(object value)
       {
           return Write(_attributes[ATTR_DSTEND], value);
       }


       /**
       * Get the DstEnd attribute [attribute ID4].
       *
       * The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff       * indicates an invalid DST end time.       * <p>       * Note that the three attributes DstStart, DstEnd and DstShift are optional, but if any       * one of them is implemented the other two must also be implemented.       * Note that this attribute should be set to a new value once every year.       * <p>       * Note that this attribute should be set to a new value once every year, and should be       * written synchronously with the DstStart attribute.       * The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff       * indicates an invalid DST end time.       * <p>       * Note that this attribute should be set to a new value once every year, and should be       * written synchronously with the DstStart attribute       *
       * The attribute is of type uint.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetDstEndAsync()
       {
           return Read(_attributes[ATTR_DSTEND]);
       }

       /**
       * Synchronously Get the DstEnd attribute [attribute ID4].
       *
       * The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff       * indicates an invalid DST end time.       * <p>       * Note that the three attributes DstStart, DstEnd and DstShift are optional, but if any       * one of them is implemented the other two must also be implemented.       * Note that this attribute should be set to a new value once every year.       * <p>       * Note that this attribute should be set to a new value once every year, and should be       * written synchronously with the DstStart attribute.       * The DstEnd attribute indicates the DST end time in seconds. The value 0xffffffff       * indicates an invalid DST end time.       * <p>       * Note that this attribute should be set to a new value once every year, and should be       * written synchronously with the DstStart attribute       *
       * The attribute is of type uint.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public uint GetDstEnd(long refreshPeriod)
       {
           if (_attributes[ATTR_DSTEND].IsLastValueCurrent(refreshPeriod))
           {
               return (uint)_attributes[ATTR_DSTEND].LastValue;
           }

           return (uint)ReadSync(_attributes[ATTR_DSTEND]);
       }


       /**
       * Set the DstShift attribute [attribute ID5].
       *
       * The DstShift attribute represents a signed offset in seconds from the standard time,       * to be applied between the times DstStart and DstEnd to calculate the Local Time.       * The value 0xffffffff indicates an invalid DST shift.       * <p>       * The range of this attribute is +/- one day. Note that the actual range of DST values       * employed by countries is much smaller than this, so the manufacturer has the       * option to impose a smaller range.       *
       * The attribute is of type int.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param dstShift the int attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetDstShift(object value)
       {
           return Write(_attributes[ATTR_DSTSHIFT], value);
       }


       /**
       * Get the DstShift attribute [attribute ID5].
       *
       * The DstShift attribute represents a signed offset in seconds from the standard time,       * to be applied between the times DstStart and DstEnd to calculate the Local Time.       * The value 0xffffffff indicates an invalid DST shift.       * <p>       * The range of this attribute is +/- one day. Note that the actual range of DST values       * employed by countries is much smaller than this, so the manufacturer has the       * option to impose a smaller range.       *
       * The attribute is of type int.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetDstShiftAsync()
       {
           return Read(_attributes[ATTR_DSTSHIFT]);
       }

       /**
       * Synchronously Get the DstShift attribute [attribute ID5].
       *
       * The DstShift attribute represents a signed offset in seconds from the standard time,       * to be applied between the times DstStart and DstEnd to calculate the Local Time.       * The value 0xffffffff indicates an invalid DST shift.       * <p>       * The range of this attribute is +/- one day. Note that the actual range of DST values       * employed by countries is much smaller than this, so the manufacturer has the       * option to impose a smaller range.       *
       * The attribute is of type int.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public int GetDstShift(long refreshPeriod)
       {
           if (_attributes[ATTR_DSTSHIFT].IsLastValueCurrent(refreshPeriod))
           {
               return (int)_attributes[ATTR_DSTSHIFT].LastValue;
           }

           return (int)ReadSync(_attributes[ATTR_DSTSHIFT]);
       }


       /**
       * Get the StandardTime attribute [attribute ID6].
       *
       * A device may derive the time by reading the Time and TimeZone attributes       * and adding them together. If implemented however, the optional StandardTime       * attribute indicates this time directly. The value 0xffffffff indicates an invalid       * Standard Time.       *
       * The attribute is of type int.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetStandardTimeAsync()
       {
           return Read(_attributes[ATTR_STANDARDTIME]);
       }

       /**
       * Synchronously Get the StandardTime attribute [attribute ID6].
       *
       * A device may derive the time by reading the Time and TimeZone attributes       * and adding them together. If implemented however, the optional StandardTime       * attribute indicates this time directly. The value 0xffffffff indicates an invalid       * Standard Time.       *
       * The attribute is of type int.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public int GetStandardTime(long refreshPeriod)
       {
           if (_attributes[ATTR_STANDARDTIME].IsLastValueCurrent(refreshPeriod))
           {
               return (int)_attributes[ATTR_STANDARDTIME].LastValue;
           }

           return (int)ReadSync(_attributes[ATTR_STANDARDTIME]);
       }


       /**
       * Get the LocalTime attribute [attribute ID7].
       *
       * A device may derive the time by reading the Time, TimeZone, DstStart, DstEnd       * and DstShift attributes and performing the calculation. If implemented however,       * the optional LocalTime attribute indicates this time directly. The value 0xffffffff       * indicates an invalid Local Time.       *
       * The attribute is of type int.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetLocalTimeAsync()
       {
           return Read(_attributes[ATTR_LOCALTIME]);
       }

       /**
       * Synchronously Get the LocalTime attribute [attribute ID7].
       *
       * A device may derive the time by reading the Time, TimeZone, DstStart, DstEnd       * and DstShift attributes and performing the calculation. If implemented however,       * the optional LocalTime attribute indicates this time directly. The value 0xffffffff       * indicates an invalid Local Time.       *
       * The attribute is of type int.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
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
