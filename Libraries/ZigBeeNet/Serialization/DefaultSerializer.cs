using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO;

namespace ZigBeeNet.Serialization
{
    public class DefaultSerializer : IZigBeeSerializer
    {
        private int _length = 0;

        private byte[] _buffer = new byte[131];
        public byte[] Payload
        {
            get
            {
                byte[] payload = new byte[_length];

                Array.Copy(_buffer, payload, _length);

                return payload;
            }
        }

        public void AppendZigBeeType(object data, DataType type)
        {
            if (data == null)
            {
                throw new ArgumentException("You can not append null data to a stream");
            }

            switch (type)
            {
                case DataType.BOOLEAN:
                    _buffer[_length++] = (bool)data ? (byte)0x01 : (byte)0x00;
                    break;
                case DataType.NWK_ADDRESS:
                case DataType.BITMAP_16_BIT:
                case DataType.SIGNED_16_BIT_INTEGER:
                case DataType.UNSIGNED_16_BIT_INTEGER:
                case DataType.ENUMERATION_16_BIT:
                case DataType.CLUSTERID:
                    ushort shortValue = ((ushort)data);
                    _buffer[_length++] = (byte)(shortValue & 0xFF);
                    _buffer[_length++] = (byte)((shortValue >> 8) & 0xFF);
                    break;
                case DataType.ENDPOINT:
                case DataType.DATA_8_BIT:
                case DataType.BITMAP_8_BIT:
                case DataType.SIGNED_8_BIT_INTEGER:
                case DataType.UNSIGNED_8_BIT_INTEGER:
                case DataType.ENUMERATION_8_BIT:
                    byte byteValue = (byte)data;
                    _buffer[_length++] = (byte)(byteValue & 0xFF);
                    break;
                case DataType.EXTENDED_PANID:
                    byte[] panId = BitConverter.GetBytes(((ExtendedPanId)data).Value);
                    _buffer[_length++] = panId[0];
                    _buffer[_length++] = panId[1];
                    _buffer[_length++] = panId[2];
                    _buffer[_length++] = panId[3];
                    _buffer[_length++] = panId[4];
                    _buffer[_length++] = panId[5];
                    _buffer[_length++] = panId[6];
                    _buffer[_length++] = panId[7];
                    break;
                case DataType.IEEE_ADDRESS:
                    byte[] address = BitConverter.GetBytes(((IeeeAddress)data).Value);
                    _buffer[_length++] = address[0];
                    _buffer[_length++] = address[1];
                    _buffer[_length++] = address[2];
                    _buffer[_length++] = address[3];
                    _buffer[_length++] = address[4];
                    _buffer[_length++] = address[5];
                    _buffer[_length++] = address[6];
                    _buffer[_length++] = address[7];
                    break;
                case DataType.N_X_ATTRIBUTE_INFORMATION:
                    break;
                case DataType.N_X_ATTRIBUTE_RECORD:
                    break;
                case DataType.N_X_ATTRIBUTE_REPORT:
                    break;
                case DataType.N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD:
                    break;
                case DataType.N_X_ATTRIBUTE_SELECTOR:
                    break;
                case DataType.N_X_ATTRIBUTE_STATUS_RECORD:
                    break;
                case DataType.N_X_EXTENSION_FIELD_SET:
                    break;
                case DataType.N_X_NEIGHBORS_INFORMATION:
                    break;
                case DataType.N_X_READ_ATTRIBUTE_STATUS_RECORD:
                    break;
                case DataType.N_X_UNSIGNED_16_BIT_INTEGER:
                    List<ushort> intArray16 = (List<ushort>)data;
                    _buffer[_length++] = (byte)intArray16.Count;
                    foreach (ushort value in intArray16)
                    {
                        _buffer[_length++] = (byte)(value & 0xFF);
                        _buffer[_length++] = (byte)((value >> 8) & 0xFF);
                    }
                    break;
                case DataType.N_X_UNSIGNED_8_BIT_INTEGER:
                    List<byte> intArrayNX8 = (List<byte>)data;
                    _buffer[_length++] = (byte)intArrayNX8.Count;
                    foreach (byte value in intArrayNX8)
                    {
                        _buffer[_length++] = (byte)(value & 0xFF);
                    }
                    break;
                case DataType.UNSIGNED_8_BIT_INTEGER_ARRAY:
                    byte[] intArrayN8 = (byte[])data;
                    foreach (byte value in intArrayN8)
                    {
                        _buffer[_length++] = (byte)(value & 0xFF);
                    }
                    break;
                case DataType.X_UNSIGNED_8_BIT_INTEGER:
                    List<byte> intArrayX8 = (List<byte>)data;
                    foreach (byte value in intArrayX8)
                    {
                        _buffer[_length++] = (byte)(value & 0xFF);
                    }
                    break;
                case DataType.N_X_ATTRIBUTE_IDENTIFIER:
                    List<ushort> intArrayX16 = (List<ushort>)data;
                    foreach (ushort value in intArrayX16)
                    {
                        _buffer[_length++] = (byte)(value & 0xFF);
                        _buffer[_length++] = (byte)((value >> 8) & 0xFF);
                    }
                    break;
                case DataType.N_X_WRITE_ATTRIBUTE_RECORD:
                    break;
                case DataType.N_X_WRITE_ATTRIBUTE_STATUS_RECORD:
                    break;
                case DataType.OCTET_STRING:
                    ByteArray array = (ByteArray)data;
                    _buffer[_length++] = ((byte)(array.Size() & 0xFF));
                    foreach (byte arrayByte in array.Get())
                    {
                        _buffer[_length++] = arrayByte;
                    }
                    break;
                case DataType.CHARACTER_STRING:
                    string str = (string)data;
                    _buffer[_length++] = ((byte)(str.Length & 0xFF));
                    foreach (byte strByte in Encoding.ASCII.GetBytes((str)))
                    {
                        _buffer[_length++] = strByte;
                    }
                    break;
                case DataType.UNSIGNED_24_BIT_INTEGER:
                    byte[] uint24Value = (byte[])data;
                    _buffer[_length++] = uint24Value[0]; //uint24Value & 0xFF;
                    _buffer[_length++] = uint24Value[1]; //(uint24Value >> 8) & 0xFF;
                    _buffer[_length++] = uint24Value[2]; //(uint24Value >> 16) & 0xFF;
                    break;
                case DataType.SIGNED_32_BIT_INTEGER:
                    int intValue = (int)data;
                    _buffer[_length++] = (byte)(intValue & 0xFF);
                    _buffer[_length++] = (byte)((intValue >> 8) & 0xFF);
                    _buffer[_length++] = (byte)((intValue >> 16) & 0xFF);
                    _buffer[_length++] = (byte)((intValue >> 24) & 0xFF);
                    break;
                case DataType.BITMAP_32_BIT:
                case DataType.UNSIGNED_32_BIT_INTEGER:
                    UInt32 uint32Value = (UInt32)data;
                    _buffer[_length++] = (byte)(uint32Value & 0xFF);
                    _buffer[_length++] = (byte)((uint32Value >> 8) & 0xFF);
                    _buffer[_length++] = (byte)((uint32Value >> 16) & 0xFF);
                    _buffer[_length++] = (byte)((uint32Value >> 24) & 0xFF);
                    break;
                case DataType.UNSIGNED_48_BIT_INTEGER:
                    long uint48Value = (long)data;
                    _buffer[_length++] = (byte)(uint48Value & 0xFF);
                    _buffer[_length++] = (byte)((uint48Value >> 8) & 0xFF);
                    _buffer[_length++] = (byte)((uint48Value >> 16) & 0xFF);
                    _buffer[_length++] = (byte)((uint48Value >> 24) & 0xFF);
                    _buffer[_length++] = (byte)((uint48Value >> 32) & 0xFF);
                    _buffer[_length++] = (byte)((uint48Value >> 40) & 0xFF);
                    break;
                case DataType.UTCTIME:
                    break;
                case DataType.ZDO_STATUS:
                    _buffer[_length++] = (byte)((ZdoStatus)data);
                    break;
                case DataType.ZCL_STATUS:
                    _buffer[_length++] = (byte)((ZclStatus)data);
                    break;
                case DataType.BYTE_ARRAY:
                    ByteArray byteArray = (ByteArray)data;
                    _buffer[_length++] = (byte)(byteArray.Size());
                    foreach (byte valByte in byteArray.Get())
                    {
                        _buffer[_length++] = (byte)(valByte & 0xff);
                    }
                    break;
                case DataType.ZIGBEE_DATA_TYPE:
                    _buffer[_length++] = (byte)((ZclDataType)data).Id;
                    break;
                case DataType.FLOAT_32_BIT:
                    float float32 = (float)data;
                    byte[] floatBytes = BitConverter.GetBytes(float32);
                    int float32Value = BitConverter.ToInt32(floatBytes, 0);
                    _buffer[_length++] = (byte)(float32Value & 0xFF);
                    _buffer[_length++] = (byte)((float32Value >> 8) & 0xFF);
                    _buffer[_length++] = (byte)((float32Value >> 16) & 0xFF);
                    _buffer[_length++] = (byte)((float32Value >> 24) & 0xFF);
                    break;
                default:
                    throw new ArgumentException("No writer defined in " + this.GetType().Name
                            + " for " + type.ToString() + " (" + (byte)type + ")");
            }
}
    }
}
