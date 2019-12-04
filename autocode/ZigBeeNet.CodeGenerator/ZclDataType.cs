using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CodeGenerator
{
	public static class ZclDataType
	{
		public static readonly Dictionary<string, DataTypeMap> Mapping;

		static ZclDataType()
		{
			Mapping = new Dictionary<string, DataTypeMap>
			{
				{ "IEEE_ADDRESS", new DataTypeMap("IeeeAddress", 0xf0, 8, false, 0xffffffff) },
                { "SECURITY_KEY", new DataTypeMap("ZigBeeKey", 0xf1, 16, false) },
                //already defined below
                //{ "EXTENDED_PANID", new DataTypeMap("long", 0, 0, false) },
                { "NODE_DESCRIPTOR", new DataTypeMap("NodeDescriptor", 0, 0, false) },
				{ "SIMPLE_DESCRIPTOR", new DataTypeMap("SimpleDescriptor", 0, 0, false) },
				{ "COMPLEX_DESCRIPTOR", new DataTypeMap("ComplexDescriptor", 0, 0, false) },
				{ "POWER_DESCRIPTOR", new DataTypeMap("PowerDescriptor", 0, 0, false) },
				{ "USER_DESCRIPTOR", new DataTypeMap("UserDescriptor", 0, 0, false) },
				{ "NEIGHBOR_TABLE", new DataTypeMap("NeighborTable", 0, 0, false) },
				{ "ROUTING_TABLE", new DataTypeMap("RoutingTable", 0, 0, false) },
				{ "NWK_ADDRESS", new DataTypeMap("ushort", 0, 0, false, 0xffff) },
				{ "N_X_IEEE_ADDRESS", new DataTypeMap("List<ulong>", 0, 0, false) },
				{ "N_X_NWK_ADDRESS", new DataTypeMap("List<ushort>", 0, 0, false) },
				{ "CLUSTERID", new DataTypeMap("ushort", 0, 0, false) },
				{ "N_X_CLUSTERID", new DataTypeMap("List<ushort>", 0, 0, false) },
				{ "ENDPOINT", new DataTypeMap("byte", 0, 0, false) },
				{ "N_X_ENDPOINT", new DataTypeMap("List<byte>", 0, 0, false) },
				{ "N_X_EXTENSION_FIELD_SET", new DataTypeMap("List<ExtensionFieldSet>", 0, 0, false) },
				{ "N_X_NEIGHBORS_INFORMATION", new DataTypeMap("List<NeighborInformation>", 0, 0, false) },
				{ "N_X_UNSIGNED_16_BIT_INTEGER", new DataTypeMap("List<ushort>", 0, 0, false) },
				{ "UNSIGNED_8_BIT_INTEGER_ARRAY", new DataTypeMap("byte[]", 0, 0, false) },
				{ "X_UNSIGNED_8_BIT_INTEGER", new DataTypeMap("List<byte>", 0, 0, false) },
				{ "N_X_UNSIGNED_8_BIT_INTEGER", new DataTypeMap("List<byte>", 0, 0, false) },
				{ "N_X_ATTRIBUTE_IDENTIFIER", new DataTypeMap("List<ushort>", 0, 0, false) },
				{ "N_X_READ_ATTRIBUTE_STATUS_RECORD", new DataTypeMap("List<ReadAttributeStatusRecord>", 0, 0, false) },
				{ "N_X_WRITE_ATTRIBUTE_RECORD", new DataTypeMap("List<WriteAttributeRecord>", 0, 0, false) },
				{ "N_X_WRITE_ATTRIBUTE_STATUS_RECORD", new DataTypeMap("List<WriteAttributeStatusRecord>", 0, 0, false) },
				{ "N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD", new DataTypeMap("List<AttributeReportingConfigurationRecord>", 0, 0, false) },
                { "N_X_ATTRIBUTE_REPORTING_STATUS_RECORD", new DataTypeMap("List<AttributeReportingStatusRecord>", 0, 0, false) },
                { "N_X_ATTRIBUTE_STATUS_RECORD", new DataTypeMap("List<AttributeStatusRecord>", 0, 0, false) },
				{ "N_X_ATTRIBUTE_RECORD", new DataTypeMap("List<AttributeRecord>", 0, 0, false) },
				{ "N_X_ATTRIBUTE_REPORT", new DataTypeMap("List<AttributeReport>", 0, 0, false) },
				{ "N_X_ATTRIBUTE_INFORMATION", new DataTypeMap("List<AttributeInformation>", 0, 0, false) },
				{ "N_X_ATTRIBUTE_SELECTOR", new DataTypeMap("object", 0, 0, false) },
				{ "N_X_EXTENDED_ATTRIBUTE_INFORMATION", new DataTypeMap("List<ExtendedAttributeInformation>", 0, 0, false) },
				{ "BOOLEAN", new DataTypeMap("bool", 0x10, 1, false, 0xff) },
				{ "SIGNED_8_BIT_INTEGER", new DataTypeMap("sbyte", 0x28, 1, true, 0x80) },
				{ "SIGNED_16_BIT_INTEGER", new DataTypeMap("short", 0x29, 2, true, 0x8000) },
                { "SIGNED_24_BIT_INTEGER", new DataTypeMap("int", 0x2a, 3, true, 0x800000) },
                { "SIGNED_32_BIT_INTEGER", new DataTypeMap("int", 0x2b, 4, true, 0x80000000) },
				{ "UNSIGNED_8_BIT_INTEGER", new DataTypeMap("byte", 0x20, 1, true, 0xff) },
				{ "UNSIGNED_16_BIT_INTEGER", new DataTypeMap("ushort", 0x21, 2, true, 0xffff) },
				{ "UNSIGNED_24_BIT_INTEGER", new DataTypeMap("uint", 0x22, 3, true, 0xffffff) },
				{ "UNSIGNED_32_BIT_INTEGER", new DataTypeMap("uint", 0x23, 4, true, 0xffffffff) },
				{ "UNSIGNED_40_BIT_INTEGER", new DataTypeMap("ulong", 0x24, 4, true, 0) },// 0xffffffffff));
                { "UNSIGNED_48_BIT_INTEGER", new DataTypeMap("ulong", 0x25, 6, true, 0) },// 0xffffffffffff));
                { "BITMAP_8_BIT", new DataTypeMap("byte", 0x18, 1, false) },
				{ "BITMAP_16_BIT", new DataTypeMap("ushort", 0x19, 2, false) },
				{ "BITMAP_24_BIT", new DataTypeMap("int", 0x1a, 3, false) },
                { "BITMAP_32_BIT", new DataTypeMap("int", 0x1b, 4, false) },
                { "BITMAP_40_BIT", new DataTypeMap("Integer", 0x1c, 5, false) },
                { "BITMAP_48_BIT", new DataTypeMap("Integer", 0x1d, 6, false) },
                { "BITMAP_64_BIT", new DataTypeMap("Integer", 0x1f, 8, false) },
                { "ENUMERATION_8_BIT", new DataTypeMap("byte", 0x30, 1, false, 0xff) },
                { "ENUMERATION_16_BIT", new DataTypeMap("ushort", 0x31, 2, false, 0xffff) },
                { "ENUMERATION_32_BIT", new DataTypeMap("int", 0x33, 4, false, 0xffffffff) },
                { "FLOAT_32_BIT", new DataTypeMap("double", 0x39, 4, true) },
				{ "DATA_8_BIT", new DataTypeMap("byte", 0x08, 1, false) },
				{ "OCTET_STRING", new DataTypeMap("ByteArray", 0x41, -1, false) },
                { "CHARACTER_STRING", new DataTypeMap("string", 0x42, -1, false) },
                { "LONG_OCTET_STRING", new DataTypeMap("ByteArray", 0x43, -1, false) },
                { "LONG_CHARACTER_STRING", new DataTypeMap("String", 0x44, -1, false) },
                { "UTCTIME", new DataTypeMap("DateTime", 0xe2, 4, true, 0xffffffff) },
				{ "ZDO_STATUS", new DataTypeMap("ZdoStatus", 0, 0, false) },
				{ "ZCL_STATUS", new DataTypeMap("ZclStatus", 0, 0, false) },
				{ "ZIGBEE_DATA_TYPE", new DataTypeMap("ZclDataType", 0, 0, false) },
				{ "EXTENDED_PANID", new DataTypeMap("ExtendedPanId", 0, 0, false) },
				{ "BINDING_TABLE", new DataTypeMap("BindingTable", 0, 0, false) },
				{ "N_X_BINDING_TABLE", new DataTypeMap("List<BindingTable>", 0, 0, false) },
				{ "BYTE_ARRAY", new DataTypeMap("ByteArray", 0, 0, false) },
                { "RAW_OCTET", new DataTypeMap("ByteArray", 0, 0, false) },
				{ "IMAGE_UPGRADE_STATUS", new DataTypeMap("ImageUpgradeStatus", 0, 0, false) }
			};
		}
	}
}
