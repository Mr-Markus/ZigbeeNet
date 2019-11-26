
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
    /// Thermostat User Interface Configuration cluster implementation (Cluster ID 0x0204).
    ///
    /// This cluster provides an interface to allow configuration of the user interface for a
    /// thermostat, or a thermostat controller device, that supports a keypad and LCD screen.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclThermostatUserInterfaceConfigurationCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0204;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Thermostat User Interface Configuration";

        // Attribute constants

        /// <summary>
        /// The TemperatureDisplayMode attribute specifies the units of the temperature
        /// displayed on the thermostat screen.
        /// </summary>
        public const ushort ATTR_TEMPERATUREDISPLAYMODE = 0x0000;

        /// <summary>
        /// The KeypadLockout attribute specifies the level of functionality that is
        /// available to the user via the keypad.
        /// </summary>
        public const ushort ATTR_KEYPADLOCKOUT = 0x0001;

        /// <summary>
        /// The ScheduleProgrammingVisibility attribute is used to hide the weekly schedule
        /// programming functionality or menu on a thermostat from a user to prevent local user
        /// programming of the weekly schedule. The schedule programming may still be
        /// performed via a remote interface, and the thermostat may operate in schedule
        /// programming mode.
        /// This command is designed to prevent local tampering with or disabling of schedules
        /// that may have been programmed by users or service providers via a more capable
        /// remote interface. The programming schedule shall continue to run even though it is
        /// not visible to the user locally at the thermostat.
        /// </summary>
        public const ushort ATTR_SCHEDULEPROGRAMMINGVISIBILITY = 0x0002;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(3);

            attributeMap.Add(ATTR_TEMPERATUREDISPLAYMODE, new ZclAttribute(this, ATTR_TEMPERATUREDISPLAYMODE, "Temperature Display Mode", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, true));
            attributeMap.Add(ATTR_KEYPADLOCKOUT, new ZclAttribute(this, ATTR_KEYPADLOCKOUT, "Keypad Lockout", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, true));
            attributeMap.Add(ATTR_SCHEDULEPROGRAMMINGVISIBILITY, new ZclAttribute(this, ATTR_SCHEDULEPROGRAMMINGVISIBILITY, "Schedule Programming Visibility", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, true));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Thermostat User Interface Configuration cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclThermostatUserInterfaceConfigurationCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
