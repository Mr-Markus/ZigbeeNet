using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.ZCL.Clusters.LevelControl;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters
{
    public class ZclLevelControlCluster : ZclCluster
    {
        /**
         * The ZigBee Cluster Library Cluster ID
         */
        public const ushort CLUSTER_ID = 0x0008;

        /**
         * The ZigBee Cluster Library Cluster Name
         */
        public const string CLUSTER_NAME = "Level Control";

        // Attribute constants
        /**
         * The CurrentLevel attribute represents the current level of this device. The
         * meaning of 'level' is device dependent. Value is between 0 and 254.
         */
        public const ushort ATTR_CURRENTLEVEL = 0x0000;
        /**
         * The RemainingTime attribute represents the time remaining until the current
         * command is complete - it is specified in 1/10ths of a second.
         */
        public const ushort ATTR_REMAININGTIME = 0x0001;
        /**
         * The OnOffTransitionTime attribute represents the time taken to move to or from
         * the target level when On of Off commands are received by an On/Off cluster on
         * the same endpoint. It is specified in 1/10ths of a second.
         * <p>
         * The actual time taken should be as close to OnOffTransitionTime as the device is
         * able. N.B. If the device is not able to move at a variable rate, the
         * OnOffTransitionTime attribute should not be implemented.
         */
        public const ushort ATTR_ONOFFTRANSITIONTIME = 0x0010;
        /**
         * The OnLevel attribute determines the value that the CurrentLevel attribute is set to
         * when the OnOff attribute of an On/Off cluster on the same endpoint is set to On. If
         * the OnLevel attribute is not implemented, or is set to 0xff, it has no effect.
         */
        public const ushort ATTR_ONLEVEL = 0x0011;
        /**
         * The OnTransitionTime attribute represents the time taken to move the current level from the
         * minimum level to the maximum level when an On command is received by an On/Off cluster on
         * the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
         * or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
         */
        public const ushort ATTR_ONTRANSITIONTIME = 0x0012;
        /**
         * The OffTransitionTime attribute represents the time taken to move the current level from the
         * maximum level to the minimum level when an Off command is received by an On/Off cluster on
         * the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
         * or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
         */
        public const ushort ATTR_OFFTRANSITIONTIME = 0x0013;
        /**
         * The DefaultMoveRate attribute determines the movement rate, in units per second, when a Move
         * command is received with a Rate parameter of 0xFF.
         */
        public const ushort ATTR_DEFAULTMOVERATE = 0x0014;

        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>();

            ZclClusterType levelControl = ZclClusterType.GetValueById(ClusterType.LEVEL_CONTROL);

            attributeMap.Add(ATTR_CURRENTLEVEL, new ZclAttribute(levelControl, ATTR_CURRENTLEVEL, "CurrentLevel", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_REMAININGTIME, new ZclAttribute(levelControl, ATTR_REMAININGTIME, "RemainingTime", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ONOFFTRANSITIONTIME, new ZclAttribute(levelControl, ATTR_ONOFFTRANSITIONTIME, "OnOffTransitionTime", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_ONLEVEL, new ZclAttribute(levelControl, ATTR_ONLEVEL, "OnLevel", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_ONTRANSITIONTIME, new ZclAttribute(levelControl, ATTR_ONTRANSITIONTIME, "OnTransitionTime", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_OFFTRANSITIONTIME, new ZclAttribute(levelControl, ATTR_OFFTRANSITIONTIME, "OffTransitionTime", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_DEFAULTMOVERATE, new ZclAttribute(levelControl, ATTR_DEFAULTMOVERATE, "DefaultMoveRate", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));

            return attributeMap;
        }

        /**
         * Default constructor to create a Level Control cluster.
         *
         * @param zigbeeEndpoint the {@link ZigBeeEndpoint}
         */
        public ZclLevelControlCluster(ZigBeeEndpoint zigbeeEndpoint, ZigBeeNetworkManager zigBeeNetworkManager) : base(zigbeeEndpoint, zigBeeNetworkManager, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /**
         * Get the <i>CurrentLevel</i> attribute [attribute ID <b>0</b>].
         * <p>
         * The CurrentLevel attribute represents the current level of this device. The
         * meaning of 'level' is device dependent. Value is between 0 and 254.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetCurrentLevelAsync()
        {
            return Read(_attributes[ATTR_CURRENTLEVEL]);
        }

        /**
         * Synchronously get the <i>CurrentLevel</i> attribute [attribute ID <b>0</b>].
         * <p>
         * The CurrentLevel attribute represents the current level of this device. The
         * meaning of 'level' is device dependent. Value is between 0 and 254.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link int} attribute value, or null on error
         */
        public int GetCurrentLevel(long refreshPeriod)
        {
            if (_attributes[ATTR_CURRENTLEVEL].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_CURRENTLEVEL].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_CURRENTLEVEL]);
        }

        /**
         * Set reporting for the <i>CurrentLevel</i> attribute [attribute ID <b>0</b>].
         * <p>
         * The CurrentLevel attribute represents the current level of this device. The
         * meaning of 'level' is device dependent. Value is between 0 and 254.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param minInterval {@link int} minimum reporting period
         * @param maxInterval {@link int} maximum reporting period
         * @param reportableChange {@link object} delta required to trigger report
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> SetCurrentLevelReporting(int minInterval, int maxInterval, object reportableChange)
        {
            return SetReporting(_attributes[ATTR_CURRENTLEVEL], minInterval, maxInterval, reportableChange);
        }

        /**
         * Get the <i>RemainingTime</i> attribute [attribute ID <b>1</b>].
         * <p>
         * The RemainingTime attribute represents the time remaining until the current
         * command is complete - it is specified in 1/10ths of a second.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetRemainingTimeAsync()
        {
            return Read(_attributes[ATTR_REMAININGTIME]);
        }

        /**
         * Synchronously get the <i>RemainingTime</i> attribute [attribute ID <b>1</b>].
         * <p>
         * The RemainingTime attribute represents the time remaining until the current
         * command is complete - it is specified in 1/10ths of a second.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link int} attribute value, or null on error
         */
        public int GetRemainingTime(long refreshPeriod)
        {
            if (_attributes[ATTR_REMAININGTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_REMAININGTIME].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_REMAININGTIME]);
        }

        /**
         * Set the <i>OnOffTransitionTime</i> attribute [attribute ID <b>16</b>].
         * <p>
         * The OnOffTransitionTime attribute represents the time taken to move to or from
         * the target level when On of Off commands are received by an On/Off cluster on
         * the same endpoint. It is specified in 1/10ths of a second.
         * <p>
         * The actual time taken should be as close to OnOffTransitionTime as the device is
         * able. N.B. If the device is not able to move at a variable rate, the
         * OnOffTransitionTime attribute should not be implemented.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param onOffTransitionTime the {@link int} attribute value to be set
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> SetOnOffTransitionTime(object value)
        {
            return Write(_attributes[ATTR_ONOFFTRANSITIONTIME], value);
        }

        /**
         * Get the <i>OnOffTransitionTime</i> attribute [attribute ID <b>16</b>].
         * <p>
         * The OnOffTransitionTime attribute represents the time taken to move to or from
         * the target level when On of Off commands are received by an On/Off cluster on
         * the same endpoint. It is specified in 1/10ths of a second.
         * <p>
         * The actual time taken should be as close to OnOffTransitionTime as the device is
         * able. N.B. If the device is not able to move at a variable rate, the
         * OnOffTransitionTime attribute should not be implemented.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetOnOffTransitionTimeAsync()
        {
            return Read(_attributes[ATTR_ONOFFTRANSITIONTIME]);
        }

        /**
         * Synchronously get the <i>OnOffTransitionTime</i> attribute [attribute ID <b>16</b>].
         * <p>
         * The OnOffTransitionTime attribute represents the time taken to move to or from
         * the target level when On of Off commands are received by an On/Off cluster on
         * the same endpoint. It is specified in 1/10ths of a second.
         * <p>
         * The actual time taken should be as close to OnOffTransitionTime as the device is
         * able. N.B. If the device is not able to move at a variable rate, the
         * OnOffTransitionTime attribute should not be implemented.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link int} attribute value, or null on error
         */
        public int GetOnOffTransitionTime(long refreshPeriod)
        {
            if (_attributes[ATTR_ONOFFTRANSITIONTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_ONOFFTRANSITIONTIME].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_ONOFFTRANSITIONTIME]);
        }

        /**
         * Set the <i>OnLevel</i> attribute [attribute ID <b>17</b>].
         * <p>
         * The OnLevel attribute determines the value that the CurrentLevel attribute is set to
         * when the OnOff attribute of an On/Off cluster on the same endpoint is set to On. If
         * the OnLevel attribute is not implemented, or is set to 0xff, it has no effect.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param onLevel the {@link int} attribute value to be set
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> SetOnLevel(object value)
        {
            return Write(_attributes[ATTR_ONLEVEL], value);
        }

        /**
         * Get the <i>OnLevel</i> attribute [attribute ID <b>17</b>].
         * <p>
         * The OnLevel attribute determines the value that the CurrentLevel attribute is set to
         * when the OnOff attribute of an On/Off cluster on the same endpoint is set to On. If
         * the OnLevel attribute is not implemented, or is set to 0xff, it has no effect.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> getOnLevelAsync()
        {
            return Read(_attributes[ATTR_ONLEVEL]);
        }

        /**
         * Synchronously get the <i>OnLevel</i> attribute [attribute ID <b>17</b>].
         * <p>
         * The OnLevel attribute determines the value that the CurrentLevel attribute is set to
         * when the OnOff attribute of an On/Off cluster on the same endpoint is set to On. If
         * the OnLevel attribute is not implemented, or is set to 0xff, it has no effect.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link int} attribute value, or null on error
         */
        public int getOnLevel(long refreshPeriod)
        {
            if (_attributes[ATTR_ONLEVEL].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_ONLEVEL].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_ONLEVEL]);
        }

        /**
         * Set the <i>OnTransitionTime</i> attribute [attribute ID <b>18</b>].
         * <p>
         * The OnTransitionTime attribute represents the time taken to move the current level from the
         * minimum level to the maximum level when an On command is received by an On/Off cluster on
         * the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
         * or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param onTransitionTime the {@link int} attribute value to be set
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> setOnTransitionTime(object value)
        {
            return Write(_attributes[ATTR_ONTRANSITIONTIME], value);
        }

        /**
         * Get the <i>OnTransitionTime</i> attribute [attribute ID <b>18</b>].
         * <p>
         * The OnTransitionTime attribute represents the time taken to move the current level from the
         * minimum level to the maximum level when an On command is received by an On/Off cluster on
         * the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
         * or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetOnTransitionTimeAsync()
        {
            return Read(_attributes[ATTR_ONTRANSITIONTIME]);
        }

        /**
         * Synchronously get the <i>OnTransitionTime</i> attribute [attribute ID <b>18</b>].
         * <p>
         * The OnTransitionTime attribute represents the time taken to move the current level from the
         * minimum level to the maximum level when an On command is received by an On/Off cluster on
         * the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
         * or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link int} attribute value, or null on error
         */
        public int GetOnTransitionTime(long refreshPeriod)
        {
            if (_attributes[ATTR_ONTRANSITIONTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_ONTRANSITIONTIME].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_ONTRANSITIONTIME]);
        }

        /**
         * Set the <i>OffTransitionTime</i> attribute [attribute ID <b>19</b>].
         * <p>
         * The OffTransitionTime attribute represents the time taken to move the current level from the
         * maximum level to the minimum level when an Off command is received by an On/Off cluster on
         * the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
         * or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param offTransitionTime the {@link int} attribute value to be set
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> SetOffTransitionTime(object value)
        {
            return Write(_attributes[ATTR_OFFTRANSITIONTIME], value);
        }

        /**
         * Get the <i>OffTransitionTime</i> attribute [attribute ID <b>19</b>].
         * <p>
         * The OffTransitionTime attribute represents the time taken to move the current level from the
         * maximum level to the minimum level when an Off command is received by an On/Off cluster on
         * the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
         * or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetOffTransitionTimeAsync()
        {
            return Read(_attributes[ATTR_OFFTRANSITIONTIME]);
        }

        /**
         * Synchronously get the <i>OffTransitionTime</i> attribute [attribute ID <b>19</b>].
         * <p>
         * The OffTransitionTime attribute represents the time taken to move the current level from the
         * maximum level to the minimum level when an Off command is received by an On/Off cluster on
         * the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
         * or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link int} attribute value, or null on error
         */
        public int GetOffTransitionTime(long refreshPeriod)
        {
            if (_attributes[ATTR_OFFTRANSITIONTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_OFFTRANSITIONTIME].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_OFFTRANSITIONTIME]);
        }

        /**
         * Set the <i>DefaultMoveRate</i> attribute [attribute ID <b>20</b>].
         * <p>
         * The DefaultMoveRate attribute determines the movement rate, in units per second, when a Move
         * command is received with a Rate parameter of 0xFF.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param defaultMoveRate the {@link int} attribute value to be set
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> SetDefaultMoveRate(object value)
        {
            return Write(_attributes[ATTR_DEFAULTMOVERATE], value);
        }

        /**
         * Get the <i>DefaultMoveRate</i> attribute [attribute ID <b>20</b>].
         * <p>
         * The DefaultMoveRate attribute determines the movement rate, in units per second, when a Move
         * command is received with a Rate parameter of 0xFF.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetDefaultMoveRateAsync()
        {
            return Read(_attributes[ATTR_DEFAULTMOVERATE]);
        }

        /**
         * Synchronously get the <i>DefaultMoveRate</i> attribute [attribute ID <b>20</b>].
         * <p>
         * The DefaultMoveRate attribute determines the movement rate, in units per second, when a Move
         * command is received with a Rate parameter of 0xFF.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link int}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link int} attribute value, or null on error
         */
        public int GetDefaultMoveRate(long refreshPeriod)
        {
            if (_attributes[ATTR_DEFAULTMOVERATE].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_DEFAULTMOVERATE].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_DEFAULTMOVERATE]);
        }

        /**
         * The Move to Level Command
         * <p>
         * On receipt of this command, a device SHALL move from its current level to the
         * value given in the Level field. The meaning of ‘level’ is device dependent –e.g.,
         * for a light it MAY mean brightness level.The movement SHALL be as continuous as
         * technically practical, i.e., not a step function, and the time taken to move to
         * the new level SHALL be equal to the value of the Transition time field, in tenths
         * of a second, or as close to this as the device is able.If the Transition time field
         * takes the value 0xffff then the time taken to move to the new level SHALL instead
         * be determined by the OnOffTransitionTimeattribute. If OnOffTransitionTime, which is
         * an optional attribute, is not present, the device SHALL move to its new level as fast
         * as it is able.
         *
         * @param level {@link int} Level
         * @param transitionTime {@link int} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> MoveToLevelCommand(byte level, ushort transitionTime)
        {
            return Send(new MoveToLevelCommand(level, transitionTime));
        }

        /**
         * The Move Command
         *
         * @param moveMode {@link int} Move mode
         * @param rate {@link int} Rate
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> MoveCommand(byte moveMode, byte rate)
        {
            return Send(new MoveCommand(moveMode, rate));
        }

        /**
         * The Step Command
         *
         * @param stepMode {@link int} Step mode
         * @param stepSize {@link int} Step size
         * @param transitionTime {@link int} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        //public Task<CommandResult> stepCommand(int stepMode, int stepSize, int transitionTime)
        //{
        //    StepCommand command = new StepCommand();

        //    // Set the fields
        //    command.setStepMode(stepMode);
        //    command.setStepSize(stepSize);
        //    command.setTransitionTime(transitionTime);

        //    return Send(command);
        //}

        /**
         * The Stop Command
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        //public Task<CommandResult> stopCommand()
        //{
        //    StopCommand command = new StopCommand();

        //    return Send(command);
        //}

        /**
         * The Move to Level (with On/Off) Command
         *
         * @param level {@link int} Level
         * @param transitionTime {@link int} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> MoveToLevelWithOnOffCommand(byte level, ushort transitionTime)
        {
            return Send(new MoveToLevelWithOnOffCommand(level, transitionTime));
        }

        /**
         * The Move (with On/Off) Command
         *
         * @param moveMode {@link int} Move mode
         * @param rate {@link int} Rate
         * @return the {@link Task<CommandResult>} command result future
         */
        //public Task<CommandResult> moveWithOnOffCommand(int moveMode, int rate)
        //{
        //    MoveWithOnOffCommand command = new MoveWithOnOffCommand();

        //    // Set the fields
        //    command.setMoveMode(moveMode);
        //    command.setRate(rate);

        //    return Send(command);
        //}

        /**
         * The Step (with On/Off) Command
         *
         * @param stepMode {@link int} Step mode
         * @param stepSize {@link int} Step size
         * @param transitionTime {@link int} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        //public Task<CommandResult> stepWithOnOffCommand(int stepMode, int stepSize, int transitionTime)
        //{
        //    StepWithOnOffCommand command = new StepWithOnOffCommand();

        //    // Set the fields
        //    command.setStepMode(stepMode);
        //    command.setStepSize(stepSize);
        //    command.setTransitionTime(transitionTime);

        //    return Send(command);
        //}

        /**
         * The Stop 2 Command
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        //public Task<CommandResult> stop2Command()
        //{
        //    Stop2Command command = new Stop2Command();

        //    return Send(command);
        //}

        public override ZclCommand GetCommandFromId(int commandId)
        {
            switch (commandId)
            {
                //case 0: // MOVE_TO_LEVEL_COMMAND
                //    return new MoveToLevelCommand();
                case 1: // MOVE_COMMAND
                    return new MoveCommand();
                //case 2: // STEP_COMMAND
                //    return new StepCommand();
                //case 3: // STOP_COMMAND
                //    return new StopCommand();
                //case 4: // MOVE_TO_LEVEL__WITH_ON_OFF__COMMAND
                //    return new MoveToLevelWithOnOffCommand();
                //case 5: // MOVE__WITH_ON_OFF__COMMAND
                //    return new MoveWithOnOffCommand();
                //case 6: // STEP__WITH_ON_OFF__COMMAND
                //    return new StepWithOnOffCommand();
                //case 7: // STOP_2_COMMAND
                //    return new Stop2Command();
                default:
                    return null;
            }
        }
    }
}
