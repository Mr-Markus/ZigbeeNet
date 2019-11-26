
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
    /// Pressure Measurement cluster implementation (Cluster ID 0x0403).
    ///
    /// The cluster provides an interface to pressure measurement functionality, including
    /// configuration and provision of notifications of pressure measurements.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclPressureMeasurementCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0403;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Pressure Measurement";

        // Attribute constants

        /// <summary>
        /// MeasuredValue represents the pressure in kPa as follows:-
        /// MeasuredValue = 10 x Pressure
        /// Where -3276.7 kPa <= Pressure <= 3276.7 kPa, corresponding to a MeasuredValue in
        /// the range 0x8001 to 0x7fff.
        /// Note:- The maximum resolution this format allows is 0.1 kPa.
        /// A MeasuredValue of 0x8000 indicates that the pressure measurement is invalid.
        /// MeasuredValue is updated continuously as new measurements are made.
        /// </summary>
        public const ushort ATTR_MEASUREDVALUE = 0x0000;

        /// <summary>
        /// The MinMeasuredValue attribute indicates the minimum value of MeasuredValue
        /// that can be measured. A value of 0x8000 means this attribute is not defined.
        /// </summary>
        public const ushort ATTR_MINMEASUREDVALUE = 0x0001;

        /// <summary>
        /// The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue
        /// that can be measured. A value of 0x8000 means this attribute is not defined.
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
        /// ScaledValue represents the pressure in Pascals as follows: ScaledValue = 10Scale
        /// x Pressure in Pa
        /// </summary>
        public const ushort ATTR_SCALEDVALUE = 0x0010;

        /// <summary>
        /// The MinScaledValue attribute indicates the minimum value of ScaledValue that can
        /// be measured. A value of 0x8000 means this attribute is not defined
        /// </summary>
        public const ushort ATTR_MINSCALEDVALUE = 0x0011;

        /// <summary>
        /// The MaxScaledValue attribute indicates the maximum value of ScaledValue that can
        /// be measured. A value of 0x8000 means this attribute is not defined.
        /// </summary>
        public const ushort ATTR_MAXSCALEDVALUE = 0x0012;

        /// <summary>
        /// The ScaledTolerance attribute indicates the magnitude of the possible error that
        /// is associated with ScaledValue. The true value is located in the range
        /// (ScaledValue – ScaledTolerance) to (ScaledValue + ScaledTolerance).
        /// </summary>
        public const ushort ATTR_SCALEDTOLERANCE = 0x0013;

        /// <summary>
        /// The Scale attribute indicates the base 10 exponent used to obtain ScaledValue.
        /// </summary>
        public const ushort ATTR_SCALE = 0x0014;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(9);

            attributeMap.Add(ATTR_MEASUREDVALUE, new ZclAttribute(this, ATTR_MEASUREDVALUE, "Measured Value", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_MINMEASUREDVALUE, new ZclAttribute(this, ATTR_MINMEASUREDVALUE, "Min Measured Value", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MAXMEASUREDVALUE, new ZclAttribute(this, ATTR_MAXMEASUREDVALUE, "Max Measured Value", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_TOLERANCE, new ZclAttribute(this, ATTR_TOLERANCE, "Tolerance", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_SCALEDVALUE, new ZclAttribute(this, ATTR_SCALEDVALUE, "Scaled Value", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_MINSCALEDVALUE, new ZclAttribute(this, ATTR_MINSCALEDVALUE, "Min Scaled Value", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_MAXSCALEDVALUE, new ZclAttribute(this, ATTR_MAXSCALEDVALUE, "Max Scaled Value", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_SCALEDTOLERANCE, new ZclAttribute(this, ATTR_SCALEDTOLERANCE, "Scaled Tolerance", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_SCALE, new ZclAttribute(this, ATTR_SCALE, "Scale", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Pressure Measurement cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclPressureMeasurementCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
