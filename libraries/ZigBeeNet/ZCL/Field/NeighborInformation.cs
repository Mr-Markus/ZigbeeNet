using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class NeighborInformation : IZclListItemField
    {
        /// <summary>
         /// The neighbor address.
         /// </summary>
        public IeeeAddress NeighborAddress;
        /// <summary>
         /// The coordinate 1
         /// </summary>
        public ushort Coordinate1;
        /// <summary>
         /// The coordinate 2
         /// </summary>
        public ushort Coordinate2;
        /// <summary>
         /// The coordinate 3
         /// </summary>
        public ushort Coordinate3;
        /// <summary>
         /// The RSSI.
         /// </summary>
        public byte Rssi;
        /// <summary>
         /// The RSSI measurement count.
         /// </summary>
        public byte MeasurementCount;


        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(NeighborAddress, DataType.IEEE_ADDRESS);
            serializer.AppendZigBeeType((short)Coordinate1, DataType.UNSIGNED_16_BIT_INTEGER);
            serializer.AppendZigBeeType((short)Coordinate2, DataType.UNSIGNED_16_BIT_INTEGER);
            serializer.AppendZigBeeType((short)Coordinate3, DataType.UNSIGNED_16_BIT_INTEGER);
            serializer.AppendZigBeeType((byte)Rssi, DataType.UNSIGNED_8_BIT_INTEGER);
            serializer.AppendZigBeeType((byte)MeasurementCount, DataType.UNSIGNED_8_BIT_INTEGER);
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            NeighborAddress = deserializer.ReadZigBeeType<IeeeAddress>(DataType.IEEE_ADDRESS);
            Coordinate1 = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            Coordinate2 = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            Coordinate3 = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            Rssi = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            MeasurementCount = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
        }

        public override string ToString()
        {
            return "Neighbor Information: coordinate1=" + Coordinate1 + ", neighborAddress=" + NeighborAddress
                    + ", coordinate2=" + Coordinate2 + ", coordinate3=" + Coordinate3 + ", rssi=" + Rssi
                    + ", measurementCount=" + MeasurementCount;
        }
    }
}
