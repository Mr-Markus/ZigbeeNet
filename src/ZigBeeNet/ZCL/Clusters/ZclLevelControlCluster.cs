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
using ZigBeeNet.ZCL.Clusters.LevelControl;

namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Level Controlcluster implementation (Cluster ID 0x0008).
    ///
    /// This cluster provides an interface for controlling a characteristic of a device that
    /// can be set to a level, for example the brightness of a light, the degree of closure of
    /// a door, or the power output of a heater.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclLevelControlCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0008;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Level Control";

        /* Attribute constants */

        /// <summary>
        /// The CurrentLevel attribute represents the current level of this device. The
        /// meaning of 'level' is device dependent. Value is between 0 and 254.
        /// </summary>
        public const ushort ATTR_CURRENTLEVEL = 0x0000;

        /// <summary>
        /// The RemainingTime attribute represents the time remaining until the current
        /// command is complete - it is specified in 1/10ths of a second.
        /// </summary>
        public const ushort ATTR_REMAININGTIME = 0x0001;

        /// <summary>
        /// The OnOffTransitionTime attribute represents the time taken to move to or from
        /// the target level when On of Off commands are received by an On/Off cluster on
        /// the same endpoint. It is specified in 1/10ths of a second.
        /// 
        /// The actual time taken should be as close to OnOffTransitionTime as the device is
        /// able. N.B. If the device is not able to move at a variable rate, the
        /// OnOffTransitionTime attribute should not be implemented.
        /// </summary>
        public const ushort ATTR_ONOFFTRANSITIONTIME = 0x0010;

        /// <summary>
        /// The OnLevel attribute determines the value that the CurrentLevel attribute is set to
        /// when the OnOff attribute of an On/Off cluster on the same endpoint is set to On. If
        /// the OnLevel attribute is not implemented, or is set to 0xff, it has no effect.
        /// </summary>
        public const ushort ATTR_ONLEVEL = 0x0011;

        /// <summary>
        /// The OnTransitionTime attribute represents the time taken to move the current level from the
        /// minimum level to the maximum level when an On command is received by an On/Off cluster on
        /// the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
        /// or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
        /// </summary>
        public const ushort ATTR_ONTRANSITIONTIME = 0x0012;

        /// <summary>
        /// The OffTransitionTime attribute represents the time taken to move the current level from the
        /// maximum level to the minimum level when an Off command is received by an On/Off cluster on
        /// the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
        /// or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
        /// </summary>
        public const ushort ATTR_OFFTRANSITIONTIME = 0x0013;

        /// <summary>
        /// The DefaultMoveRate attribute determines the movement rate, in units per second, when a Move
        /// command is received with a Rate parameter of 0xFF.
        /// </summary>
        public const ushort ATTR_DEFAULTMOVERATE = 0x0014;


        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(7);

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

        /// <summary>
        /// Default constructor to create a Level Control cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclLevelControlCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// Get the CurrentLevel attribute [attribute ID0].
        ///
        /// The CurrentLevel attribute represents the current level of this device. The
        /// meaning of 'level' is device dependent. Value is between 0 and 254.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetCurrentLevelAsync()
        {
            return Read(_attributes[ATTR_CURRENTLEVEL]);
        }

        /// <summary>
        /// Synchronously Get the CurrentLevel attribute [attribute ID0].
        ///
        /// The CurrentLevel attribute represents the current level of this device. The
        /// meaning of 'level' is device dependent. Value is between 0 and 254.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetCurrentLevel(long refreshPeriod)
        {
            if (_attributes[ATTR_CURRENTLEVEL].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_CURRENTLEVEL].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_CURRENTLEVEL]);
        }


        /// <summary>
        /// Set reporting for the CurrentLevel attribute [attribute ID0].
        ///
        /// The CurrentLevel attribute represents the current level of this device. The
        /// meaning of 'level' is device dependent. Value is between 0 and 254.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <param name="minInterval">Minimum reporting period</param>
        /// <param name="maxInterval">Maximum reporting period</param>
        /// <param name="reportableChange">Object delta required to trigger report</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetCurrentLevelReporting(ushort minInterval, ushort maxInterval, object reportableChange)
        {
            return SetReporting(_attributes[ATTR_CURRENTLEVEL], minInterval, maxInterval, reportableChange);
        }


        /// <summary>
        /// Get the RemainingTime attribute [attribute ID1].
        ///
        /// The RemainingTime attribute represents the time remaining until the current
        /// command is complete - it is specified in 1/10ths of a second.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetRemainingTimeAsync()
        {
            return Read(_attributes[ATTR_REMAININGTIME]);
        }

        /// <summary>
        /// Synchronously Get the RemainingTime attribute [attribute ID1].
        ///
        /// The RemainingTime attribute represents the time remaining until the current
        /// command is complete - it is specified in 1/10ths of a second.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetRemainingTime(long refreshPeriod)
        {
            if (_attributes[ATTR_REMAININGTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_REMAININGTIME].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_REMAININGTIME]);
        }


        /// <summary>
        /// Set the OnOffTransitionTime attribute [attribute ID16].
        ///
        /// The OnOffTransitionTime attribute represents the time taken to move to or from
        /// the target level when On of Off commands are received by an On/Off cluster on
        /// the same endpoint. It is specified in 1/10ths of a second.
        /// 
        /// The actual time taken should be as close to OnOffTransitionTime as the device is
        /// able. N.B. If the device is not able to move at a variable rate, the
        /// OnOffTransitionTime attribute should not be implemented.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="onOffTransitionTime">The ushort attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetOnOffTransitionTime(object value)
        {
            return Write(_attributes[ATTR_ONOFFTRANSITIONTIME], value);
        }


        /// <summary>
        /// Get the OnOffTransitionTime attribute [attribute ID16].
        ///
        /// The OnOffTransitionTime attribute represents the time taken to move to or from
        /// the target level when On of Off commands are received by an On/Off cluster on
        /// the same endpoint. It is specified in 1/10ths of a second.
        /// 
        /// The actual time taken should be as close to OnOffTransitionTime as the device is
        /// able. N.B. If the device is not able to move at a variable rate, the
        /// OnOffTransitionTime attribute should not be implemented.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetOnOffTransitionTimeAsync()
        {
            return Read(_attributes[ATTR_ONOFFTRANSITIONTIME]);
        }

        /// <summary>
        /// Synchronously Get the OnOffTransitionTime attribute [attribute ID16].
        ///
        /// The OnOffTransitionTime attribute represents the time taken to move to or from
        /// the target level when On of Off commands are received by an On/Off cluster on
        /// the same endpoint. It is specified in 1/10ths of a second.
        /// 
        /// The actual time taken should be as close to OnOffTransitionTime as the device is
        /// able. N.B. If the device is not able to move at a variable rate, the
        /// OnOffTransitionTime attribute should not be implemented.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetOnOffTransitionTime(long refreshPeriod)
        {
            if (_attributes[ATTR_ONOFFTRANSITIONTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ONOFFTRANSITIONTIME].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ONOFFTRANSITIONTIME]);
        }


        /// <summary>
        /// Set the OnLevel attribute [attribute ID17].
        ///
        /// The OnLevel attribute determines the value that the CurrentLevel attribute is set to
        /// when the OnOff attribute of an On/Off cluster on the same endpoint is set to On. If
        /// the OnLevel attribute is not implemented, or is set to 0xff, it has no effect.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="onLevel">The byte attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetOnLevel(object value)
        {
            return Write(_attributes[ATTR_ONLEVEL], value);
        }


        /// <summary>
        /// Get the OnLevel attribute [attribute ID17].
        ///
        /// The OnLevel attribute determines the value that the CurrentLevel attribute is set to
        /// when the OnOff attribute of an On/Off cluster on the same endpoint is set to On. If
        /// the OnLevel attribute is not implemented, or is set to 0xff, it has no effect.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetOnLevelAsync()
        {
            return Read(_attributes[ATTR_ONLEVEL]);
        }

        /// <summary>
        /// Synchronously Get the OnLevel attribute [attribute ID17].
        ///
        /// The OnLevel attribute determines the value that the CurrentLevel attribute is set to
        /// when the OnOff attribute of an On/Off cluster on the same endpoint is set to On. If
        /// the OnLevel attribute is not implemented, or is set to 0xff, it has no effect.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetOnLevel(long refreshPeriod)
        {
            if (_attributes[ATTR_ONLEVEL].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_ONLEVEL].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_ONLEVEL]);
        }


        /// <summary>
        /// Set the OnTransitionTime attribute [attribute ID18].
        ///
        /// The OnTransitionTime attribute represents the time taken to move the current level from the
        /// minimum level to the maximum level when an On command is received by an On/Off cluster on
        /// the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
        /// or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="onTransitionTime">The ushort attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetOnTransitionTime(object value)
        {
            return Write(_attributes[ATTR_ONTRANSITIONTIME], value);
        }


        /// <summary>
        /// Get the OnTransitionTime attribute [attribute ID18].
        ///
        /// The OnTransitionTime attribute represents the time taken to move the current level from the
        /// minimum level to the maximum level when an On command is received by an On/Off cluster on
        /// the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
        /// or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetOnTransitionTimeAsync()
        {
            return Read(_attributes[ATTR_ONTRANSITIONTIME]);
        }

        /// <summary>
        /// Synchronously Get the OnTransitionTime attribute [attribute ID18].
        ///
        /// The OnTransitionTime attribute represents the time taken to move the current level from the
        /// minimum level to the maximum level when an On command is received by an On/Off cluster on
        /// the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
        /// or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetOnTransitionTime(long refreshPeriod)
        {
            if (_attributes[ATTR_ONTRANSITIONTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ONTRANSITIONTIME].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ONTRANSITIONTIME]);
        }


        /// <summary>
        /// Set the OffTransitionTime attribute [attribute ID19].
        ///
        /// The OffTransitionTime attribute represents the time taken to move the current level from the
        /// maximum level to the minimum level when an Off command is received by an On/Off cluster on
        /// the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
        /// or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="offTransitionTime">The ushort attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetOffTransitionTime(object value)
        {
            return Write(_attributes[ATTR_OFFTRANSITIONTIME], value);
        }


        /// <summary>
        /// Get the OffTransitionTime attribute [attribute ID19].
        ///
        /// The OffTransitionTime attribute represents the time taken to move the current level from the
        /// maximum level to the minimum level when an Off command is received by an On/Off cluster on
        /// the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
        /// or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetOffTransitionTimeAsync()
        {
            return Read(_attributes[ATTR_OFFTRANSITIONTIME]);
        }

        /// <summary>
        /// Synchronously Get the OffTransitionTime attribute [attribute ID19].
        ///
        /// The OffTransitionTime attribute represents the time taken to move the current level from the
        /// maximum level to the minimum level when an Off command is received by an On/Off cluster on
        /// the same endpoint.  It is specified in 10ths of a second.  If this command is not implemented,
        /// or contains a value of 0xffff, the OnOffTransitionTime will be used instead.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetOffTransitionTime(long refreshPeriod)
        {
            if (_attributes[ATTR_OFFTRANSITIONTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_OFFTRANSITIONTIME].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_OFFTRANSITIONTIME]);
        }


        /// <summary>
        /// Set the DefaultMoveRate attribute [attribute ID20].
        ///
        /// The DefaultMoveRate attribute determines the movement rate, in units per second, when a Move
        /// command is received with a Rate parameter of 0xFF.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="defaultMoveRate">The ushort attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetDefaultMoveRate(object value)
        {
            return Write(_attributes[ATTR_DEFAULTMOVERATE], value);
        }


        /// <summary>
        /// Get the DefaultMoveRate attribute [attribute ID20].
        ///
        /// The DefaultMoveRate attribute determines the movement rate, in units per second, when a Move
        /// command is received with a Rate parameter of 0xFF.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetDefaultMoveRateAsync()
        {
            return Read(_attributes[ATTR_DEFAULTMOVERATE]);
        }

        /// <summary>
        /// Synchronously Get the DefaultMoveRate attribute [attribute ID20].
        ///
        /// The DefaultMoveRate attribute determines the movement rate, in units per second, when a Move
        /// command is received with a Rate parameter of 0xFF.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetDefaultMoveRate(long refreshPeriod)
        {
            if (_attributes[ATTR_DEFAULTMOVERATE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_DEFAULTMOVERATE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_DEFAULTMOVERATE]);
        }


        /// <summary>
        /// The Move to Level Command
        ///
        /// On receipt of this command, a device SHALL move from its current level to the
        /// value given in the Level field. The meaning of ‘level’ is device dependent –e.g.,
        /// for a light it MAY mean brightness level.The movement SHALL be as continuous as
        /// technically practical, i.e., not a step function, and the time taken to move to
        /// the new level SHALL be equal to the value of the Transition time field, in tenths
        /// of a second, or as close to this as the device is able.If the Transition time field
        /// takes the value 0xffff then the time taken to move to the new level SHALL instead
        /// be determined by the OnOffTransitionTimeattribute. If OnOffTransitionTime, which is
        /// an optional attribute, is not present, the device SHALL move to its new level as fast
        /// as it is able.
        ///
        /// <param name="level"><see cref="byte"/> Level</param>
        /// <param name="transitionTime"><see cref="ushort"/> Transition time</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> MoveToLevelCommand(byte level, ushort transitionTime)
        {
            MoveToLevelCommand command = new MoveToLevelCommand();

            // Set the fields
            command.Level = level;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Move Command
        ///
        /// <param name="moveMode"><see cref="byte"/> Move mode</param>
        /// <param name="rate"><see cref="byte"/> Rate</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> MoveCommand(byte moveMode, byte rate)
        {
            MoveCommand command = new MoveCommand();

            // Set the fields
            command.MoveMode = moveMode;
            command.Rate = rate;

            return Send(command);
        }

        /// <summary>
        /// The Step Command
        ///
        /// <param name="stepMode"><see cref="byte"/> Step mode</param>
        /// <param name="stepSize"><see cref="byte"/> Step size</param>
        /// <param name="transitionTime"><see cref="ushort"/> Transition time</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> StepCommand(byte stepMode, byte stepSize, ushort transitionTime)
        {
            StepCommand command = new StepCommand();

            // Set the fields
            command.StepMode = stepMode;
            command.StepSize = stepSize;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Stop Command
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> StopCommand()
        {
            StopCommand command = new StopCommand();

            return Send(command);
        }

        /// <summary>
        /// The Move to Level (with On/Off) Command
        ///
        /// <param name="level"><see cref="byte"/> Level</param>
        /// <param name="transitionTime"><see cref="ushort"/> Transition time</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> MoveToLevelWithOnOffCommand(byte level, ushort transitionTime)
        {
            MoveToLevelWithOnOffCommand command = new MoveToLevelWithOnOffCommand();

            // Set the fields
            command.Level = level;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Move (with On/Off) Command
        ///
        /// <param name="moveMode"><see cref="byte"/> Move mode</param>
        /// <param name="rate"><see cref="byte"/> Rate</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> MoveWithOnOffCommand(byte moveMode, byte rate)
        {
            MoveWithOnOffCommand command = new MoveWithOnOffCommand();

            // Set the fields
            command.MoveMode = moveMode;
            command.Rate = rate;

            return Send(command);
        }

        /// <summary>
        /// The Step (with On/Off) Command
        ///
        /// <param name="stepMode"><see cref="byte"/> Step mode</param>
        /// <param name="stepSize"><see cref="byte"/> Step size</param>
        /// <param name="transitionTime"><see cref="ushort"/> Transition time</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> StepWithOnOffCommand(byte stepMode, byte stepSize, ushort transitionTime)
        {
            StepWithOnOffCommand command = new StepWithOnOffCommand();

            // Set the fields
            command.StepMode = stepMode;
            command.StepSize = stepSize;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Stop 2 Command
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> Stop2Command()
        {
            Stop2Command command = new Stop2Command();

            return Send(command);
        }

        public override ZclCommand GetCommandFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // MOVE_TO_LEVEL_COMMAND
                    return new MoveToLevelCommand();
                case 1: // MOVE_COMMAND
                    return new MoveCommand();
                case 2: // STEP_COMMAND
                    return new StepCommand();
                case 3: // STOP_COMMAND
                    return new StopCommand();
                case 4: // MOVE_TO_LEVEL__WITH_ON_OFF__COMMAND
                    return new MoveToLevelWithOnOffCommand();
                case 5: // MOVE__WITH_ON_OFF__COMMAND
                    return new MoveWithOnOffCommand();
                case 6: // STEP__WITH_ON_OFF__COMMAND
                    return new StepWithOnOffCommand();
                case 7: // STOP_2_COMMAND
                    return new Stop2Command();
                    default:
                        return null;
            }
        }
    }
}
