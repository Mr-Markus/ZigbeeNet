using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    /// <summary>
    /// This command establishes or removes a ‘binding’ between two devices.  
    /// Once bound, an application can send messages to a device by referencing the commandId for the binding
    /// </summary>
    public class ZB_BIND_DEVICE : ZToolPacket
    {
        /// <summary>
        /// TRUE to create a binding, FALSE to remove a binding
        /// </summary>
        public bool Create { get; private set; }

        /// <summary>
        /// The Identifier of the binding 
        /// </summary>
        public DoubleByte CommandId { get; private set; }

        /// <summary>
        /// Specifies the 64-bit IEEE address of the device to bind to
        /// </summary>
        public ZToolAddress64 Destination { get; private set; }

        public ZB_BIND_DEVICE(bool create, DoubleByte commandId, ZToolAddress64 destination)
        {
            Create = create;
            CommandId = commandId;
            Destination = destination;

            byte[] framedata = new byte[11];
            framedata[0] = create ? (byte)0x01 : (byte)0x00;
            framedata[1] = commandId.Lsb;
            framedata[2] = commandId.Msb;

            byte[] dst = destination.Address;
            for (int i = 3; i < 8; i++)
            {
                framedata[i] = dst[i - 3];
            }

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_BIND_DEVICE), framedata);
        }
    }
}
