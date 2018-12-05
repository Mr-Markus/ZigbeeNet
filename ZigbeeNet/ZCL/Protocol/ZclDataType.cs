using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Fileld;

namespace ZigBeeNet.ZCL.Protocol
{

    public enum DataType
    {
        BITMAP_16_BIT,
        BITMAP_32_BIT,
        BITMAP_8_BIT,
        BOOLEAN,
        BYTE_ARRAY,
        CHARACTER_STRING,
        DATA_8_BIT,
        ENUMERATION_16_BIT,
        ENUMERATION_8_BIT,
        IEEE_ADDRESS,
        N_X_ATTRIBUTE_IDENTIFIER,
        N_X_ATTRIBUTE_INFORMATION,
        N_X_ATTRIBUTE_RECORD,
        N_X_ATTRIBUTE_REPORT,
        N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD,
        N_X_ATTRIBUTE_SELECTOR,
        N_X_ATTRIBUTE_STATUS_RECORD,
        N_X_EXTENDED_ATTRIBUTE_INFORMATION,
        N_X_EXTENSION_FIELD_SET,
        N_X_NEIGHBORS_INFORMATION,
        N_X_READ_ATTRIBUTE_STATUS_RECORD,
        N_X_UNSIGNED_16_BIT_INTEGER,
        N_X_UNSIGNED_8_BIT_INTEGER,
        N_X_WRITE_ATTRIBUTE_RECORD,
        N_X_WRITE_ATTRIBUTE_STATUS_RECORD,
        OCTET_STRING,
        SIGNED_16_BIT_INTEGER,
        SIGNED_32_BIT_INTEGER,
        SIGNED_8_BIT_INTEGER,
        UNSIGNED_16_BIT_INTEGER,
        UNSIGNED_24_BIT_INTEGER,
        UNSIGNED_32_BIT_INTEGER,
        UNSIGNED_48_BIT_INTEGER,
        UNSIGNED_8_BIT_INTEGER,
        UTCTIME,
        X_UNSIGNED_8_BIT_INTEGER,
        ZCL_STATUS,
        EXTENDED_PANID,
        BINDING_TABLE,
        CLUSTERID,
        COMPLEX_DESCRIPTOR,
        ENDPOINT,
        NEIGHBOR_TABLE,
        NODE_DESCRIPTOR,
        NWK_ADDRESS,
        N_X_BINDING_TABLE,
        N_X_IEEE_ADDRESS,
        POWER_DESCRIPTOR,
        ROUTING_TABLE,
        SIMPLE_DESCRIPTOR,
        USER_DESCRIPTOR,
        ZDO_STATUS,
        UNSIGNED_8_BIT_INTEGER_ARRAY,
        ZIGBEE_DATA_TYPE
    }

    public class ZclDataType
    {
        private static Dictionary<int, ZclDataType> _codeTypeMapping;


        public string Label { get; private set; }
        public Type DataClass { get; private set; }
        public int Id { get; private set; }
        public bool IsAnalog { get; private set; }
        public DataType DataType { get; private set; }


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

