using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Device Announce value object class.
    /// 
    /// The Device_annce is provided to enable ZigBee devices on the network to notify
    /// other ZigBee devices that the device has joined or re-joined the network,
    /// identifying the device's 64-bit IEEE address and new 16-bit NWK address, and
    /// informing the Remote Devices of the capability of the ZigBee device. This
    /// command shall be invoked for all ZigBee end devices upon join or rejoin. This
    /// command may also be invoked by ZigBee routers upon join or rejoin as part of
    /// NWK address conflict resolution. The destination addressing on this primitive is
    /// broadcast to all devices for which macRxOnWhenIdle = TRUE.
    /// 
    /// </summary>
    public class DeviceAnnounce : ZdoResponse
    {
        /// <summary>
        /// NWKAddrOfInterest command message field.
        /// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
        /// IEEEAddr command message field.
        /// </summary>
        public IeeeAddress IeeeAddr { get; set; }

        /// <summary>
        /// Capability command message field.
        /// </summary>
        public int Capability { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DeviceAnnounce()
        {
            ClusterId = 0x0013;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(IeeeAddr, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(Capability, ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            NwkAddrOfInterest = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS));
            IeeeAddr = (IeeeAddress)deserializer.Deserialize(ZclDataType.Get(DataType.IEEE_ADDRESS));
            Capability = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("DeviceAnnounce [")
                   .Append(base.ToString())
                   .Append(", nwkAddrOfInterest=")
                   .Append(NwkAddrOfInterest)
                   .Append(", ieeeAddr=")
                   .Append(IeeeAddr)
                   .Append(", capability=")
                   .Append(Capability)
                   .Append(']');

            return builder.ToString();
        }

    }
}
