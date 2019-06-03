using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    /// <summary>
    /// This callback is called asynchronously by the ZigBee stack to notify the application when data is received from a peer device
    /// </summary>
    public class ZB_RECEIVE_DATA_INDICATION : ZToolPacket
    {
        /// <summary>
        /// Specifies the short address of the peer device that sent the data
        /// </summary>
        public ZToolAddress16 Source { get; private set; }

        /// <summary>
        /// The command Id associated with the data
        /// </summary>
        public DoubleByte Command { get; private set; }

        /// <summary>
        /// Specifies the number of bytes in the Data parameter
        /// </summary>
        public DoubleByte Len { get; private set; }

        /// <summary>
        /// The data sent by the peer device
        /// </summary>
        public byte[] Data { get; private set; }

        public ZB_RECEIVE_DATA_INDICATION(byte[] framedata)
        {
            Source = new ZToolAddress16(framedata[1], framedata[0]);
            Command = new DoubleByte(framedata[3], framedata[2]);
            Len = new DoubleByte(framedata[5], framedata[4]);
            Data = new byte[framedata.Length - 6];

            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = framedata[i + 6];
            }

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_RECEIVE_DATA_INDICATION), framedata);
        }
    }
}
