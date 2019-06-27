using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    /// <summary>
    /// This callback message is in response to incoming data to any of the registered endpoints on this device
    /// </summary>
    public class AF_INCOMING_MSG : ZToolPacket
    {
        /// <summary>
        /// Specifies the group ID of the device
        /// </summary>
        public DoubleByte GroupId { get; private set; }

        /// <summary>
        /// Specifies the cluster Id (only the LSB is used in V1.0 networks.)
        /// </summary>
        public DoubleByte ClusterId { get; private set; }

        /// <summary>
        /// Specifies the ZigBee network address of the source device sending the message
        /// </summary>
        public ZToolAddress16 SrcAddr { get; private set; }

        /// <summary>
        /// Specifies the source endpoint of the message 
        /// </summary>
        public byte SrcEndpoint { get; private set; }

        /// <summary>
        /// Specifies the destination endpoint of the message 
        /// </summary>
        public byte DstEndpoint { get; private set; }

        /// <summary>
        /// Specifies if the message was a broadcast or not 
        /// </summary>
        public byte WasBroadcast { get; private set; }

        /// <summary>
        /// Indicates the link quality measured during reception 
        /// </summary>
        public byte LinkQuality { get; private set; }

        /// <summary>
        /// Specifies if the security is used or not
        /// </summary>
        public byte SecurityUse { get; private set; }

        /// <summary>
        /// Specifies the timestamp of the message
        /// </summary>
        public long TimeStamp { get; private set; }

        /// <summary>
        /// Specifies transaction sequence number of the message 
        /// </summary>
        public byte TransSeqNumber { get; private set; }

        /// <summary>
        /// Specifies the length of the data. 
        /// </summary>
        public byte Len { get; private set; }

        /// <summary>
        /// Contains 0 to 128 bytes of data. 

        /// </summary>
        public byte[] Data { get; private set; }

        public AF_INCOMING_MSG(byte[] framedata)
        {
            GroupId = new DoubleByte(framedata[1], framedata[0]);
            ClusterId = new DoubleByte(framedata[3], framedata[2]);
            SrcAddr = new ZToolAddress16(framedata[5], framedata[4]);
            SrcEndpoint = framedata[6];
            DstEndpoint = framedata[7];
            WasBroadcast = framedata[8];
            LinkQuality = framedata[9];
            SecurityUse = framedata[10];
            byte[] bytes = new byte[4];
            bytes[3] = (byte)framedata[11];
            bytes[2] = (byte)framedata[12];
            bytes[1] = (byte)framedata[13];
            bytes[0] = (byte)framedata[14];
            TimeStamp = BitConverter.ToInt32(bytes, 0);
            TransSeqNumber = framedata[15];
            Len = framedata[16];
            Data = new byte[Len];
            for (int i = 0; i < this.Data.Length; i++)
            {
                this.Data[i] = framedata[17 + i];
            }

            BuildPacket(new DoubleByte((ushort)ZToolCMD.AF_INCOMING_MSG), framedata);
        }
    }
}
