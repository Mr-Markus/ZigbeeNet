
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
    /// Dehumidification Control cluster implementation (Cluster ID 0x0203.
    ///
    /// This cluster provides an interface to dehumidification functionality.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclDehumidificationControlCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0203;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Dehumidification Control";

        // Attribute constants

        /// <summary>
        /// The RelativeHumidity attribute is an 8-bit value that represents the current
        /// relative humidity (in %) measured by a local or remote sensor. The valid range ix
        /// 0x00 – 0x64 (0% to 100%).
        /// </summary>
        public const ushort ATTR_RELATIVEHUMIDITY = 0x0000;

        /// <summary>
        /// The DehumidificationCooling attribute is an 8-bit value that specifies the
        /// current dehumidification cooling output (in %). The valid range is 0 to
        /// DehumidificationMaxCool.
        /// </summary>
        public const ushort ATTR_DEHUMIDIFICATIONCOOLING = 0x0001;

        /// <summary>
        /// The RHDehumidificationSetpoint attribute is an 8-bit value that represents the
        /// relative humidity (in %) at which dehumidification occurs. The valid range ix 0x1E
        /// – 0x64 (30% to 100%).
        /// </summary>
        public const ushort ATTR_RHDEHUMIDIFICATIONSETPOINT = 0x0010;

        /// <summary>
        /// The RelativeHumidityMode attribute is an 8-bit value that specifies how the
        /// RelativeHumidity value is being updated.
        /// </summary>
        public const ushort ATTR_RELATIVEHUMIDITYMODE = 0x0011;

        /// <summary>
        /// The DehumidificationLockout attribute is an 8-bit value that specifies whether
        /// dehumidification is allowed or not.
        /// </summary>
        public const ushort ATTR_DEHUMIDIFICATIONLOCKOUT = 0x0012;

        /// <summary>
        /// The DehumidificationHysteresis attribute is an 8-bit value that specifies the
        /// hysteresis (in %) associated with RelativeHumidity value.
        /// </summary>
        public const ushort ATTR_DEHUMIDIFICATIONHYSTERESIS = 0x0013;

        /// <summary>
        /// The DehumidificationMaxCool attribute is an 8-bit value that specifies the
        /// maximum dehumidification cooling output (in %). The valid range ix 0x14 – 0x64 (20%
        /// to 100%).
        /// </summary>
        public const ushort ATTR_DEHUMIDIFICATIONMAXCOOL = 0x0014;

        /// <summary>
        /// The RelativeHumidityDisplay attribute is an 8-bit value that specifies whether
        /// the RelativeHumidity value is displayed to the user or not.
        /// </summary>
        public const ushort ATTR_RELATIVEHUMIDITYDISPLAY = 0x0015;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(8);

            attributeMap.Add(ATTR_RELATIVEHUMIDITY, new ZclAttribute(this, ATTR_RELATIVEHUMIDITY, "Relative Humidity", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEHUMIDIFICATIONCOOLING, new ZclAttribute(this, ATTR_DEHUMIDIFICATIONCOOLING, "Dehumidification Cooling", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RHDEHUMIDIFICATIONSETPOINT, new ZclAttribute(this, ATTR_RHDEHUMIDIFICATIONSETPOINT, "Rh Dehumidification Setpoint", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_RELATIVEHUMIDITYMODE, new ZclAttribute(this, ATTR_RELATIVEHUMIDITYMODE, "Relative Humidity Mode", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, true));
            attributeMap.Add(ATTR_DEHUMIDIFICATIONLOCKOUT, new ZclAttribute(this, ATTR_DEHUMIDIFICATIONLOCKOUT, "Dehumidification Lockout", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, true));
            attributeMap.Add(ATTR_DEHUMIDIFICATIONHYSTERESIS, new ZclAttribute(this, ATTR_DEHUMIDIFICATIONHYSTERESIS, "Dehumidification Hysteresis", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_DEHUMIDIFICATIONMAXCOOL, new ZclAttribute(this, ATTR_DEHUMIDIFICATIONMAXCOOL, "Dehumidification Max Cool", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_RELATIVEHUMIDITYDISPLAY, new ZclAttribute(this, ATTR_RELATIVEHUMIDITYDISPLAY, "Relative Humidity Display", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, true));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Dehumidification Control cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclDehumidificationControlCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
