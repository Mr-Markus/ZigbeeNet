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

/**
 * Power configurationcluster implementation (Cluster ID 0x0001).
 *
 * Attributes for determining detailed information about a device’s power source(s), * and for configuring under/over voltage alarms. *
 * Code is auto-generated. Modifications may be overwritten!
 */
namespace ZigBeeNet.ZCL.Clusters
{
   public class ZclPowerConfigurationCluster : ZclCluster
   {
       /**
       * The ZigBee Cluster Library Cluster ID
       */
       public static ushort CLUSTER_ID = 0x0001;

       /**
       * The ZigBee Cluster Library Cluster Name
       */
       public static string CLUSTER_NAME = "Power configuration";

       /* Attribute constants */
       /**
        * The MainsVoltage attribute is 16-bits in length and specifies the actual (measured)        * RMS voltage (or DC voltage in the case of a DC supply) currently applied to the        * device, measured in units of 100mV.       */
       public static ushort ATTR_MAINSVOLTAGE = 0x0000;

       /**
        * The MainsFrequency attribute is 8-bits in length and represents the frequency, in        * Hertz, of the mains as determined by the device as follows:-        * <p>        * MainsFrequency = 0.5 x measured frequency        * <p>        * Where 2 Hz <= measured frequency <= 506 Hz, corresponding to a        * <p>        * MainsFrequency in the range 1 to 0xfd.        * <p>        * The maximum resolution this format allows is 2 Hz.        * The following special values of MainsFrequency apply.        * <li>0x00 indicates a frequency that is too low to be measured.</li>        * <li>0xfe indicates a frequency that is too high to be measured.</li>        * <li>0xff indicates that the frequency could not be measured.</li>       */
       public static ushort ATTR_MAINSFREQUENCY = 0x0001;

       /**
        * The MainsAlarmMask attribute is 8-bits in length and specifies which mains        * alarms may be generated. A ‘1’ in each bit position enables the alarm.       */
       public static ushort ATTR_MAINSALARMMASK = 0x0010;

       /**
        * The MainsVoltageMinThreshold attribute is 16-bits in length and specifies the        * lower alarm threshold, measured in units of 100mV, for the MainsVoltage        * attribute. The value of this attribute shall be less than MainsVoltageMaxThreshold.        * <p>        * If the value of MainsVoltage drops below the threshold specified by        * MainsVoltageMinThreshold, the device shall start a timer to expire after        * MainsVoltageDwellTripPoint seconds. If the value of this attribute increases to        * greater than or equal to MainsVoltageMinThreshold before the timer expires, the        * device shall stop and reset the timer. If the timer expires, an alarm shall be        * generated.        * <p>        * The Alarm Code field included in the generated alarm shall be 0x00.        * <p>        * If this attribute takes the value 0xffff then this alarm shall not be generated.       */
       public static ushort ATTR_MAINSVOLTAGEMINTHRESHOLD = 0x0011;

       /**
        * The MainsVoltageMaxThreshold attribute is 16-bits in length and specifies the        * upper alarm threshold, measured in units of 100mV, for the MainsVoltage        * attribute. The value of this attribute shall be greater than        * MainsVoltageMinThreshold.        * <p>        * If the value of MainsVoltage rises above the threshold specified by        * MainsVoltageMaxThreshold, the device shall start a timer to expire after        * MainsVoltageDwellTripPoint seconds. If the value of this attribute drops to lower        * than or equal to MainsVoltageMaxThreshold before the timer expires, the device        * shall stop and reset the timer. If the timer expires, an alarm shall be generated.        * <p>        * The Alarm Code field included in the generated alarm shall be 0x01.        * <p>        * If this attribute takes the value 0xffff then this alarm shall not be generated.       */
       public static ushort ATTR_MAINSVOLTAGEMAXTHRESHOLD = 0x0012;

       /**
        * The MainsVoltageDwellTripPoint attribute is 16-bits in length and specifies the        * length of time, in seconds that the value of MainsVoltage may exist beyond either        * of its thresholds before an alarm is generated.        * <p>        * If this attribute takes the value 0xffff then the associated alarms shall not be        * generated.       */
       public static ushort ATTR_MAINSVOLTAGEDWELLTRIPPOINT = 0x0013;

       /**
        * The BatteryVoltage attribute is 8-bits in length and specifies the current actual        * (measured) battery voltage, in units of 100mV.        * The value 0xff indicates an invalid or unknown reading.       */
       public static ushort ATTR_BATTERYVOLTAGE = 0x0020;

       /**
       */
       public static ushort ATTR_BATTERYPERCENTAGEREMAINING = 0x0021;

       /**
        * The BatteryManufacturer attribute is a maximum of 16 bytes in length and        * specifies the name of the battery manufacturer as a ZigBee character string.       */
       public static ushort ATTR_BATTERYMANUFACTURER = 0x0030;

       /**
        * The BatterySize attribute is an enumeration which specifies the type of battery        * being used by the device.       */
       public static ushort ATTR_BATTERYSIZE = 0x0031;

