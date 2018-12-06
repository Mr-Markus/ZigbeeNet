using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    /// <summary>
    /// This callback message is in response to the ZDO Node Descriptor Request
    /// </summary>
    public class ZDO_NODE_DESC_RSP : AsynchronousRequest
    {
        /// <summary>
        /// The message’s source network address
        /// </summary>
        public ZigBeeAddress16 SrcAddr { get; private set; }

        /// <summary>
        /// This field indicates either SUCCESS or FAILURE
        /// </summary>
        public PacketStatus Status { get; private set; }

        /// <summary>
        /// Device’s short address of this Node descriptor 
        /// </summary>
        public ZigBeeAddress16 NwkAddr { get; private set; }

        /// <summary>
        /// Logical Type
        /// </summary>
        public ZigBeeNodeType LogicalType { get; private set; }

        /// <summary>
        /// Indicates if complex descriptor is available for the node 
        /// </summary>
        public bool ComplexDescriptorAvailable { get; private set; }

        /// <summary>
        ///  Indicates if user descriptor is available for the node 
        /// </summary>
        public bool UserDescriptorAvailable { get; private set; }

        /// <summary>
        /// Node Flags assigned for APS. For V1.0 all bits are reserved
        /// </summary>
        public byte APSFlags { get; private set; }

        /// <summary>
        ///  Identifies node frequency band capabilities 
        /// </summary>
        public byte FrequencyBand { get; private set; }

        /// <summary>
        /// Capability flags stored for the MAC
        /// </summary>
        public CapabilitiesFlags MacCapabilitiesFlags { get; private set; }

        /// <summary>
        /// Specifies a manufacturer code that is allocated by the ZigBee Alliance, relating to the manufacturer to the device
        /// </summary>
        public DoubleByte ManufacturerCode { get; set; }

        /// <summary>
        /// Indicates size of maximum NPDU. This field is used as a high level indication for management
        /// </summary>
        public byte MaxBufferSize { get; private set; }

        /// <summary>
        /// Indicates maximum size of Transfer up to 0x7fff (This field is reserved in version 1.0 and shall be set to zero). 
        /// </summary>
        public DoubleByte MaxInTransferSize { get; private set; }

        /// <summary>
        /// Bit 0 - Primary Trust Center       
        ///     1 - Backup Trust Center       
        ///     2 - Primary Binding Table Cache       
        ///     3 - Backup Binding Table Cache       
        ///     4 - Primary Discovery Cache       
        ///     5 - Backup Discovery Cache 
        /// </summary>
        public DoubleByte ServerMask { get; private set; }

        /// <summary>
        /// Specifies the Descriptor capabilities 
        /// </summary>
        public byte DescriptorCapabilities { get; private set; }

        public ZDO_NODE_DESC_RSP(byte[] framedata)
        {

            SrcAddr = new ZigBeeAddress16(framedata[1], framedata[0]);
            Status = (PacketStatus)framedata[2];
            NwkAddr = new ZigBeeAddress16(framedata[4], framedata[3]);
            LogicalType = (ZigBeeNodeType)(framedata[5] & (byte)0x07);
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
