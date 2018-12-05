using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Fileld
{
    public class NeighborInformation : IZclListItemField
    {
        /**
         * The neighbor address.
         */
        public long neighborAddress;
        /**
         * The coordinate 1
         */
        public int Coordinate1;
        /**
         * The coordinate 2
         */
        public int Coordinate2;
        /**
         * The coordinate 3
         */
        public int Coordinate3;
        /**
         * The RSSI.
         */
        public int Rssi;
        /**
         * The RSSI measurement count.
         */
        public int MeasurementCount;


        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(neighborAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.AppendZigBeeType((short)Coordinate1, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.AppendZigBeeType((short)Coordinate2, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.AppendZigBeeType((short)Coordinate3, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.AppendZigBeeType((byte)Rssi, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.AppendZigBeeType((byte)MeasurementCount, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            neighborAddress = (long)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.IEEE_ADDRESS));
            Coordinate1 = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Coordinate2 = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Coordinate3 = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Rssi = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            MeasurementCount = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            return "Neighbor Information: coordinate1=" + Coordinate1 + ", neighborAddress=" + neighborAddress
                    + ", coordinate2=" + Coordinate2 + ", coordinate3=" + Coordinate3 + ", rssi=" + Rssi
                    + ", measurementCount=" + MeasurementCount;
        }
    }
}
