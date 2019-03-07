﻿// License text here

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

<summary>
Flow measurementcluster implementation (Cluster ID 0x0404).
 
 * The server cluster provides an interface to flow measurement functionality, * including configuration and provision of notifications of flow measurements. 
  Code is auto-generated. Modifications may be overwritten!
 </summary>
namespace ZigBeeNet.ZCL.Clusters
{
   public class ZclFlowMeasurementCluster : ZclCluster
   {
       <summary>
        The ZigBee Cluster Library Cluster ID
       </summary>
       public const ushort CLUSTER_ID = 0x0404;

       <summary>
        The ZigBee Cluster Library Cluster Name
       </summary>
       public const string CLUSTER_NAME = "Flow measurement";

       /* Attribute constants */
       <summary>
        * MeasuredValue represents the flow in m3/h as follows:-        * <p>        * MeasuredValue = 10 x Flow        * <p>        * Where 0 m3/h <= Flow <= 6,553.4 m3        * <p>        * /h, corresponding to a MeasuredValue in the        * range 0 to 0xfffe.        * <p>        * The maximum resolution this format allows is 0.1 m3/h.        * <p>        * A MeasuredValue of 0xffff indicates that the pressure measurement is invalid.        * <p>        * MeasuredValue is updated continuously as new measurements are made.       </summary>
       public const ushort ATTR_MEASUREDVALUE = 0x0000;

       <summary>
        * The MinMeasuredValue attribute indicates the minimum value of MeasuredValue        * that can be measured. A value of 0xffff means this attribute is not defined       </summary>
       public const ushort ATTR_MINMEASUREDVALUE = 0x0001;

       <summary>
        * The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue        * that can be measured. A value of 0xffff means this attribute is not defined.        * <p>        * MaxMeasuredValue shall be greater than MinMeasuredValue.        * <p>        * MinMeasuredValue and MaxMeasuredValue define the range of the sensor       </summary>
       public const ushort ATTR_MAXMEASUREDVALUE = 0x0002;

       <summary>
        * The Tolerance attribute indicates the magnitude of the possible error that is        * associated with MeasuredValue . The true value is located in the range        * (MeasuredValue – Tolerance) to (MeasuredValue + Tolerance).       </summary>
       public const ushort ATTR_TOLERANCE = 0x0003;


