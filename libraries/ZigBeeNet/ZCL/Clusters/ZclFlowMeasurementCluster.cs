
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
    /// Flow Measurement cluster implementation (Cluster ID 0x0404.
    ///
    /// The server cluster provides an interface to flow measurement functionality,
    /// including configuration and provision of notifications of flow measurements.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclFlowMeasurementCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0404;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Flow Measurement";

        // Attribute constants

        /// <summary>
        /// MeasuredValue represents the flow in m3/h as follows:-
        /// MeasuredValue = 10 x Flow
        /// Where 0 m3/h <= Flow <= 6,553.4 m3
        /// /h, corresponding to a MeasuredValue in the range 0 to 0xfffe.
        /// The maximum resolution this format allows is 0.1 m3/h.
        /// A MeasuredValue of 0xffff indicates that the pressure measurement is invalid.
        /// MeasuredValue is updated continuously as new measurements are made.
        /// </summary>
        public const ushort ATTR_MEASUREDVALUE = 0x0000;

        /// <summary>
        /// The MinMeasuredValue attribute indicates the minimum value of MeasuredValue
        /// that can be measured. A value of 0xffff means this attribute is not defined
        /// </summary>
        public const ushort ATTR_MINMEASUREDVALUE = 0x0001;

        /// <summary>
        /// The MaxMeasuredValue attribute indicates the maximum value of MeasuredValue
        /// that can be measured. A value of 0xffff means this attribute is not defined.
        /// MaxMeasuredValue shall be greater than MinMeasuredValue.
        /// MinMeasuredValue and MaxMeasuredValue define the range of the sensor
        /// </summary>
        public const ushort ATTR_MAXMEASUREDVALUE = 0x0002;

        /// <summary>
        /// The Tolerance attribute indicates the magnitude of the possible error that is
        /// associated with MeasuredValue . The true value is located in the range
        /// (MeasuredValue – Tolerance) to (MeasuredValue + Tolerance).
        /// </summary>
        public const ushort ATTR_TOLERANCE = 0x0003;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(4);

            attributeMap.Add(ATTR_MEASUREDVALUE, new ZclAttribute(this, ATTR_MEASUREDVALUE, "Measured Value", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_MINMEASUREDVALUE, new ZclAttribute(this, ATTR_MINMEASUREDVALUE, "Min Measured Value", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MAXMEASUREDVALUE, new ZclAttribute(this, ATTR_MAXMEASUREDVALUE, "Max Measured Value", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_TOLERANCE, new ZclAttribute(this, ATTR_TOLERANCE, "Tolerance", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, true));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Flow Measurement cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclFlowMeasurementCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
