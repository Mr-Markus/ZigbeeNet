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
using ZigBeeNet.ZCL.Clusters.Basic;

/**
 * Basiccluster implementation (Cluster ID 0x0000).
 *
 * Code is auto-generated. Modifications may be overwritten!
 */
namespace ZigBeeNet.ZCL.Clusters
{
   public class ZclBasicCluster : ZclCluster
   {
       /**
       * The ZigBee Cluster Library Cluster ID
       */
       public static ushort CLUSTER_ID = 0x0000;

       /**
       * The ZigBee Cluster Library Cluster Name
       */
       public static string CLUSTER_NAME = "Basic";

       /* Attribute constants */
       /**
        * The ZCLVersion attribute is 8 bits in length and specifies the version number of        * the ZigBee Cluster Library that all clusters on this endpoint conform to.       */
       public static ushort ATTR_ZCLVERSION = 0x0000;

       /**
        * The ApplicationVersion attribute is 8 bits in length and specifies the version        * number of the application software contained in the device. The usage of this        * attribute is manufacturer dependent.       */
       public static ushort ATTR_APPLICATIONVERSION = 0x0001;

       /**
        * The StackVersion attribute is 8 bits in length and specifies the version number        * of the implementation of the ZigBee stack contained in the device. The usage of        * this attribute is manufacturer dependent.       */
       public static ushort ATTR_STACKVERSION = 0x0002;

       /**
        * The HWVersion attribute is 8 bits in length and specifies the version number of        * the hardware of the device. The usage of this attribute is manufacturer dependent.       */
       public static ushort ATTR_HWVERSION = 0x0003;

       /**
        * The ManufacturerName attribute is a maximum of 32 bytes in length and specifies        * the name of the manufacturer as a ZigBee character string.       */
       public static ushort ATTR_MANUFACTURERNAME = 0x0004;

       /**
        * The ModelIdentifier attribute is a maximum of 32 bytes in length and specifies the        * model number (or other identifier) assigned by the manufacturer as a ZigBee character string.       */
       public static ushort ATTR_MODELIDENTIFIER = 0x0005;

       /**
        * The DateCode attribute is a ZigBee character string with a maximum length of 16 bytes.        * The first 8 characters specify the date of manufacturer of the device in international        * date notation according to ISO 8601, i.e. YYYYMMDD, e.g. 20060814.       */
       public static ushort ATTR_DATECODE = 0x0006;

       /**
        * The PowerSource attribute is 8 bits in length and specifies the source(s) of power        * available to the device. Bits b0–b6 of this attribute represent the primary power        * source of the device and bit b7 indicates whether the device has a secondary power        * source in the form of a battery backup.       */
       public static ushort ATTR_POWERSOURCE = 0x0007;

       /**
        * The LocationDescription attribute is a maximum of 16 bytes in length and describes        * the physical location of the device as a ZigBee character string.       */
       public static ushort ATTR_LOCATIONDESCRIPTION = 0x0010;

       /**
        * The PhysicalEnvironment attribute is 8 bits in length and specifies the type of        * physical environment in which the device will operate.       */
       public static ushort ATTR_PHYSICALENVIRONMENT = 0x0011;

       /**
        * The DeviceEnabled attribute is a boolean and specifies whether the device is enabled        * or disabled.       */
       public static ushort ATTR_DEVICEENABLED = 0x0012;

       /**
        * The AlarmMask attribute is 8 bits in length and specifies which of a number of general        * alarms may be generated.       */
       public static ushort ATTR_ALARMMASK = 0x0013;

       /**
        * The DisableLocalConfig attribute allows a number of local device configuration        * functions to be disabled.        * <p>        * The intention of this attribute is to allow disabling of any local configuration        * user interface, for example to prevent reset or binding buttons being activated by        * unauthorised persons in a public building.       */
       public static ushort ATTR_DISABLELOCALCONFIG = 0x0014;

       /**
        * The SWBuildIDattribute represents a detailed, manufacturer-specific reference to the version of the software.       */
       public static ushort ATTR_SWBUILDID = 0x4000;


