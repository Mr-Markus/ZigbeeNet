using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Field
{
    public class NodeDescriptor
    {
        private int _apsFlags;

        public byte BufferSize { get; private set; }
        public bool ComplexDescriptorAvailable { get; private set; }
        public ushort ManufacturerCode { get; private set; }
        public LogicalType LogicalNodeType { get; private set; } = LogicalType.UNKNOWN;
        public ServerCapabilitiesType ServerCapabilities { get; private set; }
        public ushort IncomingTransferSize { get; set; }
        public ushort OutgoingTransferSize { get; private set; }
        public bool IsuserDescriptorAvailable { get; private set; }

        public FrequencyBandType FrequencyBands { get; private set; }
        public MacCapabilitiesType MacCapabilities { get; private set; }
        public bool IsextendedEndpointListAvailable { get; private set; }
        public bool ExtendedSimpleDescriptorListAvailable { get; private set; }
        public int StackCompliance { get; private set; }

        private const int R21_BITMASK = 0xFE00;
        private const int R21_BITSHIFT = 9;

        public enum LogicalType : byte
        {
            COORDINATOR = 0,
            ROUTER = 1,
            END_DEVICE = 2,
            UNKNOWN = 0xff
        }

        [Flags]
        public enum FrequencyBandType
        {
            FREQ_868_MHZ = 0x01,
            FREQ_902_MHZ = 0x02,
            FREQ_2400_MHZ = 0x04,
        }

        [Flags]
        public enum ServerCapabilitiesType
        {
            PRIMARY_TRUST_CENTER = 0x01,
            BACKUP_TRUST_CENTER = 0x02,
            PRIMARY_BINDING_TABLE_CACHE = 0x04,
            BACKUP_BINDING_TABLE_CACHE = 0x08,
            PRIMARY_DISCOVERY_CACHE = 0x10,
            BACKUP_DISCOVERY_CACHE = 0x20,
            NETWORK_MANAGER = 0x40,
        }

        public enum DescriptorCapabilityType
        {
            EXTENDED_ACTIVE_ENDPOINT_LIST,
            EXTENDED_SIMPLE_DESCRIPTER
        }

        public enum MacCapabilitiesType
        {
            ALTERNATIVE_PAN = 0x01,
            FULL_FUNCTION_DEVICE = 0x02,
            //REDUCED_FUNCTION_DEVICE,
            MAINS_POWER = 0x04,
            RECEIVER_ON_WHEN_IDLE = 0x08,
            SECURITY_CAPABLE = 0x40,
        }

        public NodeDescriptor()
        {
            // Default constructor - does nothing
        }

        public NodeDescriptor(int apsFlags, byte bufferSize, byte macCapabilities, bool complexDescriptorAvailable,
                ushort manufacturerCode, byte logicalType, short serverMask, ushort transferSize, bool userDescriptorAvailable,
                byte frequencyBands)
        {
            ComplexDescriptorAvailable = complexDescriptorAvailable;
            IsuserDescriptorAvailable = userDescriptorAvailable;
            ManufacturerCode = manufacturerCode;
            BufferSize = bufferSize;
            IncomingTransferSize = transferSize;
            SetLogicalType(logicalType);
            SetMacCapabilities(macCapabilities);
            SetFrequencyBands(frequencyBands);
            SetServerCapabilities(serverMask);

            _apsFlags = apsFlags;
        }


        private void SetMacCapabilities(byte macCapabilities)
        {
            MacCapabilities = (MacCapabilitiesType)(macCapabilities & 0x4f);
        }

        private void SetLogicalType(byte logicalType)
        {
            LogicalNodeType = logicalType<=2 ? (LogicalType)logicalType : LogicalType.UNKNOWN;
        }

        private void SetServerCapabilities(short serverMask)
        {
            ServerCapabilities = (ServerCapabilitiesType)(serverMask & 0x00FF);
            StackCompliance = (serverMask & R21_BITMASK) >> R21_BITSHIFT;
        }

        private void SetFrequencyBands(byte frequencyBands)
        {
            FrequencyBands = (FrequencyBandType)(frequencyBands & 0x0f);
        }

        /// <summary>
         /// Serialise the contents of the structure.
         ///
         /// <param name="serializer">the <see cref="ZclFieldSerializer"> used to serialize</param>
         /// </summary>
        public byte[] Serialize(ZclFieldSerializer serializer)
        {
            // Serialize the fields
            serializer.Serialize(LogicalNodeType, DataType.SIGNED_8_BIT_INTEGER);
            serializer.Serialize(LogicalNodeType, DataType.SIGNED_8_BIT_INTEGER);
            serializer.Serialize(LogicalNodeType, DataType.SIGNED_8_BIT_INTEGER);
            serializer.Serialize(LogicalNodeType, DataType.SIGNED_8_BIT_INTEGER);
            serializer.Serialize(LogicalNodeType, DataType.SIGNED_8_BIT_INTEGER);
            serializer.Serialize(LogicalNodeType, DataType.SIGNED_8_BIT_INTEGER);
            serializer.Serialize(LogicalNodeType, DataType.SIGNED_8_BIT_INTEGER);

            return serializer.Payload;
        }

        /// <summary>
         /// Deserialise the contents of the structure.
         ///
         /// <param name="deserializer">the <see cref="ZigBeeDeserializer"> used to deserialize</param>
         /// </summary>
        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            // Deserialize the fields
            // logicalType = (LogicalType) deserializer.deserialize(ZclDataType.SIGNED_8_BIT_INTEGER);

            // Some flags...
            byte value1 = deserializer.ReadZigBeeType<byte>(DataType.DATA_8_BIT);
            byte value2 = deserializer.ReadZigBeeType<byte>(DataType.DATA_8_BIT);
            byte value3 = deserializer.ReadZigBeeType<byte>(DataType.DATA_8_BIT);

            SetLogicalType((byte)(value1 & 0x07));
            ComplexDescriptorAvailable = (value1 & 0x08) != 0;
            IsuserDescriptorAvailable = (value1 & 0x10) != 0;

            SetFrequencyBands((byte)((value2 & 0xf8) >> 3));
            SetMacCapabilities(value3);

            // complexDescriptorAvailable = (Boolean) deserializer.deserialize(ZclDataType.BOOLEAN);
            // userDescriptorAvailable = (Boolean) deserializer.deserialize(ZclDataType.BOOLEAN);
            ManufacturerCode = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            BufferSize = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            IncomingTransferSize = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);

            SetServerCapabilities(deserializer.ReadZigBeeType<short>(DataType.SIGNED_16_BIT_INTEGER));
            OutgoingTransferSize = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            sbyte descriptorCapabilities = deserializer.ReadZigBeeType<sbyte>(DataType.SIGNED_8_BIT_INTEGER);

            IsextendedEndpointListAvailable = (descriptorCapabilities & 0x01) != 0;
            ExtendedSimpleDescriptorListAvailable = (descriptorCapabilities & 0x02) != 0;
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + _apsFlags;
            result = prime * result + BufferSize;
            result = prime * result + (ComplexDescriptorAvailable ? 1231 : 1237);
            result = prime * result + (IsextendedEndpointListAvailable ? 1231 : 1237);
            result = prime * result + (ExtendedSimpleDescriptorListAvailable ? 1231 : 1237);
            result = prime * result + FrequencyBands.GetHashCode();
            result = prime * result + IncomingTransferSize;
            result = prime * result + LogicalNodeType.GetHashCode();
            result = prime * result + MacCapabilities.GetHashCode();
            result = prime * result + ManufacturerCode;
            result = prime * result + OutgoingTransferSize;
            result = prime * result + ServerCapabilities.GetHashCode();
            result = prime * result + (IsuserDescriptorAvailable ? 1231 : 1237);
            return result;
        }

        public override bool Equals(Object obj)
        {
            if (this == obj)
                return true;
            if (obj is null)
                return false;
            return (obj is NodeDescriptor other) && 
                    (_apsFlags == other._apsFlags) &&
                    (BufferSize == other.BufferSize) &&
                    (ComplexDescriptorAvailable == other.ComplexDescriptorAvailable) &&
                    (IsextendedEndpointListAvailable == other.IsextendedEndpointListAvailable) &&
                    (ExtendedSimpleDescriptorListAvailable == other.ExtendedSimpleDescriptorListAvailable) &&
                    (FrequencyBands == other.FrequencyBands) &&
                    (IncomingTransferSize == other.IncomingTransferSize) &&
                    (LogicalNodeType == other.LogicalNodeType) &&
                    (MacCapabilities == other.MacCapabilities ) &&
                    (ManufacturerCode == other.ManufacturerCode) &&
                    (OutgoingTransferSize == other.OutgoingTransferSize) &&
                    (ServerCapabilities == other.ServerCapabilities) &&
                    (IsuserDescriptorAvailable == other.IsuserDescriptorAvailable);
        }


        public override string ToString()
        {
            return "NodeDescriptor [apsFlags=" + _apsFlags + ", bufferSize=" + BufferSize + ", complexDescriptorAvailable="
                    + ComplexDescriptorAvailable + ", manufacturerCode=" + ManufacturerCode + ", logicalType=" + LogicalNodeType
                    + ", serverCapabilities=" + ServerCapabilities.ToString() + ", incomingTransferSize=" + IncomingTransferSize
                    + ", outgoingTransferSize=" + OutgoingTransferSize + ", userDescriptorAvailable="
                    + IsuserDescriptorAvailable + ", frequencyBands=" + FrequencyBands.ToString() + ", macCapabilities="
                    + MacCapabilities.ToString() + ", extendedEndpointListAvailable=" + IsextendedEndpointListAvailable
                    + ", extendedSimpleDescriptorListAvailable=" + ExtendedSimpleDescriptorListAvailable
                    + ", stackCompliance=" + StackCompliance + "]";
        }

    }
}
