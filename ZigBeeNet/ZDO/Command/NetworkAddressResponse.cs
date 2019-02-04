using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    public class NetworkAddressResponse : ZdoResponse
    {
        /**
         * IEEEAddrRemoteDev command message field.
         */
        public IeeeAddress IeeeAddrRemoteDev { get; set; }

        /**
         * NWKAddrRemoteDev command message field.
         */
        public ushort NwkAddrRemoteDev { get; set; }

        /**
         * StartIndex command message field.
         */
        public byte StartIndex { get; set; }

        /**
         * NWKAddrAssocDevList command message field.
         */
        public List<int> NwkAddrAssocDevList { get; set; }

        /**
         * Default constructor.
         */
        public NetworkAddressResponse()
        {
            ClusterId = 0x8000;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
            serializer.Serialize(IeeeAddrRemoteDev, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(NwkAddrRemoteDev, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(NwkAddrAssocDevList.Count, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            for (int cnt = 0; cnt < NwkAddrAssocDevList.Count; cnt++)
            {
                serializer.Serialize(NwkAddrAssocDevList[cnt], ZclDataType.Get(DataType.NWK_ADDRESS));
            }
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            NwkAddrAssocDevList = new List<int>();

            Status = (ZdoStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZDO_STATUS));

            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }

            IeeeAddrRemoteDev = (IeeeAddress)deserializer.Deserialize(ZclDataType.Get(DataType.IEEE_ADDRESS));
            NwkAddrRemoteDev = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS));
            byte? numAssocDev = (byte?)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            if (numAssocDev != null && numAssocDev.Value > 0)
            {
                StartIndex = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

                for (int cnt = 0; cnt < numAssocDev; cnt++)
                {
                    NwkAddrAssocDevList.Add((int)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS)));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("NetworkAddressResponse [")
                   .Append(base.ToString())
                   .Append(", status=")
                   .Append(Status)
                   .Append(", ieeeAddrRemoteDev=")
                   .Append(IeeeAddrRemoteDev)
                   .Append(", nwkAddrRemoteDev=")
                   .Append(NwkAddrRemoteDev)
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(", nwkAddrAssocDevList=")
                   .Append(NwkAddrAssocDevList)
                   .Append(']');

            return builder.ToString();
        }

    }
}