       // Attribute initialisation
       protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
       {
           Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(4);

           ZclClusterType flowmeasurement = ZclClusterType.GetValueById(ClusterType.FLOW_MEASUREMENT);

           attributeMap.Add(ATTR_MEASUREDVALUE, new ZclAttribute(flowmeasurement, ATTR_MEASUREDVALUE, "MeasuredValue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, true));
           attributeMap.Add(ATTR_MINMEASUREDVALUE, new ZclAttribute(flowmeasurement, ATTR_MINMEASUREDVALUE, "MinMeasuredValue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
           attributeMap.Add(ATTR_MAXMEASUREDVALUE, new ZclAttribute(flowmeasurement, ATTR_MAXMEASUREDVALUE, "MaxMeasuredValue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
           attributeMap.Add(ATTR_TOLERANCE, new ZclAttribute(flowmeasurement, ATTR_TOLERANCE, "Tolerance", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, true));

           return attributeMap;
       }

        Default constructor to create a Flow measurement cluster.
       
       <param name= zigbeeEndpoint the {@link ZigBeeEndpoint}
       </param>
       public ZclFlowMeasurementCluster(ZigBeeEndpoint zigbeeEndpoint)
           : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
       {
       }


       <summary>
       * Get the MeasuredValue attribute [attribute ID0].
       
       * MeasuredValue represents the flow in m3/h as follows:-       * <p>       * MeasuredValue = 10 x Flow       * <p>       * Where 0 m3/h <= Flow <= 6,553.4 m3       * <p>       * /h, corresponding to a MeasuredValue in the       * range 0 to 0xfffe.       * <p>       * The maximum resolution this format allows is 0.1 m3/h.       * <p>       * A MeasuredValue of 0xffff indicates that the pressure measurement is invalid.       * <p>       * MeasuredValue is updated continuously as new measurements are made.       
        The attribute is of type ushort.
       
        The implementation of this attribute by a device is MANDATORY
      </summary>
       <returns> the Task<CommandResult> command result Task</returns>
       
       public Task<CommandResult> GetMeasuredValueAsync()
       {
           return Read(_attributes[ATTR_MEASUREDVALUE]);
       }

       <summary>
       * Synchronously Get the MeasuredValue attribute [attribute ID0].
       
       * MeasuredValue represents the flow in m3/h as follows:-       * <p>       * MeasuredValue = 10 x Flow       * <p>       * Where 0 m3/h <= Flow <= 6,553.4 m3       * <p>       * /h, corresponding to a MeasuredValue in the       * range 0 to 0xfffe.       * <p>       * The maximum resolution this format allows is 0.1 m3/h.       * <p>       * A MeasuredValue of 0xffff indicates that the pressure measurement is invalid.       * <p>       * MeasuredValue is updated continuously as new measurements are made.       
        The attribute is of type ushort.
       
        The implementation of this attribute by a device is MANDATORY
      </summary>
       <returns> the Task<CommandResult> command result Task</returns>
       
       public ushort GetMeasuredValue(long refreshPeriod)
       {
           if (_attributes[ATTR_MEASUREDVALUE].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_MEASUREDVALUE].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_MEASUREDVALUE]);
       }


       <summary>
       * Set reporting for the MeasuredValue attribute [attribute ID0].
       
       * MeasuredValue represents the flow in m3/h as follows:-       * <p>       * MeasuredValue = 10 x Flow       * <p>       * Where 0 m3/h <= Flow <= 6,553.4 m3       * <p>       * /h, corresponding to a MeasuredValue in the       * range 0 to 0xfffe.       * <p>       * The maximum resolution this format allows is 0.1 m3/h.       * <p>       * A MeasuredValue of 0xffff indicates that the pressure measurement is invalid.       * <p>       * MeasuredValue is updated continuously as new measurements are made.       
        The attribute is of type ushort.
       
        The implementation of this attribute by a device is MANDATORY
      </summary>
       <param name= minInterval minimum reporting period<param>
       <param name= maxInterval maximum reporting period</param>
       <param name= reportableChange {@link Object} delta required to trigger report</param>
       <returns> the Task<CommandResult> command result Task</returns>
       
       public Task<CommandResult> SetMeasuredValueReporting(ushort minInterval, ushort maxInterval, object reportableChange)
       {
           return SetReporting(_attributes[ATTR_MEASUREDVALUE], minInterval, maxInterval, reportableChange);
       }


       <summary>
       * Get the MinMeasuredValue attribute [attribute ID1].
       
       * The MinMeasuredValue attribute indicates the minimum value of MeasuredValue       * that can be measured. A value of 0xffff means this attribute is not defined       
        The attribute is of type ushort.
       
        The implementation of this attribute by a device is MANDATORY
      </summary>
       <returns> the Task<CommandResult> command result Task</returns>
       
       public Task<CommandResult> GetMinMeasuredValueAsync()
       {
           return Read(_attributes[ATTR_MINMEASUREDVALUE]);
       }

       <summary>
       * Synchronously Get the MinMeasuredValue attribute [attribute ID1].
       
       * The MinMeasuredValue attribute indicates the minimum value of MeasuredValue       * that can be measured. A value of 0xffff means this attribute is not defined       
        The attribute is of type ushort.
       
        The implementation of this attribute by a device is MANDATORY
      </summary>
       <returns> the Task<CommandResult> command result Task</returns>
       
       public ushort GetMinMeasuredValue(long refreshPeriod)
       {
           if (_attributes[ATTR_MINMEASUREDVALUE].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_MINMEASUREDVALUE].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_MINMEASUREDVALUE]);
       }


       <summary>
       * Get the MaxMeasuredValue attribute [attribute ID2].
       
       * The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue       * that can be measured. A value of 0xffff means this attribute is not defined.       * <p>       * MaxMeasuredValue shall be greater than MinMeasuredValue.       * <p>       * MinMeasuredValue and MaxMeasuredValue define the range of the sensor       
        The attribute is of type ushort.
       
        The implementation of this attribute by a device is MANDATORY
      </summary>
       <returns> the Task<CommandResult> command result Task</returns>
       
       public Task<CommandResult> GetMaxMeasuredValueAsync()
       {
           return Read(_attributes[ATTR_MAXMEASUREDVALUE]);
       }

       <summary>
       * Synchronously Get the MaxMeasuredValue attribute [attribute ID2].
       
       * The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue       * that can be measured. A value of 0xffff means this attribute is not defined.       * <p>       * MaxMeasuredValue shall be greater than MinMeasuredValue.       * <p>       * MinMeasuredValue and MaxMeasuredValue define the range of the sensor       
        The attribute is of type ushort.
       
        The implementation of this attribute by a device is MANDATORY
      </summary>
       <returns> the Task<CommandResult> command result Task</returns>
       
       public ushort GetMaxMeasuredValue(long refreshPeriod)
       {
           if (_attributes[ATTR_MAXMEASUREDVALUE].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_MAXMEASUREDVALUE].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_MAXMEASUREDVALUE]);
       }


       <summary>
       * Get the Tolerance attribute [attribute ID3].
       
       * The Tolerance attribute indicates the magnitude of the possible error that is       * associated with MeasuredValue . The true value is located in the range       * (MeasuredValue – Tolerance) to (MeasuredValue + Tolerance).       
        The attribute is of type ushort.
       
        The implementation of this attribute by a device is OPTIONAL
      </summary>
       <returns> the Task<CommandResult> command result Task</returns>
       
       public Task<CommandResult> GetToleranceAsync()
       {
           return Read(_attributes[ATTR_TOLERANCE]);
       }

       <summary>
       * Synchronously Get the Tolerance attribute [attribute ID3].
       
       * The Tolerance attribute indicates the magnitude of the possible error that is       * associated with MeasuredValue . The true value is located in the range       * (MeasuredValue – Tolerance) to (MeasuredValue + Tolerance).       
        The attribute is of type ushort.
       
        The implementation of this attribute by a device is OPTIONAL
      </summary>
       <returns> the Task<CommandResult> command result Task</returns>
       
       public ushort GetTolerance(long refreshPeriod)
       {
           if (_attributes[ATTR_TOLERANCE].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_TOLERANCE].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_TOLERANCE]);
       }


       <summary>
       * Set reporting for the Tolerance attribute [attribute ID3].
       
       * The Tolerance attribute indicates the magnitude of the possible error that is       * associated with MeasuredValue . The true value is located in the range       * (MeasuredValue – Tolerance) to (MeasuredValue + Tolerance).       
        The attribute is of type ushort.
       
        The implementation of this attribute by a device is OPTIONAL
      </summary>
       <param name= minInterval minimum reporting period<param>
       <param name= maxInterval maximum reporting period</param>
       <param name= reportableChange {@link Object} delta required to trigger report</param>
       <returns> the Task<CommandResult> command result Task</returns>
       
       public Task<CommandResult> SetToleranceReporting(ushort minInterval, ushort maxInterval, object reportableChange)
       {
           return SetReporting(_attributes[ATTR_TOLERANCE], minInterval, maxInterval, reportableChange);
       }

   }
}
