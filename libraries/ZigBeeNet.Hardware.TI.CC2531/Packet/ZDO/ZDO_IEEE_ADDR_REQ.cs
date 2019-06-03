using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    /// <summary>
    /// This command will request a device’s IEEE 64-bit address.  You must subscribe to “ZDO IEEE Address Response” to receive 
    /// the data response to this message.  The response message listed below only indicates whether or not the message was received properly
    /// </summary>
    public class ZDO_IEEE_ADDR_REQ : ZToolPacket
    {
        /// <summary>
        /// Specifies the short address of the device
        /// </summary>
        public ZToolAddress16 ShortAddress { get; private set; }

        /// <summary>
        /// Value that the search was executed on
        /// </summary>
        public RequestType ReqType { get; private set; }

        /// <summary>
        /// Starting index into the list of children.  This is used to get more of the list if the list is too large for one message
        /// </summary>
        public byte StartIndex { get; private set; }

        public ZDO_IEEE_ADDR_REQ(ZToolAddress16 shortAddress, RequestType reqType, byte startIndex)
        {
            ShortAddress = shortAddress;
            ReqType = reqType;
            StartIndex = startIndex;

            List<byte> data = new List<byte>();
            data.AddRange(ShortAddress.Address);
            data.Add((byte)ReqType);
            data.Add(StartIndex);

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_IEEE_ADDR_REQ), data.ToArray());
        }

        public enum RequestType : byte
        {
            SingleDeviceResponse = 0x00,
            ExtendedIncludeAssociatedDevices  = 0x01
        }
    }
}
