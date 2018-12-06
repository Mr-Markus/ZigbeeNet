﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters
{
    public class ZclOnOffCluster : ZclCluster
    {
        /**
     * The ZigBee Cluster Library Cluster ID
     */
        public const int CLUSTER_ID = 0x0006;

        /**
         * The ZigBee Cluster Library Cluster Name
         */
        public const String CLUSTER_NAME = "On/Off";

        // Attribute constants
        /**
         * The OnOff attribute has the following values: 0 = Off, 1 = On
         */
        public const int ATTR_ONOFF = 0x0000;
        /**
         * In order to support the use case where the user gets back the last setting of the devices (e.g. level settings for lamps), a global scene is
         * introduced which is stored when the devices are turned off and recalled when the devices are turned on. The global scene is defined as the
         * scene that is stored with group identifier 0 and scene identifier 0.
         * <p>
         * The GlobalSceneControl attribute is defined in order to prevent a second off command storing the all-devices-off situation as a global
         * scene, and to prevent a second on command destroying the current settings by going back to the global scene.
         * <p>
         * The GlobalSceneControl attribute SHALL be set to TRUE after the reception of a command which causes the OnOff attribute to be set to TRUE,
         * such as a standard On command, a Move to level (with on/off) command, a Recall scene command or a On with recall global scene command.
         * <p>
         * The GlobalSceneControl attribute is set to FALSE after reception of a Off with effect command.
         */
        public const int ATTR_GLOBALSCENECONTROL = 0x4000;
        /**
         */
        public const int ATTR_OFFTIME = 0x4001;
        /**
         * The OffWaitTime attribute specifies the length of time (in 1/10ths second) that the “off” state SHALL be guarded to prevent an on command
         * turning the device back to its “on” state (e.g., when leaving a room, the lights are turned off but an occupancy sensor detects the leaving
         * person and attempts to turn the lights back on). If this attribute is set to 0x0000, the device SHALL remain in its current state.
         */
        public const int ATTR_OFFWAITTIME = 0x4002;

        // Attribute initialisation
        protected ConcurrentDictionary<int, ZclAttribute> InitializeAttributes()
        {
            ConcurrentDictionary<int, ZclAttribute> attributeMap = new ConcurrentDictionary<int, ZclAttribute>();

            attributeMap.TryAdd(ATTR_ONOFF, new ZclAttribute(ZclClusterType.ON_OFF, ATTR_ONOFF, "OnOff", ZclDataType.BOOLEAN, true, true, false, true));
            attributeMap.TryAdd(ATTR_GLOBALSCENECONTROL, new ZclAttribute(ZclClusterType.ON_OFF, ATTR_GLOBALSCENECONTROL, "GlobalSceneControl", ZclDataType.BOOLEAN, false, true, false, false));
            attributeMap.TryAdd(ATTR_OFFTIME, new ZclAttribute(ZclClusterType.ON_OFF, ATTR_OFFTIME, "OffTime", ZclDataType.UNSIGNED_16_BIT_INTEGER, false, true, true, false));
            attributeMap.TryAdd(ATTR_OFFWAITTIME, new ZclAttribute(ZclClusterType.ON_OFF, ATTR_OFFWAITTIME, "OffWaitTime", ZclDataType.UNSIGNED_16_BIT_INTEGER, false, true, true, false));

            return attributeMap;
        }

        /**
         * Default constructor to create a On/Off cluster.
         *
         * @param zigbeeEndpoint the {@link ZigBeeEndpoint}
         */
        public ZclOnOffCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /**
         * Get the <i>OnOff</i> attribute [attribute ID <b>0</b>].
         * <p>
         * The OnOff attribute has the following values: 0 = Off, 1 = On
         * <p>
         * The attribute is of type {@link Boolean}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Future<CommandResult>} command result future
         */
        public Task<CommandResult> GetOnOffAsync()
        {
            return read(attributes.get(ATTR_ONOFF));
        }

        /**
         * Synchronously get the <i>OnOff</i> attribute [attribute ID <b>0</b>].
         * <p>
         * The OnOff attribute has the following values: 0 = Off, 1 = On
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Boolean}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Boolean} attribute value, or null on error
         */
        public bool GetOnOff(long refreshPeriod)
        {
            if (attributes.get(ATTR_ONOFF).isLastValueCurrent(refreshPeriod))
            {
                return (bool)attributes.get(ATTR_ONOFF).getLastValue();
            }

            return (Boolean)readSync(attributes.get(ATTR_ONOFF));
        }

        /**
         * Set reporting for the <i>OnOff</i> attribute [attribute ID <b>0</b>].
         * <p>
         * The OnOff attribute has the following values: 0 = Off, 1 = On
         * <p>
         * The attribute is of type {@link Boolean}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param minInterval {@link int} minimum reporting period
         * @param maxInterval {@link int} maximum reporting period
         * @return the {@link Future<CommandResult>} command result future
         */
        public Future<CommandResult> setOnOffReporting(int minInterval, int maxInterval)
        {
            return setReporting(attributes.get(ATTR_ONOFF), minInterval, maxInterval);
        }

        /**
         * Get the <i>GlobalSceneControl</i> attribute [attribute ID <b>16384</b>].
         * <p>
         * In order to support the use case where the user gets back the last setting of the devices (e.g. level settings for lamps), a global scene is
         * introduced which is stored when the devices are turned off and recalled when the devices are turned on. The global scene is defined as the
         * scene that is stored with group identifier 0 and scene identifier 0.
         * <p>
         * The GlobalSceneControl attribute is defined in order to prevent a second off command storing the all-devices-off situation as a global
         * scene, and to prevent a second on command destroying the current settings by going back to the global scene.
         * <p>
         * The GlobalSceneControl attribute SHALL be set to TRUE after the reception of a command which causes the OnOff attribute to be set to TRUE,
         * such as a standard On command, a Move to level (with on/off) command, a Recall scene command or a On with recall global scene command.
         * <p>
         * The GlobalSceneControl attribute is set to FALSE after reception of a Off with effect command.
         * <p>
         * The attribute is of type {@link Boolean}.
         * <p>
         * The implementation of this attribute by a device is 
         *
         * @return the {@link Future<CommandResult>} command result future
         */
        public Future<CommandResult> getGlobalSceneControlAsync()
        {
            return read(attributes.get(ATTR_GLOBALSCENECONTROL));
        }

        /**
         * Synchronously get the <i>GlobalSceneControl</i> attribute [attribute ID <b>16384</b>].
         * <p>
         * In order to support the use case where the user gets back the last setting of the devices (e.g. level settings for lamps), a global scene is
         * introduced which is stored when the devices are turned off and recalled when the devices are turned on. The global scene is defined as the
         * scene that is stored with group identifier 0 and scene identifier 0.
         * <p>
         * The GlobalSceneControl attribute is defined in order to prevent a second off command storing the all-devices-off situation as a global
         * scene, and to prevent a second on command destroying the current settings by going back to the global scene.
         * <p>
         * The GlobalSceneControl attribute SHALL be set to TRUE after the reception of a command which causes the OnOff attribute to be set to TRUE,
         * such as a standard On command, a Move to level (with on/off) command, a Recall scene command or a On with recall global scene command.
         * <p>
         * The GlobalSceneControl attribute is set to FALSE after reception of a Off with effect command.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Boolean}.
         * <p>
         * The implementation of this attribute by a device is 
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Boolean} attribute value, or null on error
         */
        public Boolean getGlobalSceneControl(long refreshPeriod)
        {
            if (attributes.get(ATTR_GLOBALSCENECONTROL).isLastValueCurrent(refreshPeriod))
            {
                return (Boolean)attributes.get(ATTR_GLOBALSCENECONTROL).getLastValue();
            }

            return (Boolean)readSync(attributes.get(ATTR_GLOBALSCENECONTROL));
        }

        /**
         * Set the <i>OffTime</i> attribute [attribute ID <b>16385</b>].
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is 
         *
         * @param offTime the {@link Integer} attribute value to be set
         * @return the {@link Future<CommandResult>} command result future
         */
        public Future<CommandResult> setOffTime(Object value)
        {
            return write(attributes.get(ATTR_OFFTIME), value);
        }

        /**
         * Get the <i>OffTime</i> attribute [attribute ID <b>16385</b>].
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is 
         *
         * @return the {@link Future<CommandResult>} command result future
         */
        public Future<CommandResult> getOffTimeAsync()
        {
            return read(attributes.get(ATTR_OFFTIME));
        }

        /**
         * Synchronously get the <i>OffTime</i> attribute [attribute ID <b>16385</b>].
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is 
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public Integer getOffTime(long refreshPeriod)
        {
            if (attributes.get(ATTR_OFFTIME).isLastValueCurrent(refreshPeriod))
            {
                return (Integer)attributes.get(ATTR_OFFTIME).getLastValue();
            }

            return (Integer)readSync(attributes.get(ATTR_OFFTIME));
        }

        /**
         * Set the <i>OffWaitTime</i> attribute [attribute ID <b>16386</b>].
         * <p>
         * The OffWaitTime attribute specifies the length of time (in 1/10ths second) that the “off” state SHALL be guarded to prevent an on command
         * turning the device back to its “on” state (e.g., when leaving a room, the lights are turned off but an occupancy sensor detects the leaving
         * person and attempts to turn the lights back on). If this attribute is set to 0x0000, the device SHALL remain in its current state.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is 
         *
         * @param offWaitTime the {@link Integer} attribute value to be set
         * @return the {@link Future<CommandResult>} command result future
         */
        public Future<CommandResult> setOffWaitTime(Object value)
        {
            return write(attributes.get(ATTR_OFFWAITTIME), value);
        }

        /**
         * Get the <i>OffWaitTime</i> attribute [attribute ID <b>16386</b>].
         * <p>
         * The OffWaitTime attribute specifies the length of time (in 1/10ths second) that the “off” state SHALL be guarded to prevent an on command
         * turning the device back to its “on” state (e.g., when leaving a room, the lights are turned off but an occupancy sensor detects the leaving
         * person and attempts to turn the lights back on). If this attribute is set to 0x0000, the device SHALL remain in its current state.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is 
         *
         * @return the {@link Future<CommandResult>} command result future
         */
        public Future<CommandResult> getOffWaitTimeAsync()
        {
            return read(attributes.get(ATTR_OFFWAITTIME));
        }

        /**
         * Synchronously get the <i>OffWaitTime</i> attribute [attribute ID <b>16386</b>].
         * <p>
         * The OffWaitTime attribute specifies the length of time (in 1/10ths second) that the “off” state SHALL be guarded to prevent an on command
         * turning the device back to its “on” state (e.g., when leaving a room, the lights are turned off but an occupancy sensor detects the leaving
         * person and attempts to turn the lights back on). If this attribute is set to 0x0000, the device SHALL remain in its current state.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is 
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public Integer getOffWaitTime(long refreshPeriod)
        {
            if (attributes.get(ATTR_OFFWAITTIME).isLastValueCurrent(refreshPeriod))
            {
                return (Integer)attributes.get(ATTR_OFFWAITTIME).getLastValue();
            }

            return (Integer)readSync(attributes.get(ATTR_OFFWAITTIME));
        }

        /**
         * The Off Command
         *
         * @return the {@link Future<CommandResult>} command result future
         */
        public Future<CommandResult> offCommand()
        {
            OffCommand command = new OffCommand();

            return send(command);
        }

        /**
         * The On Command
         *
         * @return the {@link Future<CommandResult>} command result future
         */
        public Future<CommandResult> onCommand()
        {
            OnCommand command = new OnCommand();

            return send(command);
        }

        /**
         * The Toggle Command
         *
         * @return the {@link Future<CommandResult>} command result future
         */
        public Future<CommandResult> toggleCommand()
        {
            ToggleCommand command = new ToggleCommand();

            return send(command);
        }

        /**
         * The Off With Effect Command
         * <p>
         * The Off With Effect command allows devices to be turned off using enhanced ways of fading.
         *
         * @param effectIdentifier {@link Integer} Effect Identifier
         * @param effectVariant {@link Integer} Effect Variant
         * @return the {@link Future<CommandResult>} command result future
         */
        public Future<CommandResult> offWithEffectCommand(Integer effectIdentifier, Integer effectVariant)
        {
            OffWithEffectCommand command = new OffWithEffectCommand();

            // Set the fields
            command.setEffectIdentifier(effectIdentifier);
            command.setEffectVariant(effectVariant);

            return send(command);
        }

        /**
         * The On With Recall Global Scene Command
         * <p>
         * The On With Recall Global Scene command allows the recall of the settings when the device was turned off.
         *
         * @return the {@link Future<CommandResult>} command result future
         */
        public Future<CommandResult> onWithRecallGlobalSceneCommand()
        {
            OnWithRecallGlobalSceneCommand command = new OnWithRecallGlobalSceneCommand();

            return send(command);
        }

        /**
         * The On With Timed Off Command
         * <p>
         * The On With Timed Off command allows devices to be turned on for a specific duration
         * with a guarded off duration so that SHOULD the device be subsequently switched off,
         * further On With Timed Off commands, received during this time, are prevented from
         * turning the devices back on. Note that the device can be periodically re-kicked by
         * subsequent On With Timed Off commands, e.g., from an on/off sensor.
         *
         * @param onOffControl {@link Integer} On Off Control
         * @param onTime {@link Integer} On Time
         * @param offWaitTime {@link Integer} Off Wait Time
         * @return the {@link Future<CommandResult>} command result future
         */
        public Future<CommandResult> onWithTimedOffCommand(Integer onOffControl, Integer onTime, Integer offWaitTime)
        {
            OnWithTimedOffCommand command = new OnWithTimedOffCommand();

            // Set the fields
            command.setOnOffControl(onOffControl);
            command.setOnTime(onTime);
            command.setOffWaitTime(offWaitTime);

            return send(command);
        }

        public ZclCommand getCommandFromId(int commandId)
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
