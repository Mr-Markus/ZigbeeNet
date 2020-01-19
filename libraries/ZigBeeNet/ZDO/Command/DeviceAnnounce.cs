using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;


namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Device Announce value object class.
    ///
    ///
    /// The Device_annce is provided to enable ZigBee devices on the network to notify other
    /// ZigBee devices that the device has joined or re-joined the network, identifying the
    /// device's 64-bit IEEE address and new 16-bit NWK address, and informing the Remote
    /// Devices of the capability of the ZigBee device. This command shall be invoked for all
    /// ZigBee end devices upon join or rejoin. This command may also be invoked by ZigBee
    /// routers upon join or rejoin as part of NWK address conflict resolution. The destination
    /// addressing on this primitive is broadcast to all devices for which macRxOnWhenIdle =
    /// TRUE.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DeviceAnnounce : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0013;

        /// <summary>
        /// NWK Addr Of Interest command message field.
        /// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
        /// IEEE Addr command message field.
        /// </summary>
        public IeeeAddress IeeeAddr { get; set; }

        /// <summary>
        /// Capability command message field.
        /// </summary>
        public byte Capability { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DeviceAnnounce()
        {
            ClusterId = CLUSTER_ID;
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

            NwkAddrOfInterest = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.NWK_ADDRESS));
            IeeeAddr = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            Capability = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DeviceAnnounce [");
            builder.Append(base.ToString());
            builder.Append(", NwkAddrOfInterest=");
            builder.Append(NwkAddrOfInterest);
            builder.Append(", IeeeAddr=");
            builder.Append(IeeeAddr);
            builder.Append(", Capability=");
            builder.Append(Capability);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
