﻿using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.Serialization
{
    public class DefaultDeserializer : IZigBeeDeserializer
    {
        private int index = 0;
        private byte[] payload;

        public DefaultDeserializer(byte[] payload)
        {
            this.payload = payload;
            this.index = 0;
        }

        public bool IsEndOfStream()
        {
            return index >= payload.Length;
        }

        public int GetPosition()
        {
            return index;
        }

        public int GetSize()
        {
            return payload.Length;
        }

        public void Skip(int n)
        {
            index += n;
        }

        /**
         * {@inheritDoc}
         */
        public object ReadZigBeeType(ZclDataType type)
        {
            if (index == payload.Length)
            {
                return null;
            }

            object[] value = new object[1];
            switch (type.DataType)
            {
                case DataType.BOOLEAN:
                    value[0] = payload[index++] == 0 ? false : true;
                    break;
                case DataType.OCTET_STRING:
                    int octetSize = payload[index++];
                    value[0] = new ByteArray(payload, index, index + octetSize);
                    index += octetSize;
                    break;
                case DataType.CHARACTER_STRING:
                    int stringSize = payload[index++];
                    if (stringSize == 255)
                    {
                        value[0] = null;
                        break;
                    }
                    byte[] bytes = new byte[stringSize];
                    int length = stringSize;
                    for (int cnt = 0; cnt < stringSize; cnt++)
                    {
                        bytes[cnt] = (byte)payload[index + cnt];
                        if (payload[index + cnt] == 0)
                        {
                            length = cnt;
                            break;
                        }
                    }
                    try
                    {
                        int len = length - 0;
                        byte[] dest = new byte[len];
                        // note i is always from 0
                        for (int i = 0; i < len; i++)
                        {
                            dest[i] = bytes[0 + i]; // so 0..n = 0+x..n+x
                        }

                        value[0] = Encoding.Default.GetString(dest);
                    }
                    catch (Exception e)
                    {
                        value[0] = null;
                        break;
                    }
                    index += stringSize;
                    break;
                case DataType.ENDPOINT:
                case DataType.BITMAP_8_BIT:
                case DataType.DATA_8_BIT:
                case DataType.ENUMERATION_8_BIT:
                    value[0] = (byte)((byte)payload[index++] & 0xFF);
                    break;
                case DataType.EXTENDED_PANID:
                    int[] panId = new int[8];
                    for (int iCnt = 7; iCnt >= 0; iCnt--)
                    {
                        panId[iCnt] = payload[index + iCnt];
                    }
                    index += 8;
                    value[0] = new ExtendedPanId(panId);
                    break;
                case DataType.IEEE_ADDRESS:
                    int[] address = new int[8];
                    for (int iCnt = 7; iCnt >= 0; iCnt--)
                    {
                        address[iCnt] = payload[index + iCnt];
                    }
                    index += 8;
                    value[0] = new IeeeAddress(address);
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
                    int cntN16 = (byte)((byte)payload[index++] & 0xFF);
                    List<byte> arrayN16 = new List<byte>(cntN16);
                    for (int arrayIndex = 0; arrayIndex < cntN16; arrayIndex++)
                    {
                        arrayN16.Add((byte)(payload[index++] + ((payload[index++] << 8) & 0xffff)));
                    }
                    value[0] = arrayN16;
                    break;
                case DataType.N_X_UNSIGNED_8_BIT_INTEGER:
                    int cntN8 = (byte)((byte)payload[index++] & 0xFF);
                    List<byte> arrayN8 = new List<byte>(cntN8);
                    for (int arrayIndex = 0; arrayIndex < cntN8; arrayIndex++)
                    {
                        arrayN8.Add((byte)(payload[index++]));
                    }
                    value[0] = arrayN8;
                    break;
                case DataType.X_UNSIGNED_8_BIT_INTEGER:
                    int cntX8 = payload.Length - index;
                    List<byte> arrayX8 = new List<byte>(cntX8);
                    for (int arrayIndex = 0; arrayIndex < cntX8; arrayIndex++)
                    {
                        arrayX8.Add((byte)(payload[index++]));
                    }
                    value[0] = arrayX8;
                    break;
                case DataType.N_X_ATTRIBUTE_IDENTIFIER:
                    int cntX16 = (payload.Length - index) / 2;
                    List<byte> arrayX16 = new List<byte>(cntX16);
                    for (int arrayIndex = 0; arrayIndex < cntX16; arrayIndex++)
                    {
                        arrayX16.Add((byte)(payload[index++]));
                    }
                    value[0] = arrayX16;
                    break;
                case DataType.UNSIGNED_8_BIT_INTEGER_ARRAY:
                    int cnt8Array = payload.Length - index;
                    int[] intarray8 = new int[cnt8Array];
                    for (int arrayIndex = 0; arrayIndex < cnt8Array; arrayIndex++)
                    {
                        intarray8[arrayIndex] = payload[index++];
                    }
                    value[0] = intarray8;
                    break;
                case DataType.N_X_WRITE_ATTRIBUTE_RECORD:
                    break;
                case DataType.N_X_WRITE_ATTRIBUTE_STATUS_RECORD:
                    break;
                case DataType.CLUSTERID:
                case DataType.NWK_ADDRESS:
                case DataType.BITMAP_16_BIT:
                case DataType.ENUMERATION_16_BIT:
                case DataType.SIGNED_16_BIT_INTEGER:
                case DataType.UNSIGNED_16_BIT_INTEGER:
                    short s = (short)(payload[index++] + (payload[index++] << 8));
                    if (type == ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER))
                    {
                        value[0] = (byte)s;
                    }
                    else
                    {
                        value[0] = (byte)(s & 0xFFFF);
                    }
                    break;
                case DataType.UNSIGNED_24_BIT_INTEGER:
                    value[0] = payload[index++] + (payload[index++] << 8) + (payload[index++] << 16);
                    break;
                case DataType.BITMAP_32_BIT:
                case DataType.SIGNED_32_BIT_INTEGER:
                case DataType.UNSIGNED_32_BIT_INTEGER:
                    value[0] = payload[index++] + (payload[index++] << 8) + (payload[index++] << 16)
                            + (payload[index++] << 24);
                    break;
                case DataType.UNSIGNED_48_BIT_INTEGER:
                    value[0] = (payload[index++]) + ((long)(payload[index++]) << 8) + ((long)(payload[index++]) << 16)
                            + ((long)(payload[index++]) << 24) + ((long)(payload[index++]) << 32)
                            + ((long)(payload[index++]) << 40);
                    break;
                case DataType.SIGNED_8_BIT_INTEGER:
                    value[0] = (byte)((byte)payload[index++]);
                    break;
                case DataType.UNSIGNED_8_BIT_INTEGER:
                    value[0] = (byte)((byte)payload[index++] & 0xFF);
                    break;
                case DataType.UTCTIME:
                    break;
                case DataType.ROUTING_TABLE:
                    RoutingTable routingTable = new RoutingTable();
                    routingTable.Deserialize(this);
                    value[0] = routingTable;
                    break;
                case DataType.NEIGHBOR_TABLE:
                    NeighborTable neighborTable = new NeighborTable();
                    neighborTable.deserialize(this);
                    value[0] = neighborTable;
                    break;
                case DataType.NODE_DESCRIPTOR:
                    NodeDescriptor nodeDescriptor = new NodeDescriptor();
                    nodeDescriptor.Deserialize(this);
                    value[0] = nodeDescriptor;
                    break;
                case DataType.POWER_DESCRIPTOR:
                    PowerDescriptor powerDescriptor = new PowerDescriptor();
                    powerDescriptor.Deserialize(this);
                    value[0] = powerDescriptor;
                    break;
                case DataType.BINDING_TABLE:
                    BindingTable bindingTable = new BindingTable();
                    bindingTable.Deserialize(this);
                    value[0] = bindingTable;
                    break;
                case DataType.SIMPLE_DESCRIPTOR:
                    SimpleDescriptor simpleDescriptor = new SimpleDescriptor();
                    simpleDescriptor.Deserialize(this);
                    value[0] = simpleDescriptor;
                    break;
                case DataType.ZCL_STATUS:
                    value[0] = (ZclStatus)(payload[index++]);
                    break;
                case DataType.ZDO_STATUS:
                    value[0] = (ZdoStatus)(payload[index++]);
                    break;
                case DataType.ZIGBEE_DATA_TYPE:
                    value[0] = ZclDataType.Get(payload[index++]);
                    break;
                case DataType.BYTE_ARRAY:
                    int cntB8 = (byte)((byte)payload[index++] & 0xFF);
                    byte[] arrayB8 = new byte[cntB8];
                    for (int arrayIndex = 0; arrayIndex < cntB8; arrayIndex++)
                    {
                        arrayB8[arrayIndex] = (byte)(payload[index++] & 0xff);
                    }
                    value[0] = new ByteArray(arrayB8);
                    break;
                default:
                    throw new ArgumentException("No reader defined in " + this.GetType().Name + " for " + type.ToString() + " (" + type.Id + ")");
            }
            return value[0];
        }
    }
}
