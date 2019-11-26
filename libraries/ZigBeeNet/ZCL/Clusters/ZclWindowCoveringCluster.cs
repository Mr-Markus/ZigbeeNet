
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.WindowCovering;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Window Covering cluster implementation (Cluster ID 0x0102).
    ///
    /// Provides an interface for controlling and adjusting automatic window coverings.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclWindowCoveringCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0102;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Window Covering";

        // Attribute constants

        /// <summary>
        /// The WindowCoveringType attribute identifies the type of window covering being
        /// controlled by this endpoint.
        /// </summary>
        public const ushort ATTR_WINDOWCOVERINGTYPE = 0x0000;

        /// <summary>
        /// The PhysicalClosedLimitLift attribute identifies the maximum possible encoder
        /// position possible (in centi- meters) to position the height of the window covering
        /// – this is ignored if the device is running in Open Loop Control.
        /// </summary>
        public const ushort ATTR_PHYSICALCLOSEDLIMITLIFT = 0x0001;

        /// <summary>
        /// The PhysicalClosedLimitTilt attribute identifies the maximum possible encoder
        /// position possible (tenth of a degrees) to position the angle of the window covering
        /// – this is ignored if the device is running in Open Loop Control.
        /// </summary>
        public const ushort ATTR_PHYSICALCLOSEDLIMITTILT = 0x0002;

        /// <summary>
        /// The CurrentPositionLift attribute identifies the actual position (in
        /// centimeters) of the window covering from the top of the shade if Closed Loop Control
        /// is enabled. This attribute is ignored if the device is running in Open Loop Control.
        /// </summary>
        public const ushort ATTR_CURRENTPOSITIONLIFT = 0x0003;

        /// <summary>
        /// The CurrentPositionTilt attribute identifies the actual tilt position (in tenth
        /// of an degree) of the window covering from Open if Closed Loop Control is enabled.
        /// This attribute is ignored if the device is running in Open Loop Control.
        /// </summary>
        public const ushort ATTR_CURRENTPOSITIONTILT = 0x0004;

        /// <summary>
        /// The NumberOfActuationsLift attribute identifies the total number of lift
        /// actuations applied to the Window Covering since the device was installed.
        /// </summary>
        public const ushort ATTR_NUMBEROFACTUATIONSLIFT = 0x0005;

        /// <summary>
        /// The NumberOfActuationsTilt attribute identifies the total number of tilt
        /// actuations applied to the Window Covering since the device was installed.
        /// </summary>
        public const ushort ATTR_NUMBEROFACTUATIONSTILT = 0x0006;

        /// <summary>
        /// The ConfigStatus attribute makes configuration and status information
        /// available. To change settings, devices shall write to the Mode attribute of the
        /// Window Covering Settings Attribute Set. The behavior causing the setting or
        /// clearing of each bit is vendor specific.
        /// </summary>
        public const ushort ATTR_CONFIGSTATUS = 0x0007;

        /// <summary>
        /// The CurrentPositionLiftPercentage attribute identifies the actual position as
        /// a percentage between the InstalledOpenLimitLift attribute and the
        /// InstalledClosedLimitLift58attribute of the window covering from the up/open
        /// position if Closed Loop Control is enabled. If the device is running in Open Loop
        /// Control or the device only supports Tilt actions, this attribute is not required as
        /// an attribute but has a special interpretation when received as part of a scene
        /// command.
        /// </summary>
        public const ushort ATTR_CURRENTPOSITIONLIFTPERCENTAGE = 0x0008;

        /// <summary>
        /// The CurrentPositionTiltPercentage attribute identifies the actual position as
        /// a percentage between the InstalledOpenLimitTilt attribute and the
        /// InstalledClosedLimitTilt59attribute of the window covering from the up/open
        /// position if Closed Loop Control is enabled. If the device is running in Open Loop
        /// Control or the device only support Lift actions, this attribute is not required as
        /// an attribute but has a special interpretation when received as part of a scene
        /// command.
        /// </summary>
        public const ushort ATTR_CURRENTPOSITIONTILTPERCENTAGE = 0x0009;

        /// <summary>
        /// The InstalledOpenLimitLift attribute identifies the Open Limit for Lifting the
        /// Window Covering whether position (in centimeters) is encoded or timed. This
        /// attribute is ignored if the device is running in Open Loop Control or only supports
        /// Tilt actions.
        /// </summary>
        public const ushort ATTR_INSTALLEDOPENLIMITLIFT = 0x0010;

        /// <summary>
        /// The InstalledClosedLimitLift attribute identifies the Closed Limit for Lifting
        /// the Window Covering whether position (in centimeters) is encoded or timed. This
        /// attribute is ignored if the device is running in Open Loop Control or only supports
        /// Tilt actions.
        /// </summary>
        public const ushort ATTR_INSTALLEDCLOSEDLIMITLIFT = 0x0011;

        /// <summary>
        /// The InstalledOpenLimitTilt attribute identifies the Open Limit for Tilting the
        /// Window Covering whether position (in tenth of a degree) is encoded or timed. This
        /// attribute is ignored if the device is running in Open Loop Control or only supports
        /// Lift actions.
        /// </summary>
        public const ushort ATTR_INSTALLEDOPENLIMITTILT = 0x0012;

        /// <summary>
        /// The InstalledClosedLimitTilt attribute identifies the Closed Limit for Tilting
        /// the Window Covering whether position (in tenth of a degree) is encoded or timed.
        /// This attribute is ignored if the device is running in Open Loop Control or only
        /// supports Lift actions.
        /// </summary>
        public const ushort ATTR_INSTALLEDCLOSEDLIMITTILT = 0x0013;

        /// <summary>
        /// The VelocityLift attribute identifies the velocity (in centimeters per second)
        /// associated with Lifting the Window Covering.
        /// </summary>
        public const ushort ATTR_VELOCITYLIFT = 0x0014;

        /// <summary>
        /// The AccelerationTimeLift attribute identifies any ramp up times to reaching the
        /// velocity setting (in tenth of a second) for positioning the Window Covering.
        /// </summary>
        public const ushort ATTR_ACCELERATIONTIMELIFT = 0x0015;

        /// <summary>
        /// The DecelerationTimeLift attribute identifies any ramp down times associated
        /// with stopping the positioning (in tenth of a second) of the Window Covering.
        /// </summary>
        public const ushort ATTR_DECELERATIONTIMELIFT = 0x0016;

        /// <summary>
        /// The Mode attribute allows configuration of the Window Covering, such as:
        /// reversing the motor direction, placing the Window Covering into calibration
        /// mode, placing the motor into maintenance mode, disabling the ZigBee network, and
        /// disabling status LEDs.
        /// </summary>
        public const ushort ATTR_MODE = 0x0017;

        /// <summary>
        /// Identifies the number of Intermediate Setpoints supported by the Window Covering
        /// for Lift and then iden- tifies the position settings for those Intermediate
        /// Setpoints if Closed Loop Control is supported. This is a comma delimited ASCII
        /// character string. For example: “2,0x0013, 0x0030”
        /// </summary>
        public const ushort ATTR_INTERMEDIATESETPOINTSLIFT = 0x0018;

        /// <summary>
        /// Identifies the number of Intermediate Setpoints supported by the Window Covering
        /// for Tilt and then iden- tifies the position settings for those Intermediate
        /// Setpoints if Closed Loop Control is supported. This is a comma delimited ASCII
        /// character string. For example: “2,0x0013, 0x0030”
        /// </summary>
        public const ushort ATTR_INTERMEDIATESETPOINTSTILT = 0x0019;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(20);

            attributeMap.Add(ATTR_WINDOWCOVERINGTYPE, new ZclAttribute(this, ATTR_WINDOWCOVERINGTYPE, "Window Covering Type", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_PHYSICALCLOSEDLIMITLIFT, new ZclAttribute(this, ATTR_PHYSICALCLOSEDLIMITLIFT, "Physical Closed Limit - Lift", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PHYSICALCLOSEDLIMITTILT, new ZclAttribute(this, ATTR_PHYSICALCLOSEDLIMITTILT, "Physical Closed Limit - Tilt", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTPOSITIONLIFT, new ZclAttribute(this, ATTR_CURRENTPOSITIONLIFT, "Current Position - Lift", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTPOSITIONTILT, new ZclAttribute(this, ATTR_CURRENTPOSITIONTILT, "Current Position - Tilt", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NUMBEROFACTUATIONSLIFT, new ZclAttribute(this, ATTR_NUMBEROFACTUATIONSLIFT, "Number Of Actuations - Lift", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NUMBEROFACTUATIONSTILT, new ZclAttribute(this, ATTR_NUMBEROFACTUATIONSTILT, "Number Of Actuations - Tilt", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CONFIGSTATUS, new ZclAttribute(this, ATTR_CONFIGSTATUS, "Config Status", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTPOSITIONLIFTPERCENTAGE, new ZclAttribute(this, ATTR_CURRENTPOSITIONLIFTPERCENTAGE, "Current Position Lift Percentage", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTPOSITIONTILTPERCENTAGE, new ZclAttribute(this, ATTR_CURRENTPOSITIONTILTPERCENTAGE, "Current Position Tilt Percentage", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_INSTALLEDOPENLIMITLIFT, new ZclAttribute(this, ATTR_INSTALLEDOPENLIMITLIFT, "Installed Open Limit - Lift", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_INSTALLEDCLOSEDLIMITLIFT, new ZclAttribute(this, ATTR_INSTALLEDCLOSEDLIMITLIFT, "Installed Closed Limit - Lift", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_INSTALLEDOPENLIMITTILT, new ZclAttribute(this, ATTR_INSTALLEDOPENLIMITTILT, "Installed Open Limit - Tilt", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_INSTALLEDCLOSEDLIMITTILT, new ZclAttribute(this, ATTR_INSTALLEDCLOSEDLIMITTILT, "Installed Closed Limit - Tilt", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_VELOCITYLIFT, new ZclAttribute(this, ATTR_VELOCITYLIFT, "Velocity - Lift", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_ACCELERATIONTIMELIFT, new ZclAttribute(this, ATTR_ACCELERATIONTIMELIFT, "Acceleration Time - Lift", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_DECELERATIONTIMELIFT, new ZclAttribute(this, ATTR_DECELERATIONTIMELIFT, "Deceleration Time - Lift", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_MODE, new ZclAttribute(this, ATTR_MODE, "Mode", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, true, true));
            attributeMap.Add(ATTR_INTERMEDIATESETPOINTSLIFT, new ZclAttribute(this, ATTR_INTERMEDIATESETPOINTSLIFT, "Intermediate Setpoints - Lift", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, true));
            attributeMap.Add(ATTR_INTERMEDIATESETPOINTSTILT, new ZclAttribute(this, ATTR_INTERMEDIATESETPOINTSTILT, "Intermediate Setpoints - Tilt", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, true));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(7);

            commandMap.Add(0x0000, () => new WindowCoveringUpOpen());
            commandMap.Add(0x0001, () => new WindowCoveringDownClose());
            commandMap.Add(0x0002, () => new WindowCoveringStop());
            commandMap.Add(0x0004, () => new WindowCoveringGoToLiftValue());
            commandMap.Add(0x0005, () => new WindowCoveringGoToLiftPercentage());
            commandMap.Add(0x0007, () => new WindowCoveringGoToTiltValue());
            commandMap.Add(0x0008, () => new WindowCoveringGoToTiltPercentage());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Window Covering cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclWindowCoveringCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Window Covering Up Open
        ///
        /// Moves window covering to InstalledOpenLimit
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> WindowCoveringUpOpen()
        {
            return Send(new WindowCoveringUpOpen());
        }

        /// <summary>
        /// The Window Covering Down Close
        ///
        /// Moves window covering to InstalledClosedLimit
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> WindowCoveringDownClose()
        {
            return Send(new WindowCoveringDownClose());
        }

        /// <summary>
        /// The Window Covering Stop
        ///
        /// Stop any adjustment of window covering
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> WindowCoveringStop()
        {
            return Send(new WindowCoveringStop());
        }

        /// <summary>
        /// The Window Covering Go To Lift Value
        ///
        /// Goto the specified lift value
        ///
        /// <param name="liftValue" <see cref="ushort"> Lift Value</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> WindowCoveringGoToLiftValue(ushort liftValue)
        {
            WindowCoveringGoToLiftValue command = new WindowCoveringGoToLiftValue();

            // Set the fields
            command.LiftValue = liftValue;

            return Send(command);
        }

        /// <summary>
        /// The Window Covering Go To Lift Percentage
        ///
        /// Goto the specified lift percentage
        ///
        /// <param name="percentageLiftValue" <see cref="byte"> Percentage Lift Value</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> WindowCoveringGoToLiftPercentage(byte percentageLiftValue)
        {
            WindowCoveringGoToLiftPercentage command = new WindowCoveringGoToLiftPercentage();

            // Set the fields
            command.PercentageLiftValue = percentageLiftValue;

            return Send(command);
        }

        /// <summary>
        /// The Window Covering Go To Tilt Value
        ///
        /// Goto the specified tilt value
        ///
        /// <param name="tiltValue" <see cref="ushort"> Tilt Value</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> WindowCoveringGoToTiltValue(ushort tiltValue)
        {
            WindowCoveringGoToTiltValue command = new WindowCoveringGoToTiltValue();

            // Set the fields
            command.TiltValue = tiltValue;

            return Send(command);
        }

        /// <summary>
        /// The Window Covering Go To Tilt Percentage
        ///
        /// Goto the specified tilt percentage
        ///
        /// <param name="percentageTiltValue" <see cref="byte"> Percentage Tilt Value</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> WindowCoveringGoToTiltPercentage(byte percentageTiltValue)
        {
            WindowCoveringGoToTiltPercentage command = new WindowCoveringGoToTiltPercentage();

            // Set the fields
            command.PercentageTiltValue = percentageTiltValue;

            return Send(command);
        }
    }
}
