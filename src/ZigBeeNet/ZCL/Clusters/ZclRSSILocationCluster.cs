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
using ZigBeeNet.ZCL.Clusters.RSSILocation;

/**
 * RSSI Locationcluster implementation (Cluster ID 0x000B).
 *
 * Code is auto-generated. Modifications may be overwritten!
 */
namespace ZigBeeNet.ZCL.Clusters
{
   public class ZclRSSILocationCluster : ZclCluster
   {
       /**
       * The ZigBee Cluster Library Cluster ID
       */
       public static ushort CLUSTER_ID = 0x000B;

       /**
       * The ZigBee Cluster Library Cluster Name
       */
       public static string CLUSTER_NAME = "RSSI Location";

       /* Attribute constants */
       /**
        * The LocationType attribute is 8 bits long and is divided into bit fields.       */
       public static ushort ATTR_LOCATIONTYPE = 0x0000;

       /**
       */
       public static ushort ATTR_LOCATIONMETHOD = 0x0001;

       /**
        * The LocationAge attribute indicates the amount of time, measured in seconds, that        * has transpired since the location information was last calculated. This attribute is        * not valid if the Absolute bit of the LocationType attribute is set to one.       */
       public static ushort ATTR_LOCATIONAGE = 0x0002;

       /**
        * The QualityMeasure attribute is a measure of confidence in the corresponding        * location information. The higher the value, the more confident the transmitting        * device is in the location information. A value of 0x64 indicates complete (100%)        * confidence and a value of 0x00 indicates zero confidence. (Note: no fixed        * confidence metric is mandated – the metric may be application and manufacturer        * dependent).        * <p>        * This field is not valid if the Absolute bit of the LocationType attribute is set to one.       */
       public static ushort ATTR_QUALITYMEASURE = 0x0003;

       /**
        * The NumberOfDevices attribute is the number of devices whose location data        * were used to calculate the last location value. This attribute is related to the        * QualityMeasure attribute.       */
       public static ushort ATTR_NUMBEROFDEVICES = 0x0004;

       /**
        * The Coordinate1, Coordinate2 and Coordinate3 attributes are signed 16-bit        * integers, and represent orthogonal linear coordinates x, y, z in meters as follows.        * <p>        * x = Coordinate1 / 10, y = Coordinate2 / 10, z = Coordinate3 / 10        * <p>        * The range of x is -3276.7 to 3276.7 meters, corresponding to Coordinate1        * between 0x8001 and 0x7fff. The same range applies to y and z. A value of        * 0x8000 for any of the coordinates indicates that the coordinate is unknown.       */
       public static ushort ATTR_COORDINATE1 = 0x0010;

       /**
        * The Coordinate1, Coordinate2 and Coordinate3 attributes are signed 16-bit        * integers, and represent orthogonal linear coordinates x, y, z in meters as follows.        * <p>        * x = Coordinate1 / 10, y = Coordinate2 / 10, z = Coordinate3 / 10        * <p>        * The range of x is -3276.7 to 3276.7 meters, corresponding to Coordinate1        * between 0x8001 and 0x7fff. The same range applies to y and z. A value of        * 0x8000 for any of the coordinates indicates that the coordinate is unknown.       */
       public static ushort ATTR_COORDINATE2 = 0x0011;

       /**
        * The Coordinate1, Coordinate2 and Coordinate3 attributes are signed 16-bit        * integers, and represent orthogonal linear coordinates x, y, z in meters as follows.        * <p>        * x = Coordinate1 / 10, y = Coordinate2 / 10, z = Coordinate3 / 10        * <p>        * The range of x is -3276.7 to 3276.7 meters, corresponding to Coordinate1        * between 0x8001 and 0x7fff. The same range applies to y and z. A value of        * 0x8000 for any of the coordinates indicates that the coordinate is unknown.       */
       public static ushort ATTR_COORDINATE3 = 0x0012;

       /**
        * The Power attribute specifies the value of the average power P0, measured in        * dBm, received at a reference distance of one meter from the transmitter.        * <p>        * P0 = Power / 100        * <p>        * A value of 0x8000 indicates that Power is unknown.       */
       public static ushort ATTR_POWER = 0x0013;

       /**
        * The PathLossExponent attribute specifies the value of the Path Loss Exponent n,        * an exponent that describes the rate at which the signal power decays with        * increasing distance from the transmitter.        * <p>        * n = PathLossExponent / 100        * <p>        * A value of 0xffff indicates that PathLossExponent is unknown.       */
       public static ushort ATTR_PATHLOSSEXPONENT = 0x0014;

       /**
        * The ReportingPeriod attribute specifies the time in seconds between successive        * reports of the device's location by means of the Location Data Notification        * command. The minimum value this attribute can take is specified by the profile in        * use. If ReportingPeriod is zero, the device does not automatically report its        * location. Note that location information can always be polled at any time.       */
       public static ushort ATTR_REPORTINGPERIOD = 0x0015;

