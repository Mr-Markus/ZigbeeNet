
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
    /// Fan Control cluster implementation (Cluster ID 0x0202.
    ///
    /// This cluster specifies an interface to control the speed of a fan as part of a heating /
    /// cooling system.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclFanControlCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0202;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Fan Control";

        // Attribute constants

        /// <summary>
        /// The FanMode attribute is an 8-bit value that specifies the current speed of the fan.
        /// </summary>
        public const ushort ATTR_FANMODE = 0x0000;

        /// <summary>
        /// The FanModeSequence attribute is an 8-bit value that specifies the possible fan
        /// speeds that the thermostat can set.
        /// </summary>
        public const ushort ATTR_FANMODESEQUENCE = 0x0001;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(2);

            attributeMap.Add(ATTR_FANMODE, new ZclAttribute(this, ATTR_FANMODE, "Fan Mode", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, true));
            attributeMap.Add(ATTR_FANMODESEQUENCE, new ZclAttribute(this, ATTR_FANMODESEQUENCE, "Fan Mode Sequence", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, true));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Fan Control cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclFanControlCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
