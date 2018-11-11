using BinarySerialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public class FrameHeader
    {
        public FrameHeader(ZclFrameControl frameControl, byte commandIdentifier)
        {
            FrameControl = frameControl;
            CommandIdentifierField = commandIdentifier;
        }
        public FrameHeader(FrameType frameType, Direction direction, byte commandIdentifier, DisableDefaultResponse disableDefaultResponse = DisableDefaultResponse.Always, bool manufacturerSpecific = false)
        {
            FrameControl = new ZclFrameControl()
            {
                Type = frameType,
                Direction = direction,
                DisableDefaultResponse = disableDefaultResponse,
                ManufacturerSpecific = manufacturerSpecific
            };
            CommandIdentifierField = commandIdentifier;
        }

        /// <summary>
        /// Just for serialization
        /// </summary>
        [Ignore()]
        public bool ManufacturerSpecific
        {
            get
            {
                return FrameControl.ManufacturerSpecific;
            }
        }
        /// <summary>
        /// The frame control field is 8 bits in length and contains information defining the command type and other control flags. 
        /// Bits 5-7 are reserved for future use and SHALL be set to 0. 
        /// </summary>
        [FieldOrder(0)]
        [FieldBitLength(8)]
        public ZclFrameControl FrameControl { get; set; }

        /// <summary>
        /// The manufacturer code field is 16 bits in length and specifies the ZigBee assigned manufacturer code for proprietary extensions. 
        /// This field SHALL only be included in the ZCL frame if the manufacturer specific sub-field of the frame control field is set to True. 
        /// </summary>
        [FieldOrder(1)]
        [SerializeWhen(nameof(ManufacturerSpecific), true)]
        public ushort ManufacturerCode { get; set; }

        /// <summary>
        /// The Transaction Sequence Number field is 8 bits in length and specifies an identification number for a single transaction that 
        /// includes one or more frames in both directions. 
        /// 
        /// Each time the first frame of a transaction is generated, 
        /// a new value SHALL be copied into the field. When a frame is generated as the specified effect on receipt of a previous frame, 
        /// then it is part of a transaction, and the Transaction Sequence Number SHALL be copied from the previously received frame into the generated frame. 
        /// This includes a frame that is generated in response to request frame4. 
        /// 
        /// The Transaction Sequence Number field can be used by a controlling device, which MAY have issued multiple commands, 
        /// so that it can match the incoming responses to the relevant command. 
        /// </summary>
        [FieldOrder(2)]
        public byte TransactionSequenceNumber { get; set; }

        /// <summary>
        /// The Command Identifier field is 8 bits in length and specifies the cluster command being used. 
        /// 
        /// If the frame type sub-field of the frame control field is set to 0b00, the command identifier corresponds to one of the non-reserved values of GlobalCommands. 
        /// If the frame type sub-field of the frame control field is set to 0b01, the command identifier corresponds to a cluster specific command. 
        /// 
        /// The cluster specific command identifiers can be found in each individual document describing the clusters (see also 2.2.1.1). 
        /// </summary>
        [FieldOrder(3)]
        public byte CommandIdentifierField { get; set; }
    }
}
