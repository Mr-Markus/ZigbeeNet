using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;
using Serilog;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_MSG_CB_INCOMING : ZToolPacket
    {
        public static Dictionary<ushort, Type> ClusterToRSP { get; private set; }

        static ZDO_MSG_CB_INCOMING()
        {
            ClusterToRSP = new Dictionary<ushort, Type>() {
                    { 0x0013, typeof(ZDO_END_DEVICE_ANNCE_IND) },
                    { 0x8000, typeof(ZDO_NWK_ADDR_RSP) },
                    { 0x8001, typeof(ZDO_IEEE_ADDR_RSP) },
                    { 0x8002, typeof(ZDO_NODE_DESC_RSP) },
                    { 0x8003, typeof(ZDO_POWER_DESC_RSP) },
                    { 0x8004, typeof(ZDO_SIMPLE_DESC_RSP) },
                    { 0x8005, typeof(ZDO_ACTIVE_EP_RSP) },
                    { 0x8006, typeof(ZDO_MATCH_DESC_RSP) },
                    { 0x8011, typeof(ZDO_USER_DESC_RSP) },
                    { 0x8014, typeof(ZDO_USER_DESC_CONF) },
                    { 0x8020, typeof(ZDO_END_DEVICE_BIND_RSP) },
                    { 0x8021, typeof(ZDO_BIND_RSP) },
                    { 0x8022, typeof(ZDO_UNBIND_RSP) },
                    { 0x8031, typeof(ZDO_MGMT_LQI_RSP) },
                    { 0x8034, typeof(ZDO_MGMT_LEAVE_RSP) },
                    { 0x8036, typeof(ZDO_MGMT_PERMIT_JOIN_RSP) }
                };
        }

        /// <name>TI.ZPI2.ZDO_MSG_CB_INCOMING.SrcAddr</name>
        /// <summary>Short address (LSB-MSB) of the source of the ZDO message.</summary>
        public ZToolAddress16 SrcAddr { get; private set; }
        /// <name>TI.ZPI2.ZDO_MSG_CB_INCOMING.WasBroadcast</name>
        /// <summary>This field indicates whether or not this ZDO message was broadcast.</summary>
        public int WasBroadcast { get; private set; }
        /// <name>TI.ZPI2.ZDO_MSG_CB_INCOMING.ClusterId</name>
        /// <summary>The ZDO Cluster Id of this message.</summary>
        public DoubleByte ClusterId { get; private set; }
        /// <name>TI.ZPI2.ZDO_MSG_CB_INCOMING.SecurityUse</name>
        /// <summary>N/A - not used.</summary>
        public int SecurityUse { get; private set; }
        /// <name>TI.ZPI2.ZDO_MSG_CB_INCOMING.SeqNum</name>
        /// <summary>The sequence number of this ZDO message.</summary>
        public int SeqNum { get; private set; }
        /// <name>TI.ZPI2.ZDO_MSG_CB_INCOMING.MacDstAddr</name>
        /// <summary>TThe MAC destination short address (LSB-MSB) of the ZDO message.</summary>
        public ZToolAddress16 MacDstAddr { get; private set; }
        /// <name>TI.ZPI2.ZDO_MSG_CB_INCOMING.Data</name>
        /// <summary>The data that corresponds to the Cluster Id of the message.</summary>
        public byte[] Data { get; private set; }

        /// <name>TI.ZPI2.ZDO_MSG_CB_INCOMING</name>
        /// <summary>Constructor</summary>
        public ZDO_MSG_CB_INCOMING()
        {
        }

        public ZDO_MSG_CB_INCOMING(byte[] framedata)
        {
            SrcAddr = new ZToolAddress16(framedata[1], framedata[0]);
            WasBroadcast = framedata[2];
            ClusterId = new DoubleByte(framedata[4], framedata[3]);
            SecurityUse = framedata[5];
            SeqNum = framedata[6];
            MacDstAddr = new ZToolAddress16(framedata[8], framedata[7]);

            //Data = new byte[framedata.Length - 10];

            //Array.Copy(framedata, 9, Data, 0, framedata.Length - 10);

            Data = framedata.Skip(9).ToArray(); // Arrays.copyOfRange(framedata, 9, framedata.Length);

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_MSG_CB_INCOMING), framedata);
        }

        /// <summary>
        /// Translates the ZigBee ZDO cluster packet into a ZTool RSP packet
        /// </summary>
        public ZToolPacket Translate()
        {
            ZToolPacket newPacket = null;
            byte[] frame;

            Log.Verbose("Translating ZDO cluster callback {ClusterId}", ClusterId);

            Type newPacketClass = ClusterToRSP[ClusterId.Value];

            if (newPacketClass == null)
            {
                Log.Error("Unhandled ZDO cluster callback {Cluster}", ClusterId);
                return this;
            }
            else if (newPacketClass == typeof(ZDO_NWK_ADDR_RSP) || newPacketClass == typeof(ZDO_IEEE_ADDR_RSP))
            {
                // The address responses don't need SrcAddr. NumAssocDev and StartIndex positions are reversed.

                // The new response frame is at least 13 bytes long.
                frame = new byte[Math.Max(Data.Length, 13)];
                Array.Copy(Data, 0, frame, 0, Data.Length);
                // If RequestType == 1 there are two extra bytes in the frame
                if (Data.Length > 12)
                {
                    frame[11] = Data[12]; // NumAssocDev
                    frame[12] = Data[11]; // StartIndex
                }
                else
                {
                    frame[11] = 0;
                    frame[12] = 0;
                }
            }
            else
            {
                // Default frame translation, this works for most callbacks.
                // Get 2 extra bytes at the beginning to put source address into.
                frame = new byte[Data.Length + 2];
                Array.Copy(Data, 0, frame, 2, Data.Length);
                frame[0] = SrcAddr.Lsb;
                frame[1] = SrcAddr.Msb;
            }

            try
            {
                //newPacket = Activator.CreateInstance(newPacketClass, frame);
                switch (ClusterId.Value)
                {
                    case 0x0013:
                        newPacket = new ZDO_END_DEVICE_ANNCE_IND(frame);
                        break;
                    case 0x8000:
                        newPacket = new ZDO_NWK_ADDR_RSP(frame);
                        break;
                    case 0x8001:
                        newPacket = new ZDO_IEEE_ADDR_RSP(frame);
                        break;
                    case 0x8002:
                        newPacket = new ZDO_NODE_DESC_RSP(frame);
                        break;
                    case 0x8003:
                        newPacket = new ZDO_POWER_DESC_RSP(frame);
                        break;
                    case 0x8004:
                        newPacket = new ZDO_SIMPLE_DESC_RSP(frame);
                        break;
                    case 0x8005:
                        newPacket = new ZDO_ACTIVE_EP_RSP(frame);
                        break;
                    case 0x8006:
                        newPacket = new ZDO_MATCH_DESC_RSP(frame);
                        break;
                    case 0x8011:
                        newPacket = new ZDO_USER_DESC_RSP(frame);
                        break;
                    case 0x8014:
                        newPacket = new ZDO_USER_DESC_CONF(frame);
                        break;
                    case 0x8020:
                        newPacket = new ZDO_END_DEVICE_BIND_RSP(frame);
                        break;
                    case 0x8021:
                        newPacket = new ZDO_BIND_RSP(frame);
                        break;
                    case 0x8022:
                        newPacket = new ZDO_UNBIND_RSP(frame);
                        break;
                    case 0x8031:
                        newPacket = new ZDO_MGMT_LQI_RSP(frame);
                        break;
                    case 0x8034:
                        newPacket = new ZDO_MGMT_LEAVE_RSP(frame);
                        break;
                    case 0x8036:
                        newPacket = new ZDO_MGMT_PERMIT_JOIN_RSP(frame);
                        break;
                }
            }
            catch (Exception e)
            {
                Log.Error("Error constructing response packet {Exception}", e);
                return this;
            }

            if (newPacket != null)
                newPacket.FCS = this.FCS;

            return newPacket;
        }

        public override string ToString()
        {
            return "ZDO_MSG_CB_INCOMING{" + "ClusterId=" + ClusterId + '}';
        }
    }
}
