using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_UNBIND_REQ : ZToolPacket
    {

        public ZDO_UNBIND_REQ(ZToolAddress16 nwkDst, ZToolAddress64 ieeeSrc, byte epSrc, DoubleByte cluster,
                byte addressingMode, ZToolAddress64 ieeeDst, byte epDst)
        {
            byte[] framedata;
            if (addressingMode == 3)
            {
                framedata = new byte[23];
            }
            else
            {
                framedata = new byte[16];
            }
            framedata[0] = nwkDst.Lsb;
            framedata[1] = nwkDst.Msb;
            byte[] bytes = ieeeSrc.Address;
            for (int i = 0; i < 8; i++)
            {
                framedata[i + 2] = (byte)(bytes[7 - i] & 0xFF);
            }
            framedata[10] = epSrc;
            framedata[11] = cluster.Lsb;
            framedata[12] = cluster.Msb;
            framedata[13] = addressingMode;
            bytes = ieeeDst.Address;
            if (addressingMode == 3)
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
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_UNBIND_REQ), framedata);
        }

        public ZDO_UNBIND_REQ(ushort nwkDstAdr, ushort clusterId, ulong bindSrcAdr, byte bindSrcEP, ulong bindDstAdr,
                byte bindDstEP)
        {
            byte[] framedata = new byte[23];
            framedata[0] = nwkDstAdr.GetByte(0);
            framedata[1] = nwkDstAdr.GetByte(1);

            byte[] bSrcAddr = BitConverter.GetBytes(bindSrcAdr);

            for (int i = 0; i < 8; i++)
            {
                framedata[i + 2] = bSrcAddr[i];
            }
            framedata[10] = (byte)(bindSrcEP & 0xFF);
            framedata[11] = clusterId.GetByte(0);
            framedata[12] = clusterId.GetByte(1);
            framedata[13] = ADDRESS_MODE.ADDRESS_64_BIT;

            byte[] bDstAddr = BitConverter.GetBytes(bindDstAdr);


            for (int i = 0; i < 8; i++)
            {
                framedata[i + 14] = bDstAddr[i];
            }
            framedata[22] = (byte)(bindDstEP & 0xFF); // TODO REMOVE?!??!
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_UNBIND_REQ), framedata);
        }

        /// <name>TI.ZPI1.ZDO_UNBIND_REQ.ADDRESS_MODE</name>
        /// <summary>Specified the format of the coordinator address</summary>
        private class ADDRESS_MODE
        {
            /// <name>TI.ZPI1.ZDO_UNBIND_REQ.ADDRESS_MODE.ADDRESS_16_BIT</name>
            /// <summary>Specified the format of the coordinator address</summary>
            public const byte ADDRESS_16_BIT = 2;
            /// <name>TI.ZPI1.ZDO_UNBIND_REQ.ADDRESS_MODE.ADDRESS_64_BIT</name>
            /// <summary>Specified the format of the coordinator address</summary>
            public const byte ADDRESS_64_BIT = 3;
            /// <name>TI.ZPI1.ZDO_UNBIND_REQ.ADDRESS_MODE.ADDRESS_NOT_PRESENT</name>
            /// <summary>Specified the format of the coordinator address</summary>
            public const byte ADDRESS_NOT_PRESENT = 0;
            /// <name>TI.ZPI1.ZDO_UNBIND_REQ.ADDRESS_MODE.BROADCAST</name>
            /// <summary>Specified the format of the coordinator address</summary>
            public const byte BROADCAST = 15;
            /// <name>TI.ZPI1.ZDO_UNBIND_REQ.ADDRESS_MODE.GROUP_ADDRESS</name>
            /// <summary>Specified the format of the coordinator address</summary>
            public const byte GROUP_ADDRESS = 1;
        }
    }
}
