using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    /// <summary>
    /// This command is used to control the joining permissions and thus allows or disallows new devices from joining the network
    /// </summary>
    public class ZB_PERMIT_JOINING_REQUEST : ZToolPacket
    {
        /// <summary>
        /// The destination parameter indicates the address of the device for which the joining permissions should be set. 
        /// This is usually the local device address or the special broadcast address that denotes all routers and coordinator (0xFFFC). 
        /// This way the joining permissions of a single device or the whole network can be controlled
        /// </summary>
        public ushort Destination { get; private set; }

        public byte Timeout { get; private set; }

        public ZB_PERMIT_JOINING_REQUEST(ushort dstAddr, byte timeout)
        {
            Destination = dstAddr;
            Timeout = timeout;

            List<byte> data = new List<byte>();
            data.AddRange(Destination.GetBytes());
            data.Add(Timeout);

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_PERMIT_JOINING_REQUEST), data.ToArray());
        }
    }
}
