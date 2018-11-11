using BinarySerialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public enum FrameType : byte
    {
        Global = 0x00,
        ClusterSpecific = 0x01
    }

    public enum Direction : byte
    {
        ClientToServer = 0x00,
        ServerToClient = 0x01
    }

    public enum DisableDefaultResponse
    {
        Always = 0x00,
        OnlyOnError = 0x01
    }

    /// <summary>
    /// The frame control field is 8 bits in length and contains information defining the command type and other control flags. 
    /// The frame control field SHALL be formatted as shown in Figure 2-3. Bits 5-7 are reserved for future use and SHALL be set to 0
    /// 
    /// Bits: |     0-1     |           2           |     3     |           4               |   5-7    | 
    ///       | Frame type  | Manufacturer specific | Direction | Disable Default Response  | Reserved |
    /// </summary>
    public class ZclFrameControl
    {
        /// <summary>
        ///  If the frame type sub-field of the frame control field is set to 0b00, the command identifier 
        ///  corresponds to one of the non-reserved values of Table 2-3. 
        ///  If the frame type sub-field of the frame control field is set to 0b01, 
        ///  the command identifier corresponds to a cluster specific command
        /// </summary>
        [FieldOrder(0)]
        [FieldBitLength(2)]
        public FrameType Type { get; set; }
        /// <summary>
        /// The manufacturer specific sub-field is 1 bit in length and specifies whether this command refers 
        /// to a manufacturer specific extension. If this value is set to 1, the manufacturer code field SHALL 
        /// be present in the ZCL frame. If this value is set to 0, the manufacturer code field SHALL not be 
        /// included in the ZCL frame. Manufacturer specific clusters SHALL support global commands (Frame Type 0b00) 3. 
        /// </summary>
        [FieldOrder(1)]
        [FieldBitLength(1)]
        public bool ManufacturerSpecific { get; set; }
        /// <summary>
        /// The direction sub-field specifies the client/server direction for this command. If this value is set to 1, 
        /// the command is being sent from the server side of a cluster to the client side of a cluster. If this value 
        /// is set to 0, the command is being sent from the client side of a cluster to the server side of a cluster.
        /// </summary>
        [FieldOrder(2)]
        [FieldBitLength(1)]
        public Direction Direction { get; set; }
        /// <summary>
        /// The disable Default Response sub-field is 1 bit in length. If it is set to 0, the Default Response command will be 
        /// returned, under the conditions specified in 2.5.12.2. If it is set to 1, the Default Response command will only be 
        /// returned if there is an error, also under the conditions specified in 2.5.12.2. This field SHALL be set to 1, for 
        /// all response frames generated as the immediate and direct effect of a previously received frame.
        /// </summary>
        [FieldOrder(3)]
        [FieldBitLength(1)]
        public DisableDefaultResponse DisableDefaultResponse { get; set; }

        /// <summary>
        /// This is the field for the binary serializer to return just one byte which is the size of the FrameControl
        /// </summary>
        [Ignore()]
        public byte Frame
        {
            get
            {
                return (byte)(((byte)Type << 6) | (Convert.ToByte(ManufacturerSpecific) << 5) | ((byte)Direction << 4) | ((byte)DisableDefaultResponse << 3));
            }
        }
    }
}
