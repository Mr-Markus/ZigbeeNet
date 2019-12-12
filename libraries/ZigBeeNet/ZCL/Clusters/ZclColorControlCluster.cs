
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.ColorControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Color Control cluster implementation (Cluster ID 0x0300).
    ///
    /// This cluster provides an interface for changing the color of a light. Color is specified
    /// according to the Commission Internationale de l'Éclairage (CIE) specification CIE
    /// 1931 Color Space. Color control is carried out in terms of x,y values, as defined by this
    /// specification.
    /// Additionally, color may optionally be controlled in terms of color temperature, or as
    /// hue and saturation values based on optionally variable RGB and W color points. It is
    /// recommended that the hue and saturation are interpreted according to the HSV (aka HSB)
    /// color model.
    /// Control over luminance is not included, as this is provided by means of the Level Control
    /// cluster of the General library. It is recommended that the level provided by this
    /// cluster be interpreted as representing a proportion of the maximum intensity
    /// achievable at the current color.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclColorControlCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0300;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Color Control";

        // Attribute constants

        /// <summary>
        /// The CurrentHue attribute contains the current hue value of the light. It is updated
        /// as fast as practical during commands that change the hue.
        /// The hue in degrees shall be related to the CurrentHue attribute by the relationship
        /// Hue = CurrentHue x 360 / 254 (CurrentHue in the range 0 - 254 inclusive)
        /// If this attribute is implemented then the CurrentSaturation and ColorMode
        /// attributes shall also be implemented.
        /// </summary>
        public const ushort ATTR_CURRENTHUE = 0x0000;

        /// <summary>
        /// The CurrentSaturation attribute holds the current saturation value of the light.
        /// It is updated as fast as practical during commands that change the saturation. The
        /// saturation shall be related to the CurrentSaturation attribute by the
        /// relationship Saturation = CurrentSaturation/254 (CurrentSaturation in the
        /// range 0 - 254 inclusive) If this attribute is implemented then the CurrentHue and
        /// ColorMode attributes shall also be implemented.
        /// </summary>
        public const ushort ATTR_CURRENTSATURATION = 0x0001;

        /// <summary>
        /// The RemainingTime attribute holds the time remaining, in 1/10ths of a second,
        /// until the currently active command will be complete.
        /// </summary>
        public const ushort ATTR_REMAININGTIME = 0x0002;

        /// <summary>
        /// The CurrentX attribute contains the current value of the normalized chromaticity
        /// value x, as defined in the CIE xyY Color Space. It is updated as fast as practical
        /// during commands that change the color.
        /// The value of x shall be related to the CurrentX attribute by the relationship
        /// x = CurrentX / 65535 (CurrentX in the range 0 to 65279 inclusive)
        /// </summary>
        public const ushort ATTR_CURRENTX = 0x0003;

        /// <summary>
        /// The CurrentY attribute contains the current value of the normalized chromaticity
        /// value y, as defined in the CIE xyY Color Space. It is updated as fast as practical
        /// during commands that change the color.
        /// The value of y shall be related to the CurrentY attribute by the relationship
        /// y = CurrentY / 65535 (CurrentY in the range 0 to 65279 inclusive)
        /// </summary>
        public const ushort ATTR_CURRENTY = 0x0004;

        /// <summary>
        /// The DriftCompensation attribute indicates what mechanism, if any, is in use for
        /// compensation for color/intensity drift over time.
        /// </summary>
        public const ushort ATTR_DRIFTCOMPENSATION = 0x0005;

        /// <summary>
        /// The CompensationText attribute holds a textual indication of what mechanism, if
        /// any, is in use to compensate for color/intensity drift over time.
        /// </summary>
        public const ushort ATTR_COMPENSATIONTEXT = 0x0006;

        /// <summary>
        /// The ColorTemperature attribute contains a scaled inverse of the current value of
        /// the color temperature. It is updated as fast as practical during commands that
        /// change the color.
        /// The color temperature value in Kelvins shall be related to the ColorTemperature
        /// attribute by the relationship
        /// Color temperature = 1,000,000 / ColorTemperature (ColorTemperature in the range
        /// 1 to 65279 inclusive, giving a color temperature range from 1,000,000 Kelvins to
        /// 15.32 Kelvins).
        /// The value ColorTemperature = 0 indicates an undefined value. The value
        /// ColorTemperature = 65535 indicates an invalid value.
        /// </summary>
        public const ushort ATTR_COLORTEMPERATURE = 0x0007;

        /// <summary>
        /// The ColorMode attribute indicates which attributes are currently determining
        /// the color of the device. If either the CurrentHue or CurrentSaturation attribute
        /// is implemented, this attribute shall also be implemented, otherwise it is
        /// optional. The value of the ColorMode attribute cannot be written directly - it is
        /// set upon reception of another command in to the appropriate mode for that command.
        /// </summary>
        public const ushort ATTR_COLORMODE = 0x0008;

        /// <summary>
        /// The EnhancedCurrentHueattribute represents non-equidistant steps along the
        /// CIE 1931 color triangle, and it provides 16-bits precision. The upper 8 bits of this
        /// attribute shall be used as an index in the implementation specific XY lookup table
        /// to provide the non-equidistance steps (see the ZLL test specification for an
        /// example). The lower 8 bits shall be used to interpolate between these steps in a
        /// linear way in order to provide color zoom for the user.
        /// </summary>
        public const ushort ATTR_ENHANCEDCURRENTHUE = 0x4000;

        /// <summary>
        /// The EnhancedColorModeattribute specifies which attributes are currently
        /// determining the color of the device. To provide compatibility with standard ZCL,
        /// the original ColorModeattribute SHALLindicate ‘CurrentHueand
        /// CurrentSaturation’ when the light uses the EnhancedCurrentHueattribute.
        /// </summary>
        public const ushort ATTR_ENHANCEDCOLORMODE = 0x4001;

        /// <summary>
        /// The ColorLoopActive attribute specifies the current active status of the color
        /// loop. If this attribute has the value 0x00, the color loop SHALLnot be active. If
        /// this attribute has the value 0x01, the color loop shall be active. All other values
        /// (0x02 – 0xff) are reserved.
        /// </summary>
        public const ushort ATTR_COLORLOOPACTIVE = 0x4002;

        /// <summary>
        /// The ColorLoopDirection attribute specifies the current direction of the color
        /// loop. If this attribute has the value 0x00, the EnhancedCurrentHue attribute
        /// shall be decremented. If this attribute has the value 0x01, the
        /// EnhancedCurrentHue attribute shall be incremented. All other values (0x02 –
        /// 0xff) are reserved.
        /// </summary>
        public const ushort ATTR_COLORLOOPDIRECTION = 0x4003;

        /// <summary>
        /// The ColorLoopTime attribute specifies the number of seconds it shall take to
        /// perform a full color loop, i.e.,to cycle all values of the EnhancedCurrentHue
        /// attribute (between 0x0000 and 0xffff).
        /// </summary>
        public const ushort ATTR_COLORLOOPTIME = 0x4004;

        /// <summary>
        /// The ColorLoopStartEnhancedHueattribute specifies the value of the
        /// EnhancedCurrentHue attribute from which the color loop shall be started.
        /// </summary>
        public const ushort ATTR_COLORLOOPSTARTHUE = 0x4005;

        /// <summary>
        /// The ColorLoopStoredEnhancedHue attribute specifies the value of the
        /// EnhancedCurrentHue attribute before the color loop was started. Once the color
        /// loop is complete, the EnhancedCurrentHue attribute shall be restored to this
        /// value.
        /// </summary>
        public const ushort ATTR_COLORLOOPSTOREDHUE = 0x4006;

        /// <summary>
        /// The ColorCapabilitiesattribute specifies the color capabilities of the device
        /// supporting the color control cluster.
        /// Note:The support of the CurrentXand CurrentYattributes is mandatory regardless
        /// of color capabilities.
        /// </summary>
        public const ushort ATTR_COLORCAPABILITIES = 0x400A;

        /// <summary>
        /// The ColorTempPhysicalMinMiredsattribute indicates the minimum mired value
        /// supported by the hardware. ColorTempPhysicalMinMiredscorresponds to the
        /// maximum color temperature in kelvins supported by the hardware.
        /// ColorTempPhysicalMinMireds ≤ ColorTemperatureMireds
        /// </summary>
        public const ushort ATTR_COLORTEMPERATUREMIN = 0x400B;

        /// <summary>
        /// The ColorTempPhysicalMaxMiredsattribute indicates the maximum mired value
        /// supported by the hard-ware. ColorTempPhysicalMaxMiredscorresponds to the
        /// minimum color temperature in kelvins supported by the hardware.
        /// ColorTemperatureMireds ≤ ColorTempPhysicalMaxMireds.
        /// </summary>
        public const ushort ATTR_COLORTEMPERATUREMAX = 0x400C;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(19);

            attributeMap.Add(ATTR_CURRENTHUE, new ZclAttribute(this, ATTR_CURRENTHUE, "Current Hue", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_CURRENTSATURATION, new ZclAttribute(this, ATTR_CURRENTSATURATION, "Current Saturation", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_REMAININGTIME, new ZclAttribute(this, ATTR_REMAININGTIME, "Remaining Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTX, new ZclAttribute(this, ATTR_CURRENTX, "Current X", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_CURRENTY, new ZclAttribute(this, ATTR_CURRENTY, "Current Y", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_DRIFTCOMPENSATION, new ZclAttribute(this, ATTR_DRIFTCOMPENSATION, "Drift Compensation", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_COMPENSATIONTEXT, new ZclAttribute(this, ATTR_COMPENSATIONTEXT, "Compensation Text", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, false, false));
            attributeMap.Add(ATTR_COLORTEMPERATURE, new ZclAttribute(this, ATTR_COLORTEMPERATURE, "Color Temperature", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_COLORMODE, new ZclAttribute(this, ATTR_COLORMODE, "Color Mode", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_ENHANCEDCURRENTHUE, new ZclAttribute(this, ATTR_ENHANCEDCURRENTHUE, "Enhanced Current Hue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_ENHANCEDCOLORMODE, new ZclAttribute(this, ATTR_ENHANCEDCOLORMODE, "Enhanced Color Mode", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_COLORLOOPACTIVE, new ZclAttribute(this, ATTR_COLORLOOPACTIVE, "Color Loop Active", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_COLORLOOPDIRECTION, new ZclAttribute(this, ATTR_COLORLOOPDIRECTION, "Color Loop Direction", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_COLORLOOPTIME, new ZclAttribute(this, ATTR_COLORLOOPTIME, "Color Loop Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_COLORLOOPSTARTHUE, new ZclAttribute(this, ATTR_COLORLOOPSTARTHUE, "Color Loop Start Hue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_COLORLOOPSTOREDHUE, new ZclAttribute(this, ATTR_COLORLOOPSTOREDHUE, "Color Loop Stored Hue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_COLORCAPABILITIES, new ZclAttribute(this, ATTR_COLORCAPABILITIES, "Color Capabilities", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, false, false));
            attributeMap.Add(ATTR_COLORTEMPERATUREMIN, new ZclAttribute(this, ATTR_COLORTEMPERATUREMIN, "Color Temperature Min", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_COLORTEMPERATUREMAX, new ZclAttribute(this, ATTR_COLORTEMPERATUREMAX, "Color Temperature Max", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(19);

            commandMap.Add(0x0000, () => new MoveToHueCommand());
            commandMap.Add(0x0001, () => new MoveHueCommand());
            commandMap.Add(0x0002, () => new StepHueCommand());
            commandMap.Add(0x0003, () => new MoveToSaturationCommand());
            commandMap.Add(0x0004, () => new MoveSaturationCommand());
            commandMap.Add(0x0005, () => new StepSaturationCommand());
            commandMap.Add(0x0006, () => new MoveToHueAndSaturationCommand());
            commandMap.Add(0x0007, () => new MoveToColorCommand());
            commandMap.Add(0x0008, () => new MoveColorCommand());
            commandMap.Add(0x0009, () => new StepColorCommand());
            commandMap.Add(0x000A, () => new MoveToColorTemperatureCommand());
            commandMap.Add(0x0040, () => new EnhancedMoveToHueCommand());
            commandMap.Add(0x0041, () => new EnhancedMoveHueCommand());
            commandMap.Add(0x0042, () => new EnhancedStepHueCommand());
            commandMap.Add(0x0043, () => new EnhancedMoveToHueAndSaturationCommand());
            commandMap.Add(0x0044, () => new ColorLoopSetCommand());
            commandMap.Add(0x0047, () => new StopMoveStepCommand());
            commandMap.Add(0x004B, () => new MoveColorTemperatureCommand());
            commandMap.Add(0x004C, () => new StepColorTemperatureCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Color Control cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclColorControlCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Move To Hue Command
        ///
        /// <param name="hue" <see cref="byte"> Hue</ param >
        /// <param name="direction" <see cref="byte"> Direction</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> MoveToHueCommand(byte hue, byte direction, ushort transitionTime)
        {
            MoveToHueCommand command = new MoveToHueCommand();

            // Set the fields
            command.Hue = hue;
            command.Direction = direction;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Move Hue Command
        ///
        /// <param name="moveMode" <see cref="byte"> Move Mode</ param >
        /// <param name="rate" <see cref="byte"> Rate</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> MoveHueCommand(byte moveMode, byte rate)
        {
            MoveHueCommand command = new MoveHueCommand();

            // Set the fields
            command.MoveMode = moveMode;
            command.Rate = rate;

            return Send(command);
        }

        /// <summary>
        /// The Step Hue Command
        ///
        /// <param name="stepMode" <see cref="byte"> Step Mode</ param >
        /// <param name="stepSize" <see cref="byte"> Step Size</ param >
        /// <param name="transitionTime" <see cref="byte"> Transition Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> StepHueCommand(byte stepMode, byte stepSize, byte transitionTime)
        {
            StepHueCommand command = new StepHueCommand();

            // Set the fields
            command.StepMode = stepMode;
            command.StepSize = stepSize;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Move To Saturation Command
        ///
        /// <param name="saturation" <see cref="byte"> Saturation</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> MoveToSaturationCommand(byte saturation, ushort transitionTime)
        {
            MoveToSaturationCommand command = new MoveToSaturationCommand();

            // Set the fields
            command.Saturation = saturation;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Move Saturation Command
        ///
        /// <param name="moveMode" <see cref="byte"> Move Mode</ param >
        /// <param name="rate" <see cref="byte"> Rate</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> MoveSaturationCommand(byte moveMode, byte rate)
        {
            MoveSaturationCommand command = new MoveSaturationCommand();

            // Set the fields
            command.MoveMode = moveMode;
            command.Rate = rate;

            return Send(command);
        }

        /// <summary>
        /// The Step Saturation Command
        ///
        /// <param name="stepMode" <see cref="byte"> Step Mode</ param >
        /// <param name="stepSize" <see cref="byte"> Step Size</ param >
        /// <param name="transitionTime" <see cref="byte"> Transition Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> StepSaturationCommand(byte stepMode, byte stepSize, byte transitionTime)
        {
            StepSaturationCommand command = new StepSaturationCommand();

            // Set the fields
            command.StepMode = stepMode;
            command.StepSize = stepSize;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Move To Hue And Saturation Command
        ///
        /// <param name="hue" <see cref="byte"> Hue</ param >
        /// <param name="saturation" <see cref="byte"> Saturation</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> MoveToHueAndSaturationCommand(byte hue, byte saturation, ushort transitionTime)
        {
            MoveToHueAndSaturationCommand command = new MoveToHueAndSaturationCommand();

            // Set the fields
            command.Hue = hue;
            command.Saturation = saturation;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Move To Color Command
        ///
        /// <param name="colorX" <see cref="ushort"> Color X</ param >
        /// <param name="colorY" <see cref="ushort"> Color Y</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> MoveToColorCommand(ushort colorX, ushort colorY, ushort transitionTime)
        {
            MoveToColorCommand command = new MoveToColorCommand();

            // Set the fields
            command.ColorX = colorX;
            command.ColorY = colorY;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Move Color Command
        ///
        /// <param name="rateX" <see cref="short"> Rate X</ param >
        /// <param name="rateY" <see cref="short"> Rate Y</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> MoveColorCommand(short rateX, short rateY)
        {
            MoveColorCommand command = new MoveColorCommand();

            // Set the fields
            command.RateX = rateX;
            command.RateY = rateY;

            return Send(command);
        }

        /// <summary>
        /// The Step Color Command
        ///
        /// <param name="stepX" <see cref="short"> Step X</ param >
        /// <param name="stepY" <see cref="short"> Step Y</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> StepColorCommand(short stepX, short stepY, ushort transitionTime)
        {
            StepColorCommand command = new StepColorCommand();

            // Set the fields
            command.StepX = stepX;
            command.StepY = stepY;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Move To Color Temperature Command
        ///
        /// On receipt of this command, a device shall set the value of the ColorMode attribute,
        /// where implemented, to 0x02, and shall then move from its current color to the color
        /// given by the Color Temperature Mireds field.
        /// The movement shall be continuous, i.e., not a step function, and the time taken to
        /// move to the new color shall be equal to the Transition Time field, in 1/10ths of a
        /// second.
        ///
        /// <param name="colorTemperature" <see cref="ushort"> Color Temperature</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> MoveToColorTemperatureCommand(ushort colorTemperature, ushort transitionTime)
        {
            MoveToColorTemperatureCommand command = new MoveToColorTemperatureCommand();

            // Set the fields
            command.ColorTemperature = colorTemperature;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Enhanced Move To Hue Command
        ///
        /// The Enhanced Move to Hue command allows lamps to be moved in a smooth continuous
        /// transition from their current hue to a target hue.
        /// On receipt of this command, a device shall set the ColorMode attribute to 0x00 and
        /// set the EnhancedColorMode attribute to the value 0x03. The device shall then move
        /// from its current enhanced hue to the value given in the Enhanced Hue field.
        /// The movement shall be continuous, i.e., not a step function, and the time taken to
        /// move to the new en- hanced hue shall be equal to the Transition Time field.
        ///         ///
        /// <param name="enhancedHue" <see cref="ushort"> Enhanced Hue</ param >
        /// <param name="direction" <see cref="byte"> Direction</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> EnhancedMoveToHueCommand(ushort enhancedHue, byte direction, ushort transitionTime)
        {
            EnhancedMoveToHueCommand command = new EnhancedMoveToHueCommand();

            // Set the fields
            command.EnhancedHue = enhancedHue;
            command.Direction = direction;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Enhanced Move Hue Command
        ///
        /// The Enhanced Move to Hue command allows lamps to be moved in a smooth continuous
        /// transition from their current hue to a target hue.
        /// On receipt of this command, a device shall set the ColorMode attribute to 0x00 and
        /// set the EnhancedColorMode attribute to the value 0x03. The device shall then move
        /// from its current enhanced hue in an up or down direction in a continuous fashion.
        ///
        /// <param name="moveMode" <see cref="byte"> Move Mode</ param >
        /// <param name="rate" <see cref="ushort"> Rate</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> EnhancedMoveHueCommand(byte moveMode, ushort rate)
        {
            EnhancedMoveHueCommand command = new EnhancedMoveHueCommand();

            // Set the fields
            command.MoveMode = moveMode;
            command.Rate = rate;

            return Send(command);
        }

        /// <summary>
        /// The Enhanced Step Hue Command
        ///
        /// The Enhanced Step Hue command allows lamps to be moved in a stepped transition from
        /// their current hue to a target hue, resulting in a linear transition through XY
        /// space.
        ///
        /// <param name="stepMode" <see cref="byte"> Step Mode</ param >
        /// <param name="stepSize" <see cref="ushort"> Step Size</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> EnhancedStepHueCommand(byte stepMode, ushort stepSize, ushort transitionTime)
        {
            EnhancedStepHueCommand command = new EnhancedStepHueCommand();

            // Set the fields
            command.StepMode = stepMode;
            command.StepSize = stepSize;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Enhanced Move To Hue And Saturation Command
        ///
        /// The Enhanced Move to Hue and Saturation command allows lamps to be moved in a smooth
        /// continuous transition from their current hue to a target hue and from their current
        /// saturation to a target saturation.
        ///
        /// <param name="enhancedHue" <see cref="ushort"> Enhanced Hue</ param >
        /// <param name="saturation" <see cref="byte"> Saturation</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> EnhancedMoveToHueAndSaturationCommand(ushort enhancedHue, byte saturation, ushort transitionTime)
        {
            EnhancedMoveToHueAndSaturationCommand command = new EnhancedMoveToHueAndSaturationCommand();

            // Set the fields
            command.EnhancedHue = enhancedHue;
            command.Saturation = saturation;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /// <summary>
        /// The Color Loop Set Command
        ///
        /// The Color Loop Set command allows a color loop to be activated such that the color
        /// lamp cycles through its range of hues.
        ///
        /// <param name="updateFlags" <see cref="byte"> Update Flags</ param >
        /// <param name="action" <see cref="byte"> Action</ param >
        /// <param name="direction" <see cref="byte"> Direction</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <param name="startHue" <see cref="ushort"> Start Hue</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ColorLoopSetCommand(byte updateFlags, byte action, byte direction, ushort transitionTime, ushort startHue)
        {
            ColorLoopSetCommand command = new ColorLoopSetCommand();

            // Set the fields
            command.UpdateFlags = updateFlags;
            command.Action = action;
            command.Direction = direction;
            command.TransitionTime = transitionTime;
            command.StartHue = startHue;

            return Send(command);
        }

        /// <summary>
        /// The Stop Move Step Command
        ///
        /// The Stop Move Step command is provided to allow Move to and Step commands to be
        /// stopped. (Note this automatically provides symmetry to the Level Control
        /// cluster.)
        /// Upon receipt of this command, any Move to, Move or Step command currently in process
        /// shall be ter- minated. The values of the CurrentHue, EnhancedCurrentHue and
        /// CurrentSaturation attributes shall be left at their present value upon receipt of
        /// the Stop Move Step command, and the RemainingTime attribute shall be set to zero.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> StopMoveStepCommand()
        {
            return Send(new StopMoveStepCommand());
        }

        /// <summary>
        /// The Move Color Temperature Command
        ///
        /// The Move Color Temperature command allows the color temperature of a lamp to be
        /// moved at a specified rate.
        ///
        /// <param name="moveMode" <see cref="byte"> Move Mode</ param >
        /// <param name="rate" <see cref="ushort"> Rate</ param >
        /// <param name="colorTemperatureMinimum" <see cref="ushort"> Color Temperature Minimum</ param >
        /// <param name="colorTemperatureMaximum" <see cref="ushort"> Color Temperature Maximum</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> MoveColorTemperatureCommand(byte moveMode, ushort rate, ushort colorTemperatureMinimum, ushort colorTemperatureMaximum)
        {
            MoveColorTemperatureCommand command = new MoveColorTemperatureCommand();

            // Set the fields
            command.MoveMode = moveMode;
            command.Rate = rate;
            command.ColorTemperatureMinimum = colorTemperatureMinimum;
            command.ColorTemperatureMaximum = colorTemperatureMaximum;

            return Send(command);
        }

        /// <summary>
        /// The Step Color Temperature Command
        ///
        /// The Step Color Temperature command allows the color temperature of a lamp to be
        /// stepped with a specified step size.
        ///
        /// <param name="stepMode" <see cref="byte"> Step Mode</ param >
        /// <param name="stepSize" <see cref="ushort"> Step Size</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <param name="colorTemperatureMinimum" <see cref="ushort"> Color Temperature Minimum</ param >
        /// <param name="colorTemperatureMaximum" <see cref="ushort"> Color Temperature Maximum</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> StepColorTemperatureCommand(byte stepMode, ushort stepSize, ushort transitionTime, ushort colorTemperatureMinimum, ushort colorTemperatureMaximum)
        {
            StepColorTemperatureCommand command = new StepColorTemperatureCommand();

            // Set the fields
            command.StepMode = stepMode;
            command.StepSize = stepSize;
            command.TransitionTime = transitionTime;
            command.ColorTemperatureMinimum = colorTemperatureMinimum;
            command.ColorTemperatureMaximum = colorTemperatureMaximum;

            return Send(command);
        }
    }
}