       /**
        * The CalculationPeriod attribute specifies the time in seconds between successive        * calculations of the device's location. If CalculationPeriod is less than the        * physically possible minimum period that the calculation can be performed, the        * calculation will be repeated as frequently as possible.       */
       public static ushort ATTR_CALCULATIONPERIOD = 0x0016;

       /**
        * The NumberRSSIMeasurements attribute specifies the number of RSSI        * measurements to be used to generate one location estimate. The measurements are        * averaged to improve accuracy. NumberRSSIMeasurements must be greater than or        * equal to 1.       */
       public static ushort ATTR_NUMBERRSSIMEASUREMENTS = 0x0017;


       // Attribute initialisation
       protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
       {
           Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(13);

           ZclClusterType rSSILocation = ZclClusterType.GetValueById(ClusterType.RSSI_LOCATION);

           attributeMap.Add(ATTR_LOCATIONTYPE, new ZclAttribute(rSSILocation, ATTR_LOCATIONTYPE, "LocationType", ZclDataType.Get(DataType.DATA_8_BIT), true, true, false, false));
           attributeMap.Add(ATTR_LOCATIONMETHOD, new ZclAttribute(rSSILocation, ATTR_LOCATIONMETHOD, "LocationMethod", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
           attributeMap.Add(ATTR_LOCATIONAGE, new ZclAttribute(rSSILocation, ATTR_LOCATIONAGE, "LocationAge", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
           attributeMap.Add(ATTR_QUALITYMEASURE, new ZclAttribute(rSSILocation, ATTR_QUALITYMEASURE, "QualityMeasure", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
           attributeMap.Add(ATTR_NUMBEROFDEVICES, new ZclAttribute(rSSILocation, ATTR_NUMBEROFDEVICES, "NumberOfDevices", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
           attributeMap.Add(ATTR_COORDINATE1, new ZclAttribute(rSSILocation, ATTR_COORDINATE1, "Coordinate1", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, true, false));
           attributeMap.Add(ATTR_COORDINATE2, new ZclAttribute(rSSILocation, ATTR_COORDINATE2, "Coordinate2", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, true, false));
           attributeMap.Add(ATTR_COORDINATE3, new ZclAttribute(rSSILocation, ATTR_COORDINATE3, "Coordinate3", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_POWER, new ZclAttribute(rSSILocation, ATTR_POWER, "Power", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, true, false));
           attributeMap.Add(ATTR_PATHLOSSEXPONENT, new ZclAttribute(rSSILocation, ATTR_PATHLOSSEXPONENT, "PathLossExponent", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, true, false));
           attributeMap.Add(ATTR_REPORTINGPERIOD, new ZclAttribute(rSSILocation, ATTR_REPORTINGPERIOD, "ReportingPeriod", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_CALCULATIONPERIOD, new ZclAttribute(rSSILocation, ATTR_CALCULATIONPERIOD, "CalculationPeriod", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_NUMBERRSSIMEASUREMENTS, new ZclAttribute(rSSILocation, ATTR_NUMBERRSSIMEASUREMENTS, "NumberRSSIMeasurements", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, true, false));

           return attributeMap;
       }

       /**
       * Default constructor to create a RSSI Location cluster.
       *
       * @param zigbeeEndpoint the {@link ZigBeeEndpoint}
       */
       public ZclRSSILocationCluster(ZigBeeEndpoint zigbeeEndpoint)
           : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
       {
       }


       /**
       * Get the LocationType attribute [attribute ID0].
       *
       * The LocationType attribute is 8 bits long and is divided into bit fields.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetLocationTypeAsync()
       {
           return Read(_attributes[ATTR_LOCATIONTYPE]);
       }

       /**
       * Synchronously Get the LocationType attribute [attribute ID0].
       *
       * The LocationType attribute is 8 bits long and is divided into bit fields.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetLocationType(long refreshPeriod)
       {
           if (_attributes[ATTR_LOCATIONTYPE].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_LOCATIONTYPE].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_LOCATIONTYPE]);
       }


       /**
       * Get the LocationMethod attribute [attribute ID1].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetLocationMethodAsync()
       {
           return Read(_attributes[ATTR_LOCATIONMETHOD]);
       }

       /**
       * Synchronously Get the LocationMethod attribute [attribute ID1].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetLocationMethod(long refreshPeriod)
       {
           if (_attributes[ATTR_LOCATIONMETHOD].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_LOCATIONMETHOD].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_LOCATIONMETHOD]);
       }


       /**
       * Get the LocationAge attribute [attribute ID2].
       *
       * The LocationAge attribute indicates the amount of time, measured in seconds, that       * has transpired since the location information was last calculated. This attribute is       * not valid if the Absolute bit of the LocationType attribute is set to one.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetLocationAgeAsync()
       {
           return Read(_attributes[ATTR_LOCATIONAGE]);
       }

       /**
       * Synchronously Get the LocationAge attribute [attribute ID2].
       *
       * The LocationAge attribute indicates the amount of time, measured in seconds, that       * has transpired since the location information was last calculated. This attribute is       * not valid if the Absolute bit of the LocationType attribute is set to one.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public ushort GetLocationAge(long refreshPeriod)
       {
           if (_attributes[ATTR_LOCATIONAGE].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_LOCATIONAGE].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_LOCATIONAGE]);
       }


       /**
       * Get the QualityMeasure attribute [attribute ID3].
       *
       * The QualityMeasure attribute is a measure of confidence in the corresponding       * location information. The higher the value, the more confident the transmitting       * device is in the location information. A value of 0x64 indicates complete (100%)       * confidence and a value of 0x00 indicates zero confidence. (Note: no fixed       * confidence metric is mandated – the metric may be application and manufacturer       * dependent).       * <p>       * This field is not valid if the Absolute bit of the LocationType attribute is set to one.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetQualityMeasureAsync()
       {
           return Read(_attributes[ATTR_QUALITYMEASURE]);
       }

       /**
       * Synchronously Get the QualityMeasure attribute [attribute ID3].
       *
       * The QualityMeasure attribute is a measure of confidence in the corresponding       * location information. The higher the value, the more confident the transmitting       * device is in the location information. A value of 0x64 indicates complete (100%)       * confidence and a value of 0x00 indicates zero confidence. (Note: no fixed       * confidence metric is mandated – the metric may be application and manufacturer       * dependent).       * <p>       * This field is not valid if the Absolute bit of the LocationType attribute is set to one.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetQualityMeasure(long refreshPeriod)
       {
           if (_attributes[ATTR_QUALITYMEASURE].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_QUALITYMEASURE].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_QUALITYMEASURE]);
       }


       /**
       * Get the NumberOfDevices attribute [attribute ID4].
       *
       * The NumberOfDevices attribute is the number of devices whose location data       * were used to calculate the last location value. This attribute is related to the       * QualityMeasure attribute.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetNumberOfDevicesAsync()
       {
           return Read(_attributes[ATTR_NUMBEROFDEVICES]);
       }

       /**
       * Synchronously Get the NumberOfDevices attribute [attribute ID4].
       *
       * The NumberOfDevices attribute is the number of devices whose location data       * were used to calculate the last location value. This attribute is related to the       * QualityMeasure attribute.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetNumberOfDevices(long refreshPeriod)
       {
           if (_attributes[ATTR_NUMBEROFDEVICES].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_NUMBEROFDEVICES].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_NUMBEROFDEVICES]);
       }


       /**
       * Set the Coordinate1 attribute [attribute ID16].
       *
       * The Coordinate1, Coordinate2 and Coordinate3 attributes are signed 16-bit       * integers, and represent orthogonal linear coordinates x, y, z in meters as follows.       * <p>       * x = Coordinate1 / 10, y = Coordinate2 / 10, z = Coordinate3 / 10       * <p>       * The range of x is -3276.7 to 3276.7 meters, corresponding to Coordinate1       * between 0x8001 and 0x7fff. The same range applies to y and z. A value of       * 0x8000 for any of the coordinates indicates that the coordinate is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @param coordinate1 the short attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetCoordinate1(object value)
       {
           return Write(_attributes[ATTR_COORDINATE1], value);
       }


       /**
       * Get the Coordinate1 attribute [attribute ID16].
       *
       * The Coordinate1, Coordinate2 and Coordinate3 attributes are signed 16-bit       * integers, and represent orthogonal linear coordinates x, y, z in meters as follows.       * <p>       * x = Coordinate1 / 10, y = Coordinate2 / 10, z = Coordinate3 / 10       * <p>       * The range of x is -3276.7 to 3276.7 meters, corresponding to Coordinate1       * between 0x8001 and 0x7fff. The same range applies to y and z. A value of       * 0x8000 for any of the coordinates indicates that the coordinate is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetCoordinate1Async()
       {
           return Read(_attributes[ATTR_COORDINATE1]);
       }

       /**
       * Synchronously Get the Coordinate1 attribute [attribute ID16].
       *
       * The Coordinate1, Coordinate2 and Coordinate3 attributes are signed 16-bit       * integers, and represent orthogonal linear coordinates x, y, z in meters as follows.       * <p>       * x = Coordinate1 / 10, y = Coordinate2 / 10, z = Coordinate3 / 10       * <p>       * The range of x is -3276.7 to 3276.7 meters, corresponding to Coordinate1       * between 0x8001 and 0x7fff. The same range applies to y and z. A value of       * 0x8000 for any of the coordinates indicates that the coordinate is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetCoordinate1(long refreshPeriod)
       {
           if (_attributes[ATTR_COORDINATE1].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_COORDINATE1].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_COORDINATE1]);
       }


       /**
       * Set the Coordinate2 attribute [attribute ID17].
       *
       * The Coordinate1, Coordinate2 and Coordinate3 attributes are signed 16-bit       * integers, and represent orthogonal linear coordinates x, y, z in meters as follows.       * <p>       * x = Coordinate1 / 10, y = Coordinate2 / 10, z = Coordinate3 / 10       * <p>       * The range of x is -3276.7 to 3276.7 meters, corresponding to Coordinate1       * between 0x8001 and 0x7fff. The same range applies to y and z. A value of       * 0x8000 for any of the coordinates indicates that the coordinate is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @param coordinate2 the short attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetCoordinate2(object value)
       {
           return Write(_attributes[ATTR_COORDINATE2], value);
       }


       /**
       * Get the Coordinate2 attribute [attribute ID17].
       *
       * The Coordinate1, Coordinate2 and Coordinate3 attributes are signed 16-bit       * integers, and represent orthogonal linear coordinates x, y, z in meters as follows.       * <p>       * x = Coordinate1 / 10, y = Coordinate2 / 10, z = Coordinate3 / 10       * <p>       * The range of x is -3276.7 to 3276.7 meters, corresponding to Coordinate1       * between 0x8001 and 0x7fff. The same range applies to y and z. A value of       * 0x8000 for any of the coordinates indicates that the coordinate is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetCoordinate2Async()
       {
           return Read(_attributes[ATTR_COORDINATE2]);
       }

       /**
       * Synchronously Get the Coordinate2 attribute [attribute ID17].
       *
       * The Coordinate1, Coordinate2 and Coordinate3 attributes are signed 16-bit       * integers, and represent orthogonal linear coordinates x, y, z in meters as follows.       * <p>       * x = Coordinate1 / 10, y = Coordinate2 / 10, z = Coordinate3 / 10       * <p>       * The range of x is -3276.7 to 3276.7 meters, corresponding to Coordinate1       * between 0x8001 and 0x7fff. The same range applies to y and z. A value of       * 0x8000 for any of the coordinates indicates that the coordinate is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetCoordinate2(long refreshPeriod)
       {
           if (_attributes[ATTR_COORDINATE2].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_COORDINATE2].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_COORDINATE2]);
       }


       /**
       * Set the Coordinate3 attribute [attribute ID18].
       *
       * The Coordinate1, Coordinate2 and Coordinate3 attributes are signed 16-bit       * integers, and represent orthogonal linear coordinates x, y, z in meters as follows.       * <p>       * x = Coordinate1 / 10, y = Coordinate2 / 10, z = Coordinate3 / 10       * <p>       * The range of x is -3276.7 to 3276.7 meters, corresponding to Coordinate1       * between 0x8001 and 0x7fff. The same range applies to y and z. A value of       * 0x8000 for any of the coordinates indicates that the coordinate is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param coordinate3 the short attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetCoordinate3(object value)
       {
           return Write(_attributes[ATTR_COORDINATE3], value);
       }


       /**
       * Get the Coordinate3 attribute [attribute ID18].
       *
       * The Coordinate1, Coordinate2 and Coordinate3 attributes are signed 16-bit       * integers, and represent orthogonal linear coordinates x, y, z in meters as follows.       * <p>       * x = Coordinate1 / 10, y = Coordinate2 / 10, z = Coordinate3 / 10       * <p>       * The range of x is -3276.7 to 3276.7 meters, corresponding to Coordinate1       * between 0x8001 and 0x7fff. The same range applies to y and z. A value of       * 0x8000 for any of the coordinates indicates that the coordinate is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetCoordinate3Async()
       {
           return Read(_attributes[ATTR_COORDINATE3]);
       }

       /**
       * Synchronously Get the Coordinate3 attribute [attribute ID18].
       *
       * The Coordinate1, Coordinate2 and Coordinate3 attributes are signed 16-bit       * integers, and represent orthogonal linear coordinates x, y, z in meters as follows.       * <p>       * x = Coordinate1 / 10, y = Coordinate2 / 10, z = Coordinate3 / 10       * <p>       * The range of x is -3276.7 to 3276.7 meters, corresponding to Coordinate1       * between 0x8001 and 0x7fff. The same range applies to y and z. A value of       * 0x8000 for any of the coordinates indicates that the coordinate is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetCoordinate3(long refreshPeriod)
       {
           if (_attributes[ATTR_COORDINATE3].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_COORDINATE3].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_COORDINATE3]);
       }


       /**
       * Set the Power attribute [attribute ID19].
       *
       * The Power attribute specifies the value of the average power P0, measured in       * dBm, received at a reference distance of one meter from the transmitter.       * <p>       * P0 = Power / 100       * <p>       * A value of 0x8000 indicates that Power is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @param power the short attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetPower(object value)
       {
           return Write(_attributes[ATTR_POWER], value);
       }


       /**
       * Get the Power attribute [attribute ID19].
       *
       * The Power attribute specifies the value of the average power P0, measured in       * dBm, received at a reference distance of one meter from the transmitter.       * <p>       * P0 = Power / 100       * <p>       * A value of 0x8000 indicates that Power is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetPowerAsync()
       {
           return Read(_attributes[ATTR_POWER]);
       }

       /**
       * Synchronously Get the Power attribute [attribute ID19].
       *
       * The Power attribute specifies the value of the average power P0, measured in       * dBm, received at a reference distance of one meter from the transmitter.       * <p>       * P0 = Power / 100       * <p>       * A value of 0x8000 indicates that Power is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetPower(long refreshPeriod)
       {
           if (_attributes[ATTR_POWER].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_POWER].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_POWER]);
       }


       /**
       * Set the PathLossExponent attribute [attribute ID20].
       *
       * The PathLossExponent attribute specifies the value of the Path Loss Exponent n,       * an exponent that describes the rate at which the signal power decays with       * increasing distance from the transmitter.       * <p>       * n = PathLossExponent / 100       * <p>       * A value of 0xffff indicates that PathLossExponent is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @param pathLossExponent the short attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetPathLossExponent(object value)
       {
           return Write(_attributes[ATTR_PATHLOSSEXPONENT], value);
       }


       /**
       * Get the PathLossExponent attribute [attribute ID20].
       *
       * The PathLossExponent attribute specifies the value of the Path Loss Exponent n,       * an exponent that describes the rate at which the signal power decays with       * increasing distance from the transmitter.       * <p>       * n = PathLossExponent / 100       * <p>       * A value of 0xffff indicates that PathLossExponent is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetPathLossExponentAsync()
       {
           return Read(_attributes[ATTR_PATHLOSSEXPONENT]);
       }

       /**
       * Synchronously Get the PathLossExponent attribute [attribute ID20].
       *
       * The PathLossExponent attribute specifies the value of the Path Loss Exponent n,       * an exponent that describes the rate at which the signal power decays with       * increasing distance from the transmitter.       * <p>       * n = PathLossExponent / 100       * <p>       * A value of 0xffff indicates that PathLossExponent is unknown.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetPathLossExponent(long refreshPeriod)
       {
           if (_attributes[ATTR_PATHLOSSEXPONENT].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_PATHLOSSEXPONENT].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_PATHLOSSEXPONENT]);
       }


       /**
       * Set the ReportingPeriod attribute [attribute ID21].
       *
       * The ReportingPeriod attribute specifies the time in seconds between successive       * reports of the device's location by means of the Location Data Notification       * command. The minimum value this attribute can take is specified by the profile in       * use. If ReportingPeriod is zero, the device does not automatically report its       * location. Note that location information can always be polled at any time.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param reportingPeriod the short attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetReportingPeriod(object value)
       {
           return Write(_attributes[ATTR_REPORTINGPERIOD], value);
       }


       /**
       * Get the ReportingPeriod attribute [attribute ID21].
       *
       * The ReportingPeriod attribute specifies the time in seconds between successive       * reports of the device's location by means of the Location Data Notification       * command. The minimum value this attribute can take is specified by the profile in       * use. If ReportingPeriod is zero, the device does not automatically report its       * location. Note that location information can always be polled at any time.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetReportingPeriodAsync()
       {
           return Read(_attributes[ATTR_REPORTINGPERIOD]);
       }

       /**
       * Synchronously Get the ReportingPeriod attribute [attribute ID21].
       *
       * The ReportingPeriod attribute specifies the time in seconds between successive       * reports of the device's location by means of the Location Data Notification       * command. The minimum value this attribute can take is specified by the profile in       * use. If ReportingPeriod is zero, the device does not automatically report its       * location. Note that location information can always be polled at any time.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetReportingPeriod(long refreshPeriod)
       {
           if (_attributes[ATTR_REPORTINGPERIOD].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_REPORTINGPERIOD].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_REPORTINGPERIOD]);
       }


       /**
       * Set the CalculationPeriod attribute [attribute ID22].
       *
       * The CalculationPeriod attribute specifies the time in seconds between successive       * calculations of the device's location. If CalculationPeriod is less than the       * physically possible minimum period that the calculation can be performed, the       * calculation will be repeated as frequently as possible.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param calculationPeriod the short attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetCalculationPeriod(object value)
       {
           return Write(_attributes[ATTR_CALCULATIONPERIOD], value);
       }


       /**
       * Get the CalculationPeriod attribute [attribute ID22].
       *
       * The CalculationPeriod attribute specifies the time in seconds between successive       * calculations of the device's location. If CalculationPeriod is less than the       * physically possible minimum period that the calculation can be performed, the       * calculation will be repeated as frequently as possible.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetCalculationPeriodAsync()
       {
           return Read(_attributes[ATTR_CALCULATIONPERIOD]);
       }

       /**
       * Synchronously Get the CalculationPeriod attribute [attribute ID22].
       *
       * The CalculationPeriod attribute specifies the time in seconds between successive       * calculations of the device's location. If CalculationPeriod is less than the       * physically possible minimum period that the calculation can be performed, the       * calculation will be repeated as frequently as possible.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetCalculationPeriod(long refreshPeriod)
       {
           if (_attributes[ATTR_CALCULATIONPERIOD].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_CALCULATIONPERIOD].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_CALCULATIONPERIOD]);
       }


       /**
       * Set the NumberRSSIMeasurements attribute [attribute ID23].
       *
       * The NumberRSSIMeasurements attribute specifies the number of RSSI       * measurements to be used to generate one location estimate. The measurements are       * averaged to improve accuracy. NumberRSSIMeasurements must be greater than or       * equal to 1.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param numberRSSIMeasurements the short attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetNumberRSSIMeasurements(object value)
       {
           return Write(_attributes[ATTR_NUMBERRSSIMEASUREMENTS], value);
       }


       /**
       * Get the NumberRSSIMeasurements attribute [attribute ID23].
       *
       * The NumberRSSIMeasurements attribute specifies the number of RSSI       * measurements to be used to generate one location estimate. The measurements are       * averaged to improve accuracy. NumberRSSIMeasurements must be greater than or       * equal to 1.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetNumberRSSIMeasurementsAsync()
       {
           return Read(_attributes[ATTR_NUMBERRSSIMEASUREMENTS]);
       }

       /**
       * Synchronously Get the NumberRSSIMeasurements attribute [attribute ID23].
       *
       * The NumberRSSIMeasurements attribute specifies the number of RSSI       * measurements to be used to generate one location estimate. The measurements are       * averaged to improve accuracy. NumberRSSIMeasurements must be greater than or       * equal to 1.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetNumberRSSIMeasurements(long refreshPeriod)
       {
           if (_attributes[ATTR_NUMBERRSSIMEASUREMENTS].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_NUMBERRSSIMEASUREMENTS].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_NUMBERRSSIMEASUREMENTS]);
       }


       /**
       * The Set Absolute Location Command
       *
       * @param coordinate1 {@link short} Coordinate 1
       * @param coordinate2 {@link short} Coordinate 2
       * @param coordinate3 {@link short} Coordinate 3
       * @param power {@link short} Power
       * @param pathLossExponent {@link ushort} Path Loss Exponent
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetAbsoluteLocationCommand(short coordinate1, short coordinate2, short coordinate3, short power, ushort pathLossExponent)
       {
           SetAbsoluteLocationCommand command = new SetAbsoluteLocationCommand();

           // Set the fields
           command.Coordinate1 = coordinate1;
           command.Coordinate2 = coordinate2;
           command.Coordinate3 = coordinate3;
           command.Power = power;
           command.PathLossExponent = pathLossExponent;

           return Send(command);
       }

       /**
       * The Set Device Configuration Command
       *
       * @param power {@link short} Power
       * @param pathLossExponent {@link ushort} Path Loss Exponent
       * @param calculationPeriod {@link ushort} Calculation Period
       * @param numberRSSIMeasurements {@link byte} Number RSSI Measurements
       * @param reportingPeriod {@link ushort} Reporting Period
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetDeviceConfigurationCommand(short power, ushort pathLossExponent, ushort calculationPeriod, byte numberRSSIMeasurements, ushort reportingPeriod)
       {
           SetDeviceConfigurationCommand command = new SetDeviceConfigurationCommand();

           // Set the fields
           command.Power = power;
           command.PathLossExponent = pathLossExponent;
           command.CalculationPeriod = calculationPeriod;
           command.NumberRSSIMeasurements = numberRSSIMeasurements;
           command.ReportingPeriod = reportingPeriod;

           return Send(command);
       }

       /**
       * The Get Device Configuration Command
       *
       * @param targetAddress {@link IeeeAddress} Target Address
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetDeviceConfigurationCommand(IeeeAddress targetAddress)
       {
           GetDeviceConfigurationCommand command = new GetDeviceConfigurationCommand();

           // Set the fields
           command.TargetAddress = targetAddress;

           return Send(command);
       }

       /**
       * The Get Location Data Command
       *
       * @param header {@link byte} Header
       * @param numberResponses {@link byte} Number Responses
       * @param targetAddress {@link IeeeAddress} Target Address
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetLocationDataCommand(byte header, byte numberResponses, IeeeAddress targetAddress)
       {
           GetLocationDataCommand command = new GetLocationDataCommand();

           // Set the fields
           command.Header = header;
           command.NumberResponses = numberResponses;
           command.TargetAddress = targetAddress;

           return Send(command);
       }

       /**
       * The RSSI Response
       *
       * @param replyingDevice {@link IeeeAddress} Replying Device
       * @param coordinate1 {@link short} Coordinate 1
       * @param coordinate2 {@link short} Coordinate 2
       * @param coordinate3 {@link short} Coordinate 3
       * @param rSSI {@link sbyte} RSSI
       * @param numberRSSIMeasurements {@link byte} Number RSSI Measurements
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> RSSIResponse(IeeeAddress replyingDevice, short coordinate1, short coordinate2, short coordinate3, sbyte rSSI, byte numberRSSIMeasurements)
       {
           RSSIResponse command = new RSSIResponse();

           // Set the fields
           command.ReplyingDevice = replyingDevice;
           command.Coordinate1 = coordinate1;
           command.Coordinate2 = coordinate2;
           command.Coordinate3 = coordinate3;
           command.RSSI = rSSI;
           command.NumberRSSIMeasurements = numberRSSIMeasurements;

           return Send(command);
       }

       /**
       * The Send Pings Command
       *
       * @param targetAddress {@link IeeeAddress} Target Address
       * @param numberRSSIMeasurements {@link byte} Number RSSI Measurements
       * @param calculationPeriod {@link ushort} Calculation Period
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SendPingsCommand(IeeeAddress targetAddress, byte numberRSSIMeasurements, ushort calculationPeriod)
       {
           SendPingsCommand command = new SendPingsCommand();

           // Set the fields
           command.TargetAddress = targetAddress;
           command.NumberRSSIMeasurements = numberRSSIMeasurements;
           command.CalculationPeriod = calculationPeriod;

           return Send(command);
       }

       /**
       * The Anchor Node Announce Command
       *
       * @param anchorNodeAddress {@link IeeeAddress} Anchor Node Address
       * @param coordinate1 {@link short} Coordinate 1
       * @param coordinate2 {@link short} Coordinate 2
       * @param coordinate3 {@link short} Coordinate 3
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> AnchorNodeAnnounceCommand(IeeeAddress anchorNodeAddress, short coordinate1, short coordinate2, short coordinate3)
       {
           AnchorNodeAnnounceCommand command = new AnchorNodeAnnounceCommand();

           // Set the fields
           command.AnchorNodeAddress = anchorNodeAddress;
           command.Coordinate1 = coordinate1;
           command.Coordinate2 = coordinate2;
           command.Coordinate3 = coordinate3;

           return Send(command);
       }

       /**
       * The Device Configuration Response
       *
       * @param status {@link byte} Status
       * @param power {@link short} Power
       * @param pathLossExponent {@link ushort} Path Loss Exponent
       * @param calculationPeriod {@link ushort} Calculation Period
       * @param numberRSSIMeasurements {@link byte} Number RSSI Measurements
       * @param reportingPeriod {@link ushort} Reporting Period
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> DeviceConfigurationResponse(byte status, short power, ushort pathLossExponent, ushort calculationPeriod, byte numberRSSIMeasurements, ushort reportingPeriod)
       {
           DeviceConfigurationResponse command = new DeviceConfigurationResponse();

           // Set the fields
           command.Status = status;
           command.Power = power;
           command.PathLossExponent = pathLossExponent;
           command.CalculationPeriod = calculationPeriod;
           command.NumberRSSIMeasurements = numberRSSIMeasurements;
           command.ReportingPeriod = reportingPeriod;

           return Send(command);
       }

       /**
       * The Location Data Response
       *
       * @param status {@link byte} Status
       * @param locationType {@link byte} Location Type
       * @param coordinate1 {@link short} Coordinate 1
       * @param coordinate2 {@link short} Coordinate 2
       * @param coordinate3 {@link short} Coordinate 3
       * @param power {@link short} Power
       * @param pathLossExponent {@link ushort} Path Loss Exponent
       * @param locationMethod {@link byte} Location Method
       * @param qualityMeasure {@link byte} Quality Measure
       * @param locationAge {@link ushort} Location Age
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> LocationDataResponse(byte status, byte locationType, short coordinate1, short coordinate2, short coordinate3, short power, ushort pathLossExponent, byte locationMethod, byte qualityMeasure, ushort locationAge)
       {
           LocationDataResponse command = new LocationDataResponse();

           // Set the fields
           command.Status = status;
           command.LocationType = locationType;
           command.Coordinate1 = coordinate1;
           command.Coordinate2 = coordinate2;
           command.Coordinate3 = coordinate3;
           command.Power = power;
           command.PathLossExponent = pathLossExponent;
           command.LocationMethod = locationMethod;
           command.QualityMeasure = qualityMeasure;
           command.LocationAge = locationAge;

           return Send(command);
       }

       /**
       * The Location Data Notification Command
       *
       * @param locationType {@link byte} Location Type
       * @param coordinate1 {@link short} Coordinate 1
       * @param coordinate2 {@link short} Coordinate 2
       * @param coordinate3 {@link short} Coordinate 3
       * @param power {@link short} Power
       * @param pathLossExponent {@link ushort} Path Loss Exponent
       * @param locationMethod {@link byte} Location Method
       * @param qualityMeasure {@link byte} Quality Measure
       * @param locationAge {@link ushort} Location Age
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> LocationDataNotificationCommand(byte locationType, short coordinate1, short coordinate2, short coordinate3, short power, ushort pathLossExponent, byte locationMethod, byte qualityMeasure, ushort locationAge)
       {
           LocationDataNotificationCommand command = new LocationDataNotificationCommand();

           // Set the fields
           command.LocationType = locationType;
           command.Coordinate1 = coordinate1;
           command.Coordinate2 = coordinate2;
           command.Coordinate3 = coordinate3;
           command.Power = power;
           command.PathLossExponent = pathLossExponent;
           command.LocationMethod = locationMethod;
           command.QualityMeasure = qualityMeasure;
           command.LocationAge = locationAge;

           return Send(command);
       }

       /**
       * The Compact Location Data Notification Command
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> CompactLocationDataNotificationCommand()
       {
           CompactLocationDataNotificationCommand command = new CompactLocationDataNotificationCommand();

           return Send(command);
       }

       /**
       * The RSSI Ping Command
       *
       * @param locationType {@link byte} Location Type
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> RSSIPingCommand(byte locationType)
       {
           RSSIPingCommand command = new RSSIPingCommand();

           // Set the fields
           command.LocationType = locationType;

           return Send(command);
       }

       /**
       * The RSSI Request Command
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> RSSIRequestCommand()
       {
           RSSIRequestCommand command = new RSSIRequestCommand();

           return Send(command);
       }

       /**
       * The Report RSSI Measurements Command
       *
       * @param reportingAddress {@link IeeeAddress} Reporting Address
       * @param numberOfNeighbors {@link byte} Number of Neighbors
       * @param neighborsInformation {@link List<NeighborInformation>} Neighbors Information
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> ReportRSSIMeasurementsCommand(IeeeAddress reportingAddress, byte numberOfNeighbors, List<NeighborInformation> neighborsInformation)
       {
           ReportRSSIMeasurementsCommand command = new ReportRSSIMeasurementsCommand();

           // Set the fields
           command.ReportingAddress = reportingAddress;
           command.NumberOfNeighbors = numberOfNeighbors;
           command.NeighborsInformation = neighborsInformation;

           return Send(command);
       }

       /**
       * The Request Own Location Command
       *
       * @param requestingAddress {@link IeeeAddress} Requesting Address
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> RequestOwnLocationCommand(IeeeAddress requestingAddress)
       {
           RequestOwnLocationCommand command = new RequestOwnLocationCommand();

           // Set the fields
           command.RequestingAddress = requestingAddress;

           return Send(command);
       }

       public override ZclCommand GetCommandFromId(int commandId)
       {
           switch (commandId)
           {
               case 0: // SET_ABSOLUTE_LOCATION_COMMAND
                   return new SetAbsoluteLocationCommand();
               case 1: // SET_DEVICE_CONFIGURATION_COMMAND
                   return new SetDeviceConfigurationCommand();
               case 2: // GET_DEVICE_CONFIGURATION_COMMAND
                   return new GetDeviceConfigurationCommand();
               case 3: // GET_LOCATION_DATA_COMMAND
                   return new GetLocationDataCommand();
               case 4: // RSSI_RESPONSE
                   return new RSSIResponse();
               case 5: // SEND_PINGS_COMMAND
                   return new SendPingsCommand();
               case 6: // ANCHOR_NODE_ANNOUNCE_COMMAND
                   return new AnchorNodeAnnounceCommand();
                   default:
                       return null;
           }
       }

       public ZclCommand getResponseFromId(int commandId)
       {
           switch (commandId)
           {
               case 0: // DEVICE_CONFIGURATION_RESPONSE
                   return new DeviceConfigurationResponse();
               case 1: // LOCATION_DATA_RESPONSE
                   return new LocationDataResponse();
               case 2: // LOCATION_DATA_NOTIFICATION_COMMAND
                   return new LocationDataNotificationCommand();
               case 3: // COMPACT_LOCATION_DATA_NOTIFICATION_COMMAND
                   return new CompactLocationDataNotificationCommand();
               case 4: // RSSI_PING_COMMAND
                   return new RSSIPingCommand();
               case 5: // RSSI_REQUEST_COMMAND
                   return new RSSIRequestCommand();
               case 6: // REPORT_RSSI_MEASUREMENTS_COMMAND
                   return new ReportRSSIMeasurementsCommand();
               case 7: // REQUEST_OWN_LOCATION_COMMAND
                   return new RequestOwnLocationCommand();
                   default:
                       return null;
           }
       }
   }
}
