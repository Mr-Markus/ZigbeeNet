using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

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

        public ZDO_BIND_REQ(ZigbeeAddress16 nwkDst, ZigbeeAddress64 ieeeSrc, byte epSrc, ZclCluster cluster,
            Address_Mode addressingMode, ZigbeeAddress64 ieeeDst, byte epDst)
        {

            byte[] framedata;
            if (addressingMode == Address_Mode.ADDRESS_64_BIT)
            {
                framedata = new byte[23];
            }
            else
            {
                framedata = new byte[16];
            }
            framedata[0] = nwkDst.DoubleByte.Lsb;
            framedata[1] = nwkDst.DoubleByte.Msb;
            byte[] bytes = ieeeSrc.ToByteArray();
            for (int i = 0; i < 8; i++)
            {
                framedata[i + 2] = (byte)(bytes[7 - i] & 0xFF);
            }
            framedata[10] = epSrc;

            DoubleByte clusterByte = new DoubleByte((ushort)cluster);

            framedata[11] = clusterByte.Lsb;
            framedata[12] = clusterByte.Msb;
            framedata[13] = (byte)addressingMode;
            bytes = ieeeDst.ToByteArray();
            if (addressingMode == Address_Mode.ADDRESS_64_BIT)
            {
                for (int i = 0; i < 8; i++)
                {
                    framedata[i + 14] = (byte)(bytes[7 - i] & 0xFF);
                }
                framedata[22] = epDst;
            }
            else
            {
                framedata[14] = bytes[7];
                framedata[15] = bytes[6];
            }

            BuildPacket(CommandType.ZDO_BIND_REQ, framedata);
        }        
    }
}