            _codeTypeMapping[0x19] = new ZclDataType("16-bit Bitmap", typeof(int), 0x19, false, DataType.BITMAP_16_BIT);
            _codeTypeMapping[0x1B] = new ZclDataType("32-bit Bitmap", typeof(int), 0x1B, false, DataType.BITMAP_32_BIT);
            _codeTypeMapping[0x18] = new ZclDataType("Bitmap 8-bit", typeof(int), 0x18, false, DataType.BITMAP_8_BIT);
            _codeTypeMapping[0x10] = new ZclDataType("Boolean", typeof(bool), 0x10, false, DataType.BOOLEAN);
            _codeTypeMapping[0x00] = new ZclDataType("Byte array", typeof(ByteArray), 0x00, false, DataType.BYTE_ARRAY);
            _codeTypeMapping[0x42] = new ZclDataType("Character String", typeof(string), 0x42, false, DataType.CHARACTER_STRING);
            _codeTypeMapping[0x08] = new ZclDataType("8-bit data", typeof(int), 0x08, false, DataType.DATA_8_BIT);
            _codeTypeMapping[0x31] = new ZclDataType("16-bit enumeration", typeof(int), 0x31, false, DataType.ENUMERATION_16_BIT);
            _codeTypeMapping[0x30] = new ZclDataType("8-bit Enumeration", typeof(int), 0x30, false, DataType.ENUMERATION_8_BIT);
            _codeTypeMapping[0xF0] = new ZclDataType("IEEE Address", typeof(IeeeAddress), 0xF0, false, DataType.IEEE_ADDRESS);
            _codeTypeMapping[0x00] = new ZclDataType("N X Attribute identifier", typeof(int), 0x00, false, DataType.N_X_ATTRIBUTE_IDENTIFIER);
            _codeTypeMapping[0x00] = new ZclDataType("N X Attribute information", typeof(AttributeInformation), 0x00, false, DataType.N_X_ATTRIBUTE_INFORMATION);
            _codeTypeMapping[0x00] = new ZclDataType("N X Attribute record", typeof(AttributeRecord), 0x00, false, DataType.N_X_ATTRIBUTE_RECORD);
            _codeTypeMapping[0x00] = new ZclDataType("N X Attribute report", typeof(AttributeReport), 0x00, false, DataType.N_X_ATTRIBUTE_REPORT);
            _codeTypeMapping[0x00] = new ZclDataType("N X Attribute reporting configuration record", typeof(AttributeReportingConfigurationRecord), 0x00, false, DataType.N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD);
            _codeTypeMapping[0x00] = new ZclDataType("N X Attribute selector", typeof(object), 0x00, false, DataType.N_X_ATTRIBUTE_SELECTOR);
            _codeTypeMapping[0x00] = new ZclDataType("N X Attribute status record", typeof(AttributeStatusRecord), 0x00, false, DataType.N_X_ATTRIBUTE_STATUS_RECORD);
            _codeTypeMapping[0x00] = new ZclDataType("N x Extended Attribute Information", typeof(ExtendedAttributeInformation), 0x00, false, DataType.N_X_EXTENDED_ATTRIBUTE_INFORMATION);
            _codeTypeMapping[0x00] = new ZclDataType("N X Extension field set", typeof(ExtensionFieldSet), 0x00, false, DataType.N_X_EXTENSION_FIELD_SET);
            _codeTypeMapping[0x00] = new ZclDataType("N X Neighbors information", typeof(NeighborInformation), 0x00, false, DataType.N_X_NEIGHBORS_INFORMATION);
            _codeTypeMapping[0x00] = new ZclDataType("N X Read attribute status record", typeof(ReadAttributeStatusRecord), 0x00, false, DataType.N_X_READ_ATTRIBUTE_STATUS_RECORD);
            _codeTypeMapping[0x00] = new ZclDataType("N X Unsigned 16-bit integer", typeof(int), 0x00, false, DataType.N_X_UNSIGNED_16_BIT_INTEGER);
            _codeTypeMapping[0x00] = new ZclDataType("N x Unsigned 8-bit Integer", typeof(int), 0x00, false, DataType.N_X_UNSIGNED_8_BIT_INTEGER);
            _codeTypeMapping[0x00] = new ZclDataType("N X Write attribute record", typeof(WriteAttributeRecord), 0x00, false, DataType.N_X_WRITE_ATTRIBUTE_RECORD);
            _codeTypeMapping[0x00] = new ZclDataType("N X Write attribute status record", typeof(WriteAttributeStatusRecord), 0x00, false, DataType.N_X_WRITE_ATTRIBUTE_STATUS_RECORD);
            _codeTypeMapping[0x41] = new ZclDataType("Octet string", typeof(ByteArray), 0x41, false, DataType.OCTET_STRING);
            _codeTypeMapping[0x29] = new ZclDataType("Signed 16-bit Integer", typeof(int), 0x29, true, DataType.SIGNED_16_BIT_INTEGER);
            _codeTypeMapping[0x2B] = new ZclDataType("Signed 32-bit Integer", typeof(int), 0x2B, true, DataType.SIGNED_32_BIT_INTEGER);
            _codeTypeMapping[0x28] = new ZclDataType("Signed 8-bit Integer", typeof(int), 0x28, true, DataType.SIGNED_8_BIT_INTEGER);
            _codeTypeMapping[0x21] = new ZclDataType("Unsigned 16-bit integer", typeof(int), 0x21, true, DataType.UNSIGNED_16_BIT_INTEGER);
            _codeTypeMapping[0x22] = new ZclDataType("Unsigned 24-bit integer", typeof(int), 0x22, true, DataType.UNSIGNED_24_BIT_INTEGER);
            _codeTypeMapping[0x23] = new ZclDataType("Unsigned 32-bit integer", typeof(int), 0x23, true, DataType.UNSIGNED_32_BIT_INTEGER);
            _codeTypeMapping[0x25] = new ZclDataType("Unsigned 48-bit integer", typeof(long), 0x25, true, DataType.UNSIGNED_48_BIT_INTEGER);
            _codeTypeMapping[0x20] = new ZclDataType("Unsigned 8-bit integer", typeof(int), 0x20, true, DataType.UNSIGNED_8_BIT_INTEGER);
            _codeTypeMapping[0xE2] = new ZclDataType("UTCTime", typeof(DateTime), 0xE2, true, DataType.UTCTIME);
            _codeTypeMapping[0x00] = new ZclDataType("X Unsigned 8-bit integer", typeof(int), 0x00, false, DataType.X_UNSIGNED_8_BIT_INTEGER);
            _codeTypeMapping[0x00] = new ZclDataType("Zcl Status", typeof(ZclStatus), 0x00, false, DataType.ZCL_STATUS);
            _codeTypeMapping[0x00] = new ZclDataType("EXTENDED_PANID", typeof(ExtendedPanId), 0x00, false, DataType.EXTENDED_PANID);
            //_codeTypeMapping[0x00] = new ZclDataType("Binding Table", BindingTable.class, 0x00, false, DataType.BINDING_TABLE);
            _codeTypeMapping[0x00] = new ZclDataType("ClusterId", typeof(int), 0x00, false, DataType.CLUSTERID);
            //_codeTypeMapping[0x00] = new ZclDataType("Complex Descriptor", ComplexDescriptor.class, 0x00, false, DataType.COMPLEX_DESCRIPTOR);
            _codeTypeMapping[0x00] = new ZclDataType("Endpoint", typeof(int), 0x00, false, DataType.ENDPOINT);
            //_codeTypeMapping[0x00] = new ZclDataType("Neighbor Table", typeof(NeighborTable), 0x00, false, DataType.NEIGHBOR_TABLE);
            //_codeTypeMapping[0x00] = new ZclDataType("Node Descriptor", NodeDescriptor.class, 0x00, false, DataType.NODE_DESCRIPTOR);
            _codeTypeMapping[0x00] = new ZclDataType("NWK address", typeof(int), 0x00, false, DataType.NWK_ADDRESS);
            //_codeTypeMapping[0x00] = new ZclDataType("N x Binding Table", BindingTable.class, 0x00, false, DataType.N_X_BINDING_TABLE);
            _codeTypeMapping[0x00] = new ZclDataType("N X IEEE Address", typeof(long), 0x00, false, DataType.N_X_IEEE_ADDRESS);
            //_codeTypeMapping[0x00] = new ZclDataType("Power Descriptor", PowerDescriptor.class, 0x00, false, DataType.POWER_DESCRIPTOR);
            //_codeTypeMapping[0x00] = new ZclDataType("Routing Table", RoutingTable.class, 0x00, false, DataType.ROUTING_TABLE);
            //_codeTypeMapping[0x00] = new ZclDataType("Simple Descriptor", SimpleDescriptor.class, 0x00, false, DataType.SIMPLE_DESCRIPTOR);
            //_codeTypeMapping[0x00] = new ZclDataType("User Descriptor", UserDescriptor.class, 0x00, false, DataType.USER_DESCRIPTOR);
            //_codeTypeMapping[0x00] = new ZclDataType("Zdo Status", ZdoStatus.class, 0x00, false, DataType.ZDO_STATUS);
            _codeTypeMapping[0x00] = new ZclDataType("Unsigned 8 bit Integer Array", typeof(int[]), 0x00, false, DataType.UNSIGNED_8_BIT_INTEGER_ARRAY);
            _codeTypeMapping[0x00] = new ZclDataType("ZigBee Data Type", typeof(ZclDataType), 0x00, false, DataType.ZIGBEE_DATA_TYPE);
        }

