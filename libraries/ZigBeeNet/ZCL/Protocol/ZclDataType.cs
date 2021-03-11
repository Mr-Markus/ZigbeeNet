using System;
using System.Collections.Generic;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.ZCL.Protocol
{
    public class ZclDataType
    {
        private static ZclDataType[] _codeTypeMapping;

        internal Type DataClass { get; set; }

        public string Label { get; set; }
        public byte Id { get; set; }
        public bool IsAnalog { get; set; }
        public DataType DataType { get; set; }

        public ZclDataType()
        {

        }

        private ZclDataType(string label, Type dataClass, byte id, bool analogue, DataType dataType)
        {
            this.Label = label;
            this.DataClass = dataClass;
            this.Id = id;
            this.IsAnalog = analogue;
            this.DataType = dataType;
        }

        static ZclDataType()
        {
            _codeTypeMapping = new ZclDataType[256];
            void Add(ZclDataType zclDataType) => _codeTypeMapping[(byte)zclDataType.DataType]=zclDataType;

            Add( new ZclDataType("8-bit data", typeof(byte), 0x08, false, DataType.DATA_8_BIT) );
            Add( new ZclDataType("16-bit data", null, 0x09, false, DataType.DATA_16_BIT) );
            Add( new ZclDataType("24-bit data", null, 0x0A, false, DataType.DATA_24_BIT) );
            Add( new ZclDataType("32-bit data", null, 0x0B, false, DataType.DATA_32_BIT) );
            Add( new ZclDataType("40-bit data", null, 0x0C, false, DataType.DATA_40_BIT) );
            Add( new ZclDataType("48-bit data", null, 0x0D, false, DataType.DATA_48_BIT) );
            Add( new ZclDataType("56-bit data", null, 0x0E, false, DataType.DATA_56_BIT) );
            Add( new ZclDataType("64-bit data", null, 0x0F, false, DataType.DATA_64_BIT) );
            Add( new ZclDataType("Boolean", typeof(bool), 0x10, false, DataType.BOOLEAN) );
            Add( new ZclDataType("8-bit Bitmap", typeof(byte), 0x18, false, DataType.BITMAP_8_BIT) );
            Add( new ZclDataType("16-bit Bitmap", typeof(ushort), 0x19, false, DataType.BITMAP_16_BIT) );
            Add( new ZclDataType("24-bit Bitmap", typeof(uint), 0x1A, false, DataType.BITMAP_24_BIT) );
            Add( new ZclDataType("32-bit Bitmap", typeof(uint), 0x1B, false, DataType.BITMAP_32_BIT) );
            Add( new ZclDataType("40-bit Bitmap", typeof(ulong), 0x1C, false, DataType.BITMAP_40_BIT) );
            Add( new ZclDataType("48-bit Bitmap", typeof(ulong), 0x1D, false, DataType.BITMAP_48_BIT) );
            Add( new ZclDataType("56-bit Bitmap", typeof(ulong), 0x1E, false, DataType.BITMAP_56_BIT) );
            Add( new ZclDataType("64-bit Bitmap", typeof(ulong), 0x1F, false, DataType.BITMAP_64_BIT) );
            Add( new ZclDataType("Unsigned 8-bit integer", typeof(byte), 0x20, true, DataType.UNSIGNED_8_BIT_INTEGER) );
            Add( new ZclDataType("Unsigned 16-bit integer", typeof(ushort), 0x21, true, DataType.UNSIGNED_16_BIT_INTEGER) );
            Add( new ZclDataType("Unsigned 24-bit integer", typeof(uint), 0x22, true, DataType.UNSIGNED_24_BIT_INTEGER) );
            Add( new ZclDataType("Unsigned 32-bit integer", typeof(uint), 0x23, true, DataType.UNSIGNED_32_BIT_INTEGER) );
            Add( new ZclDataType("Unsigned 40-bit integer", typeof(ulong), 0x24, true, DataType.UNSIGNED_40_BIT_INTEGER) );
            Add( new ZclDataType("Unsigned 48-bit integer", typeof(ulong), 0x25, true, DataType.UNSIGNED_48_BIT_INTEGER) );
            Add( new ZclDataType("Unsigned 56-bit integer", typeof(ulong), 0x26, true, DataType.UNSIGNED_56_BIT_INTEGER) );
            Add( new ZclDataType("Unsigned 64-bit integer", typeof(ulong), 0x27, true, DataType.UNSIGNED_64_BIT_INTEGER) );
            Add( new ZclDataType("Signed 8-bit Integer", typeof(sbyte), 0x28, true, DataType.SIGNED_8_BIT_INTEGER) );
            Add( new ZclDataType("Signed 16-bit Integer", typeof(short), 0x29, true, DataType.SIGNED_16_BIT_INTEGER) );
            Add( new ZclDataType("Signed 24-bit Integer", typeof(int), 0x2A, true, DataType.SIGNED_24_BIT_INTEGER) );
            Add( new ZclDataType("Signed 32-bit Integer", typeof(int), 0x2B, true, DataType.SIGNED_32_BIT_INTEGER) );
            Add( new ZclDataType("Signed 40-bit Integer", typeof(long), 0x2C, true, DataType.SIGNED_40_BIT_INTEGER) );
            Add( new ZclDataType("Signed 48-bit Integer", typeof(long), 0x2D, true, DataType.SIGNED_48_BIT_INTEGER) );
            Add( new ZclDataType("Signed 56-bit Integer", typeof(long), 0x2E, true, DataType.SIGNED_56_BIT_INTEGER) );
            Add( new ZclDataType("Signed 64-bit Integer", typeof(long), 0x2F, true, DataType.SIGNED_64_BIT_INTEGER) );
            Add( new ZclDataType("8-bit Enumeration", typeof(byte), 0x30, false, DataType.ENUMERATION_8_BIT) );
            Add( new ZclDataType("16-bit Enumeration", typeof(ushort), 0x31, false, DataType.ENUMERATION_16_BIT) );
            Add( new ZclDataType("32-bit Enumeration", typeof(uint), 0x32, false, DataType.ENUMERATION_32_BIT) );
            Add( new ZclDataType("Semi precision float", typeof(float), 0x38, true, DataType.FLOAT_16_BIT) );
            Add( new ZclDataType("Single precision float", typeof(float), 0x39, true, DataType.FLOAT_32_BIT) );
            Add( new ZclDataType("Double precision float", typeof(double), 0x3A, true, DataType.FLOAT_64_BIT) );
            Add( new ZclDataType("Octet string", typeof(ByteArray), 0x41, false, DataType.OCTET_STRING) );
            Add( new ZclDataType("Character String", typeof(string), 0x42, false, DataType.CHARACTER_STRING) );
            Add( new ZclDataType("Long Octet string", typeof(ByteArray), 0x43, false, DataType.LONG_OCTET_STRING) );
            Add( new ZclDataType("Long Character String", typeof(string), 0x44, false, DataType.LONG_CHARACTER_STRING) );
            Add( new ZclDataType("ARRAY", null, 0x48, false, DataType.ORDERED_SEQUENCE_ARRAY) );
            Add( new ZclDataType("ORDERED_SEQUENCE_STRUCTURE", null, 0x4C, false, DataType.ORDERED_SEQUENCE_STRUCTURE) );
            Add( new ZclDataType("SET", null, 0x50, false, DataType.COLLECTION_SET) );
            Add( new ZclDataType("COLLECTION_BAG", null, 0x51, false, DataType.COLLECTION_BAG) );
            Add( new ZclDataType("Time", null, 0xE0, true, DataType.TIME_OF_DAY) );
            Add( new ZclDataType("Date", null, 0xE1, true, DataType.DATE) );
            Add( new ZclDataType("UTCTime", typeof(DateTime), 0xE2, true, DataType.UTCTIME) );
            Add( new ZclDataType("ClusterId", typeof(int), 0xE8, false, DataType.CLUSTERID) );
            Add( new ZclDataType("AttributeId", null, 0xE9, false, DataType.ATTRIBUTEID) );
            Add( new ZclDataType("BACNetId", null, 0xEA, false, DataType.BACNET_OID) );
            Add( new ZclDataType("IEEE Address", typeof(IeeeAddress), 0xF0, false, DataType.IEEE_ADDRESS) );
            Add( new ZclDataType("ZigBee Key", typeof(ZigBeeKey), 0xF1, false, DataType.SECURITY_KEY) );

            Add( new ZclDataType("Byte array", typeof(ByteArray), 0x00, false, DataType.BYTE_ARRAY) );
            Add( new ZclDataType("N X Attribute identifier", typeof(ushort), 0x00, false, DataType.N_X_ATTRIBUTE_IDENTIFIER) );
            Add( new ZclDataType("N X Attribute information", typeof(AttributeInformation), 0x00, false, DataType.N_X_ATTRIBUTE_INFORMATION) );
            Add( new ZclDataType("N X Attribute record", typeof(AttributeRecord), 0x00, false, DataType.N_X_ATTRIBUTE_RECORD) );
            Add( new ZclDataType("N X Attribute report", typeof(AttributeReport), 0x00, false, DataType.N_X_ATTRIBUTE_REPORT) );
            Add( new ZclDataType("N X Attribute reporting status record", typeof(AttributeReportingStatusRecord), 0x00, false, DataType.N_X_ATTRIBUTE_REPORTING_STATUS_RECORD) );
            Add( new ZclDataType("N X Attribute reporting configuration record", typeof(AttributeReportingConfigurationRecord), 0x00, false, DataType.N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD) );
            Add( new ZclDataType("N X Attribute selector", typeof(object), 0x00, false, DataType.N_X_ATTRIBUTE_SELECTOR) );
            Add( new ZclDataType("N X Attribute status record", typeof(AttributeStatusRecord), 0x00, false, DataType.N_X_ATTRIBUTE_STATUS_RECORD) );
            Add( new ZclDataType("N x Extended Attribute Information", typeof(ExtendedAttributeInformation), 0x00, false, DataType.N_X_EXTENDED_ATTRIBUTE_INFORMATION) );
            Add( new ZclDataType("N X Extension field set", typeof(ExtensionFieldSet), 0x00, false, DataType.N_X_EXTENSION_FIELD_SET) );
            Add( new ZclDataType("N X Neighbors information", typeof(NeighborInformation), 0x00, false, DataType.N_X_NEIGHBORS_INFORMATION) );
            Add( new ZclDataType("N X Read attribute status record", typeof(ReadAttributeStatusRecord), 0x00, false, DataType.N_X_READ_ATTRIBUTE_STATUS_RECORD) );
            Add( new ZclDataType("N x Unsigned 8-bit Integer", typeof(List<byte>), 0x00, false, DataType.N_X_UNSIGNED_8_BIT_INTEGER) );
            Add( new ZclDataType("N X Unsigned 16-bit integer", typeof(List<ushort>), 0x00, false, DataType.N_X_UNSIGNED_16_BIT_INTEGER) );
            Add( new ZclDataType("N X Write attribute record", typeof(WriteAttributeRecord), 0x00, false, DataType.N_X_WRITE_ATTRIBUTE_RECORD) );
            Add( new ZclDataType("N X Write attribute status record", typeof(WriteAttributeStatusRecord), 0x00, false, DataType.N_X_WRITE_ATTRIBUTE_STATUS_RECORD) );
            Add( new ZclDataType("X Unsigned 8-bit integer", typeof(List<byte>), 0x00, false, DataType.X_UNSIGNED_8_BIT_INTEGER) );
            Add( new ZclDataType("Zcl Status", typeof(ZclStatus), 0x00, false, DataType.ZCL_STATUS) );
            Add( new ZclDataType("EXTENDED_PANID", typeof(ExtendedPanId), 0x00, false, DataType.EXTENDED_PANID) );
            Add( new ZclDataType("Binding Table", typeof(BindingTable), 0x00, false, DataType.BINDING_TABLE) );
            Add( new ZclDataType("Complex Descriptor", typeof(ComplexDescriptor), 0x00, false, DataType.COMPLEX_DESCRIPTOR) );
            Add( new ZclDataType("Endpoint", typeof(int), 0x00, false, DataType.ENDPOINT) );
            Add( new ZclDataType("Neighbor Table", typeof(NeighborTable), 0x00, false, DataType.NEIGHBOR_TABLE) );
            Add( new ZclDataType("Node Descriptor", typeof(NodeDescriptor), 0x00, false, DataType.NODE_DESCRIPTOR) );
            Add( new ZclDataType("NWK address", typeof(ushort), 0x00, false, DataType.NWK_ADDRESS) );
            Add( new ZclDataType("N x Binding Table", typeof(BindingTable), 0x00, false, DataType.N_X_BINDING_TABLE) );
            Add( new ZclDataType("N X IEEE Address", typeof(ulong), 0x00, false, DataType.N_X_IEEE_ADDRESS) );
            Add( new ZclDataType("Power Descriptor", typeof(PowerDescriptor), 0x00, false, DataType.POWER_DESCRIPTOR) );
            Add( new ZclDataType("Routing Table", typeof(RoutingTable), 0x00, false, DataType.ROUTING_TABLE) );
            Add( new ZclDataType("Simple Descriptor", typeof(SimpleDescriptor), 0x00, false, DataType.SIMPLE_DESCRIPTOR) );
            Add( new ZclDataType("User Descriptor", typeof(UserDescriptor), 0x00, false, DataType.USER_DESCRIPTOR) );
            Add( new ZclDataType("Zdo Status", typeof(ZdoStatus), 0x00, false, DataType.ZDO_STATUS) );
            Add( new ZclDataType("Unsigned 8 bit Integer Array", typeof(byte[]), 0x00, false, DataType.UNSIGNED_8_BIT_INTEGER_ARRAY) );
            Add( new ZclDataType("RAW_OCTET", typeof(ByteArray), 0x00, false, DataType.RAW_OCTET) );
            Add( new ZclDataType("ZigBee Data Type", typeof(ZclDataType), 0x00, false, DataType.ZIGBEE_DATA_TYPE) );
        }

        public static ZclDataType Get(byte id)
        {
            return _codeTypeMapping[id];
        } 
        
        public static ZclDataType Get(DataType type)
        {
            return _codeTypeMapping[(byte)type];
        }
        
        public override string ToString()
        {
            return Label;
        }
    }
}

