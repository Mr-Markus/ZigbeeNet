using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL
{
    public class ZclHeader
    {
        private static byte MASK_FRAME_TYPE = 0b00000011;
        private static byte MASK_MANUFACTURER_SPECIFIC = 0b00000100;
        private static byte MASK_DIRECTION = 0b00001000;
        private static byte MASK_DEFAULT_RESPONSE = 0b00010000;
                       
        private const byte FRAME_TYPE_ENTIRE_PROFILE = 0x00;
        private const byte FRAME_TYPE_CLUSTER_SPECIFIC = 0x01;

        /// <summary>
        /// The frame type sub-field
        /// Specifies if this is a <i>generic</i> command used across the entire profile ENTIRE_PROFILE_COMMAND
        /// or a command that is specific to a single cluster CLUSTER_SPECIFIC_COMMAND.
        /// </summary>
        public ZclFrameType FrameType { get; set; }

        /// <summary>
        /// The manufacturer specific sub-field is 1 bit in length and specifies whether this command refers to a
        /// manufacturer specific extension to a profile. If this value is set to 1, the manufacturer code field shall be
        /// present in the ZCL frame. If this value is set to 0, the manufacturer code field shall not be included in the ZCL
        /// frame.
        /// </summary>
        public bool ManufacturerSpecific { get; set; }

        /// <summary>
        /// The direction sub-field specifies the client/server direction for this command. If this value is set to 1, the
        /// command is being sent from the server side of a cluster to the client side of a cluster. If this value is set to
        /// 0, the command is being sent from the client side of a cluster to the server side of a cluster.
        /// </summary>
        public ZclCommandDirection Direction { get; set; }

        /// <summary>
        /// The disable default response sub-field is 1 bit in length. If it is set to 0, the Default response command will
        /// be returned. If it is set to 1, the Default response command will only be returned if there is an error.
        /// </summary>
        public bool DisableDefaultResponse { get; set; }

        /// <summary>
        /// The manufacturer code field is 16 bits in length and specifies the ZigBee assigned manufacturer code for proprietary extensions. 
        /// This field SHALL only be included in the ZCL frame if the manufacturer specific sub-field of the frame control field is set to True. 
        /// </summary>
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
        public byte SequenceNumber { get; set; }

        /// <summary>
        /// The Command Identifier field is 8 bits in length and specifies the cluster command being used. 
        /// 
        /// If the frame type sub-field of the frame control field is set to 0b00, the command identifier corresponds to one of the non-reserved values of GlobalCommands. 
        /// If the frame type sub-field of the frame control field is set to 0b01, the command identifier corresponds to a cluster specific command. 
        /// 
        /// The cluster specific command identifiers can be found in each individual document describing the clusters (see also 2.2.1.1). 
        /// </summary>
        public byte CommandId { get; set; }

        public ZclHeader()
        {
            ManufacturerSpecific = false;
            ManufacturerCode = 0;
            DisableDefaultResponse = false;
        }

        public ZclHeader(ZclFieldDeserializer fieldDeserializer)
        {
            byte frameControl = (byte)fieldDeserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            switch (frameControl & MASK_FRAME_TYPE)
            {
                case FRAME_TYPE_CLUSTER_SPECIFIC:
                    FrameType = ZclFrameType.CLUSTER_SPECIFIC_COMMAND;
                    break;
                case FRAME_TYPE_ENTIRE_PROFILE:
                    FrameType = ZclFrameType.ENTIRE_PROFILE_COMMAND;
                    break;
                default:
                    break;
            }

            DisableDefaultResponse = (frameControl & MASK_DEFAULT_RESPONSE) != 0;
            Direction = (frameControl & MASK_DIRECTION) == 0 ? ZclCommandDirection.CLIENT_TO_SERVER
                    : ZclCommandDirection.SERVER_TO_CLIENT;
            ManufacturerSpecific = (frameControl & MASK_MANUFACTURER_SPECIFIC) != 0;

            // If manufacturerSpecific is set then get the manufacturer code
            if (ManufacturerSpecific)
            {
                ManufacturerCode = (ushort)fieldDeserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }
            SequenceNumber = (byte)fieldDeserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            CommandId = (byte)fieldDeserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public byte[] Serialize(ZclFieldSerializer fieldSerializer, byte[] payload)
        {
            byte frameControl = 0;

            switch (FrameType)
            {
                case ZclFrameType.CLUSTER_SPECIFIC_COMMAND:
                    frameControl |= FRAME_TYPE_CLUSTER_SPECIFIC;
                    break;
                case ZclFrameType.ENTIRE_PROFILE_COMMAND:
                    frameControl |= FRAME_TYPE_ENTIRE_PROFILE;
                    break;
                default:
                    break;
            }

            frameControl |= Direction == ZclCommandDirection.SERVER_TO_CLIENT ? MASK_DIRECTION : (byte)0b00000000;
            frameControl |= DisableDefaultResponse ? MASK_DEFAULT_RESPONSE : (byte)0b00000000;

            byte[] zclFrame = new byte[payload.Length + 3];
            zclFrame[0] = frameControl;
            zclFrame[1] = SequenceNumber;
            zclFrame[2] = CommandId;

            for (int i = 0; i < payload.Length; i++)
            {
                zclFrame[i + 3] = payload[i];
            }

            return zclFrame;
        }

        public override string ToString()
        {
            return "ZclHeader [frameType=" + FrameType + ", manufacturerSpecific=" + ManufacturerSpecific + ", direction="
                    + Direction + ", disableDefaultResponse=" + DisableDefaultResponse + ", manufacturerCode="
                    + ManufacturerCode + ", sequenceNumber=" + SequenceNumber + ", commandId=" + CommandId + "]";
        }
    }
}
