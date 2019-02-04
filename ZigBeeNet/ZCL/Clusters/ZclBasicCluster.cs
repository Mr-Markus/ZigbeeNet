using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.ZCL.Clusters.Basic;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters
{
    public class ZclBasicCluster : ZclCluster
    {
        /**
     * The ZigBee Cluster Library Cluster ID
     */
        public const ushort CLUSTER_ID = 0x0000;

        /**
         * The ZigBee Cluster Library Cluster Name
         */
        public const String CLUSTER_NAME = "Basic";

        // Attribute constants
        /**
         * The ZCLVersion attribute is 8 bits in length and specifies the version number of
         * the ZigBee Cluster Library that all clusters on this endpoint conform to.
         */
        public const ushort ATTR_ZCLVERSION = 0x0000;
        /**
         * The ApplicationVersion attribute is 8 bits in length and specifies the version
         * number of the application software contained in the device. The usage of this
         * attribute is manufacturer dependent.
         */
        public const ushort ATTR_APPLICATIONVERSION = 0x0001;
        /**
         * The StackVersion attribute is 8 bits in length and specifies the version number
         * of the implementation of the ZigBee stack contained in the device. The usage of
         * this attribute is manufacturer dependent.
         */
        public const ushort ATTR_STACKVERSION = 0x0002;
        /**
         * The HWVersion attribute is 8 bits in length and specifies the version number of
         * the hardware of the device. The usage of this attribute is manufacturer dependent.
         */
        public const ushort ATTR_HWVERSION = 0x0003;
        /**
         * The ManufacturerName attribute is a maximum of 32 bytes in length and specifies
         * the name of the manufacturer as a ZigBee character string.
         */
        public const ushort ATTR_MANUFACTURERNAME = 0x0004;
        /**
         * The ModelIdentifier attribute is a maximum of 32 bytes in length and specifies the
         * model number (or other identifier) assigned by the manufacturer as a ZigBee character string.
         */
        public const ushort ATTR_MODELIDENTIFIER = 0x0005;
        /**
         * The DateCode attribute is a ZigBee character string with a maximum length of 16 bytes.
         * The first 8 characters specify the date of manufacturer of the device in international
         * date notation according to ISO 8601, i.e. YYYYMMDD, e.g. 20060814.
         */
        public const ushort ATTR_DATECODE = 0x0006;
        /**
         * The PowerSource attribute is 8 bits in length and specifies the source(s) of power
         * available to the device. Bits b0–b6 of this attribute represent the primary power
         * source of the device and bit b7 indicates whether the device has a secondary power
         * source in the form of a battery backup.
         */
        public const ushort ATTR_POWERSOURCE = 0x0007;
        /**
         * The LocationDescription attribute is a maximum of 16 bytes in length and describes
         * the physical location of the device as a ZigBee character string.
         */
        public const ushort ATTR_LOCATIONDESCRIPTION = 0x0010;
        /**
         * The PhysicalEnvironment attribute is 8 bits in length and specifies the type of
         * physical environment in which the device will operate.
         */
        public const ushort ATTR_PHYSICALENVIRONMENT = 0x0011;
        /**
         * The DeviceEnabled attribute is a boolean and specifies whether the device is enabled
         * or disabled.
         */
        public const ushort ATTR_DEVICEENABLED = 0x0012;
        /**
         * The AlarmMask attribute is 8 bits in length and specifies which of a number of general
         * alarms may be generated.
         */
        public const ushort ATTR_ALARMMASK = 0x0013;
        /**
         * The DisableLocalConfig attribute allows a number of local device configuration
         * functions to be disabled.
         * <p>
         * The intention of this attribute is to allow disabling of any local configuration
         * user interface, for example to prevent reset or binding buttons being activated by
         * unauthorised persons in a public building.
         */
        public const ushort ATTR_DISABLELOCALCONFIG = 0x0014;
        /**
         * The SWBuildIDattribute represents a detailed, manufacturer-specific reference to the version of the software.
         */
        public const ushort ATTR_SWBUILDID = 0x4000;

        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>();

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
         * Get the <i>ZCLVersion</i> attribute [attribute ID <b>0</b>].
         * <p>
         * The ZCLVersion attribute is 8 bits in length and specifies the version number of
         * the ZigBee Cluster Library that all clusters on this endpoint conform to.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetZclVersionAsync()
        {
            return Read(_attributes[ATTR_ZCLVERSION]);
        }

        /**
         * Synchronously get the <i>ZCLVersion</i> attribute [attribute ID <b>0</b>].
         * <p>
         * The ZCLVersion attribute is 8 bits in length and specifies the version number of
         * the ZigBee Cluster Library that all clusters on this endpoint conform to.
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
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public byte GetZclVersion(long refreshPeriod)
        {
            if (_attributes[ATTR_ZCLVERSION].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_ZCLVERSION].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_ZCLVERSION]);
        }

        /**
         * Get the <i>ApplicationVersion</i> attribute [attribute ID <b>1</b>].
         * <p>
         * The ApplicationVersion attribute is 8 bits in length and specifies the version
         * number of the application software contained in the device. The usage of this
         * attribute is manufacturer dependent.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetApplicationVersionAsync()
        {
            return Read(_attributes[ATTR_APPLICATIONVERSION]);
        }

        /**
         * Synchronously get the <i>ApplicationVersion</i> attribute [attribute ID <b>1</b>].
         * <p>
         * The ApplicationVersion attribute is 8 bits in length and specifies the version
         * number of the application software contained in the device. The usage of this
         * attribute is manufacturer dependent.
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
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
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
         * Get the <i>StackVersion</i> attribute [attribute ID <b>2</b>].
         * <p>
         * The StackVersion attribute is 8 bits in length and specifies the version number
         * of the implementation of the ZigBee stack contained in the device. The usage of
         * this attribute is manufacturer dependent.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> getStackVersionAsync()
        {
            return Read(_attributes[ATTR_STACKVERSION]);
        }

        /**
         * Synchronously get the <i>StackVersion</i> attribute [attribute ID <b>2</b>].
         * <p>
         * The StackVersion attribute is 8 bits in length and specifies the version number
         * of the implementation of the ZigBee stack contained in the device. The usage of
         * this attribute is manufacturer dependent.
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
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public ushort GetStackVersion(long refreshPeriod)
        {
            if (_attributes[ATTR_STACKVERSION].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_STACKVERSION].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_STACKVERSION]);
        }

        /**
         * Get the <i>HWVersion</i> attribute [attribute ID <b>3</b>].
         * <p>
         * The HWVersion attribute is 8 bits in length and specifies the version number of
         * the hardware of the device. The usage of this attribute is manufacturer dependent.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetHwVersionAsync()
        {
            return Read(_attributes[ATTR_HWVERSION]);
        }

        /**
         * Synchronously get the <i>HWVersion</i> attribute [attribute ID <b>3</b>].
         * <p>
         * The HWVersion attribute is 8 bits in length and specifies the version number of
         * the hardware of the device. The usage of this attribute is manufacturer dependent.
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
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public byte GetHwVersion(long refreshPeriod)
        {
            if (_attributes[ATTR_HWVERSION].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_HWVERSION].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_HWVERSION]);
        }

        /**
         * Get the <i>ManufacturerName</i> attribute [attribute ID <b>4</b>].
         * <p>
         * The ManufacturerName attribute is a maximum of 32 bytes in length and specifies
         * the name of the manufacturer as a ZigBee character string.
         * <p>
         * The attribute is of type {@link String}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetManufacturerNameAsync()
        {
            return Read(_attributes[ATTR_MANUFACTURERNAME]);
        }

        /**
         * Synchronously get the <i>ManufacturerName</i> attribute [attribute ID <b>4</b>].
         * <p>
         * The ManufacturerName attribute is a maximum of 32 bytes in length and specifies
         * the name of the manufacturer as a ZigBee character string.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link String}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link String} attribute value, or null on error
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
         * Get the <i>ModelIdentifier</i> attribute [attribute ID <b>5</b>].
         * <p>
         * The ModelIdentifier attribute is a maximum of 32 bytes in length and specifies the
         * model number (or other identifier) assigned by the manufacturer as a ZigBee character string.
         * <p>
         * The attribute is of type {@link String}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetModelIdentifierAsync()
        {
            return Read(_attributes[ATTR_MODELIDENTIFIER]);
        }

        /**
         * Synchronously get the <i>ModelIdentifier</i> attribute [attribute ID <b>5</b>].
         * <p>
         * The ModelIdentifier attribute is a maximum of 32 bytes in length and specifies the
         * model number (or other identifier) assigned by the manufacturer as a ZigBee character string.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link String}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link String} attribute value, or null on error
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
         * Get the <i>DateCode</i> attribute [attribute ID <b>6</b>].
         * <p>
         * The DateCode attribute is a ZigBee character string with a maximum length of 16 bytes.
         * The first 8 characters specify the date of manufacturer of the device in international
         * date notation according to ISO 8601, i.e. YYYYMMDD, e.g. 20060814.
         * <p>
         * The attribute is of type {@link String}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetDateCodeAsync()
        {
            return Read(_attributes[ATTR_DATECODE]);
        }

        /**
         * Synchronously get the <i>DateCode</i> attribute [attribute ID <b>6</b>].
         * <p>
         * The DateCode attribute is a ZigBee character string with a maximum length of 16 bytes.
         * The first 8 characters specify the date of manufacturer of the device in international
         * date notation according to ISO 8601, i.e. YYYYMMDD, e.g. 20060814.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link String}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link String} attribute value, or null on error
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
         * Get the <i>PowerSource</i> attribute [attribute ID <b>7</b>].
         * <p>
         * The PowerSource attribute is 8 bits in length and specifies the source(s) of power
         * available to the device. Bits b0–b6 of this attribute represent the primary power
         * source of the device and bit b7 indicates whether the device has a secondary power
         * source in the form of a battery backup.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetPowerSourceAsync()
        {
            return Read(_attributes[ATTR_POWERSOURCE]);
        }

        /**
         * Synchronously get the <i>PowerSource</i> attribute [attribute ID <b>7</b>].
         * <p>
         * The PowerSource attribute is 8 bits in length and specifies the source(s) of power
         * available to the device. Bits b0–b6 of this attribute represent the primary power
         * source of the device and bit b7 indicates whether the device has a secondary power
         * source in the form of a battery backup.
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
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
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
         * Set the <i>LocationDescription</i> attribute [attribute ID <b>16</b>].
         * <p>
         * The LocationDescription attribute is a maximum of 16 bytes in length and describes
         * the physical location of the device as a ZigBee character string.
         * <p>
         * The attribute is of type {@link String}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param locationDescription the {@link String} attribute value to be set
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> SetLocationDescription(Object value)
        {
            return Write(_attributes[ATTR_LOCATIONDESCRIPTION], value);
        }

        /**
         * Get the <i>LocationDescription</i> attribute [attribute ID <b>16</b>].
         * <p>
         * The LocationDescription attribute is a maximum of 16 bytes in length and describes
         * the physical location of the device as a ZigBee character string.
         * <p>
         * The attribute is of type {@link String}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetLocationDescriptionAsync()
        {
            return Read(_attributes[ATTR_LOCATIONDESCRIPTION]);
        }

        /**
         * Synchronously get the <i>LocationDescription</i> attribute [attribute ID <b>16</b>].
         * <p>
         * The LocationDescription attribute is a maximum of 16 bytes in length and describes
         * the physical location of the device as a ZigBee character string.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link String}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link String} attribute value, or null on error
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
         * Set the <i>PhysicalEnvironment</i> attribute [attribute ID <b>17</b>].
         * <p>
         * The PhysicalEnvironment attribute is 8 bits in length and specifies the type of
         * physical environment in which the device will operate.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param physicalEnvironment the {@link Integer} attribute value to be set
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> SetPhysicalEnvironment(Object value)
        {
            return Write(_attributes[ATTR_PHYSICALENVIRONMENT], value);
        }

        /**
         * Get the <i>PhysicalEnvironment</i> attribute [attribute ID <b>17</b>].
         * <p>
         * The PhysicalEnvironment attribute is 8 bits in length and specifies the type of
         * physical environment in which the device will operate.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetPhysicalEnvironmentAsync()
        {
            return Read(_attributes[ATTR_PHYSICALENVIRONMENT]);
        }

        /**
         * Synchronously get the <i>PhysicalEnvironment</i> attribute [attribute ID <b>17</b>].
         * <p>
         * The PhysicalEnvironment attribute is 8 bits in length and specifies the type of
         * physical environment in which the device will operate.
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
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
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
         * Set the <i>DeviceEnabled</i> attribute [attribute ID <b>18</b>].
         * <p>
         * The DeviceEnabled attribute is a boolean and specifies whether the device is enabled
         * or disabled.
         * <p>
         * The attribute is of type {@link Boolean}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param deviceEnabled the {@link Boolean} attribute value to be set
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> SetDeviceEnabled(object value)
        {
            return Write(_attributes[ATTR_DEVICEENABLED], value);
        }

        /**
         * Get the <i>DeviceEnabled</i> attribute [attribute ID <b>18</b>].
         * <p>
         * The DeviceEnabled attribute is a boolean and specifies whether the device is enabled
         * or disabled.
         * <p>
         * The attribute is of type {@link Boolean}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetDeviceEnabledAsync()
        {
            return Read(_attributes[ATTR_DEVICEENABLED]);
        }

        /**
         * Synchronously get the <i>DeviceEnabled</i> attribute [attribute ID <b>18</b>].
         * <p>
         * The DeviceEnabled attribute is a boolean and specifies whether the device is enabled
         * or disabled.
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
        public bool GetDeviceEnabled(long refreshPeriod)
        {
            if (_attributes[ATTR_DEVICEENABLED].IsLastValueCurrent(refreshPeriod))
            {
                return (bool)_attributes[ATTR_DEVICEENABLED].LastValue;
            }

            return (bool)ReadSync(_attributes[ATTR_DEVICEENABLED]);
        }

        /**
         * Set the <i>AlarmMask</i> attribute [attribute ID <b>19</b>].
         * <p>
         * The AlarmMask attribute is 8 bits in length and specifies which of a number of general
         * alarms may be generated.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param alarmMask the {@link Integer} attribute value to be set
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> SetAlarmMask(object value)
        {
            return Write(_attributes[ATTR_ALARMMASK], value);
        }

        /**
         * Get the <i>AlarmMask</i> attribute [attribute ID <b>19</b>].
         * <p>
         * The AlarmMask attribute is 8 bits in length and specifies which of a number of general
         * alarms may be generated.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetAlarmMaskAsync()
        {
            return Read(_attributes[ATTR_ALARMMASK]);
        }

        /**
         * Synchronously get the <i>AlarmMask</i> attribute [attribute ID <b>19</b>].
         * <p>
         * The AlarmMask attribute is 8 bits in length and specifies which of a number of general
         * alarms may be generated.
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
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
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
         * Set the <i>DisableLocalConfig</i> attribute [attribute ID <b>20</b>].
         * <p>
         * The DisableLocalConfig attribute allows a number of local device configuration
         * functions to be disabled.
         * <p>
         * The intention of this attribute is to allow disabling of any local configuration
         * user interface, for example to prevent reset or binding buttons being activated by
         * unauthorised persons in a public building.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param disableLocalConfig the {@link Integer} attribute value to be set
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> SetDisableLocalConfig(object value)
        {
            return Write(_attributes[ATTR_DISABLELOCALCONFIG], value);
        }

        /**
         * Get the <i>DisableLocalConfig</i> attribute [attribute ID <b>20</b>].
         * <p>
         * The DisableLocalConfig attribute allows a number of local device configuration
         * functions to be disabled.
         * <p>
         * The intention of this attribute is to allow disabling of any local configuration
         * user interface, for example to prevent reset or binding buttons being activated by
         * unauthorised persons in a public building.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetDisableLocalConfigAsync()
        {
            return Read(_attributes[ATTR_DISABLELOCALCONFIG]);
        }

        /**
         * Synchronously get the <i>DisableLocalConfig</i> attribute [attribute ID <b>20</b>].
         * <p>
         * The DisableLocalConfig attribute allows a number of local device configuration
         * functions to be disabled.
         * <p>
         * The intention of this attribute is to allow disabling of any local configuration
         * user interface, for example to prevent reset or binding buttons being activated by
         * unauthorised persons in a public building.
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
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
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
         * Get the <i>SWBuildID</i> attribute [attribute ID <b>16384</b>].
         * <p>
         * The SWBuildIDattribute represents a detailed, manufacturer-specific reference to the version of the software.
         * <p>
         * The attribute is of type {@link String}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result Task
         */
        public Task<CommandResult> GetSwBuildIdAsync()
        {
            return Read(_attributes[ATTR_SWBUILDID]);
        }

        /**
         * Synchronously get the <i>SWBuildID</i> attribute [attribute ID <b>16384</b>].
         * <p>
         * The SWBuildIDattribute represents a detailed, manufacturer-specific reference to the version of the software.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link String}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link String} attribute value, or null on error
         */
        public string GetSwBuildId(long refreshPeriod)
        {
            if (_attributes[ATTR_SWBUILDID].IsLastValueCurrent(refreshPeriod))
            {
                return (string)_attributes[ATTR_SWBUILDID].LastValue;
            }

            return (string)ReadSync(_attributes[ATTR_SWBUILDID]);
        }

        /**
         * The Reset to Factory Defaults Command
         * <p>
         * On receipt of this command, the device resets all the attributes of all its clusters
         * to their factory defaults. Note that ZigBee networking functionality,bindings, groups
         * or other persistent data are not affected by this command
         *
         * @return the {@link Task<CommandResult>} command result Task
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
