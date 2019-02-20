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
 * Pressure measurementcluster implementation (Cluster ID 0x0403).
 *
 * The cluster provides an interface to pressure measurement functionality, * including configuration and provision of notifications of pressure measurements. *
 * Code is auto-generated. Modifications may be overwritten!
 */
namespace ZigBeeNet.ZCL.Clusters
{
   public class ZclPressureMeasurementCluster : ZclCluster
   {
       /**
       * The ZigBee Cluster Library Cluster ID
       */
       public static ushort CLUSTER_ID = 0x0403;

       /**
       * The ZigBee Cluster Library Cluster Name
       */
       public static string CLUSTER_NAME = "Pressure measurement";

       /* Attribute constants */
       /**
        * MeasuredValue represents the pressure in kPa as follows:-        * <p>        * MeasuredValue = 10 x Pressure        * <p>        * Where -3276.7 kPa <= Pressure <= 3276.7 kPa, corresponding to a        * MeasuredValue in the range 0x8001 to 0x7fff.        * <p>        * Note:- The maximum resolution this format allows is 0.1 kPa.        * <p>        * A MeasuredValue of 0x8000 indicates that the pressure measurement is invalid.        * MeasuredValue is updated continuously as new measurements are made.       */
       public static ushort ATTR_MEASUREDVALUE = 0x0000;

       /**
        * The MinMeasuredValue attribute indicates the minimum value of MeasuredValue        * that can be measured. A value of 0x8000 means this attribute is not defined.       */
       public static ushort ATTR_MINMEASUREDVALUE = 0x0001;

       /**
        * The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue        * that can be measured. A value of 0x8000 means this attribute is not defined.        * <p>        * MaxMeasuredValue shall be greater than MinMeasuredValue.        * <p>        * MinMeasuredValue and MaxMeasuredValue define the range of the sensor.       */
       public static ushort ATTR_MAXMEASUREDVALUE = 0x0002;

       /**
        * The Tolerance attribute indicates the magnitude of the possible error that is        * associated with MeasuredValue . The true value is located in the range        * (MeasuredValue – Tolerance) to (MeasuredValue + Tolerance).       */
       public static ushort ATTR_TOLERANCE = 0x0003;

       /**
       */
       public static ushort ATTR_SCALEDVALUE = 0x0010;

       /**
       */
       public static ushort ATTR_MINSCALEDVALUE = 0x0011;

       /**
       */
       public static ushort ATTR_MAXSCALEDVALUE = 0x0012;

       /**
       */
       public static ushort ATTR_SCALEDTOLERANCE = 0x0013;

       /**
       */
       public static ushort ATTR_SCALE = 0x0014;


