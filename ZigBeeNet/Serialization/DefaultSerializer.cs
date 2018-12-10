using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.Serialization
{
    public class DefaultSerializer : IZigBeeSerializer
    {
        private int _length = 0;

        private byte[] _payload;
        public byte[] Payload
        {
            get
            {
                return _payload;
            }
            set
            {
                _payload = value;
            }
        }

        public void AppendZigBeeType(object data, ZclDataType type)
        {
            if (data == null)
            {
                throw new ArgumentException("You can not append null data to a stream");
            }

            //    throw new NotImplementedException();

            //    switch (type.DataType)
            //    {
            //        case DataType.BOOLEAN:
            //            _payload[_length++] = (bool)data ? (byte)0x01 : (byte)0x00;
            //            break;
            //        case DataType.NWK_ADDRESS:
            //        case DataType.BITMAP_16_BIT:
            //        case DataType.SIGNED_16_BIT_INTEGER:
            //        case DataType.UNSIGNED_16_BIT_INTEGER:
            //        case DataType.ENUMERATION_16_BIT:
            //        case DataType.CLUSTERID:
            //            ushort shortValue = ((ushort)data);
            //            _payload[_length++] = (byte)(shortValue & 0xFF);
            //            _payload[_length++] = (byte)((shortValue >> 8) & 0xFF);
            //            break;
            //        case DataType.ENDPOINT:
            //        case DataType.DATA_8_BIT:
            //        case DataType.BITMAP_8_BIT:
            //        case DataType.SIGNED_8_BIT_INTEGER:
            //        case DataType.UNSIGNED_8_BIT_INTEGER:
            //        case DataType.ENUMERATION_8_BIT:
            //            byte byteValue = (byte)data;
            //            _payload[_length++] = (byte)(byteValue & 0xFF);
            //            break;
            //        case DataType.EXTENDED_PANID:
            //            byte[] panId = BitConverter.GetBytes(((ExtendedPanId)data).Value);
            //            _payload[_length++] = panId[0];
            //            _payload[_length++] = panId[1];
            //            _payload[_length++] = panId[2];
            //            _payload[_length++] = panId[3];
            //            _payload[_length++] = panId[4];
            //            _payload[_length++] = panId[5];
            //            _payload[_length++] = panId[6];
            //            _payload[_length++] = panId[7];
            //            break;
            //        case DataType.IEEE_ADDRESS:
            //            byte[] address = BitConverter.GetBytes(((IeeeAddress)data).Value);
            //            _payload[_length++] = address[0];
            //            _payload[_length++] = address[1];
            //            _payload[_length++] = address[2];
            //            _payload[_length++] = address[3];
            //            _payload[_length++] = address[4];
            //            _payload[_length++] = address[5];
            //            _payload[_length++] = address[6];
            //            _payload[_length++] = address[7];
            //            break;
            //        case DataType.N_X_ATTRIBUTE_INFORMATION:
            //            break;
            //        case DataType.N_X_ATTRIBUTE_RECORD:
            //            break;
            //        case DataType.N_X_ATTRIBUTE_REPORT:
            //            break;
            //        case DataType.N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD:
            //            break;
            //        case DataType.N_X_ATTRIBUTE_SELECTOR:
            //            break;
            //        case DataType.N_X_ATTRIBUTE_STATUS_RECORD:
            //            break;
            //        case DataType.N_X_EXTENSION_FIELD_SET:
            //            break;
            //        case DataType.N_X_NEIGHBORS_INFORMATION:
            //            break;
            //        case DataType.N_X_READ_ATTRIBUTE_STATUS_RECORD:
            //            break;
            //        case DataType.N_X_UNSIGNED_16_BIT_INTEGER:
            //            List<Integer> intArray16 = (List<Integer>)data;
            //            _payload[_length++] = intArray16.size();
            //            for (int value : intArray16)
            //            {
            //                _payload[_length++] = value & 0xFF;
            //                _payload[_length++] = (value >> 8) & 0xFF;
            //            }
            //            break;
            //        case DataType.N_X_UNSIGNED_8_BIT_INTEGER:
            //            List<Integer> intArrayNX8 = (List<Integer>)data;
            //            _payload[_length++] = intArrayNX8.size();
            //            for (int value : intArrayNX8)
            //            {
            //                _payload[_length++] = value & 0xFF;
            //            }
            //            break;
            //        case DataType.UNSIGNED_8_BIT_INTEGER_ARRAY:
            //            int[] intArrayN8 = (int[])data;
            //            for (int value : intArrayN8)
            //            {
            //                _payload[_length++] = value & 0xFF;
            //            }
            //            break;
            //        case DataType.X_UNSIGNED_8_BIT_INTEGER:
            //            List<Integer> intArrayX8 = (List<Integer>)data;
            //            for (int value : intArrayX8)
            //            {
            //                _payload[_length++] = value & 0xFF;
            //            }
            //            break;
            //        case DataType.N_X_ATTRIBUTE_IDENTIFIER:
            //            List<Integer> intArrayX16 = (List<Integer>)data;
            //            for (int value : intArrayX16)
            //            {
            //                _payload[_length++] = value & 0xFF;
            //                _payload[_length++] = (value >> 8) & 0xFF;
            //            }
            //            break;
            //        case DataType.N_X_WRITE_ATTRIBUTE_RECORD:
            //            break;
            //        case DataType.N_X_WRITE_ATTRIBUTE_STATUS_RECORD:
            //            break;
            //        case DataType.OCTET_STRING:
            //            ByteArray array = (ByteArray)data;
            //            _payload[_length++] = ((byte)(array.size() & 0xFF));
            //            for (byte arrayByte : array.get())
            //            {
            //                _payload[_length++] = arrayByte;
            //            }
            //            break;
            //        case DataType.CHARACTER_STRING:
            //            String str = (String)data;
            //            _payload[_length++] = ((byte)(str._length() & 0xFF));
            //            for (int strByte : str.getBytes())
            //            {
            //                _payload[_length++] = strByte;
            //            }
            //            break;
            //        case DataType.UNSIGNED_24_BIT_INTEGER:
            //            int uint24Value = (Integer)data;
            //            _payload[_length++] = uint24Value & 0xFF;
            //            _payload[_length++] = (uint24Value >> 8) & 0xFF;
            //            _payload[_length++] = (uint24Value >> 16) & 0xFF;
            //            break;
            //        case DataType.SIGNED_32_BIT_INTEGER:
            //            int intValue = (Integer)data;
            //            _payload[_length++] = intValue & 0xFF;
            //            _payload[_length++] = (intValue >> 8) & 0xFF;
            //            _payload[_length++] = (intValue >> 16) & 0xFF;
            //            _payload[_length++] = (intValue >> 24) & 0xFF;
            //            break;
            //        case DataType.BITMAP_32_BIT:
            //        case DataType.UNSIGNED_32_BIT_INTEGER:
            //            int uint32Value = (Integer)data;
            //            _payload[_length++] = uint32Value & 0xFF;
            //            _payload[_length++] = (uint32Value >> 8) & 0xFF;
            //            _payload[_length++] = (uint32Value >> 16) & 0xFF;
            //            _payload[_length++] = (uint32Value >> 24) & 0xFF;
            //            break;
            //        case DataType.UNSIGNED_48_BIT_INTEGER:
            //            long uint48Value = (Long)data;
            //            _payload[_length++] = (int)(uint48Value & 0xFF);
            //            _payload[_length++] = (int)((uint48Value >> 8) & 0xFF);
            //            _payload[_length++] = (int)((uint48Value >> 16) & 0xFF);
            //            _payload[_length++] = (int)((uint48Value >> 24) & 0xFF);
            //            _payload[_length++] = (int)((uint48Value >> 32) & 0xFF);
            //            _payload[_length++] = (int)((uint48Value >> 40) & 0xFF);
            //            break;
            //        case DataType.UTCTIME:
            //            break;
            //        case DataType.ZDO_STATUS:
            //            _payload[_length++] = ((ZdoStatus)data).getId();
            //            break;
            //        case DataType.ZCL_STATUS:
            //            _payload[_length++] = ((ZclStatus)data).getId();
            //            break;
            //        case DataType.BYTE_ARRAY:
            //            ByteArray byteArray = (ByteArray)data;
            //            _payload[_length++] = byteArray.size();
            //            for (byte valByte : byteArray.get())
            //            {
            //                _payload[_length++] = valByte & 0xff;
            //            }
            //            break;
            //        case DataType.ZIGBEE_DATA_TYPE:
            //            _payload[_length++] = ((ZclDataType)data).getId();
            //            break;
            //        default:
            //            throw new IllegalArgumentException("No writer defined in " + ZigBeeDeserializer.class.getSimpleName()
            //                + " for " + type.toString() + " (" + type.getId() + ")");
            //}
        }
    }
}
