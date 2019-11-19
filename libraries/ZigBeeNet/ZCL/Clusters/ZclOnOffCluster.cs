
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.OnOff;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// On/Off cluster implementation (Cluster ID 0x0006.
    ///
    /// Attributes and commands for switching devices between ‘On’ and ‘Off’ states.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclOnOffCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0006;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "On/Off";

        // Attribute constants

        /// <summary>
        /// The OnOff attribute has the following values: 0 = Off, 1 = On
        /// </summary>
        public const ushort ATTR_ONOFF = 0x0000;

        /// <summary>
        /// In order to support the use case where the user gets back the last setting of the
        /// devices (e.g. level settings for lamps), a global scene is introduced which is
        /// stored when the devices are turned off and recalled when the devices are turned on.
        /// The global scene is defined as the scene that is stored with group identifier 0 and
        /// scene identifier 0.
        /// The GlobalSceneControl attribute is defined in order to prevent a second off
        /// command storing the all-devices-off situation as a global scene, and to prevent a
        /// second on command destroying the current settings by going back to the global
        /// scene.
        /// The GlobalSceneControl attribute shall be set to TRUE after the reception of a
        /// command which causes the OnOff attribute to be set to TRUE, such as a standard On
        /// command, a Move to level (with on/off) command, a Recall scene command or a On with
        /// recall global scene command.
        /// The GlobalSceneControl attribute is set to FALSE after reception of a Off with
        /// effect command.
        /// </summary>
        public const ushort ATTR_GLOBALSCENECONTROL = 0x4000;

        /// <summary>
        /// The OnTime attribute specifies the length of time (in 1/10ths second) that the “on”
        /// state shall be maintained before automatically transitioning to the “off” state
        /// when using the On with timed off command. If this attribute is set to 0x0000 or
        /// 0xffff, the device shall remain in its current state.
        /// </summary>
        public const ushort ATTR_ONTIME = 0x4001;

        /// <summary>
        /// The OffWaitTime attribute specifies the length of time (in 1/10ths second) that
        /// the “off” state shall be guarded to prevent an on command turning the device back to
        /// its “on” state (e.g., when leaving a room, the lights are turned off but an occupancy
        /// sensor detects the leaving person and attempts to turn the lights back on). If this
        /// attribute is set to 0x0000, the device shall remain in its current state.
        /// </summary>
        public const ushort ATTR_OFFWAITTIME = 0x4002;

        /// <summary>
        ///         /// </summary>
        public const ushort ATTR_STARTUPONOFF = 0x4003;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(5);

            attributeMap.Add(ATTR_ONOFF, new ZclAttribute(this, ATTR_ONOFF, "On Off", ZclDataType.Get(DataType.BOOLEAN), true, true, false, true));
            attributeMap.Add(ATTR_GLOBALSCENECONTROL, new ZclAttribute(this, ATTR_GLOBALSCENECONTROL, "Global Scene Control", ZclDataType.Get(DataType.BOOLEAN), true, true, false, false));
            attributeMap.Add(ATTR_ONTIME, new ZclAttribute(this, ATTR_ONTIME, "On Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, true, false));
            attributeMap.Add(ATTR_OFFWAITTIME, new ZclAttribute(this, ATTR_OFFWAITTIME, "Off Wait Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, true, false));
            attributeMap.Add(ATTR_STARTUPONOFF, new ZclAttribute(this, ATTR_STARTUPONOFF, "Start Up On Off", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(6);

            commandMap.Add(0x0000, () => new OffCommand());
            commandMap.Add(0x0001, () => new OnCommand());
            commandMap.Add(0x0002, () => new ToggleCommand());
            commandMap.Add(0x0040, () => new OffWithEffectCommand());
            commandMap.Add(0x0041, () => new OnWithRecallGlobalSceneCommand());
            commandMap.Add(0x0042, () => new OnWithTimedOffCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a On/Off cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclOnOffCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Off Command
        ///
        /// On receipt of this command, a device shall enter its ‘Off’ state. This state is
        /// device dependent, but it is recommended that it is used for power off or similar
        /// functions. On receipt of the Off command, the OnTime attribute shall be set to
        /// 0x0000.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> OffCommand()
        {
            return Send(new OffCommand());
        }

        /// <summary>
        /// The On Command
        ///
        /// On receipt of this command, a device shall enter its ‘On’ state. This state is device
        /// dependent, but it is recommended that it is used for power on or similar functions.
        /// On receipt of the On command, if the value of the OnTime attribute is equal to 0x0000,
        /// the device shall set the OffWaitTime attribute to 0x0000.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> OnCommand()
        {
            return Send(new OnCommand());
        }

        /// <summary>
        /// The Toggle Command
        ///
        /// On receipt of this command, if a device is in its ‘Off’ state it shall enter its ‘On’
        /// state. Otherwise, if it is in its ‘On’ state it shall enter its ‘Off’ state. On
        /// receipt of the Toggle command, if the value of the OnOff attribute is equal to 0x00
        /// and if the value of the OnTime attribute is equal to 0x0000, the device shall set the
        /// OffWaitTime attribute to 0x0000. If the value of the OnOff attribute is equal to
        /// 0x01, the OnTime attribute shall be set to 0x0000.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ToggleCommand()
        {
            return Send(new ToggleCommand());
        }

        /// <summary>
        /// The Off With Effect Command
        ///
        /// The Off With Effect command allows devices to be turned off using enhanced ways of
        /// fading.
        ///
        /// <param name="effectIdentifier" <see cref="byte"> Effect Identifier</ param >
        /// <param name="effectVariant" <see cref="byte"> Effect Variant</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> OffWithEffectCommand(byte effectIdentifier, byte effectVariant)
        {
            OffWithEffectCommand command = new OffWithEffectCommand();

            // Set the fields
            command.EffectIdentifier = effectIdentifier;
            command.EffectVariant = effectVariant;

            return Send(command);
        }

        /// <summary>
        /// The On With Recall Global Scene Command
        ///
        /// The On With Recall Global Scene command allows the recall of the settings when the
        /// device was turned off.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> OnWithRecallGlobalSceneCommand()
        {
            return Send(new OnWithRecallGlobalSceneCommand());
        }

        /// <summary>
        /// The On With Timed Off Command
        ///
        /// The On With Timed Off command allows devices to be turned on for a specific duration
        /// with a guarded off duration so that should the device be subsequently switched off,
        /// further On With Timed Off commands, received during this time, are prevented from
        /// turning the devices back on. Note that the device can be periodically re-kicked by
        /// subsequent On With Timed Off commands, e.g., from an on/off sensor.
        ///
        /// <param name="onOffControl" <see cref="byte"> On Off Control</ param >
        /// <param name="onTime" <see cref="ushort"> On Time</ param >
        /// <param name="offWaitTime" <see cref="ushort"> Off Wait Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> OnWithTimedOffCommand(byte onOffControl, ushort onTime, ushort offWaitTime)
        {
            OnWithTimedOffCommand command = new OnWithTimedOffCommand();

            // Set the fields
            command.OnOffControl = onOffControl;
            command.OnTime = onTime;
            command.OffWaitTime = offWaitTime;

            return Send(command);
        }
    }
}
