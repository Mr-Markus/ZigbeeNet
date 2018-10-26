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
        public ZAddress16 DstAddr { get; private set; }

        /// <summary>
        /// 64 bit Binding source IEEE addres
        /// </summary>
        public ZAddress64 SrcAddress { get; private set; }

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
        public ZAddress64 DstAddress { get; private set; }

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

        public ZDO_BIND_REQ(ZAddress16 nwkDst, ZAddress64 ieeeSrc, byte srcEp, DoubleByte cluster, Address_Mode addressMode, ZAddress ieeeDst, byte dstEp = 0x00)
        {
            List<byte> framedata = new List<byte>();

            framedata.AddRange(nwkDst.ToByteArray());
            framedata.AddRange(ieeeSrc.ToByteArray());
            framedata.Add(srcEp);
            framedata.Add(cluster.Low);
            framedata.Add(cluster.High);
            framedata.Add((byte)addressMode);
            framedata.AddRange(ieeeDst.ToByteArray());

            if(addressMode == Address_Mode.ADDRESS_64_BIT)
            {
                if(ieeeDst is ZAddress64 z64)
                {
                    framedata.AddRange(ieeeDst.ToByteArray());
                    framedata.Add(dstEp);
                }
                else
                {
                    throw new ArgumentException($"Mode is {addressMode}, but {nameof(ieeeDst)} is not a ZAddress64");
                }
            } else
            {
                framedata.AddRange(ieeeDst.ToByteArray());
            }

            BuildPacket(CommandType.ZDO_BIND_REQ, framedata.ToArray());
        }
    }
}