       /**
        * The BatteryAHrRating attribute is 16-bits in length and specifies the Ampere-hour        * rating of the battery, measured in units of 10mAHr.       */
       public static ushort ATTR_BATTERYAHRRATING = 0x0032;

       /**
        * The BatteryQuantity attribute is 8-bits in length and specifies the number of        * battery cells used to power the device.       */
       public static ushort ATTR_BATTERYQUANTITY = 0x0033;

       /**
        * The BatteryRatedVoltage attribute is 8-bits in length and specifies the rated        * voltage of the battery being used in the device, measured in units of 100mV.       */
       public static ushort ATTR_BATTERYRATEDVOLTAGE = 0x0034;

       /**
        * The BatteryAlarmMask attribute is 8-bits in length and specifies which battery        * alarms may be generated.       */
       public static ushort ATTR_BATTERYALARMMASK = 0x0035;

       /**
        * The BatteryVoltageMinThreshold attribute is 8-bits in length and specifies the low        * voltage alarm threshold, measured in units of 100mV, for the BatteryVoltage        * attribute.        * <p>        * If the value of BatteryVoltage drops below the threshold specified by        * BatteryVoltageMinThreshold an alarm shall be generated.        * <p>        * The Alarm Code field included in the generated alarm shall be 0x10.        * <p>        * If this attribute takes the value 0xff then this alarm shall not be generated.       */
       public static ushort ATTR_BATTERYVOLTAGEMINTHRESHOLD = 0x0036;

       /**
       */
       public static ushort ATTR_BATTERYVOLTAGETHRESHOLD1 = 0x0037;

       /**
       */
       public static ushort ATTR_BATTERYVOLTAGETHRESHOLD2 = 0x0038;

       /**
       */
       public static ushort ATTR_BATTERYVOLTAGETHRESHOLD3 = 0x0039;

       /**
       */
       public static ushort ATTR_BATTERYPERCENTAGEMINTHRESHOLD = 0x003A;

       /**
       */
       public static ushort ATTR_BATTERYPERCENTAGETHRESHOLD1 = 0x003B;

       /**
       */
       public static ushort ATTR_BATTERYPERCENTAGETHRESHOLD2 = 0x003C;

       /**
       */
       public static ushort ATTR_BATTERYPERCENTAGETHRESHOLD3 = 0x003D;

       /**
       */
       public static ushort ATTR_BATTERYALARMSTATE = 0x003E;


