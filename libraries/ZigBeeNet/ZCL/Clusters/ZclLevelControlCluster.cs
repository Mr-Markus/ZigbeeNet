
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.LevelControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Level Control cluster implementation (Cluster ID 0x0008).
    ///
    /// This cluster provides an interface for controlling a characteristic of a device that
    /// can be set to a level, for example the brightness of a light, the degree of closure of a
    /// door, or the power output of a heater.
    /// For many applications, a close relationship between this cluster and the OnOff cluster
    /// is needed. This section describes the dependencies that are required when an endpoint
    /// that implements the Level Control server cluster also implements the On/Off server
    /// cluster.
    /// The OnOff attribute of the On/Off cluster and the CurrentLevel attribute of the Level
    /// Control cluster are intrinsically independent variables, as they are on different
    /// clusters. However, when both clusters are implemented on the same endpoint,
    /// dependencies may be introduced between them. Facilities are provided to introduce
    /// dependencies if required.
    /// There are two sets of commands provided in the Level Control cluster. These are
    /// identical, except that the first set (Move to Level, Move and Step) shall NOT affect the
    /// OnOff attribute, whereas the second set ('with On/Off' variants) SHALL.
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

        // Attribute constants

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
        ///         /// </summary>
        public const ushort ATTR_OPTIONS = 0x000F;

        /// <summary>
        /// The OnOffTransitionTime attribute represents the time taken to move to or from the
        /// target level when On of Off commands are received by an On/Off cluster on the same
        /// endpoint. It is specified in 1/10ths of a second.
        /// The actual time taken should be as close to OnOffTransitionTime as the device is
        /// able. N.B. If the device is not able to move at a variable rate, the
        /// OnOffTransitionTime attribute should not be implemented.
        /// </summary>
        public const ushort ATTR_ONOFFTRANSITIONTIME = 0x0010;

        /// <summary>
        /// The OnLevel attribute determines the value that the CurrentLevel attribute is set
        /// to when the OnOff attribute of an On/Off cluster on the same endpoint is set to On. If
        /// the OnLevel attribute is not implemented, or is set to 0xff, it has no effect.
        /// </summary>
        public const ushort ATTR_ONLEVEL = 0x0011;

        /// <summary>
        /// The OnTransitionTime attribute represents the time taken to move the current
        /// level from the minimum level to the maximum level when an On command is received by an
        /// On/Off cluster on the same endpoint. It is specified in 10ths of a second. If this
        /// command is not implemented, or contains a value of 0xffff, the
        /// OnOffTransitionTime will be used instead.
        /// </summary>
        public const ushort ATTR_ONTRANSITIONTIME = 0x0012;

        /// <summary>
        /// The OffTransitionTime attribute represents the time taken to move the current
        /// level from the maximum level to the minimum level when an Off command is received by
        /// an On/Off cluster on the same endpoint. It is specified in 10ths of a second. If this
        /// command is not implemented, or contains a value of 0xffff, the
        /// OnOffTransitionTime will be used instead.
        /// </summary>
        public const ushort ATTR_OFFTRANSITIONTIME = 0x0013;

        /// <summary>
        /// The DefaultMoveRate attribute determines the movement rate, in units per second,
        /// when a Move command is received with a Rate parameter of 0xFF.
        /// </summary>
        public const ushort ATTR_DEFAULTMOVERATE = 0x0014;

        /// <summary>
        ///         /// </summary>
        public const ushort ATTR_STARTUPCURRENTLEVEL = 0x4000;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(9);

            attributeMap.Add(ATTR_CURRENTLEVEL, new ZclAttribute(this, ATTR_CURRENTLEVEL, "Current Level", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_REMAININGTIME, new ZclAttribute(this, ATTR_REMAININGTIME, "Remaining Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_OPTIONS, new ZclAttribute(this, ATTR_OPTIONS, "Options", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_ONOFFTRANSITIONTIME, new ZclAttribute(this, ATTR_ONOFFTRANSITIONTIME, "On Off Transition Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_ONLEVEL, new ZclAttribute(this, ATTR_ONLEVEL, "On Level", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_ONTRANSITIONTIME, new ZclAttribute(this, ATTR_ONTRANSITIONTIME, "On Transition Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_OFFTRANSITIONTIME, new ZclAttribute(this, ATTR_OFFTRANSITIONTIME, "Off Transition Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_DEFAULTMOVERATE, new ZclAttribute(this, ATTR_DEFAULTMOVERATE, "Default Move Rate", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_STARTUPCURRENTLEVEL, new ZclAttribute(this, ATTR_STARTUPCURRENTLEVEL, "Start Up Current Level", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(8);

            commandMap.Add(0x0000, () => new MoveToLevelCommand());
            commandMap.Add(0x0001, () => new MoveCommand());
            commandMap.Add(0x0002, () => new StepCommand());
            commandMap.Add(0x0003, () => new StopCommand());
            commandMap.Add(0x0004, () => new MoveToLevelWithOnOffCommand());
            commandMap.Add(0x0005, () => new MoveWithOnOffCommand());
            commandMap.Add(0x0006, () => new StepWithOnOffCommand());
            commandMap.Add(0x0007, () => new Stop2Command());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Level Control cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclLevelControlCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Move To Level Command
        ///
        /// On receipt of this command, a device shall move from its current level to the value
        /// given in the Level field. The meaning of ‘level’ is device dependent –e.g., for a
        /// light it may mean brightness level.The movement shall be as continuous as
        /// technically practical, i.e., not a step function, and the time taken to move to the
        /// new level shall be equal to the value of the Transition time field, in tenths of a
        /// second, or as close to this as the device is able.If the Transition time field takes
        /// the value 0xffff then the time taken to move to the new level shall instead be
        /// determined by the OnOffTransitionTimeattribute. If OnOffTransitionTime,
        /// which is an optional attribute, is not present, the device shall move to its new
        /// level as fast as it is able.
        ///
        /// <param name="level" <see cref="byte"> Level</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
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
        /// <param name="moveMode" <see cref="byte"> Move Mode</ param >
        /// <param name="rate" <see cref="byte"> Rate</ param >
        /// <returns> the command result Task </returns>
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
        /// <param name="stepMode" <see cref="byte"> Step Mode</ param >
        /// <param name="stepSize" <see cref="byte"> Step Size</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
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
        /// Upon receipt of this command, any Move to Level, Move or Step command (and their
        /// 'with On/Off' variants) currently in process shall be terminated. The value of
        /// CurrentLevel shall be left at its value upon receipt of the Stop command, and
        /// RemainingTime shall be set to zero.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> StopCommand()
        {
            return Send(new StopCommand());
        }

        /// <summary>
        /// The Move To Level (with On/Off) Command
        ///
        /// On receipt of this command, a device shall move from its current level to the value
        /// given in the Level field. The meaning of ‘level’ is device dependent –e.g., for a
        /// light it may mean brightness level.The movement shall be as continuous as
        /// technically practical, i.e., not a step function, and the time taken to move to the
        /// new level shall be equal to the value of the Transition time field, in tenths of a
        /// second, or as close to this as the device is able.If the Transition time field takes
        /// the value 0xffff then the time taken to move to the new level shall instead be
        /// determined by the OnOffTransitionTimeattribute. If OnOffTransitionTime,
        /// which is an optional attribute, is not present, the device shall move to its new
        /// level as fast as it is able.
        ///
        /// <param name="level" <see cref="byte"> Level</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
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
        /// <param name="moveMode" <see cref="byte"> Move Mode</ param >
        /// <param name="rate" <see cref="byte"> Rate</ param >
        /// <returns> the command result Task </returns>
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
        /// <param name="stepMode" <see cref="byte"> Step Mode</ param >
        /// <param name="stepSize" <see cref="byte"> Step Size</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
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
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> Stop2Command()
        {
            return Send(new Stop2Command());
        }
    }
}