        public static ZclDataType Get(int id)
        {
            return _codeTypeMapping[id];
        }

        public static ZclDataType Get(DataType type)
        {
            return _codeTypeMapping.Values.Single(dt => dt.DataType == type);
        }
    }

    //public enum ZclDataType
    //{
    //    BITMAP_16_BIT("16-bit Bitmap", typeof(int), 0x19, false),
    //BITMAP_32_BIT("32-bit Bitmap", typeof(int), 0x1B, false),
    //BITMAP_8_BIT("Bitmap 8-bit", typeof(int), 0x18, false),
    //BOOLEAN("Boolean", typeof(bool), 0x10, false),
    //BYTE_ARRAY("Byte array", ByteArray.class, 0x00, false),
    //CHARACTER_STRING("Character String", typeof(string), 0x42, false),
    //DATA_8_BIT("8-bit data", typeof(int), 0x08, false),
    //ENUMERATION_16_BIT("16-bit enumeration", typeof(int), 0x31, false),
    //ENUMERATION_8_BIT("8-bit Enumeration", typeof(int), 0x30, false),
    //IEEE_ADDRESS("IEEE Address", typeof(ZigbeeAddress64), 0xF0, false),
    //N_X_ATTRIBUTE_IDENTIFIER("N X Attribute identifier", typeof(int), 0x00, false),
    //N_X_ATTRIBUTE_INFORMATION("N X Attribute information", AttributeInformation.class, 0x00, false),
    //N_X_ATTRIBUTE_RECORD("N X Attribute record", typeof(AttributeRecord), 0x00, false),
    //N_X_ATTRIBUTE_REPORT("N X Attribute report", AttributeReport.class, 0x00, false),
    //N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD("N X Attribute reporting configuration record", AttributeReportingConfigurationRecord.class, 0x00, false),
    //N_X_ATTRIBUTE_SELECTOR("N X Attribute selector", typeof(object), 0x00, false),
    //N_X_ATTRIBUTE_STATUS_RECORD("N X Attribute status record", AttributeStatusRecord.class, 0x00, false),
    //N_X_EXTENDED_ATTRIBUTE_INFORMATION("N x Extended Attribute Information", ExtendedAttributeInformation.class, 0x00, false),
    //N_X_EXTENSION_FIELD_SET("N X Extension field set", ExtensionFieldSet.class, 0x00, false),
    //N_X_NEIGHBORS_INFORMATION("N X Neighbors information", NeighborInformation.class, 0x00, false),
    //N_X_READ_ATTRIBUTE_STATUS_RECORD("N X Read attribute status record", ReadAttributeStatusRecord.class, 0x00, false),
    //N_X_UNSIGNED_16_BIT_INTEGER("N X Unsigned 16-bit integer", typeof(int), 0x00, false),
    //N_X_UNSIGNED_8_BIT_INTEGER("N x Unsigned 8-bit Integer", typeof(int), 0x00, false),
    //N_X_WRITE_ATTRIBUTE_RECORD("N X Write attribute record", Writetypeof(AttributeRecord), 0x00, false),
    //N_X_WRITE_ATTRIBUTE_STATUS_RECORD("N X Write attribute status record", WriteAttributeStatusRecord.class, 0x00, false),
    //OCTET_STRING("Octet string", ByteArray.class, 0x41, false),
    //SIGNED_16_BIT_INTEGER("Signed 16-bit Integer", typeof(int), 0x29, true),
    //SIGNED_32_BIT_INTEGER("Signed 32-bit Integer", typeof(int), 0x2B, true),
    //SIGNED_8_BIT_INTEGER("Signed 8-bit Integer", typeof(int), 0x28, true),
    //UNSIGNED_16_BIT_INTEGER("Unsigned 16-bit integer", typeof(int), 0x21, true),
    //UNSIGNED_24_BIT_INTEGER("Unsigned 24-bit integer", typeof(int), 0x22, true),
    //UNSIGNED_32_BIT_INTEGER("Unsigned 32-bit integer", typeof(int), 0x23, true),
    //UNSIGNED_48_BIT_INTEGER("Unsigned 48-bit integer", typeof(long), 0x25, true),
    //UNSIGNED_8_BIT_INTEGER("Unsigned 8-bit integer", typeof(int), 0x20, true),
    //UTCTIME("UTCTime", Calendar.class, 0xE2, true),
    //X_UNSIGNED_8_BIT_INTEGER("X Unsigned 8-bit integer", typeof(int), 0x00, false),
    //ZCL_STATUS("Zcl Status", typeof(ZclStatus), 0x00, false),
    //EXTENDED_PANID("EXTENDED_PANID", ExtendedPanId.class, 0x00, false),
    //BINDING_TABLE("Binding Table", BindingTable.class, 0x00, false),
    //CLUSTERID("ClusterId", typeof(int), 0x00, false),
    //COMPLEX_DESCRIPTOR("Complex Descriptor", ComplexDescriptor.class, 0x00, false),
    //ENDPOINT("Endpoint", typeof(int), 0x00, false),
    //NEIGHBOR_TABLE("Neighbor Table", NeighborTable.class, 0x00, false),
    //NODE_DESCRIPTOR("Node Descriptor", NodeDescriptor.class, 0x00, false),
    //NWK_ADDRESS("NWK address", typeof(int), 0x00, false),
    //N_X_BINDING_TABLE("N x Binding Table", BindingTable.class, 0x00, false),
    //N_X_IEEE_ADDRESS("N X IEEE Address", typeof(long), 0x00, false),
    //POWER_DESCRIPTOR("Power Descriptor", PowerDescriptor.class, 0x00, false),
    //ROUTING_TABLE("Routing Table", RoutingTable.class, 0x00, false),
    //SIMPLE_DESCRIPTOR("Simple Descriptor", SimpleDescriptor.class, 0x00, false),
    //USER_DESCRIPTOR("User Descriptor", UserDescriptor.class, 0x00, false),
    //ZDO_STATUS("Zdo Status", ZdoStatus.class, 0x00, false),
    //UNSIGNED_8_BIT_INTEGER_ARRAY("Unsigned 8 bit Integer Array", int[].class, 0x00, false),
    //ZIGBEE_DATA_TYPE("ZigBee Data Type", typeof(ZclDataType), 0x00, false);

}