       // Attribute initialisation
       protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
       {
           Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(14);

           ZclClusterType basic = ZclClusterType.GetValueById(ClusterType.BASIC);

           attributeMap.Add(ATTR_ZCLVERSION, new ZclAttribute(basic, ATTR_ZCLVERSION, "ZCLVersion", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
           attributeMap.Add(ATTR_APPLICATIONVERSION, new ZclAttribute(basic, ATTR_APPLICATIONVERSION, "ApplicationVersion", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
           attributeMap.Add(ATTR_STACKVERSION, new ZclAttribute(basic, ATTR_STACKVERSION, "StackVersion", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
           attributeMap.Add(ATTR_HWVERSION, new ZclAttribute(basic, ATTR_HWVERSION, "HWVersion", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
           attributeMap.Add(ATTR_MANUFACTURERNAME, new ZclAttribute(basic, ATTR_MANUFACTURERNAME, "ManufacturerName", ZclDataType.Get(DataType.CHARACTER_STRING), true, true, false, false));
           attributeMap.Add(ATTR_MODELIDENTIFIER, new ZclAttribute(basic, ATTR_MODELIDENTIFIER, "ModelIdentifier", ZclDataType.Get(DataType.CHARACTER_STRING), true, true, false, false));
           attributeMap.Add(ATTR_DATECODE, new ZclAttribute(basic, ATTR_DATECODE, "DateCode", ZclDataType.Get(DataType.CHARACTER_STRING), true, true, false, false));
           attributeMap.Add(ATTR_POWERSOURCE, new ZclAttribute(basic, ATTR_POWERSOURCE, "PowerSource", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
           attributeMap.Add(ATTR_LOCATIONDESCRIPTION, new ZclAttribute(basic, ATTR_LOCATIONDESCRIPTION, "LocationDescription", ZclDataType.Get(DataType.CHARACTER_STRING), true, true, true, false));
           attributeMap.Add(ATTR_PHYSICALENVIRONMENT, new ZclAttribute(basic, ATTR_PHYSICALENVIRONMENT, "PhysicalEnvironment", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, true, false));
           attributeMap.Add(ATTR_DEVICEENABLED, new ZclAttribute(basic, ATTR_DEVICEENABLED, "DeviceEnabled", ZclDataType.Get(DataType.BOOLEAN), true, true, true, false));
           attributeMap.Add(ATTR_ALARMMASK, new ZclAttribute(basic, ATTR_ALARMMASK, "AlarmMask", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, true, false));
           attributeMap.Add(ATTR_DISABLELOCALCONFIG, new ZclAttribute(basic, ATTR_DISABLELOCALCONFIG, "DisableLocalConfig", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, true, false));
           attributeMap.Add(ATTR_SWBUILDID, new ZclAttribute(basic, ATTR_SWBUILDID, "SWBuildID", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, false, false));

           return attributeMap;
       }

       /**
       * Default constructor to create a Basic cluster.
       *
       * @param zigbeeEndpoint the {@link ZigBeeEndpoint}
       */
       public ZclBasicCluster(ZigBeeEndpoint zigbeeEndpoint)
           : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
       {
       }


       /**
       * Get the ZCLVersion attribute [attribute ID0].
       *
       * The ZCLVersion attribute is 8 bits in length and specifies the version number of       * the ZigBee Cluster Library that all clusters on this endpoint conform to.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetZCLVersionAsync()
       {
           return Read(_attributes[ATTR_ZCLVERSION]);
       }

       /**
       * Synchronously Get the ZCLVersion attribute [attribute ID0].
       *
       * The ZCLVersion attribute is 8 bits in length and specifies the version number of       * the ZigBee Cluster Library that all clusters on this endpoint conform to.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetZCLVersion(long refreshPeriod)
       {
           if (_attributes[ATTR_ZCLVERSION].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_ZCLVERSION].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_ZCLVERSION]);
       }


       /**
       * Get the ApplicationVersion attribute [attribute ID1].
       *
       * The ApplicationVersion attribute is 8 bits in length and specifies the version       * number of the application software contained in the device. The usage of this       * attribute is manufacturer dependent.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetApplicationVersionAsync()
       {
           return Read(_attributes[ATTR_APPLICATIONVERSION]);
       }

       /**
       * Synchronously Get the ApplicationVersion attribute [attribute ID1].
       *
       * The ApplicationVersion attribute is 8 bits in length and specifies the version       * number of the application software contained in the device. The usage of this       * attribute is manufacturer dependent.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetApplicationVersion(long refreshPeriod)
       {
           if (_attributes[ATTR_APPLICATIONVERSION].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_APPLICATIONVERSION].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_APPLICATIONVERSION]);
       }


       /**
       * Get the StackVersion attribute [attribute ID2].
       *
       * The StackVersion attribute is 8 bits in length and specifies the version number       * of the implementation of the ZigBee stack contained in the device. The usage of       * this attribute is manufacturer dependent.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetStackVersionAsync()
       {
           return Read(_attributes[ATTR_STACKVERSION]);
       }

       /**
       * Synchronously Get the StackVersion attribute [attribute ID2].
       *
       * The StackVersion attribute is 8 bits in length and specifies the version number       * of the implementation of the ZigBee stack contained in the device. The usage of       * this attribute is manufacturer dependent.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetStackVersion(long refreshPeriod)
       {
           if (_attributes[ATTR_STACKVERSION].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_STACKVERSION].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_STACKVERSION]);
       }


       /**
       * Get the HWVersion attribute [attribute ID3].
       *
       * The HWVersion attribute is 8 bits in length and specifies the version number of       * the hardware of the device. The usage of this attribute is manufacturer dependent.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetHWVersionAsync()
       {
           return Read(_attributes[ATTR_HWVERSION]);
       }

       /**
       * Synchronously Get the HWVersion attribute [attribute ID3].
       *
       * The HWVersion attribute is 8 bits in length and specifies the version number of       * the hardware of the device. The usage of this attribute is manufacturer dependent.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetHWVersion(long refreshPeriod)
       {
           if (_attributes[ATTR_HWVERSION].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_HWVERSION].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_HWVERSION]);
       }


       /**
       * Get the ManufacturerName attribute [attribute ID4].
       *
       * The ManufacturerName attribute is a maximum of 32 bytes in length and specifies       * the name of the manufacturer as a ZigBee character string.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetManufacturerNameAsync()
       {
           return Read(_attributes[ATTR_MANUFACTURERNAME]);
       }

       /**
       * Synchronously Get the ManufacturerName attribute [attribute ID4].
       *
       * The ManufacturerName attribute is a maximum of 32 bytes in length and specifies       * the name of the manufacturer as a ZigBee character string.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public string GetManufacturerName(long refreshPeriod)
       {
           if (_attributes[ATTR_MANUFACTURERNAME].IsLastValueCurrent(refreshPeriod))
           {
               return (string)_attributes[ATTR_MANUFACTURERNAME].LastValue;
           }

           return (string)ReadSync(_attributes[ATTR_MANUFACTURERNAME]);
       }


       /**
       * Get the ModelIdentifier attribute [attribute ID5].
       *
       * The ModelIdentifier attribute is a maximum of 32 bytes in length and specifies the       * model number (or other identifier) assigned by the manufacturer as a ZigBee character string.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetModelIdentifierAsync()
       {
           return Read(_attributes[ATTR_MODELIDENTIFIER]);
       }

       /**
       * Synchronously Get the ModelIdentifier attribute [attribute ID5].
       *
       * The ModelIdentifier attribute is a maximum of 32 bytes in length and specifies the       * model number (or other identifier) assigned by the manufacturer as a ZigBee character string.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public string GetModelIdentifier(long refreshPeriod)
       {
           if (_attributes[ATTR_MODELIDENTIFIER].IsLastValueCurrent(refreshPeriod))
           {
               return (string)_attributes[ATTR_MODELIDENTIFIER].LastValue;
           }

           return (string)ReadSync(_attributes[ATTR_MODELIDENTIFIER]);
       }


       /**
       * Get the DateCode attribute [attribute ID6].
       *
       * The DateCode attribute is a ZigBee character string with a maximum length of 16 bytes.       * The first 8 characters specify the date of manufacturer of the device in international       * date notation according to ISO 8601, i.e. YYYYMMDD, e.g. 20060814.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetDateCodeAsync()
       {
           return Read(_attributes[ATTR_DATECODE]);
       }

       /**
       * Synchronously Get the DateCode attribute [attribute ID6].
       *
       * The DateCode attribute is a ZigBee character string with a maximum length of 16 bytes.       * The first 8 characters specify the date of manufacturer of the device in international       * date notation according to ISO 8601, i.e. YYYYMMDD, e.g. 20060814.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public string GetDateCode(long refreshPeriod)
       {
           if (_attributes[ATTR_DATECODE].IsLastValueCurrent(refreshPeriod))
           {
               return (string)_attributes[ATTR_DATECODE].LastValue;
           }

           return (string)ReadSync(_attributes[ATTR_DATECODE]);
       }


       /**
       * Get the PowerSource attribute [attribute ID7].
       *
       * The PowerSource attribute is 8 bits in length and specifies the source(s) of power       * available to the device. Bits b0–b6 of this attribute represent the primary power       * source of the device and bit b7 indicates whether the device has a secondary power       * source in the form of a battery backup.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetPowerSourceAsync()
       {
           return Read(_attributes[ATTR_POWERSOURCE]);
       }

       /**
       * Synchronously Get the PowerSource attribute [attribute ID7].
       *
       * The PowerSource attribute is 8 bits in length and specifies the source(s) of power       * available to the device. Bits b0–b6 of this attribute represent the primary power       * source of the device and bit b7 indicates whether the device has a secondary power       * source in the form of a battery backup.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetPowerSource(long refreshPeriod)
       {
           if (_attributes[ATTR_POWERSOURCE].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_POWERSOURCE].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_POWERSOURCE]);
       }


       /**
       * Set the LocationDescription attribute [attribute ID16].
       *
       * The LocationDescription attribute is a maximum of 16 bytes in length and describes       * the physical location of the device as a ZigBee character string.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @param locationDescription the string attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetLocationDescription(object value)
       {
           return Write(_attributes[ATTR_LOCATIONDESCRIPTION], value);
       }


       /**
       * Get the LocationDescription attribute [attribute ID16].
       *
       * The LocationDescription attribute is a maximum of 16 bytes in length and describes       * the physical location of the device as a ZigBee character string.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetLocationDescriptionAsync()
       {
           return Read(_attributes[ATTR_LOCATIONDESCRIPTION]);
       }

       /**
       * Synchronously Get the LocationDescription attribute [attribute ID16].
       *
       * The LocationDescription attribute is a maximum of 16 bytes in length and describes       * the physical location of the device as a ZigBee character string.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public string GetLocationDescription(long refreshPeriod)
       {
           if (_attributes[ATTR_LOCATIONDESCRIPTION].IsLastValueCurrent(refreshPeriod))
           {
               return (string)_attributes[ATTR_LOCATIONDESCRIPTION].LastValue;
           }

           return (string)ReadSync(_attributes[ATTR_LOCATIONDESCRIPTION]);
       }


       /**
       * Set the PhysicalEnvironment attribute [attribute ID17].
       *
       * The PhysicalEnvironment attribute is 8 bits in length and specifies the type of       * physical environment in which the device will operate.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @param physicalEnvironment the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetPhysicalEnvironment(object value)
       {
           return Write(_attributes[ATTR_PHYSICALENVIRONMENT], value);
       }


       /**
       * Get the PhysicalEnvironment attribute [attribute ID17].
       *
       * The PhysicalEnvironment attribute is 8 bits in length and specifies the type of       * physical environment in which the device will operate.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetPhysicalEnvironmentAsync()
       {
           return Read(_attributes[ATTR_PHYSICALENVIRONMENT]);
       }

       /**
       * Synchronously Get the PhysicalEnvironment attribute [attribute ID17].
       *
       * The PhysicalEnvironment attribute is 8 bits in length and specifies the type of       * physical environment in which the device will operate.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetPhysicalEnvironment(long refreshPeriod)
       {
           if (_attributes[ATTR_PHYSICALENVIRONMENT].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_PHYSICALENVIRONMENT].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_PHYSICALENVIRONMENT]);
       }


       /**
       * Set the DeviceEnabled attribute [attribute ID18].
       *
       * The DeviceEnabled attribute is a boolean and specifies whether the device is enabled       * or disabled.       *
       * The attribute is of type bool.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @param deviceEnabled the bool attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetDeviceEnabled(object value)
       {
           return Write(_attributes[ATTR_DEVICEENABLED], value);
       }


       /**
       * Get the DeviceEnabled attribute [attribute ID18].
       *
       * The DeviceEnabled attribute is a boolean and specifies whether the device is enabled       * or disabled.       *
       * The attribute is of type bool.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetDeviceEnabledAsync()
       {
           return Read(_attributes[ATTR_DEVICEENABLED]);
       }

       /**
       * Synchronously Get the DeviceEnabled attribute [attribute ID18].
       *
       * The DeviceEnabled attribute is a boolean and specifies whether the device is enabled       * or disabled.       *
       * The attribute is of type bool.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public bool GetDeviceEnabled(long refreshPeriod)
       {
           if (_attributes[ATTR_DEVICEENABLED].IsLastValueCurrent(refreshPeriod))
           {
               return (bool)_attributes[ATTR_DEVICEENABLED].LastValue;
           }

           return (bool)ReadSync(_attributes[ATTR_DEVICEENABLED]);
       }


       /**
       * Set the AlarmMask attribute [attribute ID19].
       *
       * The AlarmMask attribute is 8 bits in length and specifies which of a number of general       * alarms may be generated.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @param alarmMask the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetAlarmMask(object value)
       {
           return Write(_attributes[ATTR_ALARMMASK], value);
       }


       /**
       * Get the AlarmMask attribute [attribute ID19].
       *
       * The AlarmMask attribute is 8 bits in length and specifies which of a number of general       * alarms may be generated.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetAlarmMaskAsync()
       {
           return Read(_attributes[ATTR_ALARMMASK]);
       }

       /**
       * Synchronously Get the AlarmMask attribute [attribute ID19].
       *
       * The AlarmMask attribute is 8 bits in length and specifies which of a number of general       * alarms may be generated.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetAlarmMask(long refreshPeriod)
       {
           if (_attributes[ATTR_ALARMMASK].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_ALARMMASK].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_ALARMMASK]);
       }


       /**
       * Set the DisableLocalConfig attribute [attribute ID20].
       *
       * The DisableLocalConfig attribute allows a number of local device configuration       * functions to be disabled.       * <p>       * The intention of this attribute is to allow disabling of any local configuration       * user interface, for example to prevent reset or binding buttons being activated by       * unauthorised persons in a public building.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @param disableLocalConfig the byte attribute value to be set
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> SetDisableLocalConfig(object value)
       {
           return Write(_attributes[ATTR_DISABLELOCALCONFIG], value);
       }


       /**
       * Get the DisableLocalConfig attribute [attribute ID20].
       *
       * The DisableLocalConfig attribute allows a number of local device configuration       * functions to be disabled.       * <p>       * The intention of this attribute is to allow disabling of any local configuration       * user interface, for example to prevent reset or binding buttons being activated by       * unauthorised persons in a public building.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetDisableLocalConfigAsync()
       {
           return Read(_attributes[ATTR_DISABLELOCALCONFIG]);
       }

       /**
       * Synchronously Get the DisableLocalConfig attribute [attribute ID20].
       *
       * The DisableLocalConfig attribute allows a number of local device configuration       * functions to be disabled.       * <p>       * The intention of this attribute is to allow disabling of any local configuration       * user interface, for example to prevent reset or binding buttons being activated by       * unauthorised persons in a public building.       *
       * The attribute is of type byte.
       *
       * The implementation of this attribute by a device is MANDATORY
       *
       * @return the Task<CommandResult> command result Task
       */
       public byte GetDisableLocalConfig(long refreshPeriod)
       {
           if (_attributes[ATTR_DISABLELOCALCONFIG].IsLastValueCurrent(refreshPeriod))
           {
               return (byte)_attributes[ATTR_DISABLELOCALCONFIG].LastValue;
           }

           return (byte)ReadSync(_attributes[ATTR_DISABLELOCALCONFIG]);
       }


       /**
       * Get the SWBuildID attribute [attribute ID16384].
       *
       * The SWBuildIDattribute represents a detailed, manufacturer-specific reference to the version of the software.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> GetSWBuildIDAsync()
       {
           return Read(_attributes[ATTR_SWBUILDID]);
       }

       /**
       * Synchronously Get the SWBuildID attribute [attribute ID16384].
       *
       * The SWBuildIDattribute represents a detailed, manufacturer-specific reference to the version of the software.       *
       * The attribute is of type string.
       *
       * The implementation of this attribute by a device is OPTIONAL
       *
       * @return the Task<CommandResult> command result Task
       */
       public string GetSWBuildID(long refreshPeriod)
       {
           if (_attributes[ATTR_SWBUILDID].IsLastValueCurrent(refreshPeriod))
           {
               return (string)_attributes[ATTR_SWBUILDID].LastValue;
           }

           return (string)ReadSync(_attributes[ATTR_SWBUILDID]);
       }


       /**
       * The Reset to Factory Defaults Command
       *
       * On receipt of this command, the device resets all the attributes of all its clusters       * to their factory defaults. Note that ZigBee networking functionality,bindings, groups       * or other persistent data are not affected by this command       *
       * @return the Task<CommandResult> command result Task
       */
       public Task<CommandResult> ResetToFactoryDefaultsCommand()
       {
           ResetToFactoryDefaultsCommand command = new ResetToFactoryDefaultsCommand();

           return Send(command);
       }

       public override ZclCommand GetCommandFromId(int commandId)
       {
           switch (commandId)
           {
               case 0: // RESET_TO_FACTORY_DEFAULTS_COMMAND
                   return new ResetToFactoryDefaultsCommand();
                   default:
                       return null;
           }
       }
   }
}
