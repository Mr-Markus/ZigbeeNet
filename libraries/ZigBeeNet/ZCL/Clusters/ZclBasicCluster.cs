
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Basic;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Basic cluster implementation (Cluster ID 0x0000).
    ///
    /// This cluster supports an interface to the node or physical device. It provides
    /// attributes and commands for determining basic information, setting user information
    /// such as location, and resetting to factory defaults.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclBasicCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0000;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Basic";

        // Attribute constants

        /// <summary>
        /// The ZCLVersion attribute is 8 bits in length and specifies the version number of the
        /// ZigBee Cluster Library that all clusters on this endpoint conform to.
        /// </summary>
        public const ushort ATTR_ZCLVERSION = 0x0000;

        /// <summary>
        /// The ApplicationVersion attribute is 8 bits in length and specifies the version
        /// number of the application software contained in the device. The usage of this
        /// attribute is manufacturer dependent.
        /// </summary>
        public const ushort ATTR_APPLICATIONVERSION = 0x0001;

        /// <summary>
        /// The StackVersion attribute is 8 bits in length and specifies the version number of
        /// the implementation of the ZigBee stack contained in the device. The usage of this
        /// attribute is manufacturer dependent.
        /// </summary>
        public const ushort ATTR_STACKVERSION = 0x0002;

        /// <summary>
        /// The HWVersion attribute is 8 bits in length and specifies the version number of the
        /// hardware of the device. The usage of this attribute is manufacturer dependent.
        /// </summary>
        public const ushort ATTR_HWVERSION = 0x0003;

        /// <summary>
        /// The ManufacturerName attribute is a maximum of 32 bytes in length and specifies the
        /// name of the manufacturer as a ZigBee character string.
        /// </summary>
        public const ushort ATTR_MANUFACTURERNAME = 0x0004;

        /// <summary>
        /// The ModelIdentifier attribute is a maximum of 32 bytes in length and specifies the
        /// model number (or other identifier) assigned by the manufacturer as a ZigBee
        /// character string.
        /// </summary>
        public const ushort ATTR_MODELIDENTIFIER = 0x0005;

        /// <summary>
        /// The DateCode attribute is a ZigBee character string with a maximum length of 16
        /// bytes. The first 8 characters specify the date of manufacturer of the device in
        /// international date notation according to ISO 8601, i.e. YYYYMMDD, e.g. 20060814.
        /// </summary>
        public const ushort ATTR_DATECODE = 0x0006;

        /// <summary>
        /// The PowerSource attribute is 8 bits in length and specifies the source(s) of power
        /// available to the device. Bits b0–b6 of this attribute represent the primary power
        /// source of the device and bit b7 indicates whether the device has a secondary power
        /// source in the form of a battery backup.
        /// </summary>
        public const ushort ATTR_POWERSOURCE = 0x0007;

        /// <summary>
        ///         /// </summary>
        public const ushort ATTR_GENERICDEVICECLASS = 0x0008;

        /// <summary>
        ///         /// </summary>
        public const ushort ATTR_GENERICDEVICETYPE = 0x0009;

        /// <summary>
        ///         /// </summary>
        public const ushort ATTR_PRODUCTCODE = 0x000A;

        /// <summary>
        ///         /// </summary>
        public const ushort ATTR_PRODUCTURL = 0x000B;

        /// <summary>
        /// The LocationDescription attribute is a maximum of 16 bytes in length and describes
        /// the physical location of the device as a ZigBee character string.
        /// </summary>
        public const ushort ATTR_LOCATIONDESCRIPTION = 0x0010;

        /// <summary>
        /// The PhysicalEnvironment attribute is 8 bits in length and specifies the type of
        /// physical environment in which the device will operate.
        /// </summary>
        public const ushort ATTR_PHYSICALENVIRONMENT = 0x0011;

        /// <summary>
        /// The DeviceEnabled attribute is a boolean and specifies whether the device is
        /// enabled or disabled.
        /// </summary>
        public const ushort ATTR_DEVICEENABLED = 0x0012;

        /// <summary>
        /// The AlarmMask attribute is 8 bits in length and specifies which of a number of
        /// general alarms may be generated.
        /// </summary>
        public const ushort ATTR_ALARMMASK = 0x0013;

        /// <summary>
        /// The DisableLocalConfig attribute allows a number of local device configuration
        /// functions to be disabled.
        /// The intention of this attribute is to allow disabling of any local configuration
        /// user interface, for example to prevent reset or binding buttons being activated by
        /// unauthorised persons in a public building.
        /// </summary>
        public const ushort ATTR_DISABLELOCALCONFIG = 0x0014;

        /// <summary>
        /// The SWBuildIDattribute represents a detailed, manufacturer-specific
        /// reference to the version of the software.
        /// </summary>
        public const ushort ATTR_SWBUILDID = 0x4000;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(18);

            attributeMap.Add(ATTR_ZCLVERSION, new ZclAttribute(this, ATTR_ZCLVERSION, "ZCL Version", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APPLICATIONVERSION, new ZclAttribute(this, ATTR_APPLICATIONVERSION, "Application Version", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_STACKVERSION, new ZclAttribute(this, ATTR_STACKVERSION, "Stack Version", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_HWVERSION, new ZclAttribute(this, ATTR_HWVERSION, "HW Version", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MANUFACTURERNAME, new ZclAttribute(this, ATTR_MANUFACTURERNAME, "Manufacturer Name", ZclDataType.Get(DataType.CHARACTER_STRING), true, true, false, false));
            attributeMap.Add(ATTR_MODELIDENTIFIER, new ZclAttribute(this, ATTR_MODELIDENTIFIER, "Model Identifier", ZclDataType.Get(DataType.CHARACTER_STRING), true, true, false, false));
            attributeMap.Add(ATTR_DATECODE, new ZclAttribute(this, ATTR_DATECODE, "Date Code", ZclDataType.Get(DataType.CHARACTER_STRING), true, true, false, false));
            attributeMap.Add(ATTR_POWERSOURCE, new ZclAttribute(this, ATTR_POWERSOURCE, "Power Source", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_GENERICDEVICECLASS, new ZclAttribute(this, ATTR_GENERICDEVICECLASS, "Generic Device Class", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_GENERICDEVICETYPE, new ZclAttribute(this, ATTR_GENERICDEVICETYPE, "Generic Device Type", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_PRODUCTCODE, new ZclAttribute(this, ATTR_PRODUCTCODE, "Product Code", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, false, false));
            attributeMap.Add(ATTR_PRODUCTURL, new ZclAttribute(this, ATTR_PRODUCTURL, "Product URL", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, false, false));
            attributeMap.Add(ATTR_LOCATIONDESCRIPTION, new ZclAttribute(this, ATTR_LOCATIONDESCRIPTION, "Location Description", ZclDataType.Get(DataType.CHARACTER_STRING), true, true, true, false));
            attributeMap.Add(ATTR_PHYSICALENVIRONMENT, new ZclAttribute(this, ATTR_PHYSICALENVIRONMENT, "Physical Environment", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, true, false));
            attributeMap.Add(ATTR_DEVICEENABLED, new ZclAttribute(this, ATTR_DEVICEENABLED, "Device Enabled", ZclDataType.Get(DataType.BOOLEAN), true, true, true, false));
            attributeMap.Add(ATTR_ALARMMASK, new ZclAttribute(this, ATTR_ALARMMASK, "Alarm Mask", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, true, false));
            attributeMap.Add(ATTR_DISABLELOCALCONFIG, new ZclAttribute(this, ATTR_DISABLELOCALCONFIG, "Disable Local Config", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, true, false));
            attributeMap.Add(ATTR_SWBUILDID, new ZclAttribute(this, ATTR_SWBUILDID, "SW Build ID", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(1);

            commandMap.Add(0x0000, () => new ResetToFactoryDefaultsCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Basic cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclBasicCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Reset To Factory Defaults Command
        ///
        /// On receipt of this command, the device resets all the attributes of all its clusters
        /// to their factory defaults. Note that ZigBee networking functionality,bindings,
        /// groups or other persistent data are not affected by this command
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ResetToFactoryDefaultsCommand()
        {
            return Send(new ResetToFactoryDefaultsCommand());
        }
    }
}
