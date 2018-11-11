using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    /// <summary>
    /// This command is generated to request a Bind
    /// </summary>
    public class ZDO_BIND_REQ : SynchronousRequest
    {
        /// <summary>
        /// Specifies the destination address of the device generating the bind request
        /// </summary>
        public ZigbeeAddress16 DstAddr { get; private set; }

        /// <summary>
        /// 64 bit Binding source IEEE addres
        /// </summary>
        public ZigbeeAddress64 SrcAddress { get; private set; }

        /// <summary>
        /// Specifies the binding source endpoint
        /// </summary>
        public byte SrcEndpoint{ get; private set; }

        /// <summary>
        /// Specifies the cluster Id to match in message
        /// </summary>
        public DoubleByte ClusterId { get; private set; }

        /// <summary>
        /// Specifies destination address mode
        /// </summary>
        public Address_Mode DstAddrMode { get; private set; }

        /// <summary>
        /// Binding destination IEEE address. Not to be confused with DstAddr
        /// </summary>
        public ZigbeeAddress64 DstAddress { get; private set; }

        /// <summary>
        /// Specifies the binding destination endpoint. It is used only when DstAddrMode is 64 bits extended address
        /// </summary>
        public byte DstEndpoint { get; private set; }

        public enum Address_Mode :byte
        {
            ADDRESS_NOT_PRESENT = 0x00,
            GROUP_ADDRESS = 0x01,
            ADDRESS_16_BIT = 0x02,
            ADDRESS_64_BIT = 0x03,
            BROADCAST = 0xFF 
        }

        public ZDO_BIND_REQ(ZigbeeAddress16 dstAddr, ZigbeeAddress64 srcAddress, byte srcEndpoint, DoubleByte cluster, Address_Mode dstAddrMode, ZigbeeAddress dstAddress, byte dstEndpoint = 0x00)
        {
            List<byte> framedata = new List<byte>();

            framedata.AddRange(dstAddr.ToByteArray());
            framedata.AddRange(srcAddress.ToByteArray());
            framedata.Add(srcEndpoint);
            framedata.Add(cluster.Lsb);
            framedata.Add(cluster.Msb);
            framedata.Add((byte)dstAddrMode);

            if(dstAddrMode == Address_Mode.ADDRESS_64_BIT)
            {
                if(dstAddress is ZigbeeAddress64 z64)
                {
                    framedata.AddRange(dstAddress.ToByteArray());
                    framedata.Add(dstEndpoint);
                }
                else
                {
                    throw new ArgumentException($"Mode is {dstAddrMode}, but {nameof(dstAddress)} is not a ZigbeeAddress64");
                }
            } else
            {
                framedata.AddRange(dstAddress.ToByteArray());
            }

            BuildPacket(CommandType.ZDO_BIND_REQ, framedata.ToArray());
        }
    }
}
