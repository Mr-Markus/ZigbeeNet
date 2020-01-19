
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Power Configuration cluster implementation (Cluster ID 0x0001).
    ///
    /// Attributes for determining detailed information about a device’s power source(s),
    /// and for configuring under/over voltage alarms.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclPowerConfigurationCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0001;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Power Configuration";

        // Attribute constants

        /// <summary>
        /// The MainsVoltage attribute is 16-bits in length and specifies the actual
        /// (measured) RMS voltage (or DC voltage in the case of a DC supply) currently applied
        /// to the device, measured in units of 100mV.
        /// </summary>
        public const ushort ATTR_MAINSVOLTAGE = 0x0000;

        /// <summary>
        /// The MainsFrequency attribute is 8-bits in length and represents the frequency, in
        /// Hertz, of the mains as determined by the device as follows:-
        /// MainsFrequency = 0.5 x measured frequency
        /// Where 2 Hz <= measured frequency <= 506 Hz, corresponding to a MainsFrequency in the
        /// range 1 to 0xfd.
        /// The maximum resolution this format allows is 2 Hz. The following special values of
        /// MainsFrequency apply. <li>0x00 indicates a frequency that is too low to be
        /// measured.</li> <li>0xfe indicates a frequency that is too high to be
        /// measured.</li> <li>0xff indicates that the frequency could not be
        /// measured.</li>
        /// </summary>
        public const ushort ATTR_MAINSFREQUENCY = 0x0001;

        /// <summary>
        /// The MainsAlarmMask attribute is 8-bits in length and specifies which mains alarms
        /// may be generated. A ‘1’ in each bit position enables the alarm.
        /// </summary>
        public const ushort ATTR_MAINSALARMMASK = 0x0010;

        /// <summary>
        /// The MainsVoltageMinThreshold attribute is 16-bits in length and specifies the
        /// lower alarm threshold, measured in units of 100mV, for the MainsVoltage
        /// attribute. The value of this attribute shall be less than
        /// MainsVoltageMaxThreshold.
        /// If the value of MainsVoltage drops below the threshold specified by
        /// MainsVoltageMinThreshold, the device shall start a timer to expire after
        /// MainsVoltageDwellTripPoint seconds. If the value of this attribute increases to
        /// greater than or equal to MainsVoltageMinThreshold before the timer expires, the
        /// device shall stop and reset the timer. If the timer expires, an alarm shall be
        /// generated.
        /// The Alarm Code field included in the generated alarm shall be 0x00.
        /// If this attribute takes the value 0xffff then this alarm shall not be generated.
        /// </summary>
        public const ushort ATTR_MAINSVOLTAGEMINTHRESHOLD = 0x0011;

        /// <summary>
        /// The MainsVoltageMaxThreshold attribute is 16-bits in length and specifies the
        /// upper alarm threshold, measured in units of 100mV, for the MainsVoltage
        /// attribute. The value of this attribute shall be greater than
        /// MainsVoltageMinThreshold.
        /// If the value of MainsVoltage rises above the threshold specified by
        /// MainsVoltageMaxThreshold, the device shall start a timer to expire after
        /// MainsVoltageDwellTripPoint seconds. If the value of this attribute drops to
        /// lower than or equal to MainsVoltageMaxThreshold before the timer expires, the
        /// device shall stop and reset the timer. If the timer expires, an alarm shall be
        /// generated.
        /// The Alarm Code field included in the generated alarm shall be 0x01.
        /// If this attribute takes the value 0xffff then this alarm shall not be generated.
        /// </summary>
        public const ushort ATTR_MAINSVOLTAGEMAXTHRESHOLD = 0x0012;

        /// <summary>
        /// The MainsVoltageDwellTripPoint attribute is 16-bits in length and specifies the
        /// length of time, in seconds that the value of MainsVoltage may exist beyond either of
        /// its thresholds before an alarm is generated.
        /// If this attribute takes the value 0xffff then the associated alarms shall not be
        /// generated.
        /// </summary>
        public const ushort ATTR_MAINSVOLTAGEDWELLTRIPPOINT = 0x0013;

        /// <summary>
        /// The BatteryVoltage attribute is 8-bits in length and specifies the current actual
        /// (measured) battery voltage, in units of 100mV. The value 0xff indicates an invalid
        /// or unknown reading.
        /// </summary>
        public const ushort ATTR_BATTERYVOLTAGE = 0x0020;
        public const ushort ATTR_BATTERYPERCENTAGEREMAINING = 0x0021;

        /// <summary>
        /// The BatteryManufacturer attribute is a maximum of 16 bytes in length and specifies
        /// the name of the battery manufacturer as a ZigBee character string.
        /// </summary>
        public const ushort ATTR_BATTERYMANUFACTURER = 0x0030;

        /// <summary>
        /// The BatterySize attribute is an enumeration which specifies the type of battery
        /// being used by the device.
        /// </summary>
        public const ushort ATTR_BATTERYSIZE = 0x0031;

        /// <summary>
        /// The BatteryAHrRating attribute is 16-bits in length and specifies the
        /// Ampere-hour rating of the battery, measured in units of 10mAHr.
        /// </summary>
        public const ushort ATTR_BATTERYAHRRATING = 0x0032;

        /// <summary>
        /// The BatteryQuantity attribute is 8-bits in length and specifies the number of
        /// battery cells used to power the device.
        /// </summary>
        public const ushort ATTR_BATTERYQUANTITY = 0x0033;

        /// <summary>
        /// The BatteryRatedVoltage attribute is 8-bits in length and specifies the rated
        /// voltage of the battery being used in the device, measured in units of 100mV.
        /// </summary>
        public const ushort ATTR_BATTERYRATEDVOLTAGE = 0x0034;

        /// <summary>
        /// The BatteryAlarmMask attribute is 8-bits in length and specifies which battery
        /// alarms may be generated.
        /// </summary>
        public const ushort ATTR_BATTERYALARMMASK = 0x0035;

        /// <summary>
        /// The BatteryVoltageMinThreshold attribute is 8-bits in length and specifies the
        /// low voltage alarm threshold, measured in units of 100mV, for the BatteryVoltage
        /// attribute.
        /// If the value of BatteryVoltage drops below the threshold specified by
        /// BatteryVoltageMinThreshold an alarm shall be generated.
        /// The Alarm Code field included in the generated alarm shall be 0x10.
        /// If this attribute takes the value 0xff then this alarm shall not be generated.
        /// </summary>
        public const ushort ATTR_BATTERYVOLTAGEMINTHRESHOLD = 0x0036;

        /// <summary>
        /// Specify the low voltage alarm thresholds, measured in units of 100mV, for the
        /// BatteryVoltage attribute.
        /// </summary>
        public const ushort ATTR_BATTERYVOLTAGETHRESHOLD1 = 0x0037;

        /// <summary>
        /// Specify the low voltage alarm thresholds, measured in units of 100mV, for the
        /// BatteryVoltage attribute.
        /// </summary>
        public const ushort ATTR_BATTERYVOLTAGETHRESHOLD2 = 0x0038;

        /// <summary>
        /// Specify the low voltage alarm thresholds, measured in units of 100mV, for the
        /// BatteryVoltage attribute.
        /// </summary>
        public const ushort ATTR_BATTERYVOLTAGETHRESHOLD3 = 0x0039;

        /// <summary>
        /// Specifies the low battery percentage alarm threshold, measured in percentage
        /// (i.e., zero to 100%), for the BatteryPercentageRemaining attribute.
        /// </summary>
        public const ushort ATTR_BATTERYPERCENTAGEMINTHRESHOLD = 0x003A;

        /// <summary>
        /// Specifies the low battery percentage alarm threshold, measured in percentage
        /// (i.e., zero to 100%), for the BatteryPercentageRemaining attribute.
        /// </summary>
        public const ushort ATTR_BATTERYPERCENTAGETHRESHOLD1 = 0x003B;

        /// <summary>
        /// Specifies the low battery percentage alarm threshold, measured in percentage
        /// (i.e., zero to 100%), for the BatteryPercentageRemaining attribute.
        /// </summary>
        public const ushort ATTR_BATTERYPERCENTAGETHRESHOLD2 = 0x003C;

        /// <summary>
        /// Specifies the low battery percentage alarm threshold, measured in percentage
        /// (i.e., zero to 100%), for the BatteryPercentageRemaining attribute.
        /// </summary>
        public const ushort ATTR_BATTERYPERCENTAGETHRESHOLD3 = 0x003D;

        /// <summary>
        /// Specifies the current state of the device's battery alarms. This attribute
        /// provides a persistent record of a device's battery alarm conditions as well as a
        /// mechanism for reporting changes to those conditions, including the elimination
        /// of battery alarm states (e.g., when a battery is replaced).
        /// </summary>
        public const ushort ATTR_BATTERYALARMSTATE = 0x003E;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(23);

            attributeMap.Add(ATTR_MAINSVOLTAGE, new ZclAttribute(this, ATTR_MAINSVOLTAGE, "Mains Voltage", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_MAINSFREQUENCY, new ZclAttribute(this, ATTR_MAINSFREQUENCY, "Mains Frequency", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_MAINSALARMMASK, new ZclAttribute(this, ATTR_MAINSALARMMASK, "Mains Alarm Mask", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, true, false));
            attributeMap.Add(ATTR_MAINSVOLTAGEMINTHRESHOLD, new ZclAttribute(this, ATTR_MAINSVOLTAGEMINTHRESHOLD, "Mains Voltage Min Threshold", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_MAINSVOLTAGEMAXTHRESHOLD, new ZclAttribute(this, ATTR_MAINSVOLTAGEMAXTHRESHOLD, "Mains Voltage Max Threshold", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_MAINSVOLTAGEDWELLTRIPPOINT, new ZclAttribute(this, ATTR_MAINSVOLTAGEDWELLTRIPPOINT, "Mains Voltage Dwell Trip Point", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYVOLTAGE, new ZclAttribute(this, ATTR_BATTERYVOLTAGE, "Battery Voltage", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BATTERYPERCENTAGEREMAINING, new ZclAttribute(this, ATTR_BATTERYPERCENTAGEREMAINING, "Battery Percentage Remaining", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_BATTERYMANUFACTURER, new ZclAttribute(this, ATTR_BATTERYMANUFACTURER, "Battery Manufacturer", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYSIZE, new ZclAttribute(this, ATTR_BATTERYSIZE, "Battery Size", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYAHRRATING, new ZclAttribute(this, ATTR_BATTERYAHRRATING, "Battery A Hr Rating", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYQUANTITY, new ZclAttribute(this, ATTR_BATTERYQUANTITY, "Battery Quantity", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYRATEDVOLTAGE, new ZclAttribute(this, ATTR_BATTERYRATEDVOLTAGE, "Battery Rated Voltage", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYALARMMASK, new ZclAttribute(this, ATTR_BATTERYALARMMASK, "Battery Alarm Mask", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYVOLTAGEMINTHRESHOLD, new ZclAttribute(this, ATTR_BATTERYVOLTAGEMINTHRESHOLD, "Battery Voltage Min Threshold", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYVOLTAGETHRESHOLD1, new ZclAttribute(this, ATTR_BATTERYVOLTAGETHRESHOLD1, "Battery Voltage Threshold 1", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYVOLTAGETHRESHOLD2, new ZclAttribute(this, ATTR_BATTERYVOLTAGETHRESHOLD2, "Battery Voltage Threshold 2", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYVOLTAGETHRESHOLD3, new ZclAttribute(this, ATTR_BATTERYVOLTAGETHRESHOLD3, "Battery Voltage Threshold 3", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYPERCENTAGEMINTHRESHOLD, new ZclAttribute(this, ATTR_BATTERYPERCENTAGEMINTHRESHOLD, "Battery Percentage Min Threshold", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYPERCENTAGETHRESHOLD1, new ZclAttribute(this, ATTR_BATTERYPERCENTAGETHRESHOLD1, "Battery Percentage Threshold 1", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYPERCENTAGETHRESHOLD2, new ZclAttribute(this, ATTR_BATTERYPERCENTAGETHRESHOLD2, "Battery Percentage Threshold 2", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYPERCENTAGETHRESHOLD3, new ZclAttribute(this, ATTR_BATTERYPERCENTAGETHRESHOLD3, "Battery Percentage Threshold 3", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_BATTERYALARMSTATE, new ZclAttribute(this, ATTR_BATTERYALARMSTATE, "Battery Alarm State", ZclDataType.Get(DataType.BITMAP_32_BIT), false, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Power Configuration cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclPowerConfigurationCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
