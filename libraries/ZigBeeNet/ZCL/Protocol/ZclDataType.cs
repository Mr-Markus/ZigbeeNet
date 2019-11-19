using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.ZCL.Protocol
{
    public class ZclDataType
    {
        private static Dictionary<int, ZclDataType> _codeTypeMapping;
               
        public string Label { get; set; }
        public Type DataClass { get; set; }
        public int Id { get; set; }
        public bool IsAnalog { get; set; }
        public DataType DataType { get; set; }

        public ZclDataType()
        {

        }

        private ZclDataType(string label, Type dataClass, int id, bool analogue, DataType dataType)
        {
            this.Label = label;
            this.DataClass = dataClass;
            this.Id = id;
            this.IsAnalog = analogue;
            this.DataType = dataType;
        }

        static ZclDataType()
        {
            _codeTypeMapping = new Dictionary<int, ZclDataType>();

            _codeTypeMapping[0x08] = new ZclDataType("8-bit data", typeof(byte), 0x08, false, DataType.DATA_8_BIT);
            _codeTypeMapping[0x09] = new ZclDataType("16-bit data", null, 0x09, false, DataType.DATA_16_BIT);
            _codeTypeMapping[0x0A] = new ZclDataType("24-bit data", null, 0x0A, false, DataType.DATA_24_BIT);
            _codeTypeMapping[0x0B] = new ZclDataType("32-bit data", null, 0x0B, false, DataType.DATA_32_BIT);
            _codeTypeMapping[0x0C] = new ZclDataType("40-bit data", null, 0x0C, false, DataType.DATA_40_BIT);
            _codeTypeMapping[0x0D] = new ZclDataType("48-bit data", null, 0x0D, false, DataType.DATA_48_BIT);
            _codeTypeMapping[0x0E] = new ZclDataType("56-bit data", null, 0x0E, false, DataType.DATA_56_BIT);
            _codeTypeMapping[0x0F] = new ZclDataType("64-bit data", null, 0x0F, false, DataType.DATA_64_BIT);
            _codeTypeMapping[0x10] = new ZclDataType("Boolean", typeof(bool), 0x10, false, DataType.BOOLEAN);
            _codeTypeMapping[0x18] = new ZclDataType("8-bit Bitmap", typeof(byte), 0x18, false, DataType.BITMAP_8_BIT);
            _codeTypeMapping[0x19] = new ZclDataType("16-bit Bitmap", typeof(ushort), 0x19, false, DataType.BITMAP_16_BIT);
            _codeTypeMapping[0x1A] = new ZclDataType("24-bit Bitmap", typeof(uint), 0x1A, false, DataType.BITMAP_24_BIT);
            _codeTypeMapping[0x1B] = new ZclDataType("32-bit Bitmap", typeof(uint), 0x1B, false, DataType.BITMAP_32_BIT);
            _codeTypeMapping[0x1C] = new ZclDataType("40-bit Bitmap", typeof(ulong), 0x1C, false, DataType.BITMAP_40_BIT);
            _codeTypeMapping[0x1D] = new ZclDataType("48-bit Bitmap", typeof(ulong), 0x1D, false, DataType.BITMAP_48_BIT);
            _codeTypeMapping[0x1E] = new ZclDataType("56-bit Bitmap", typeof(ulong), 0x1E, false, DataType.BITMAP_56_BIT);
            _codeTypeMapping[0x1F] = new ZclDataType("64-bit Bitmap", typeof(ulong), 0x1F, false, DataType.BITMAP_64_BIT);
            _codeTypeMapping[0x20] = new ZclDataType("Unsigned 8-bit integer", typeof(byte), 0x20, true, DataType.UNSIGNED_8_BIT_INTEGER);
            _codeTypeMapping[0x21] = new ZclDataType("Unsigned 16-bit integer", typeof(ushort), 0x21, true, DataType.UNSIGNED_16_BIT_INTEGER);
            _codeTypeMapping[0x22] = new ZclDataType("Unsigned 24-bit integer", typeof(uint), 0x22, true, DataType.UNSIGNED_24_BIT_INTEGER);
            _codeTypeMapping[0x23] = new ZclDataType("Unsigned 32-bit integer", typeof(uint), 0x23, true, DataType.UNSIGNED_32_BIT_INTEGER);
            _codeTypeMapping[0x24] = new ZclDataType("Unsigned 40-bit integer", typeof(ulong), 0x24, true, DataType.UNSIGNED_40_BIT_INTEGER);
            _codeTypeMapping[0x25] = new ZclDataType("Unsigned 48-bit integer", typeof(ulong), 0x25, true, DataType.UNSIGNED_48_BIT_INTEGER);
            _codeTypeMapping[0x26] = new ZclDataType("Unsigned 56-bit integer", typeof(ulong), 0x26, true, DataType.UNSIGNED_56_BIT_INTEGER);
            _codeTypeMapping[0x27] = new ZclDataType("Unsigned 64-bit integer", typeof(ulong), 0x27, true, DataType.UNSIGNED_64_BIT_INTEGER);
            _codeTypeMapping[0x28] = new ZclDataType("Signed 8-bit Integer", typeof(sbyte), 0x28, true, DataType.SIGNED_8_BIT_INTEGER);
            _codeTypeMapping[0x29] = new ZclDataType("Signed 16-bit Integer", typeof(short), 0x29, true, DataType.SIGNED_16_BIT_INTEGER);
            _codeTypeMapping[0x2A] = new ZclDataType("Signed 24-bit Integer", typeof(int), 0x2A, true, DataType.SIGNED_24_BIT_INTEGER);
            _codeTypeMapping[0x2B] = new ZclDataType("Signed 32-bit Integer", typeof(int), 0x2B, true, DataType.SIGNED_32_BIT_INTEGER);
            _codeTypeMapping[0x2C] = new ZclDataType("Signed 40-bit Integer", typeof(long), 0x2C, true, DataType.SIGNED_40_BIT_INTEGER);
            _codeTypeMapping[0x2D] = new ZclDataType("Signed 48-bit Integer", typeof(long), 0x2D, true, DataType.SIGNED_48_BIT_INTEGER);
            _codeTypeMapping[0x2E] = new ZclDataType("Signed 56-bit Integer", typeof(long), 0x2E, true, DataType.SIGNED_56_BIT_INTEGER);
            _codeTypeMapping[0x2F] = new ZclDataType("Signed 64-bit Integer", typeof(long), 0x2F, true, DataType.SIGNED_64_BIT_INTEGER);
            _codeTypeMapping[0x30] = new ZclDataType("8-bit Enumeration", typeof(byte), 0x30, false, DataType.ENUMERATION_8_BIT);
            _codeTypeMapping[0x31] = new ZclDataType("16-bit Enumeration", typeof(ushort), 0x31, false, DataType.ENUMERATION_16_BIT);
            _codeTypeMapping[0x32] = new ZclDataType("32-bit Enumeration", typeof(uint), 0x32, false, DataType.ENUMERATION_32_BIT);
            _codeTypeMapping[0x38] = new ZclDataType("Semi precision float", typeof(float), 0x38, true, DataType.FLOAT_16_BIT);
            _codeTypeMapping[0x39] = new ZclDataType("Single precision float", typeof(float), 0x39, true, DataType.FLOAT_32_BIT);
            _codeTypeMapping[0x3A] = new ZclDataType("Double precision float", typeof(double), 0x3A, true, DataType.FLOAT_64_BIT);
            _codeTypeMapping[0x41] = new ZclDataType("Octet string", typeof(ByteArray), 0x41, false, DataType.OCTET_STRING);
            _codeTypeMapping[0x42] = new ZclDataType("Character String", typeof(string), 0x42, false, DataType.CHARACTER_STRING);
            _codeTypeMapping[0x43] = new ZclDataType("Long Octet string", typeof(ByteArray), 0x43, false, DataType.LONG_OCTET_STRING);
            _codeTypeMapping[0x44] = new ZclDataType("Long Character String", typeof(string), 0x44, false, DataType.LONG_CHARACTER_STRING);
            _codeTypeMapping[0x48] = new ZclDataType("ARRAY", null, 0x48, false, DataType.ORDERED_SEQUENCE_ARRAY);
            _codeTypeMapping[0x4C] = new ZclDataType("ORDERED_SEQUENCE_STRUCTURE", null, 0x4C, false, DataType.ORDERED_SEQUENCE_STRUCTURE);
            _codeTypeMapping[0x50] = new ZclDataType("SET", null, 0x50, false, DataType.COLLECTION_SET);
            _codeTypeMapping[0x51] = new ZclDataType("COLLECTION_BAG", null, 0x51, false, DataType.COLLECTION_BAG);
            _codeTypeMapping[0xE0] = new ZclDataType("Time", null, 0xE0, true, DataType.TIME_OF_DAY);
            _codeTypeMapping[0xE1] = new ZclDataType("Date", null, 0xE1, true, DataType.DATE);
            _codeTypeMapping[0xE2] = new ZclDataType("UTCTime", typeof(DateTime), 0xE2, true, DataType.UTCTIME);
            _codeTypeMapping[0xE8] = new ZclDataType("ClusterId", typeof(int), 0xE8, false, DataType.CLUSTERID);
            _codeTypeMapping[0xE9] = new ZclDataType("AttributeId", null, 0xE9, false, DataType.ATTRIBUTEID);
            _codeTypeMapping[0xEA] = new ZclDataType("BACNetId", null, 0xEA, false, DataType.BACNET_OID);
            _codeTypeMapping[0xF0] = new ZclDataType("IEEE Address", typeof(IeeeAddress), 0xF0, false, DataType.IEEE_ADDRESS);
            _codeTypeMapping[0xF1] = new ZclDataType("ZigBee Key", typeof(ZigBeeKey), 0xF1, false, DataType.SECURITY_KEY);

            _codeTypeMapping[0x80] = new ZclDataType("Byte array", typeof(ByteArray), 0x00, false, DataType.BYTE_ARRAY);
            _codeTypeMapping[0x81] = new ZclDataType("N X Attribute identifier", typeof(ushort), 0x00, false, DataType.N_X_ATTRIBUTE_IDENTIFIER);
            _codeTypeMapping[0x82] = new ZclDataType("N X Attribute information", typeof(AttributeInformation), 0x00, false, DataType.N_X_ATTRIBUTE_INFORMATION);
            _codeTypeMapping[0x83] = new ZclDataType("N X Attribute record", typeof(AttributeRecord), 0x00, false, DataType.N_X_ATTRIBUTE_RECORD);
            _codeTypeMapping[0x84] = new ZclDataType("N X Attribute report", typeof(AttributeReport), 0x00, false, DataType.N_X_ATTRIBUTE_REPORT);
            _codeTypeMapping[0x85] = new ZclDataType("N X Attribute reporting status record", typeof(AttributeReportingStatusRecord), 0x00, false, DataType.N_X_ATTRIBUTE_REPORTING_STATUS_RECORD);
            _codeTypeMapping[0x86] = new ZclDataType("N X Attribute reporting configuration record", typeof(AttributeReportingConfigurationRecord), 0x00, false, DataType.N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD);
            _codeTypeMapping[0x87] = new ZclDataType("N X Attribute selector", typeof(object), 0x00, false, DataType.N_X_ATTRIBUTE_SELECTOR);
            _codeTypeMapping[0x88] = new ZclDataType("N X Attribute status record", typeof(AttributeStatusRecord), 0x00, false, DataType.N_X_ATTRIBUTE_STATUS_RECORD);
            _codeTypeMapping[0x89] = new ZclDataType("N x Extended Attribute Information", typeof(ExtendedAttributeInformation), 0x00, false, DataType.N_X_EXTENDED_ATTRIBUTE_INFORMATION);
            _codeTypeMapping[0x8A] = new ZclDataType("N X Extension field set", typeof(ExtensionFieldSet), 0x00, false, DataType.N_X_EXTENSION_FIELD_SET);
            _codeTypeMapping[0x8B] = new ZclDataType("N X Neighbors information", typeof(NeighborInformation), 0x00, false, DataType.N_X_NEIGHBORS_INFORMATION);
            _codeTypeMapping[0x8C] = new ZclDataType("N X Read attribute status record", typeof(ReadAttributeStatusRecord), 0x00, false, DataType.N_X_READ_ATTRIBUTE_STATUS_RECORD);
            _codeTypeMapping[0x8D] = new ZclDataType("N x Unsigned 8-bit Integer", typeof(List<byte>), 0x00, false, DataType.N_X_UNSIGNED_8_BIT_INTEGER);
            _codeTypeMapping[0x8E] = new ZclDataType("N X Unsigned 16-bit integer", typeof(List<ushort>), 0x00, false, DataType.N_X_UNSIGNED_16_BIT_INTEGER);
            _codeTypeMapping[0x8F] = new ZclDataType("N X Write attribute record", typeof(WriteAttributeRecord), 0x00, false, DataType.N_X_WRITE_ATTRIBUTE_RECORD);
            _codeTypeMapping[0x90] = new ZclDataType("N X Write attribute status record", typeof(WriteAttributeStatusRecord), 0x00, false, DataType.N_X_WRITE_ATTRIBUTE_STATUS_RECORD);
            _codeTypeMapping[0x91] = new ZclDataType("X Unsigned 8-bit integer", typeof(List<byte>), 0x00, false, DataType.X_UNSIGNED_8_BIT_INTEGER);
            _codeTypeMapping[0x92] = new ZclDataType("Zcl Status", typeof(ZclStatus), 0x00, false, DataType.ZCL_STATUS);
            _codeTypeMapping[0x93] = new ZclDataType("EXTENDED_PANID", typeof(ExtendedPanId), 0x00, false, DataType.EXTENDED_PANID);
            _codeTypeMapping[0x94] = new ZclDataType("Binding Table", typeof(BindingTable), 0x00, false, DataType.BINDING_TABLE);
            _codeTypeMapping[0x95] = new ZclDataType("Complex Descriptor", typeof(ComplexDescriptor), 0x00, false, DataType.COMPLEX_DESCRIPTOR);
            _codeTypeMapping[0x96] = new ZclDataType("Endpoint", typeof(int), 0x00, false, DataType.ENDPOINT);
            _codeTypeMapping[0x97] = new ZclDataType("Neighbor Table", typeof(NeighborTable), 0x00, false, DataType.NEIGHBOR_TABLE);
            _codeTypeMapping[0x98] = new ZclDataType("Node Descriptor", typeof(NodeDescriptor), 0x00, false, DataType.NODE_DESCRIPTOR);
            _codeTypeMapping[0x99] = new ZclDataType("NWK address", typeof(ushort), 0x00, false, DataType.NWK_ADDRESS);
            _codeTypeMapping[0x9A] = new ZclDataType("N x Binding Table", typeof(BindingTable), 0x00, false, DataType.N_X_BINDING_TABLE);
            _codeTypeMapping[0x9B] = new ZclDataType("N X IEEE Address", typeof(long), 0x00, false, DataType.N_X_IEEE_ADDRESS);
            _codeTypeMapping[0x9C] = new ZclDataType("Power Descriptor", typeof(PowerDescriptor), 0x00, false, DataType.POWER_DESCRIPTOR);
            _codeTypeMapping[0x9D] = new ZclDataType("Routing Table", typeof(RoutingTable), 0x00, false, DataType.ROUTING_TABLE);
            _codeTypeMapping[0x9E] = new ZclDataType("Simple Descriptor", typeof(SimpleDescriptor), 0x00, false, DataType.SIMPLE_DESCRIPTOR);
            _codeTypeMapping[0x9F] = new ZclDataType("User Descriptor", typeof(UserDescriptor), 0x00, false, DataType.USER_DESCRIPTOR);
            _codeTypeMapping[0xA0] = new ZclDataType("Zdo Status", typeof(ZdoStatus), 0x00, false, DataType.ZDO_STATUS);
            _codeTypeMapping[0xA1] = new ZclDataType("Unsigned 8 bit Integer Array", typeof(byte[]), 0x00, false, DataType.UNSIGNED_8_BIT_INTEGER_ARRAY);
            _codeTypeMapping[0xA2] = new ZclDataType("RAW_OCTET", typeof(ByteArray), 0x00, false, DataType.RAW_OCTET);
            _codeTypeMapping[0xA3] = new ZclDataType("ZigBee Data Type", typeof(ZclDataType), 0x00, false, DataType.ZIGBEE_DATA_TYPE);
        }

        public static ZclDataType Get(int id)
        {
            return _codeTypeMapping[id];
        }

        public static ZclDataType Get(DataType type)
        {
            return _codeTypeMapping.Values.Single(dt => dt.DataType == type);
        }

        public override string ToString()
        {
            return Label;
        }
    }
}

