using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    /// <summary>
    /// This command is generated to request a Management Leave Request for the target device and is used to remove
    /// devices from the network.
    /// </summary>
    public class ZDO_MGMT_LEAVE_REQ : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_MGMT_LEAVE_REQ.DeviceAddress</name>
        /// <summary>The 64 bit IEEE Address of the device you want to leave.</summary>
        public ZToolAddress64 DeviceAddress { get; private set; }
        /// <name>TI.ZPI1.ZDO_MGMT_LEAVE_REQ.DstAddr</name>
        /// <summary>Destination network address.</summary>
        public ZToolAddress16 DstAddr { get; private set; }
        /// <name>TI.ZPI1.ZDO_MGMT_LEAVE_REQ.RemoveChildren</name>
        /// <summary>This field has a value of 1 if the device being asked to leave the network is also being asked to
        /// remove its child devices, if any. Otherwise it has a value of 0. Currently, the stack profile of Home Control
        /// specifies that this field should always be set to 0</summary>
        public byte RemoveChildren_Rejoin { get; private set; }

        /// <name>TI.ZPI1.ZDO_MGMT_LEAVE_REQ</name>
        /// <summary>Constructor</summary>
        public ZDO_MGMT_LEAVE_REQ()
        {
        }

        public ZDO_MGMT_LEAVE_REQ(ZToolAddress16 num1, ZToolAddress64 num2, byte flag1)
        {
            this.DstAddr = num1;
            this.DeviceAddress = num2;
            this.RemoveChildren_Rejoin = flag1;

            byte[] framedata = new byte[11];
            framedata[0] = this.DstAddr.Lsb;
            framedata[1] = this.DstAddr.Msb;
            for (int i = 0; i < 8; i++)
            {
                framedata[2 + i] = this.DeviceAddress.Address[7 - i];
            }
            framedata[10] = this.RemoveChildren_Rejoin;
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_MGMT_LEAVE_REQ), framedata);
        }
    }
}
