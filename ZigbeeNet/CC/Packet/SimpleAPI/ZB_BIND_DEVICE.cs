using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.SimpleAPI
{
    /// <summary>
    /// This command establishes or removes a ‘binding’ between two devices.  
    /// Once bound, an application can send messages to a device by referencing the commandId for the binding
    /// </summary>
    public class ZB_BIND_DEVICE : SynchronousRequest
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
        public ZigBeeAddress64 Destination { get; private set; }

        public ZB_BIND_DEVICE(bool create, DoubleByte commandId, ZigBeeAddress64 destination)
        {
            Create = create;
            CommandId = commandId;
            Destination = destination;

            byte[] framedata = new byte[11];
            framedata[0] = create ? (byte)0x01 : (byte)0x00;
            framedata[1] = commandId.Lsb;
            framedata[2] = commandId.Msb;

            byte[] dst = destination.ToByteArray();
            for (int i = 3; i < 8; i++)
            {
                framedata[i] = dst[i - 3];
            }

            BuildPacket(CommandType.ZB_BIND_DEVICE, framedata);
        }
    }
}