       // Attribute initialisation
       protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
       {
           Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(23);

           ZclClusterType powerconfiguration = ZclClusterType.GetValueById(ClusterType.POWER_CONFIGURATION);

           attributeMap.Add(ATTR_MAINSVOLTAGE, new ZclAttribute(powerconfiguration, ATTR_MAINSVOLTAGE, "MainsVoltage", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
           attributeMap.Add(ATTR_MAINSFREQUENCY, new ZclAttribute(powerconfiguration, ATTR_MAINSFREQUENCY, "MainsFrequency", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
           attributeMap.Add(ATTR_MAINSALARMMASK, new ZclAttribute(powerconfiguration, ATTR_MAINSALARMMASK, "MainsAlarmMask", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, true, false));
           attributeMap.Add(ATTR_MAINSVOLTAGEMINTHRESHOLD, new ZclAttribute(powerconfiguration, ATTR_MAINSVOLTAGEMINTHRESHOLD, "MainsVoltageMinThreshold", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_MAINSVOLTAGEMAXTHRESHOLD, new ZclAttribute(powerconfiguration, ATTR_MAINSVOLTAGEMAXTHRESHOLD, "MainsVoltageMaxThreshold", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_MAINSVOLTAGEDWELLTRIPPOINT, new ZclAttribute(powerconfiguration, ATTR_MAINSVOLTAGEDWELLTRIPPOINT, "MainsVoltageDwellTripPoint", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYVOLTAGE, new ZclAttribute(powerconfiguration, ATTR_BATTERYVOLTAGE, "BatteryVoltage", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
           attributeMap.Add(ATTR_BATTERYPERCENTAGEREMAINING, new ZclAttribute(powerconfiguration, ATTR_BATTERYPERCENTAGEREMAINING, "BatteryPercentageRemaining", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, true));
           attributeMap.Add(ATTR_BATTERYMANUFACTURER, new ZclAttribute(powerconfiguration, ATTR_BATTERYMANUFACTURER, "BatteryManufacturer", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYSIZE, new ZclAttribute(powerconfiguration, ATTR_BATTERYSIZE, "BatterySize", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYAHRRATING, new ZclAttribute(powerconfiguration, ATTR_BATTERYAHRRATING, "BatteryAHrRating", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYQUANTITY, new ZclAttribute(powerconfiguration, ATTR_BATTERYQUANTITY, "BatteryQuantity", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYRATEDVOLTAGE, new ZclAttribute(powerconfiguration, ATTR_BATTERYRATEDVOLTAGE, "BatteryRatedVoltage", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYALARMMASK, new ZclAttribute(powerconfiguration, ATTR_BATTERYALARMMASK, "BatteryAlarmMask", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYVOLTAGEMINTHRESHOLD, new ZclAttribute(powerconfiguration, ATTR_BATTERYVOLTAGEMINTHRESHOLD, "BatteryVoltageMinThreshold", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYVOLTAGETHRESHOLD1, new ZclAttribute(powerconfiguration, ATTR_BATTERYVOLTAGETHRESHOLD1, "BatteryVoltageThreshold1", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYVOLTAGETHRESHOLD2, new ZclAttribute(powerconfiguration, ATTR_BATTERYVOLTAGETHRESHOLD2, "BatteryVoltageThreshold2", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYVOLTAGETHRESHOLD3, new ZclAttribute(powerconfiguration, ATTR_BATTERYVOLTAGETHRESHOLD3, "BatteryVoltageThreshold3", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYPERCENTAGEMINTHRESHOLD, new ZclAttribute(powerconfiguration, ATTR_BATTERYPERCENTAGEMINTHRESHOLD, "BatteryPercentageMinThreshold", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYPERCENTAGETHRESHOLD1, new ZclAttribute(powerconfiguration, ATTR_BATTERYPERCENTAGETHRESHOLD1, "BatteryPercentageThreshold1", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYPERCENTAGETHRESHOLD2, new ZclAttribute(powerconfiguration, ATTR_BATTERYPERCENTAGETHRESHOLD2, "BatteryPercentageThreshold2", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYPERCENTAGETHRESHOLD3, new ZclAttribute(powerconfiguration, ATTR_BATTERYPERCENTAGETHRESHOLD3, "BatteryPercentageThreshold3", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
           attributeMap.Add(ATTR_BATTERYALARMSTATE, new ZclAttribute(powerconfiguration, ATTR_BATTERYALARMSTATE, "BatteryAlarmState", ZclDataType.Get(DataType.BITMAP_32_BIT), false, true, false, false));

           return attributeMap;
       }

       /**
       * Default constructor to create a Power configuration cluster.
       *
       * @param zigbeeEndpoint the {@link ZigBeeEndpoint}
       */
       public ZclPowerConfigurationCluster(ZigBeeEndpoint zigbeeEndpoint)
           : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
       {
       }


       /**
       * Get the MainsVoltage attribute [attribute ID0].
       *
       * The MainsVoltage attribute is 16-bits in length and specifies the actual (measured)       * RMS voltage (or DC voltage in the case of a DC supply) currently applied to the       * device, measured in units of 100mV.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetMainsVoltageAsync()
       {
           return Read(_attributes[ATTR_MAINSVOLTAGE]);
       }

       /**
       * Synchronously Get the MainsVoltage attribute [attribute ID0].
       *
       * The MainsVoltage attribute is 16-bits in length and specifies the actual (measured)       * RMS voltage (or DC voltage in the case of a DC supply) currently applied to the       * device, measured in units of 100mV.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public ushort GetMainsVoltage(long refreshPeriod)
       {
           if (_attributes[ATTR_MAINSVOLTAGE].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_MAINSVOLTAGE].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_MAINSVOLTAGE]);
       }


       /**
       * Get the MainsFrequency attribute [attribute ID1].
       *
       * The MainsFrequency attribute is 8-bits in length and represents the frequency, in       * Hertz, of the mains as determined by the device as follows:-       * <p>       * MainsFrequency = 0.5 x measured frequency       * <p>       * Where 2 Hz <= measured frequency <= 506 Hz, corresponding to a       * <p>       * MainsFrequency in the range 1 to 0xfd.       * <p>       * The maximum resolution this format allows is 2 Hz.       * The following special values of MainsFrequency apply.       * <li>0x00 indicates a frequency that is too low to be measured.</li>       * <li>0xfe indicates a frequency that is too high to be measured.</li>       * <li>0xff indicates that the frequency could not be measured.</li>       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetMainsFrequencyAsync()
       {
           return Read(_attributes[ATTR_MAINSFREQUENCY]);
       }

       /**
       * Synchronously Get the MainsFrequency attribute [attribute ID1].
       *
       * The MainsFrequency attribute is 8-bits in length and represents the frequency, in       * Hertz, of the mains as determined by the device as follows:-       * <p>       * MainsFrequency = 0.5 x measured frequency       * <p>       * Where 2 Hz <= measured frequency <= 506 Hz, corresponding to a       * <p>       * MainsFrequency in the range 1 to 0xfd.       * <p>       * The maximum resolution this format allows is 2 Hz.       * The following special values of MainsFrequency apply.       * <li>0x00 indicates a frequency that is too low to be measured.</li>       * <li>0xfe indicates a frequency that is too high to be measured.</li>       * <li>0xff indicates that the frequency could not be measured.</li>       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public ushort GetMainsFrequency(long refreshPeriod)
       {
           if (_attributes[ATTR_MAINSFREQUENCY].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_MAINSFREQUENCY].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_MAINSFREQUENCY]);
       }


       /**
       * Set the MainsAlarmMask attribute [attribute ID16].
       *
       * The MainsAlarmMask attribute is 8-bits in length and specifies which mains       * alarms may be generated. A ‘1’ in each bit position enables the alarm.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param mainsAlarmMask the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetMainsAlarmMask(object value)
       {
           return Write(_attributes[ATTR_MAINSALARMMASK], value);
       }


       /**
       * Get the MainsAlarmMask attribute [attribute ID16].
       *
       * The MainsAlarmMask attribute is 8-bits in length and specifies which mains       * alarms may be generated. A ‘1’ in each bit position enables the alarm.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetMainsAlarmMaskAsync()
       {
           return Read(_attributes[ATTR_MAINSALARMMASK]);
       }

       /**
       * Synchronously Get the MainsAlarmMask attribute [attribute ID16].
       *
       * The MainsAlarmMask attribute is 8-bits in length and specifies which mains       * alarms may be generated. A ‘1’ in each bit position enables the alarm.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetMainsAlarmMask(long refreshPeriod)
       {
           if (_attributes[ATTR_MAINSALARMMASK].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_MAINSALARMMASK].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_MAINSALARMMASK]);
       }


       /**
       * Set the MainsVoltageMinThreshold attribute [attribute ID17].
       *
       * The MainsVoltageMinThreshold attribute is 16-bits in length and specifies the       * lower alarm threshold, measured in units of 100mV, for the MainsVoltage       * attribute. The value of this attribute shall be less than MainsVoltageMaxThreshold.       * <p>       * If the value of MainsVoltage drops below the threshold specified by       * MainsVoltageMinThreshold, the device shall start a timer to expire after       * MainsVoltageDwellTripPoint seconds. If the value of this attribute increases to       * greater than or equal to MainsVoltageMinThreshold before the timer expires, the       * device shall stop and reset the timer. If the timer expires, an alarm shall be       * generated.       * <p>       * The Alarm Code field included in the generated alarm shall be 0x00.       * <p>       * If this attribute takes the value 0xffff then this alarm shall not be generated.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param mainsVoltageMinThreshold the ushort attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetMainsVoltageMinThreshold(object value)
       {
           return Write(_attributes[ATTR_MAINSVOLTAGEMINTHRESHOLD], value);
       }


       /**
       * Get the MainsVoltageMinThreshold attribute [attribute ID17].
       *
       * The MainsVoltageMinThreshold attribute is 16-bits in length and specifies the       * lower alarm threshold, measured in units of 100mV, for the MainsVoltage       * attribute. The value of this attribute shall be less than MainsVoltageMaxThreshold.       * <p>       * If the value of MainsVoltage drops below the threshold specified by       * MainsVoltageMinThreshold, the device shall start a timer to expire after       * MainsVoltageDwellTripPoint seconds. If the value of this attribute increases to       * greater than or equal to MainsVoltageMinThreshold before the timer expires, the       * device shall stop and reset the timer. If the timer expires, an alarm shall be       * generated.       * <p>       * The Alarm Code field included in the generated alarm shall be 0x00.       * <p>       * If this attribute takes the value 0xffff then this alarm shall not be generated.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetMainsVoltageMinThresholdAsync()
       {
           return Read(_attributes[ATTR_MAINSVOLTAGEMINTHRESHOLD]);
       }

       /**
       * Synchronously Get the MainsVoltageMinThreshold attribute [attribute ID17].
       *
       * The MainsVoltageMinThreshold attribute is 16-bits in length and specifies the       * lower alarm threshold, measured in units of 100mV, for the MainsVoltage       * attribute. The value of this attribute shall be less than MainsVoltageMaxThreshold.       * <p>       * If the value of MainsVoltage drops below the threshold specified by       * MainsVoltageMinThreshold, the device shall start a timer to expire after       * MainsVoltageDwellTripPoint seconds. If the value of this attribute increases to       * greater than or equal to MainsVoltageMinThreshold before the timer expires, the       * device shall stop and reset the timer. If the timer expires, an alarm shall be       * generated.       * <p>       * The Alarm Code field included in the generated alarm shall be 0x00.       * <p>       * If this attribute takes the value 0xffff then this alarm shall not be generated.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public ushort GetMainsVoltageMinThreshold(long refreshPeriod)
       {
           if (_attributes[ATTR_MAINSVOLTAGEMINTHRESHOLD].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_MAINSVOLTAGEMINTHRESHOLD].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_MAINSVOLTAGEMINTHRESHOLD]);
       }


       /**
       * Set the MainsVoltageMaxThreshold attribute [attribute ID18].
       *
       * The MainsVoltageMaxThreshold attribute is 16-bits in length and specifies the       * upper alarm threshold, measured in units of 100mV, for the MainsVoltage       * attribute. The value of this attribute shall be greater than       * MainsVoltageMinThreshold.       * <p>       * If the value of MainsVoltage rises above the threshold specified by       * MainsVoltageMaxThreshold, the device shall start a timer to expire after       * MainsVoltageDwellTripPoint seconds. If the value of this attribute drops to lower       * than or equal to MainsVoltageMaxThreshold before the timer expires, the device       * shall stop and reset the timer. If the timer expires, an alarm shall be generated.       * <p>       * The Alarm Code field included in the generated alarm shall be 0x01.       * <p>       * If this attribute takes the value 0xffff then this alarm shall not be generated.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param mainsVoltageMaxThreshold the ushort attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetMainsVoltageMaxThreshold(object value)
       {
           return Write(_attributes[ATTR_MAINSVOLTAGEMAXTHRESHOLD], value);
       }


       /**
       * Get the MainsVoltageMaxThreshold attribute [attribute ID18].
       *
       * The MainsVoltageMaxThreshold attribute is 16-bits in length and specifies the       * upper alarm threshold, measured in units of 100mV, for the MainsVoltage       * attribute. The value of this attribute shall be greater than       * MainsVoltageMinThreshold.       * <p>       * If the value of MainsVoltage rises above the threshold specified by       * MainsVoltageMaxThreshold, the device shall start a timer to expire after       * MainsVoltageDwellTripPoint seconds. If the value of this attribute drops to lower       * than or equal to MainsVoltageMaxThreshold before the timer expires, the device       * shall stop and reset the timer. If the timer expires, an alarm shall be generated.       * <p>       * The Alarm Code field included in the generated alarm shall be 0x01.       * <p>       * If this attribute takes the value 0xffff then this alarm shall not be generated.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetMainsVoltageMaxThresholdAsync()
       {
           return Read(_attributes[ATTR_MAINSVOLTAGEMAXTHRESHOLD]);
       }

       /**
       * Synchronously Get the MainsVoltageMaxThreshold attribute [attribute ID18].
       *
       * The MainsVoltageMaxThreshold attribute is 16-bits in length and specifies the       * upper alarm threshold, measured in units of 100mV, for the MainsVoltage       * attribute. The value of this attribute shall be greater than       * MainsVoltageMinThreshold.       * <p>       * If the value of MainsVoltage rises above the threshold specified by       * MainsVoltageMaxThreshold, the device shall start a timer to expire after       * MainsVoltageDwellTripPoint seconds. If the value of this attribute drops to lower       * than or equal to MainsVoltageMaxThreshold before the timer expires, the device       * shall stop and reset the timer. If the timer expires, an alarm shall be generated.       * <p>       * The Alarm Code field included in the generated alarm shall be 0x01.       * <p>       * If this attribute takes the value 0xffff then this alarm shall not be generated.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public ushort GetMainsVoltageMaxThreshold(long refreshPeriod)
       {
           if (_attributes[ATTR_MAINSVOLTAGEMAXTHRESHOLD].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_MAINSVOLTAGEMAXTHRESHOLD].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_MAINSVOLTAGEMAXTHRESHOLD]);
       }


       /**
       * Set the MainsVoltageDwellTripPoint attribute [attribute ID19].
       *
       * The MainsVoltageDwellTripPoint attribute is 16-bits in length and specifies the       * length of time, in seconds that the value of MainsVoltage may exist beyond either       * of its thresholds before an alarm is generated.       * <p>       * If this attribute takes the value 0xffff then the associated alarms shall not be       * generated.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param mainsVoltageDwellTripPoint the ushort attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetMainsVoltageDwellTripPoint(object value)
       {
           return Write(_attributes[ATTR_MAINSVOLTAGEDWELLTRIPPOINT], value);
       }


       /**
       * Get the MainsVoltageDwellTripPoint attribute [attribute ID19].
       *
       * The MainsVoltageDwellTripPoint attribute is 16-bits in length and specifies the       * length of time, in seconds that the value of MainsVoltage may exist beyond either       * of its thresholds before an alarm is generated.       * <p>       * If this attribute takes the value 0xffff then the associated alarms shall not be       * generated.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetMainsVoltageDwellTripPointAsync()
       {
           return Read(_attributes[ATTR_MAINSVOLTAGEDWELLTRIPPOINT]);
       }

       /**
       * Synchronously Get the MainsVoltageDwellTripPoint attribute [attribute ID19].
       *
       * The MainsVoltageDwellTripPoint attribute is 16-bits in length and specifies the       * length of time, in seconds that the value of MainsVoltage may exist beyond either       * of its thresholds before an alarm is generated.       * <p>       * If this attribute takes the value 0xffff then the associated alarms shall not be       * generated.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public ushort GetMainsVoltageDwellTripPoint(long refreshPeriod)
       {
           if (_attributes[ATTR_MAINSVOLTAGEDWELLTRIPPOINT].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_MAINSVOLTAGEDWELLTRIPPOINT].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_MAINSVOLTAGEDWELLTRIPPOINT]);
       }


       /**
       * Get the BatteryVoltage attribute [attribute ID32].
       *
       * The BatteryVoltage attribute is 8-bits in length and specifies the current actual       * (measured) battery voltage, in units of 100mV.       * The value 0xff indicates an invalid or unknown reading.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryVoltageAsync()
       {
           return Read(_attributes[ATTR_BATTERYVOLTAGE]);
       }

       /**
       * Synchronously Get the BatteryVoltage attribute [attribute ID32].
       *
       * The BatteryVoltage attribute is 8-bits in length and specifies the current actual       * (measured) battery voltage, in units of 100mV.       * The value 0xff indicates an invalid or unknown reading.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryVoltage(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYVOLTAGE].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYVOLTAGE].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYVOLTAGE]);
       }


       /**
       * Get the BatteryPercentageRemaining attribute [attribute ID33].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryPercentageRemainingAsync()
       {
           return Read(_attributes[ATTR_BATTERYPERCENTAGEREMAINING]);
       }

       /**
       * Synchronously Get the BatteryPercentageRemaining attribute [attribute ID33].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryPercentageRemaining(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYPERCENTAGEREMAINING].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYPERCENTAGEREMAINING].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYPERCENTAGEREMAINING]);
       }


       /**
       * Set reporting for the BatteryPercentageRemaining attribute [attribute ID33].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param minInterval minimum reporting period
       * @param maxInterval maximum reporting period
       * @param reportableChange {@link Object} delta required to trigger report
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryPercentageRemainingReporting(ushort minInterval, ushort maxInterval, object reportableChange)
       {
           return SetReporting(_attributes[ATTR_BATTERYPERCENTAGEREMAINING], minInterval, maxInterval, reportableChange);
       }


       /**
       * Set the BatteryManufacturer attribute [attribute ID48].
       *
       * The BatteryManufacturer attribute is a maximum of 16 bytes in length and       * specifies the name of the battery manufacturer as a ZigBee character string.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryManufacturer the string attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryManufacturer(object value)
       {
           return Write(_attributes[ATTR_BATTERYMANUFACTURER], value);
       }


       /**
       * Get the BatteryManufacturer attribute [attribute ID48].
       *
       * The BatteryManufacturer attribute is a maximum of 16 bytes in length and       * specifies the name of the battery manufacturer as a ZigBee character string.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryManufacturerAsync()
       {
           return Read(_attributes[ATTR_BATTERYMANUFACTURER]);
       }

       /**
       * Synchronously Get the BatteryManufacturer attribute [attribute ID48].
       *
       * The BatteryManufacturer attribute is a maximum of 16 bytes in length and       * specifies the name of the battery manufacturer as a ZigBee character string.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public string GetBatteryManufacturer(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYMANUFACTURER].IsLastValueCurrent(refreshPeriod))
           {
               return (string)_attributes[ATTR_BATTERYMANUFACTURER].LastValue;
           }

           return (string)ReadSync(_attributes[ATTR_BATTERYMANUFACTURER]);
       }


       /**
       * Set the BatterySize attribute [attribute ID49].
       *
       * The BatterySize attribute is an enumeration which specifies the type of battery       * being used by the device.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batterySize the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatterySize(object value)
       {
           return Write(_attributes[ATTR_BATTERYSIZE], value);
       }


       /**
       * Get the BatterySize attribute [attribute ID49].
       *
       * The BatterySize attribute is an enumeration which specifies the type of battery       * being used by the device.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatterySizeAsync()
       {
           return Read(_attributes[ATTR_BATTERYSIZE]);
       }

       /**
       * Synchronously Get the BatterySize attribute [attribute ID49].
       *
       * The BatterySize attribute is an enumeration which specifies the type of battery       * being used by the device.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatterySize(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYSIZE].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYSIZE].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYSIZE]);
       }


       /**
       * Set the BatteryAHrRating attribute [attribute ID50].
       *
       * The BatteryAHrRating attribute is 16-bits in length and specifies the Ampere-hour       * rating of the battery, measured in units of 10mAHr.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryAHrRating the ushort attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryAHrRating(object value)
       {
           return Write(_attributes[ATTR_BATTERYAHRRATING], value);
       }


       /**
       * Get the BatteryAHrRating attribute [attribute ID50].
       *
       * The BatteryAHrRating attribute is 16-bits in length and specifies the Ampere-hour       * rating of the battery, measured in units of 10mAHr.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryAHrRatingAsync()
       {
           return Read(_attributes[ATTR_BATTERYAHRRATING]);
       }

       /**
       * Synchronously Get the BatteryAHrRating attribute [attribute ID50].
       *
       * The BatteryAHrRating attribute is 16-bits in length and specifies the Ampere-hour       * rating of the battery, measured in units of 10mAHr.       *
       * The attribute is of type ushort.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public ushort GetBatteryAHrRating(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYAHRRATING].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_BATTERYAHRRATING].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_BATTERYAHRRATING]);
       }


       /**
       * Set the BatteryQuantity attribute [attribute ID51].
       *
       * The BatteryQuantity attribute is 8-bits in length and specifies the number of       * battery cells used to power the device.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryQuantity the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryQuantity(object value)
       {
           return Write(_attributes[ATTR_BATTERYQUANTITY], value);
       }


       /**
       * Get the BatteryQuantity attribute [attribute ID51].
       *
       * The BatteryQuantity attribute is 8-bits in length and specifies the number of       * battery cells used to power the device.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryQuantityAsync()
       {
           return Read(_attributes[ATTR_BATTERYQUANTITY]);
       }

       /**
       * Synchronously Get the BatteryQuantity attribute [attribute ID51].
       *
       * The BatteryQuantity attribute is 8-bits in length and specifies the number of       * battery cells used to power the device.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryQuantity(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYQUANTITY].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYQUANTITY].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYQUANTITY]);
       }


       /**
       * Set the BatteryRatedVoltage attribute [attribute ID52].
       *
       * The BatteryRatedVoltage attribute is 8-bits in length and specifies the rated       * voltage of the battery being used in the device, measured in units of 100mV.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryRatedVoltage the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryRatedVoltage(object value)
       {
           return Write(_attributes[ATTR_BATTERYRATEDVOLTAGE], value);
       }


       /**
       * Get the BatteryRatedVoltage attribute [attribute ID52].
       *
       * The BatteryRatedVoltage attribute is 8-bits in length and specifies the rated       * voltage of the battery being used in the device, measured in units of 100mV.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryRatedVoltageAsync()
       {
           return Read(_attributes[ATTR_BATTERYRATEDVOLTAGE]);
       }

       /**
       * Synchronously Get the BatteryRatedVoltage attribute [attribute ID52].
       *
       * The BatteryRatedVoltage attribute is 8-bits in length and specifies the rated       * voltage of the battery being used in the device, measured in units of 100mV.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryRatedVoltage(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYRATEDVOLTAGE].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYRATEDVOLTAGE].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYRATEDVOLTAGE]);
       }


       /**
       * Set the BatteryAlarmMask attribute [attribute ID53].
       *
       * The BatteryAlarmMask attribute is 8-bits in length and specifies which battery       * alarms may be generated.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryAlarmMask the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryAlarmMask(object value)
       {
           return Write(_attributes[ATTR_BATTERYALARMMASK], value);
       }


       /**
       * Get the BatteryAlarmMask attribute [attribute ID53].
       *
       * The BatteryAlarmMask attribute is 8-bits in length and specifies which battery       * alarms may be generated.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryAlarmMaskAsync()
       {
           return Read(_attributes[ATTR_BATTERYALARMMASK]);
       }

       /**
       * Synchronously Get the BatteryAlarmMask attribute [attribute ID53].
       *
       * The BatteryAlarmMask attribute is 8-bits in length and specifies which battery       * alarms may be generated.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryAlarmMask(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYALARMMASK].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYALARMMASK].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYALARMMASK]);
       }


       /**
       * Set the BatteryVoltageMinThreshold attribute [attribute ID54].
       *
       * The BatteryVoltageMinThreshold attribute is 8-bits in length and specifies the low       * voltage alarm threshold, measured in units of 100mV, for the BatteryVoltage       * attribute.       * <p>       * If the value of BatteryVoltage drops below the threshold specified by       * BatteryVoltageMinThreshold an alarm shall be generated.       * <p>       * The Alarm Code field included in the generated alarm shall be 0x10.       * <p>       * If this attribute takes the value 0xff then this alarm shall not be generated.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryVoltageMinThreshold the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryVoltageMinThreshold(object value)
       {
           return Write(_attributes[ATTR_BATTERYVOLTAGEMINTHRESHOLD], value);
       }


       /**
       * Get the BatteryVoltageMinThreshold attribute [attribute ID54].
       *
       * The BatteryVoltageMinThreshold attribute is 8-bits in length and specifies the low       * voltage alarm threshold, measured in units of 100mV, for the BatteryVoltage       * attribute.       * <p>       * If the value of BatteryVoltage drops below the threshold specified by       * BatteryVoltageMinThreshold an alarm shall be generated.       * <p>       * The Alarm Code field included in the generated alarm shall be 0x10.       * <p>       * If this attribute takes the value 0xff then this alarm shall not be generated.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryVoltageMinThresholdAsync()
       {
           return Read(_attributes[ATTR_BATTERYVOLTAGEMINTHRESHOLD]);
       }

       /**
       * Synchronously Get the BatteryVoltageMinThreshold attribute [attribute ID54].
       *
       * The BatteryVoltageMinThreshold attribute is 8-bits in length and specifies the low       * voltage alarm threshold, measured in units of 100mV, for the BatteryVoltage       * attribute.       * <p>       * If the value of BatteryVoltage drops below the threshold specified by       * BatteryVoltageMinThreshold an alarm shall be generated.       * <p>       * The Alarm Code field included in the generated alarm shall be 0x10.       * <p>       * If this attribute takes the value 0xff then this alarm shall not be generated.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryVoltageMinThreshold(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYVOLTAGEMINTHRESHOLD].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYVOLTAGEMINTHRESHOLD].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYVOLTAGEMINTHRESHOLD]);
       }


       /**
       * Set the BatteryVoltageThreshold1 attribute [attribute ID55].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryVoltageThreshold1 the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryVoltageThreshold1(object value)
       {
           return Write(_attributes[ATTR_BATTERYVOLTAGETHRESHOLD1], value);
       }


       /**
       * Get the BatteryVoltageThreshold1 attribute [attribute ID55].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryVoltageThreshold1Async()
       {
           return Read(_attributes[ATTR_BATTERYVOLTAGETHRESHOLD1]);
       }

       /**
       * Synchronously Get the BatteryVoltageThreshold1 attribute [attribute ID55].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryVoltageThreshold1(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYVOLTAGETHRESHOLD1].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYVOLTAGETHRESHOLD1].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYVOLTAGETHRESHOLD1]);
       }


       /**
       * Set the BatteryVoltageThreshold2 attribute [attribute ID56].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryVoltageThreshold2 the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryVoltageThreshold2(object value)
       {
           return Write(_attributes[ATTR_BATTERYVOLTAGETHRESHOLD2], value);
       }


       /**
       * Get the BatteryVoltageThreshold2 attribute [attribute ID56].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryVoltageThreshold2Async()
       {
           return Read(_attributes[ATTR_BATTERYVOLTAGETHRESHOLD2]);
       }

       /**
       * Synchronously Get the BatteryVoltageThreshold2 attribute [attribute ID56].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryVoltageThreshold2(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYVOLTAGETHRESHOLD2].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYVOLTAGETHRESHOLD2].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYVOLTAGETHRESHOLD2]);
       }


       /**
       * Set the BatteryVoltageThreshold3 attribute [attribute ID57].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryVoltageThreshold3 the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryVoltageThreshold3(object value)
       {
           return Write(_attributes[ATTR_BATTERYVOLTAGETHRESHOLD3], value);
       }


       /**
       * Get the BatteryVoltageThreshold3 attribute [attribute ID57].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryVoltageThreshold3Async()
       {
           return Read(_attributes[ATTR_BATTERYVOLTAGETHRESHOLD3]);
       }

       /**
       * Synchronously Get the BatteryVoltageThreshold3 attribute [attribute ID57].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryVoltageThreshold3(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYVOLTAGETHRESHOLD3].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYVOLTAGETHRESHOLD3].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYVOLTAGETHRESHOLD3]);
       }


       /**
       * Set the BatteryPercentageMinThreshold attribute [attribute ID58].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryPercentageMinThreshold the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryPercentageMinThreshold(object value)
       {
           return Write(_attributes[ATTR_BATTERYPERCENTAGEMINTHRESHOLD], value);
       }


       /**
       * Get the BatteryPercentageMinThreshold attribute [attribute ID58].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryPercentageMinThresholdAsync()
       {
           return Read(_attributes[ATTR_BATTERYPERCENTAGEMINTHRESHOLD]);
       }

       /**
       * Synchronously Get the BatteryPercentageMinThreshold attribute [attribute ID58].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryPercentageMinThreshold(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYPERCENTAGEMINTHRESHOLD].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYPERCENTAGEMINTHRESHOLD].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYPERCENTAGEMINTHRESHOLD]);
       }


       /**
       * Set the BatteryPercentageThreshold1 attribute [attribute ID59].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryPercentageThreshold1 the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryPercentageThreshold1(object value)
       {
           return Write(_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD1], value);
       }


       /**
       * Get the BatteryPercentageThreshold1 attribute [attribute ID59].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryPercentageThreshold1Async()
       {
           return Read(_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD1]);
       }

       /**
       * Synchronously Get the BatteryPercentageThreshold1 attribute [attribute ID59].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryPercentageThreshold1(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD1].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD1].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD1]);
       }


       /**
       * Set the BatteryPercentageThreshold2 attribute [attribute ID60].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryPercentageThreshold2 the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryPercentageThreshold2(object value)
       {
           return Write(_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD2], value);
       }


       /**
       * Get the BatteryPercentageThreshold2 attribute [attribute ID60].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryPercentageThreshold2Async()
       {
           return Read(_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD2]);
       }

       /**
       * Synchronously Get the BatteryPercentageThreshold2 attribute [attribute ID60].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryPercentageThreshold2(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD2].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD2].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD2]);
       }


       /**
       * Set the BatteryPercentageThreshold3 attribute [attribute ID61].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @param batteryPercentageThreshold3 the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetBatteryPercentageThreshold3(object value)
       {
           return Write(_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD3], value);
       }


       /**
       * Get the BatteryPercentageThreshold3 attribute [attribute ID61].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryPercentageThreshold3Async()
       {
           return Read(_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD3]);
       }

       /**
       * Synchronously Get the BatteryPercentageThreshold3 attribute [attribute ID61].
       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetBatteryPercentageThreshold3(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD3].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD3].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_BATTERYPERCENTAGETHRESHOLD3]);
       }


       /**
       * Get the BatteryAlarmState attribute [attribute ID62].
       *
       * The attribute is of type int.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetBatteryAlarmStateAsync()
       {
           return Read(_attributes[ATTR_BATTERYALARMSTATE]);
       }

       /**
       * Synchronously Get the BatteryAlarmState attribute [attribute ID62].
       *
       * The attribute is of type int.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public int GetBatteryAlarmState(long refreshPeriod)
       {
           if (_attributes[ATTR_BATTERYALARMSTATE].IsLastValueCurrent(refreshPeriod))
           {
               return (int)_attributes[ATTR_BATTERYALARMSTATE].LastValue;
           }

           return (int)ReadSync(_attributes[ATTR_BATTERYALARMSTATE]);
       }

   }
}
