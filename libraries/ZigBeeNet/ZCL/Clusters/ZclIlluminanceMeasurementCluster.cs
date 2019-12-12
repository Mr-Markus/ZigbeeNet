
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
    /// Illuminance Measurement cluster implementation (Cluster ID 0x0400).
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
        public const string CLUSTER_NAME = "Illuminance Measurement";

        // Attribute constants

        /// <summary>
        /// MeasuredValue represents the Illuminance in Lux (symbol lx) as follows:-
        /// MeasuredValue = 10,000 x log10 Illuminance + 1
        /// Where 1 lx <= Illuminance <=3.576 Mlx, corresponding to a MeasuredValue in the
        /// range 1 to 0xfffe.
        /// The following special values of MeasuredValue apply. <li>0x0000 indicates a
        /// value of Illuminance that is too low to be measured.</li> <li>0xffff indicates
        /// that the Illuminance measurement is invalid.</li>
        /// </summary>
        public const ushort ATTR_MEASUREDVALUE = 0x0000;

        /// <summary>
        /// The MinMeasuredValue attribute indicates the minimum value of MeasuredValue
        /// that can be measured. A value of 0xffff indicates that this attribute is not
        /// defined.
        /// </summary>
        public const ushort ATTR_MINMEASUREDVALUE = 0x0001;

        /// <summary>
        /// The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue
        /// that can be measured. A value of 0xffff indicates that this attribute is not
        /// defined.
        /// MaxMeasuredValue shall be greater than MinMeasuredValue.
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

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(5);

            attributeMap.Add(ATTR_MEASUREDVALUE, new ZclAttribute(this, ATTR_MEASUREDVALUE, "Measured Value", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_MINMEASUREDVALUE, new ZclAttribute(this, ATTR_MINMEASUREDVALUE, "Min Measured Value", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MAXMEASUREDVALUE, new ZclAttribute(this, ATTR_MAXMEASUREDVALUE, "Max Measured Value", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_TOLERANCE, new ZclAttribute(this, ATTR_TOLERANCE, "Tolerance", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_LIGHTSENSORTYPE, new ZclAttribute(this, ATTR_LIGHTSENSORTYPE, "Light Sensor Type", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Illuminance Measurement cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclIlluminanceMeasurementCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
