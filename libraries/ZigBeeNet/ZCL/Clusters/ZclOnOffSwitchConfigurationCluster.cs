
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
    /// On / Off Switch Configuration cluster implementation (Cluster ID 0x0007.
    ///
    /// Attributes and commands for configuring On/Off switching devices
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclOnOffSwitchConfigurationCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0007;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "On / Off Switch Configuration";

        // Attribute constants

        /// <summary>
        /// The SwitchType attribute specifies the basic functionality of the On/Off
        /// switching device.
        /// </summary>
        public const ushort ATTR_SWITCHTYPE = 0x0000;

        /// <summary>
        /// The SwitchActions attribute is 8 bits in length and specifies the commands of the
        /// On/Off cluster to be generated when the switch moves between its two states
        /// </summary>
        public const ushort ATTR_SWITCHACTIONS = 0x0010;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(2);

            attributeMap.Add(ATTR_SWITCHTYPE, new ZclAttribute(this, ATTR_SWITCHTYPE, "Switch Type", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_SWITCHACTIONS, new ZclAttribute(this, ATTR_SWITCHACTIONS, "Switch Actions", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, true, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a On / Off Switch Configuration cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclOnOffSwitchConfigurationCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
