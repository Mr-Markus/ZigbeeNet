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
    /// Illuminance measurementcluster implementation (Cluster ID 0x0400).
    ///
    /// The cluster provides an interface to illuminance measurement functionality,
    /// including configuration and provision of notifications of illuminance
    /// measurements.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclIlluminanceMeasurementCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0400;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Illuminance measurement";

        /* Attribute constants */

        /// <summary>
        /// MeasuredValue represents the Illuminance in Lux (symbol lx) as follows:-
        /// 
        /// MeasuredValue = 10,000 x log10 Illuminance + 1
        /// 
        /// Where 1 lx <= Illuminance <=3.576 Mlx, corresponding to a MeasuredValue in
        /// the range 1 to 0xfffe.
        /// 
        /// The following special values of MeasuredValue apply.
        /// <li>0x0000 indicates a value of Illuminance that is too low to be measured.</li>
        /// <li>0xffff indicates that the Illuminance measurement is invalid.</li>
        /// </summary>
        public const ushort ATTR_MEASUREDVALUE = 0x0000;

        /// <summary>
        /// The MinMeasuredValue attribute indicates the minimum value of MeasuredValue
        /// that can be measured. A value of 0xffff indicates that this attribute is not defined.
        /// </summary>
        public const ushort ATTR_MINMEASUREDVALUE = 0x0001;

        /// <summary>
        /// The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue
        /// that can be measured. A value of 0xffff indicates that this attribute is not defined.
        /// 
        /// MaxMeasuredValue shall be greater than MinMeasuredValue.
        /// 
        /// MinMeasuredValue and MaxMeasuredValue define the range of the sensor.
        /// </summary>
        public const ushort ATTR_MAXMEASUREDVALUE = 0x0002;

        /// <summary>
        /// The Tolerance attribute indicates the magnitude of the possible error that is
        /// associated with MeasuredValue . The true value is located in the range
        /// (MeasuredValue – Tolerance) to (MeasuredValue + Tolerance).
        /// </summary>
        public const ushort ATTR_TOLERANCE = 0x0003;

        /// <summary>
        /// The LightSensorType attribute specifies the electronic type of the light sensor.
        /// </summary>
        public const ushort ATTR_LIGHTSENSORTYPE = 0x0004;


        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(5);

            ZclClusterType illuminancemeasurement = ZclClusterType.GetValueById(ClusterType.ILLUMINANCE_MEASUREMENT);

            attributeMap.Add(ATTR_MEASUREDVALUE, new ZclAttribute(illuminancemeasurement, ATTR_MEASUREDVALUE, "MeasuredValue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_MINMEASUREDVALUE, new ZclAttribute(illuminancemeasurement, ATTR_MINMEASUREDVALUE, "MinMeasuredValue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MAXMEASUREDVALUE, new ZclAttribute(illuminancemeasurement, ATTR_MAXMEASUREDVALUE, "MaxMeasuredValue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_TOLERANCE, new ZclAttribute(illuminancemeasurement, ATTR_TOLERANCE, "Tolerance", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_LIGHTSENSORTYPE, new ZclAttribute(illuminancemeasurement, ATTR_LIGHTSENSORTYPE, "LightSensorType", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Illuminance measurement cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclIlluminanceMeasurementCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// Get the MeasuredValue attribute [attribute ID0].
        ///
        /// MeasuredValue represents the Illuminance in Lux (symbol lx) as follows:-
        /// 
        /// MeasuredValue = 10,000 x log10 Illuminance + 1
        /// 
        /// Where 1 lx <= Illuminance <=3.576 Mlx, corresponding to a MeasuredValue in
        /// the range 1 to 0xfffe.
        /// 
        /// The following special values of MeasuredValue apply.
        /// <li>0x0000 indicates a value of Illuminance that is too low to be measured.</li>
        /// <li>0xffff indicates that the Illuminance measurement is invalid.</li>
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetMeasuredValueAsync()
        {
            return Read(_attributes[ATTR_MEASUREDVALUE]);
        }

        /// <summary>
        /// Synchronously Get the MeasuredValue attribute [attribute ID0].
        ///
        /// MeasuredValue represents the Illuminance in Lux (symbol lx) as follows:-
        /// 
        /// MeasuredValue = 10,000 x log10 Illuminance + 1
        /// 
        /// Where 1 lx <= Illuminance <=3.576 Mlx, corresponding to a MeasuredValue in
        /// the range 1 to 0xfffe.
        /// 
        /// The following special values of MeasuredValue apply.
        /// <li>0x0000 indicates a value of Illuminance that is too low to be measured.</li>
        /// <li>0xffff indicates that the Illuminance measurement is invalid.</li>
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetMeasuredValue(long refreshPeriod)
        {
            if (_attributes[ATTR_MEASUREDVALUE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_MEASUREDVALUE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_MEASUREDVALUE]);
        }


        /// <summary>
        /// Set reporting for the MeasuredValue attribute [attribute ID0].
        ///
        /// MeasuredValue represents the Illuminance in Lux (symbol lx) as follows:-
        /// 
        /// MeasuredValue = 10,000 x log10 Illuminance + 1
        /// 
        /// Where 1 lx <= Illuminance <=3.576 Mlx, corresponding to a MeasuredValue in
        /// the range 1 to 0xfffe.
        /// 
        /// The following special values of MeasuredValue apply.
        /// <li>0x0000 indicates a value of Illuminance that is too low to be measured.</li>
        /// <li>0xffff indicates that the Illuminance measurement is invalid.</li>
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <param name="minInterval">Minimum reporting period</param>
        /// <param name="maxInterval">Maximum reporting period</param>
        /// <param name="reportableChange">Object delta required to trigger report</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetMeasuredValueReporting(ushort minInterval, ushort maxInterval, object reportableChange)
        {
            return SetReporting(_attributes[ATTR_MEASUREDVALUE], minInterval, maxInterval, reportableChange);
        }


        /// <summary>
        /// Get the MinMeasuredValue attribute [attribute ID1].
        ///
        /// The MinMeasuredValue attribute indicates the minimum value of MeasuredValue
        /// that can be measured. A value of 0xffff indicates that this attribute is not defined.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetMinMeasuredValueAsync()
        {
            return Read(_attributes[ATTR_MINMEASUREDVALUE]);
        }

        /// <summary>
        /// Synchronously Get the MinMeasuredValue attribute [attribute ID1].
        ///
        /// The MinMeasuredValue attribute indicates the minimum value of MeasuredValue
        /// that can be measured. A value of 0xffff indicates that this attribute is not defined.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetMinMeasuredValue(long refreshPeriod)
        {
            if (_attributes[ATTR_MINMEASUREDVALUE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_MINMEASUREDVALUE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_MINMEASUREDVALUE]);
        }


        /// <summary>
        /// Get the MaxMeasuredValue attribute [attribute ID2].
        ///
        /// The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue
        /// that can be measured. A value of 0xffff indicates that this attribute is not defined.
        /// 
        /// MaxMeasuredValue shall be greater than MinMeasuredValue.
        /// 
        /// MinMeasuredValue and MaxMeasuredValue define the range of the sensor.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetMaxMeasuredValueAsync()
        {
            return Read(_attributes[ATTR_MAXMEASUREDVALUE]);
        }

        /// <summary>
        /// Synchronously Get the MaxMeasuredValue attribute [attribute ID2].
        ///
        /// The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue
        /// that can be measured. A value of 0xffff indicates that this attribute is not defined.
        /// 
        /// MaxMeasuredValue shall be greater than MinMeasuredValue.
        /// 
        /// MinMeasuredValue and MaxMeasuredValue define the range of the sensor.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetMaxMeasuredValue(long refreshPeriod)
        {
            if (_attributes[ATTR_MAXMEASUREDVALUE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_MAXMEASUREDVALUE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_MAXMEASUREDVALUE]);
        }


        /// <summary>
        /// Get the Tolerance attribute [attribute ID3].
        ///
        /// The Tolerance attribute indicates the magnitude of the possible error that is
        /// associated with MeasuredValue . The true value is located in the range
        /// (MeasuredValue – Tolerance) to (MeasuredValue + Tolerance).
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetToleranceAsync()
        {
            return Read(_attributes[ATTR_TOLERANCE]);
        }

        /// <summary>
        /// Synchronously Get the Tolerance attribute [attribute ID3].
        ///
        /// The Tolerance attribute indicates the magnitude of the possible error that is
        /// associated with MeasuredValue . The true value is located in the range
        /// (MeasuredValue – Tolerance) to (MeasuredValue + Tolerance).
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetTolerance(long refreshPeriod)
        {
            if (_attributes[ATTR_TOLERANCE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_TOLERANCE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_TOLERANCE]);
        }


        /// <summary>
        /// Set reporting for the Tolerance attribute [attribute ID3].
        ///
        /// The Tolerance attribute indicates the magnitude of the possible error that is
        /// associated with MeasuredValue . The true value is located in the range
        /// (MeasuredValue – Tolerance) to (MeasuredValue + Tolerance).
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="minInterval">Minimum reporting period</param>
        /// <param name="maxInterval">Maximum reporting period</param>
        /// <param name="reportableChange">Object delta required to trigger report</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetToleranceReporting(ushort minInterval, ushort maxInterval, object reportableChange)
        {
            return SetReporting(_attributes[ATTR_TOLERANCE], minInterval, maxInterval, reportableChange);
        }


        /// <summary>
        /// Get the LightSensorType attribute [attribute ID4].
        ///
        /// The LightSensorType attribute specifies the electronic type of the light sensor.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetLightSensorTypeAsync()
        {
            return Read(_attributes[ATTR_LIGHTSENSORTYPE]);
        }

        /// <summary>
        /// Synchronously Get the LightSensorType attribute [attribute ID4].
        ///
        /// The LightSensorType attribute specifies the electronic type of the light sensor.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetLightSensorType(long refreshPeriod)
        {
            if (_attributes[ATTR_LIGHTSENSORTYPE].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_LIGHTSENSORTYPE].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_LIGHTSENSORTYPE]);
        }

    }
}
