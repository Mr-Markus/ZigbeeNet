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
            if(data == null)
            {
                throw new ArgumentNullException("data");
            }

            //switch (type)
            //{
            //    case ZclDataType.Boolean:
            //        _payload[length++] = Convert.ToBoolean(data);
            //        break;
            //    case ZclDataType. NWK_ADDRESS:
            //    case ZclDataType.Map16:
            //    case ZclDataType.Int16:
            //    case ZclDataType.UInt16:
            //    case ZclDataType.Enum16:
            //    case ZclDataType.ClusterId:
            //        short shortValue = ((Number)data).shortValue();
            //        _payload[length++] = shortValue & 0xFF;
            //        _payload[length++] = (shortValue >> 8) & 0xFF;
            //        break;
            //    case ENDPOINT:
            //    case DATA_8_BIT:
            //    case BITMAP_8_BIT:
            //    case SIGNED_8_BIT_INTEGER:
            //    case UNSIGNED_8_BIT_INTEGER:
            //    case ENUMERATION_8_BIT:
            //        final byte byteValue = ((Number)data).byteValue();
            //        _payload[length++] = byteValue & 0xFF;
            //        break;
            //    case EXTENDED_PANID:
            //        byte[] panId = new ZigbeeAddress64((byte[])data).ToByteArray();
            //        _payload[length++] = panId[0];
            //        _payload[length++] = panId[1];
            //        _payload[length++] = panId[2];
            //        _payload[length++] = panId[3];
            //        _payload[length++] = panId[4];
            //        _payload[length++] = panId[5];
            //        _payload[length++] = panId[6];
            //        _payload[length++] = panId[7];
            //        break;
            //    case ZclDataType.IeeeAddress:
            //        byte[] address = new ZigbeeAddress64((byte[])data).ToByteArray();
            //        _payload[length++] = address[0];
            //        _payload[length++] = address[1];
            //        _payload[length++] = address[2];
            //        _payload[length++] = address[3];
            //        _payload[length++] = address[4];
            //        _payload[length++] = address[5];
            //        _payload[length++] = address[6];
            //        _payload[length++] = address[7];
            //        break;
            //    case N_X_ATTRIBUTE_INFORMATION:
            //        break;
            //    case N_X_ATTRIBUTE_RECORD:
            //        break;
            //    case N_X_ATTRIBUTE_REPORT:
            //        break;
            //    case N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD:
            //        break;
            //    case N_X_ATTRIBUTE_SELECTOR:
            //        break;
            //    case N_X_ATTRIBUTE_STATUS_RECORD:
            //        break;
            //    case N_X_EXTENSION_FIELD_SET:
            //        break;
            //    case N_X_NEIGHBORS_INFORMATION:
            //        break;
            //    case N_X_READ_ATTRIBUTE_STATUS_RECORD:
            //        break;
            //    case N_X_UNSIGNED_16_BIT_INTEGER:
            //        List<Integer> intArray16 = (List<Integer>)data;
            //        _payload[length++] = intArray16.size();
            //        for (int value : intArray16)
            //        {
            //            _payload[length++] = value & 0xFF;
            //            _payload[length++] = (value >> 8) & 0xFF;
            //        }
            //        break;
            //    case N_X_UNSIGNED_8_BIT_INTEGER:
            //        List<Integer> intArrayNX8 = (List<Integer>)data;
            //        _payload[length++] = intArrayNX8.size();
            //        for (int value : intArrayNX8)
            //        {
            //            _payload[length++] = value & 0xFF;
            //        }
            //        break;
            //    case UNSIGNED_8_BIT_INTEGER_ARRAY:
            //        int[] intArrayN8 = (int[])data;
            //        for (int value : intArrayN8)
            //        {
            //            _payload[length++] = value & 0xFF;
            //        }
            //        break;
            //    case X_UNSIGNED_8_BIT_INTEGER:
            //        List<Integer> intArrayX8 = (List<Integer>)data;
            //        for (int value : intArrayX8)
            //        {
            //            _payload[length++] = value & 0xFF;
            //        }
            //        break;
            //    case N_X_ATTRIBUTE_IDENTIFIER:
            //        List<Integer> intArrayX16 = (List<Integer>)data;
            //        for (int value : intArrayX16)
            //        {
            //            _payload[length++] = value & 0xFF;
            //            _payload[length++] = (value >> 8) & 0xFF;
            //        }
            //        break;
            //    case N_X_WRITE_ATTRIBUTE_RECORD:
            //        break;
            //    case N_X_WRITE_ATTRIBUTE_STATUS_RECORD:
            //        break;
            //    case OCTET_STRING:
            //        final ByteArray array = (ByteArray)data;
            //        _payload[length++] = ((byte)(array.size() & 0xFF));
            //        for (byte arrayByte : array.get())
            //        {
            //            _payload[length++] = arrayByte;
            //        }
            //        break;
            //    case CHARACTER_STRING:
            //        final String str = (String)data;
            //        _payload[length++] = ((byte)(str.length() & 0xFF));
            //        for (int strByte : str.getBytes())
            //        {
            //            _payload[length++] = strByte;
            //        }
            //        break;
            //    case SIGNED_32_BIT_INTEGER:
            //        final int intValue = (Integer)data;
            //        _payload[length++] = intValue & 0xFF;
            //        _payload[length++] = (intValue >> 8) & 0xFF;
            //        _payload[length++] = (intValue >> 16) & 0xFF;
            //        _payload[length++] = (intValue >> 24) & 0xFF;
            //        break;
            //    case BITMAP_32_BIT:
            //    case UNSIGNED_32_BIT_INTEGER:
            //        final int uintValue = (Integer)data;
            //        _payload[length++] = uintValue & 0xFF;
            //        _payload[length++] = (uintValue >> 8) & 0xFF;
            //        _payload[length++] = (uintValue >> 16) & 0xFF;
            //        _payload[length++] = (uintValue >> 24) & 0xFF;
            //        break;
            //    case UTCTIME:
            //        break;
            //    case ZDO_STATUS:
            //        _payload[length++] = ((ZdoStatus)data).getId();
            //        break;
            //    case ZCL_STATUS:
            //        _payload[length++] = ((ZclStatus)data).getId();
            //        break;
            //    case BYTE_ARRAY:
            //        final ByteArray byteArray = (ByteArray)data;
            //        _payload[length++] = byteArray.size();
            //        for (byte valByte : byteArray.get())
            //        {
            //            _payload[length++] = valByte & 0xff;
            //        }
            //        break;
            //    case ZIGBEE_DATA_TYPE:
            //        _payload[length++] = ((ZclDataType)data).getId();
            //        break;
            //    default:
            //        throw new IllegalArgumentException("No writer defined in " + ZigBeeDeserializer.class.getSimpleName()
            //            + " for " + type.toString() + " (" + type.getId() + ")");
            //}
        }
    }
}
