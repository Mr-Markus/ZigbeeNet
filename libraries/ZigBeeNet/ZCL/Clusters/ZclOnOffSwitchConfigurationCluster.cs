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
    /// On/off Switch Configurationcluster implementation (Cluster ID 0x0007).
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
        public const string CLUSTER_NAME = "On/off Switch Configuration";

        /* Attribute constants */

        /// <summary>
        /// The SwitchTypeattribute  specifies  the  basic  functionality  of  the  On/Off  switching  device.
        /// </summary>
        public const ushort ATTR_SWITCHTYPE = 0x0000;

        /// <summary>
        /// The SwitchActions attribute is 8 bits in length and specifies the commands of the On/Off cluster
        /// to be generated when the switch moves between its two states
        /// </summary>
        public const ushort ATTR_SWITCHACTIONS = 0x0010;


        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(2);

            ZclClusterType onoffSwitchConfiguration = ZclClusterType.GetValueById(ClusterType.ON_OFF_SWITCH_CONFIGURATION);

            attributeMap.Add(ATTR_SWITCHTYPE, new ZclAttribute(onoffSwitchConfiguration, ATTR_SWITCHTYPE, "SwitchType", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_SWITCHACTIONS, new ZclAttribute(onoffSwitchConfiguration, ATTR_SWITCHACTIONS, "SwitchActions", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, true, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a On/off Switch Configuration cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclOnOffSwitchConfigurationCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// Get the SwitchType attribute [attribute ID0].
        ///
        /// The SwitchTypeattribute  specifies  the  basic  functionality  of  the  On/Off  switching  device.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetSwitchTypeAsync()
        {
            return Read(_attributes[ATTR_SWITCHTYPE]);
        }

        /// <summary>
        /// Synchronously Get the SwitchType attribute [attribute ID0].
        ///
        /// The SwitchTypeattribute  specifies  the  basic  functionality  of  the  On/Off  switching  device.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetSwitchType(long refreshPeriod)
        {
            if (_attributes[ATTR_SWITCHTYPE].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_SWITCHTYPE].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_SWITCHTYPE]);
        }


        /// <summary>
        /// Set the SwitchActions attribute [attribute ID16].
        ///
        /// The SwitchActions attribute is 8 bits in length and specifies the commands of the On/Off cluster
        /// to be generated when the switch moves between its two states
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <param name="switchActions">The byte attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetSwitchActions(object value)
        {
            return Write(_attributes[ATTR_SWITCHACTIONS], value);
        }


        /// <summary>
        /// Get the SwitchActions attribute [attribute ID16].
        ///
        /// The SwitchActions attribute is 8 bits in length and specifies the commands of the On/Off cluster
        /// to be generated when the switch moves between its two states
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetSwitchActionsAsync()
        {
            return Read(_attributes[ATTR_SWITCHACTIONS]);
        }

        /// <summary>
        /// Synchronously Get the SwitchActions attribute [attribute ID16].
        ///
        /// The SwitchActions attribute is 8 bits in length and specifies the commands of the On/Off cluster
        /// to be generated when the switch moves between its two states
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetSwitchActions(long refreshPeriod)
        {
            if (_attributes[ATTR_SWITCHACTIONS].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_SWITCHACTIONS].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_SWITCHACTIONS]);
        }

    }
}