       // Attribute initialisation
       protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
       {
           Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(9);

           ZclClusterType pressuremeasurement = ZclClusterType.GetValueById(ClusterType.PRESSURE_MEASUREMENT);

           attributeMap.Add(ATTR_MEASUREDVALUE, new ZclAttribute(pressuremeasurement, ATTR_MEASUREDVALUE, "MeasuredValue", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, true));
           attributeMap.Add(ATTR_MINMEASUREDVALUE, new ZclAttribute(pressuremeasurement, ATTR_MINMEASUREDVALUE, "MinMeasuredValue", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
           attributeMap.Add(ATTR_MAXMEASUREDVALUE, new ZclAttribute(pressuremeasurement, ATTR_MAXMEASUREDVALUE, "MaxMeasuredValue", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, true));
           attributeMap.Add(ATTR_TOLERANCE, new ZclAttribute(pressuremeasurement, ATTR_TOLERANCE, "Tolerance", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
           attributeMap.Add(ATTR_SCALEDVALUE, new ZclAttribute(pressuremeasurement, ATTR_SCALEDVALUE, "ScaledValue", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, true));
           attributeMap.Add(ATTR_MINSCALEDVALUE, new ZclAttribute(pressuremeasurement, ATTR_MINSCALEDVALUE, "MinScaledValue", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
           attributeMap.Add(ATTR_MAXSCALEDVALUE, new ZclAttribute(pressuremeasurement, ATTR_MAXSCALEDVALUE, "MaxScaledValue", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
           attributeMap.Add(ATTR_SCALEDTOLERANCE, new ZclAttribute(pressuremeasurement, ATTR_SCALEDTOLERANCE, "ScaledTolerance", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, true));
           attributeMap.Add(ATTR_SCALE, new ZclAttribute(pressuremeasurement, ATTR_SCALE, "Scale", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));

           return attributeMap;
       }

       /**
       * Default constructor to create a Pressure measurement cluster.
       *
       * @param zigbeeEndpoint the {@link ZigBeeEndpoint}
       */
       public ZclPressureMeasurementCluster(ZigBeeEndpoint zigbeeEndpoint)
           : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
       {
       }


       /**
       * Get the MeasuredValue attribute [attribute ID0].
       *
       * MeasuredValue represents the pressure in kPa as follows:-       * <p>       * MeasuredValue = 10 x Pressure       * <p>       * Where -3276.7 kPa <= Pressure <= 3276.7 kPa, corresponding to a       * MeasuredValue in the range 0x8001 to 0x7fff.       * <p>       * Note:- The maximum resolution this format allows is 0.1 kPa.       * <p>       * A MeasuredValue of 0x8000 indicates that the pressure measurement is invalid.       * MeasuredValue is updated continuously as new measurements are made.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetMeasuredValueAsync()
       {
           return Read(_attributes[ATTR_MEASUREDVALUE]);
       }

       /**
       * Synchronously Get the MeasuredValue attribute [attribute ID0].
       *
       * MeasuredValue represents the pressure in kPa as follows:-       * <p>       * MeasuredValue = 10 x Pressure       * <p>       * Where -3276.7 kPa <= Pressure <= 3276.7 kPa, corresponding to a       * MeasuredValue in the range 0x8001 to 0x7fff.       * <p>       * Note:- The maximum resolution this format allows is 0.1 kPa.       * <p>       * A MeasuredValue of 0x8000 indicates that the pressure measurement is invalid.       * MeasuredValue is updated continuously as new measurements are made.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetMeasuredValue(long refreshPeriod)
       {
           if (_attributes[ATTR_MEASUREDVALUE].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_MEASUREDVALUE].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_MEASUREDVALUE]);
       }


       /**
       * Set reporting for the MeasuredValue attribute [attribute ID0].
       *
       * MeasuredValue represents the pressure in kPa as follows:-       * <p>       * MeasuredValue = 10 x Pressure       * <p>       * Where -3276.7 kPa <= Pressure <= 3276.7 kPa, corresponding to a       * MeasuredValue in the range 0x8001 to 0x7fff.       * <p>       * Note:- The maximum resolution this format allows is 0.1 kPa.       * <p>       * A MeasuredValue of 0x8000 indicates that the pressure measurement is invalid.       * MeasuredValue is updated continuously as new measurements are made.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @param minInterval minimum reporting period
       * @param maxInterval maximum reporting period
       * @param reportableChange {@link Object} delta required to trigger report
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetMeasuredValueReporting(ushort minInterval, ushort maxInterval, object reportableChange)
       {
           return SetReporting(_attributes[ATTR_MEASUREDVALUE], minInterval, maxInterval, reportableChange);
       }


       /**
       * Get the MinMeasuredValue attribute [attribute ID1].
       *
       * The MinMeasuredValue attribute indicates the minimum value of MeasuredValue       * that can be measured. A value of 0x8000 means this attribute is not defined.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetMinMeasuredValueAsync()
       {
           return Read(_attributes[ATTR_MINMEASUREDVALUE]);
       }

       /**
       * Synchronously Get the MinMeasuredValue attribute [attribute ID1].
       *
       * The MinMeasuredValue attribute indicates the minimum value of MeasuredValue       * that can be measured. A value of 0x8000 means this attribute is not defined.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetMinMeasuredValue(long refreshPeriod)
       {
           if (_attributes[ATTR_MINMEASUREDVALUE].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_MINMEASUREDVALUE].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_MINMEASUREDVALUE]);
       }


       /**
       * Get the MaxMeasuredValue attribute [attribute ID2].
       *
       * The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue       * that can be measured. A value of 0x8000 means this attribute is not defined.       * <p>       * MaxMeasuredValue shall be greater than MinMeasuredValue.       * <p>       * MinMeasuredValue and MaxMeasuredValue define the range of the sensor.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetMaxMeasuredValueAsync()
       {
           return Read(_attributes[ATTR_MAXMEASUREDVALUE]);
       }

       /**
       * Synchronously Get the MaxMeasuredValue attribute [attribute ID2].
       *
       * The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue       * that can be measured. A value of 0x8000 means this attribute is not defined.       * <p>       * MaxMeasuredValue shall be greater than MinMeasuredValue.       * <p>       * MinMeasuredValue and MaxMeasuredValue define the range of the sensor.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetMaxMeasuredValue(long refreshPeriod)
       {
           if (_attributes[ATTR_MAXMEASUREDVALUE].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_MAXMEASUREDVALUE].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_MAXMEASUREDVALUE]);
       }


       /**
       * Set reporting for the MaxMeasuredValue attribute [attribute ID2].
       *
       * The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue       * that can be measured. A value of 0x8000 means this attribute is not defined.       * <p>       * MaxMeasuredValue shall be greater than MinMeasuredValue.       * <p>       * MinMeasuredValue and MaxMeasuredValue define the range of the sensor.       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @param minInterval minimum reporting period
       * @param maxInterval maximum reporting period
       * @param reportableChange {@link Object} delta required to trigger report
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetMaxMeasuredValueReporting(ushort minInterval, ushort maxInterval, object reportableChange)
       {
           return SetReporting(_attributes[ATTR_MAXMEASUREDVALUE], minInterval, maxInterval, reportableChange);
       }


       /**
       * Get the Tolerance attribute [attribute ID3].
       *
       * The Tolerance attribute indicates the magnitude of the possible error that is       * associated with MeasuredValue . The true value is located in the range       * (MeasuredValue – Tolerance) to (MeasuredValue + Tolerance).       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetToleranceAsync()
       {
           return Read(_attributes[ATTR_TOLERANCE]);
       }

       /**
       * Synchronously Get the Tolerance attribute [attribute ID3].
       *
       * The Tolerance attribute indicates the magnitude of the possible error that is       * associated with MeasuredValue . The true value is located in the range       * (MeasuredValue – Tolerance) to (MeasuredValue + Tolerance).       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public ushort GetTolerance(long refreshPeriod)
       {
           if (_attributes[ATTR_TOLERANCE].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_TOLERANCE].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_TOLERANCE]);
       }


       /**
       * Get the ScaledValue attribute [attribute ID16].
       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetScaledValueAsync()
       {
           return Read(_attributes[ATTR_SCALEDVALUE]);
       }

       /**
       * Synchronously Get the ScaledValue attribute [attribute ID16].
       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetScaledValue(long refreshPeriod)
       {
           if (_attributes[ATTR_SCALEDVALUE].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_SCALEDVALUE].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_SCALEDVALUE]);
       }


       /**
       * Set reporting for the ScaledValue attribute [attribute ID16].
       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param minInterval minimum reporting period
       * @param maxInterval maximum reporting period
       * @param reportableChange {@link Object} delta required to trigger report
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetScaledValueReporting(ushort minInterval, ushort maxInterval, object reportableChange)
       {
           return SetReporting(_attributes[ATTR_SCALEDVALUE], minInterval, maxInterval, reportableChange);
       }


       /**
       * Get the MinScaledValue attribute [attribute ID17].
       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetMinScaledValueAsync()
       {
           return Read(_attributes[ATTR_MINSCALEDVALUE]);
       }

       /**
       * Synchronously Get the MinScaledValue attribute [attribute ID17].
       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetMinScaledValue(long refreshPeriod)
       {
           if (_attributes[ATTR_MINSCALEDVALUE].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_MINSCALEDVALUE].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_MINSCALEDVALUE]);
       }


       /**
       * Get the MaxScaledValue attribute [attribute ID18].
       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetMaxScaledValueAsync()
       {
           return Read(_attributes[ATTR_MAXSCALEDVALUE]);
       }

       /**
       * Synchronously Get the MaxScaledValue attribute [attribute ID18].
       *
       * The attribute is of type short.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public short GetMaxScaledValue(long refreshPeriod)
       {
           if (_attributes[ATTR_MAXSCALEDVALUE].IsLastValueCurrent(refreshPeriod))
           {
               return (short)_attributes[ATTR_MAXSCALEDVALUE].LastValue;
           }

           return (short)ReadSync(_attributes[ATTR_MAXSCALEDVALUE]);
       }


       /**
       * Get the ScaledTolerance attribute [attribute ID19].
       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetScaledToleranceAsync()
       {
           return Read(_attributes[ATTR_SCALEDTOLERANCE]);
       }

       /**
       * Synchronously Get the ScaledTolerance attribute [attribute ID19].
       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public ushort GetScaledTolerance(long refreshPeriod)
       {
           if (_attributes[ATTR_SCALEDTOLERANCE].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_SCALEDTOLERANCE].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_SCALEDTOLERANCE]);
       }


       /**
       * Set reporting for the ScaledTolerance attribute [attribute ID19].
       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param minInterval minimum reporting period
       * @param maxInterval maximum reporting period
       * @param reportableChange {@link Object} delta required to trigger report
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetScaledToleranceReporting(ushort minInterval, ushort maxInterval, object reportableChange)
       {
           return SetReporting(_attributes[ATTR_SCALEDTOLERANCE], minInterval, maxInterval, reportableChange);
       }


       /**
       * Get the Scale attribute [attribute ID20].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetScaleAsync()
       {
           return Read(_attributes[ATTR_SCALE]);
       }

       /**
       * Synchronously Get the Scale attribute [attribute ID20].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetScale(long refreshPeriod)
       {
           if (_attributes[ATTR_SCALE].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_SCALE].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_SCALE]);
       }

   }
}
