using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.SimpleAPI
{
    /// <summary>
    /// This command initiates transmission of data to a peer device.
    /// </summary>
    public class ZB_SEND_DATA_REQUEST : SynchronousRequest
    {

        /// <summary>
        /// The command Id to send with the message.
        /// If the ZB_BINDING_ADDR destination is used, this parameter also indicates the binding to use. 
        /// </summary>
        public DoubleByte CommandId { get; private set; }

        /// <summary>
        /// The destination of the data.
        /// The destination can be one of the following:
        /// - 16-Bit short address of device [0-0xfffD]
        /// - ZB_BROADCAST_ADDR sends the data to all devices in the network.
        /// - ZB_BINDING_ADDR sends the data to a previously bound device. 
        /// </summary>
        public ZAddress16 Destination { get; private set; }

        /// <summary>
        /// A handle used to Identify the send data request. 
        /// </summary>
        public int Handle { get; private set; }

        /// <summary>
        /// Specifies the size of the Data buffer in bytes. 
        /// </summary>
        public int PayloadLength { get; private set; }

        /// <summary>
        /// Buffer to hold the configuration property.
        /// </summary>
        public byte[] PayloadValue { get; private set; }

        /// <summary>
        /// The max number of hops the packet can travel through before it is dropped.
        /// </summary>
        public int Radius { get; private set; }

        /// <summary>
        /// TRUE if requesting acknowledgement from the destination. 
        /// </summary>
        public int Ack { get; private set; }

        public ZB_SEND_DATA_REQUEST()
        {
            PayloadValue = new byte[0xff];
        }

        public ZB_SEND_DATA_REQUEST(ZAddress16 destination, DoubleByte commandId, int handle, int ack, int radius, int payloadLength, byte[] buffer)
        {
            // TODO: check buffer length
            Destination = destination;
            CommandId = commandId;
            Handle = handle;
            Ack = ack;
            Radius = radius;
            PayloadLength = payloadLength;
            PayloadValue = buffer;

            byte[] framedata = new byte[PayloadValue.Length + 8];
            framedata[0] = Destination.DoubleByte.Low;
            framedata[1] = Destination.DoubleByte.High;
            framedata[2] = CommandId.Low;
            framedata[3] = CommandId.High;
            framedata[4] = (byte)Handle;
            framedata[5] = (byte)Ack;
            framedata[6] = (byte)Radius;
            framedata[7] = (byte)PayloadLength;

            for (int i = 0; i < this.PayloadValue.Length; i++)
            {
                framedata[i + 8] = this.PayloadValue[i];
            }

            BuildPacket(CommandType.ZB_SEND_DATA_REQUEST, framedata);
        }
    }
}
