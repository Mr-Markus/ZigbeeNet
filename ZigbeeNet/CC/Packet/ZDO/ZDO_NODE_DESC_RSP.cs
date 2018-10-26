using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    /// <summary>
    /// This callback message is in response to the ZDO Node Descriptor Request
    /// </summary>
    public class ZDO_NODE_DESC_RSP : AsynchronousRequest
    {
        public ZAddress16 SrcAddr { get; private set; }

        public PacketStatus Status { get; private set; }

        public ZAddress16 NwkAddr { get; private set; }

        public NodeType LogicalType { get; private set; }

        public bool ComplexDescriptorAvailable { get; private set; }

        public bool UserDescriptorAvailable { get; private set; }

        public byte APSFlags { get; private set; }

        public byte FrequencyBand { get; private set; }

        public CapabilitiesFlags MacCapabilitiesFlags { get; private set; }

        public DoubleByte ManufacturerCode { get; set; }

        public byte MaxBufferSize { get; private set; }

        public DoubleByte MaxInTransferSize { get; private set; }

        public DoubleByte ServerMask { get; private set; }

        public byte DescriptorCapabilities { get; private set; }

        public ZDO_NODE_DESC_RSP(byte[] framedata)
        {

            SrcAddr = new ZAddress16(framedata[0], framedata[1]);
            Status = (PacketStatus)framedata[2];
            NwkAddr = new ZAddress16(framedata[3], framedata[4]);
            LogicalType = (NodeType)(framedata[5] & (byte)0x07);
            ComplexDescriptorAvailable = ((framedata[5] & (0x08)) >> 3) == 1;
            UserDescriptorAvailable = ((framedata[5] & (16)) >> 4) == 1;
            APSFlags = (byte)(framedata[6] & (byte)0x0F);
            FrequencyBand = (byte)(framedata[6] & (byte)0xF0 >> 4);
            MacCapabilitiesFlags = (CapabilitiesFlags)framedata[10];
            ManufacturerCode = new DoubleByte(framedata[11], framedata[12]);
            MaxBufferSize = framedata[13];
            MaxInTransferSize = new DoubleByte(framedata[14], framedata[15]);
            ServerMask = new DoubleByte(framedata[16], framedata[17]);

            BuildPacket(CommandType.ZDO_NODE_DESC_RSP, framedata);
        }

        public enum CapabilitiesFlags
        {
            CAPINFO_DEVICETYPE_RFD = 0x00,
            CAPINFO_ALTPANCOORD = 0x01,
            CAPINFO_DEVICETYPE_FFD = 0x02,
            CAPINFO_POWER_AC = 0x04,
            CAPINFO_RCVR_ON_IDLE = 0x08,
            CAPINFO_SECURITY_CAPABLE = 0x40,
            CAPINFO_ALLOC_ADDR = 0x80
        }

        public enum SERVER_CAPABILITY
        {
            BACKUP_TRUST_CENTER = 0x02,            
            BAK_BIND_TABLE_CACHE = 0x08,            
            BAK_DISC_CACHE = 50,            
            NONE = 0,            
            PRIM_BIND_TABLE_CACHE = 4,            
            PRIM_DISC_CACHE = 0x16,            
            PRIM_TRUST_CENTER = 1
        }
    }
}
