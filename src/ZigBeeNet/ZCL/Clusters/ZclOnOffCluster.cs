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
using ZigBeeNet.ZCL.Clusters.OnOff;

namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// On/Offcluster implementation (Cluster ID 0x0006).
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

        /* Attribute constants */

        /// <summary>
        /// The OnOff attribute has the following values: 0 = Off, 1 = On
        /// </summary>
        public const ushort ATTR_ONOFF = 0x0000;

        /// <summary>
        /// In order to support the use case where the user gets back the last setting of the devices (e.g. level settings for lamps), a global scene is
        /// introduced which is stored when the devices are turned off and recalled when the devices are turned on. The global scene is defined as the
        /// scene that is stored with group identifier 0 and scene identifier 0.
        /// 
        /// The GlobalSceneControl attribute is defined in order to prevent a second off command storing the all-devices-off situation as a global
        /// scene, and to prevent a second on command destroying the current settings by going back to the global scene.
        /// 
        /// The GlobalSceneControl attribute SHALL be set to TRUE after the reception of a command which causes the OnOff attribute to be set to TRUE,
        /// such as a standard On command, a Move to level (with on/off) command, a Recall scene command or a On with recall global scene command.
        /// 
        /// The GlobalSceneControl attribute is set to FALSE after reception of a Off with effect command.
        /// </summary>
        public const ushort ATTR_GLOBALSCENECONTROL = 0x4000;

        /// <summary>
        /// </summary>
        public const ushort ATTR_OFFTIME = 0x4001;

        /// <summary>
        /// The OffWaitTime attribute specifies the length of time (in 1/10ths second) that the “off” state SHALL be guarded to prevent an on command
        /// turning the device back to its “on” state (e.g., when leaving a room, the lights are turned off but an occupancy sensor detects the leaving
        /// person and attempts to turn the lights back on). If this attribute is set to 0x0000, the device SHALL remain in its current state.
        /// </summary>
        public const ushort ATTR_OFFWAITTIME = 0x4002;


        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(4);

            ZclClusterType onOff = ZclClusterType.GetValueById(ClusterType.ON_OFF);

            attributeMap.Add(ATTR_ONOFF, new ZclAttribute(onOff, ATTR_ONOFF, "OnOff", ZclDataType.Get(DataType.BOOLEAN), true, true, false, true));
            attributeMap.Add(ATTR_GLOBALSCENECONTROL, new ZclAttribute(onOff, ATTR_GLOBALSCENECONTROL, "GlobalSceneControl", ZclDataType.Get(DataType.BOOLEAN), false, true, false, false));
            attributeMap.Add(ATTR_OFFTIME, new ZclAttribute(onOff, ATTR_OFFTIME, "OffTime", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_OFFWAITTIME, new ZclAttribute(onOff, ATTR_OFFWAITTIME, "OffWaitTime", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a On/Off cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclOnOffCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// Get the OnOff attribute [attribute ID0].
        ///
        /// The OnOff attribute has the following values: 0 = Off, 1 = On
        ///
        /// The attribute is of type bool.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetOnOffAsync()
        {
            return Read(_attributes[ATTR_ONOFF]);
        }

        /// <summary>
        /// Synchronously Get the OnOff attribute [attribute ID0].
        ///
        /// The OnOff attribute has the following values: 0 = Off, 1 = On
        ///
        /// The attribute is of type bool.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public bool GetOnOff(long refreshPeriod)
        {
            if (_attributes[ATTR_ONOFF].IsLastValueCurrent(refreshPeriod))
            {
                return (bool)_attributes[ATTR_ONOFF].LastValue;
            }

            return (bool)ReadSync(_attributes[ATTR_ONOFF]);
        }


        /// <summary>
        /// Set reporting for the OnOff attribute [attribute ID0].
        ///
        /// The OnOff attribute has the following values: 0 = Off, 1 = On
        ///
        /// The attribute is of type bool.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <param name="minInterval">Minimum reporting period</param>
        /// <param name="maxInterval">Maximum reporting period</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetOnOffReporting(ushort minInterval, ushort maxInterval)
        {
            return SetReporting(_attributes[ATTR_ONOFF], minInterval, maxInterval);
        }


        /// <summary>
        /// Get the GlobalSceneControl attribute [attribute ID16384].
        ///
        /// In order to support the use case where the user gets back the last setting of the devices (e.g. level settings for lamps), a global scene is
        /// introduced which is stored when the devices are turned off and recalled when the devices are turned on. The global scene is defined as the
        /// scene that is stored with group identifier 0 and scene identifier 0.
        /// 
        /// The GlobalSceneControl attribute is defined in order to prevent a second off command storing the all-devices-off situation as a global
        /// scene, and to prevent a second on command destroying the current settings by going back to the global scene.
        /// 
        /// The GlobalSceneControl attribute SHALL be set to TRUE after the reception of a command which causes the OnOff attribute to be set to TRUE,
        /// such as a standard On command, a Move to level (with on/off) command, a Recall scene command or a On with recall global scene command.
        /// 
        /// The GlobalSceneControl attribute is set to FALSE after reception of a Off with effect command.
        ///
        /// The attribute is of type bool.
        ///
        /// The implementation of this attribute by a device is 
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetGlobalSceneControlAsync()
        {
            return Read(_attributes[ATTR_GLOBALSCENECONTROL]);
        }

        /// <summary>
        /// Synchronously Get the GlobalSceneControl attribute [attribute ID16384].
        ///
        /// In order to support the use case where the user gets back the last setting of the devices (e.g. level settings for lamps), a global scene is
        /// introduced which is stored when the devices are turned off and recalled when the devices are turned on. The global scene is defined as the
        /// scene that is stored with group identifier 0 and scene identifier 0.
        /// 
        /// The GlobalSceneControl attribute is defined in order to prevent a second off command storing the all-devices-off situation as a global
        /// scene, and to prevent a second on command destroying the current settings by going back to the global scene.
        /// 
        /// The GlobalSceneControl attribute SHALL be set to TRUE after the reception of a command which causes the OnOff attribute to be set to TRUE,
        /// such as a standard On command, a Move to level (with on/off) command, a Recall scene command or a On with recall global scene command.
        /// 
        /// The GlobalSceneControl attribute is set to FALSE after reception of a Off with effect command.
        ///
        /// The attribute is of type bool.
        ///
        /// The implementation of this attribute by a device is 
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public bool GetGlobalSceneControl(long refreshPeriod)
        {
            if (_attributes[ATTR_GLOBALSCENECONTROL].IsLastValueCurrent(refreshPeriod))
            {
                return (bool)_attributes[ATTR_GLOBALSCENECONTROL].LastValue;
            }

            return (bool)ReadSync(_attributes[ATTR_GLOBALSCENECONTROL]);
        }


        /// <summary>
        /// Set the OffTime attribute [attribute ID16385].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is 
        ///
        /// <param name="offTime">The ushort attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetOffTime(object value)
        {
            return Write(_attributes[ATTR_OFFTIME], value);
        }


        /// <summary>
        /// Get the OffTime attribute [attribute ID16385].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is 
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetOffTimeAsync()
        {
            return Read(_attributes[ATTR_OFFTIME]);
        }

        /// <summary>
        /// Synchronously Get the OffTime attribute [attribute ID16385].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is 
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetOffTime(long refreshPeriod)
        {
            if (_attributes[ATTR_OFFTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_OFFTIME].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_OFFTIME]);
        }


        /// <summary>
        /// Set the OffWaitTime attribute [attribute ID16386].
        ///
        /// The OffWaitTime attribute specifies the length of time (in 1/10ths second) that the “off” state SHALL be guarded to prevent an on command
        /// turning the device back to its “on” state (e.g., when leaving a room, the lights are turned off but an occupancy sensor detects the leaving
        /// person and attempts to turn the lights back on). If this attribute is set to 0x0000, the device SHALL remain in its current state.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is 
        ///
        /// <param name="offWaitTime">The ushort attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetOffWaitTime(object value)
        {
            return Write(_attributes[ATTR_OFFWAITTIME], value);
        }


        /// <summary>
        /// Get the OffWaitTime attribute [attribute ID16386].
        ///
        /// The OffWaitTime attribute specifies the length of time (in 1/10ths second) that the “off” state SHALL be guarded to prevent an on command
        /// turning the device back to its “on” state (e.g., when leaving a room, the lights are turned off but an occupancy sensor detects the leaving
        /// person and attempts to turn the lights back on). If this attribute is set to 0x0000, the device SHALL remain in its current state.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is 
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetOffWaitTimeAsync()
        {
            return Read(_attributes[ATTR_OFFWAITTIME]);
        }

        /// <summary>
        /// Synchronously Get the OffWaitTime attribute [attribute ID16386].
        ///
        /// The OffWaitTime attribute specifies the length of time (in 1/10ths second) that the “off” state SHALL be guarded to prevent an on command
        /// turning the device back to its “on” state (e.g., when leaving a room, the lights are turned off but an occupancy sensor detects the leaving
        /// person and attempts to turn the lights back on). If this attribute is set to 0x0000, the device SHALL remain in its current state.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is 
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetOffWaitTime(long refreshPeriod)
        {
            if (_attributes[ATTR_OFFWAITTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_OFFWAITTIME].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_OFFWAITTIME]);
        }


        /// <summary>
        /// The Off Command
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> OffCommand()
        {
            OffCommand command = new OffCommand();

            return Send(command);
        }

        /// <summary>
        /// The On Command
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> OnCommand()
        {
            OnCommand command = new OnCommand();

            return Send(command);
        }

        /// <summary>
        /// The Toggle Command
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ToggleCommand()
        {
            ToggleCommand command = new ToggleCommand();

            return Send(command);
        }

        /// <summary>
        /// The Off With Effect Command
        ///
        /// The Off With Effect command allows devices to be turned off using enhanced ways of fading.
        ///
        /// <param name="effectIdentifier"><see cref="byte"/> Effect Identifier</param>
        /// <param name="effectVariant"><see cref="byte"/> Effect Variant</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
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
        /// The On With Recall Global Scene command allows the recall of the settings when the device was turned off.
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> OnWithRecallGlobalSceneCommand()
        {
            OnWithRecallGlobalSceneCommand command = new OnWithRecallGlobalSceneCommand();

            return Send(command);
        }

        /// <summary>
        /// The On With Timed Off Command
        ///
        /// The On With Timed Off command allows devices to be turned on for a specific duration
        /// with a guarded off duration so that SHOULD the device be subsequently switched off,
        /// further On With Timed Off commands, received during this time, are prevented from
        /// turning the devices back on. Note that the device can be periodically re-kicked by
        /// subsequent On With Timed Off commands, e.g., from an on/off sensor.
        ///
        /// <param name="onOffControl"><see cref="byte"/> On Off Control</param>
        /// <param name="onTime"><see cref="ushort"/> On Time</param>
        /// <param name="offWaitTime"><see cref="ushort"/> Off Wait Time</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
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

        public override ZclCommand GetCommandFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // OFF_COMMAND
                    return new OffCommand();
                case 1: // ON_COMMAND
                    return new OnCommand();
                case 2: // TOGGLE_COMMAND
                    return new ToggleCommand();
                case 64: // OFF_WITH_EFFECT_COMMAND
                    return new OffWithEffectCommand();
                case 65: // ON_WITH_RECALL_GLOBAL_SCENE_COMMAND
                    return new OnWithRecallGlobalSceneCommand();
                case 66: // ON_WITH_TIMED_OFF_COMMAND
                    return new OnWithTimedOffCommand();
                    default:
                        return null;
            }
        }
    }
}
