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
        public List<ServerCapabilitiesType> ServerCapabilities { get; private set; } = new List<ServerCapabilitiesType>();
        public ushort IncomingTransferSize { get; set; }
        public ushort OutgoingTransferSize { get; private set; }
        public bool IsuserDescriptorAvailable { get; private set; }

        public List<FrequencyBandType> FrequencyBands { get; } = new List<FrequencyBandType>();
        public List<MacCapabilitiesType> MacCapabilities { get; } = new List<MacCapabilitiesType>();
        public bool IsextendedEndpointListAvailable { get; private set; }
        public bool ExtendedSimpleDescriptorListAvailable { get; private set; }
        public int StackCompliance { get; private set; }

        private const int R21_BITMASK = 0xFE00;
        private const int R21_BITSHIFT = 9;

        public enum LogicalType
        {
            COORDINATOR = 0,
            ROUTER = 1,
            END_DEVICE = 2,
            UNKNOWN = 99
        }

        public enum FrequencyBandType
        {
            FREQ_868_MHZ,
            FREQ_902_MHZ,
            FREQ_2400_MHZ
        }

        public enum ServerCapabilitiesType
        {
            PRIMARY_TRUST_CENTER,
            BACKUP_TRUST_CENTER,
            PRIMARY_BINDING_TABLE_CACHE,
            BACKUP_BINDING_TABLE_CACHE,
            PRIMARY_DISCOVERY_CACHE,
            BACKUP_DISCOVERY_CACHE,
            NETWORK_MANAGER
        }

        public enum DescriptorCapabilityType
        {
            EXTENDED_ACTIVE_ENDPOINT_LIST,
            EXTENDED_SIMPLE_DESCRIPTER
        }

        public enum MacCapabilitiesType
        {
            ALTERNATIVE_PAN,
            FULL_FUNCTION_DEVICE,
            REDUCED_FUNCTION_DEVICE,
            MAINS_POWER,
            RECEIVER_ON_WHEN_IDLE,
            SECURITY_CAPABLE,
            ADDRESS_ALLOCATION
        }

        public NodeDescriptor()
        {
            // Default constructor - does nothing
        }

        public NodeDescriptor(int apsFlags, byte bufferSize, int macCapabilities, bool complexDescriptorAvailable,
                ushort manufacturerCode, int logicalType, short serverMask, ushort transferSize, bool userDescriptorAvailable,
                int frequencyBands)
        {
            this.ComplexDescriptorAvailable = complexDescriptorAvailable;
            this.IsuserDescriptorAvailable = userDescriptorAvailable;
            this.ManufacturerCode = manufacturerCode;
            this.BufferSize = bufferSize;
            this.IncomingTransferSize = transferSize;
            SetLogicalType(logicalType);
            SetMacCapabilities(macCapabilities);
            SetFrequencyBands(frequencyBands);
            SetServerCapabilities(serverMask);

            this._apsFlags = apsFlags;
        }


        private void SetMacCapabilities(int macCapabilities)
        {
            this.MacCapabilities.Clear();
            if ((macCapabilities & 0x01) != 0)
            {
                this.MacCapabilities.Add(MacCapabilitiesType.ALTERNATIVE_PAN);
            }
            if ((macCapabilities & 0x02) != 0)
            {
                this.MacCapabilities.Add(MacCapabilitiesType.FULL_FUNCTION_DEVICE);
            }
            else
            {
                this.MacCapabilities.Add(MacCapabilitiesType.REDUCED_FUNCTION_DEVICE);
            }
            if ((macCapabilities & 0x04) != 0)
            {
                this.MacCapabilities.Add(MacCapabilitiesType.MAINS_POWER);
            }
            if ((macCapabilities & 0x08) != 0)
            {
                this.MacCapabilities.Add(MacCapabilitiesType.RECEIVER_ON_WHEN_IDLE);
            }
            if ((macCapabilities & 0x40) != 0)
            {
                this.MacCapabilities.Add(MacCapabilitiesType.SECURITY_CAPABLE);
            }
        }

        private void SetLogicalType(int logicalType)
        {
            switch (logicalType)
            {
                case 0:
                    this.LogicalNodeType = LogicalType.COORDINATOR;
                    break;
                case 1:
                    this.LogicalNodeType = LogicalType.ROUTER;
                    break;
                case 2:
                    this.LogicalNodeType = LogicalType.END_DEVICE;
                    break;
                default:
                    this.LogicalNodeType = LogicalType.UNKNOWN;
                    break;
            }
        }

        private void SetServerCapabilities(short serverMask)
        {
            this.ServerCapabilities.Clear();
            if ((serverMask & 0x01) != 0)
            {
                this.ServerCapabilities.Add(ServerCapabilitiesType.PRIMARY_TRUST_CENTER);
            }
            if ((serverMask & 0x02) != 0)
            {
                this.ServerCapabilities.Add(ServerCapabilitiesType.BACKUP_TRUST_CENTER);
            }
            if ((serverMask & 0x04) != 0)
            {
                this.ServerCapabilities.Add(ServerCapabilitiesType.PRIMARY_BINDING_TABLE_CACHE);
            }
            if ((serverMask & 0x08) != 0)
            {
                this.ServerCapabilities.Add(ServerCapabilitiesType.BACKUP_BINDING_TABLE_CACHE);
            }
            if ((serverMask & 0x10) != 0)
            {
                this.ServerCapabilities.Add(ServerCapabilitiesType.PRIMARY_DISCOVERY_CACHE);
            }
            if ((serverMask & 0x20) != 0)
            {
                this.ServerCapabilities.Add(ServerCapabilitiesType.BACKUP_DISCOVERY_CACHE);
            }
            if ((serverMask & 0x40) != 0)
            {
                this.ServerCapabilities.Add(ServerCapabilitiesType.NETWORK_MANAGER);
            }

            StackCompliance = (serverMask & R21_BITMASK) >> R21_BITSHIFT;
        }

        private void SetFrequencyBands(int frequencyBands)
        {
            this.FrequencyBands.Clear();
            if ((frequencyBands & 0x01) != 0)
            {
                this.FrequencyBands.Add(FrequencyBandType.FREQ_868_MHZ);
            }
            if ((frequencyBands & 0x04) != 0)
            {
                this.FrequencyBands.Add(FrequencyBandType.FREQ_902_MHZ);
            }
            if ((frequencyBands & 0x08) != 0)
            {
                this.FrequencyBands.Add(FrequencyBandType.FREQ_2400_MHZ);
            }
        }

        /// <summary>
         /// Serialise the contents of the structure.
         ///
         /// <param name="serializer">the <see cref="ZclFieldSerializer"> used to serialize</param>
         /// </summary>
        public byte[] Serialize(ZclFieldSerializer serializer)
        {
            // Serialize the fields
            serializer.Serialize(LogicalNodeType, ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER));
            serializer.Serialize(LogicalNodeType, ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER));
            serializer.Serialize(LogicalNodeType, ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER));
            serializer.Serialize(LogicalNodeType, ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER));
            serializer.Serialize(LogicalNodeType, ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER));
            serializer.Serialize(LogicalNodeType, ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER));
            serializer.Serialize(LogicalNodeType, ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER));

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

            SetLogicalType(value1 & 0x07);
            ComplexDescriptorAvailable = (value1 & 0x08) != 0;
            IsuserDescriptorAvailable = (value1 & 0x10) != 0;

            SetFrequencyBands((value2 & 0xf8) >> 3);
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
            int prime = 31;
            int result = 1;
            result = prime * result + _apsFlags;
            result = prime * result + BufferSize;
            result = prime * result + (ComplexDescriptorAvailable ? 1231 : 1237);
            result = prime * result + (IsextendedEndpointListAvailable ? 1231 : 1237);
            result = prime * result + (ExtendedSimpleDescriptorListAvailable ? 1231 : 1237);
            result = prime * result + ((FrequencyBands == null) ? 0 : FrequencyBands.GetHashCode());
            result = prime * result + IncomingTransferSize;
            result = prime * result + LogicalNodeType.GetHashCode();
            result = prime * result + ((MacCapabilities == null) ? 0 : MacCapabilities.GetHashCode());
            result = prime * result + ManufacturerCode;
            result = prime * result + OutgoingTransferSize;
            result = prime * result + ((ServerCapabilities == null) ? 0 : ServerCapabilities.GetHashCode());
            result = prime * result + (IsuserDescriptorAvailable ? 1231 : 1237);
            return result;
        }

        public override bool Equals(Object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            NodeDescriptor other = (NodeDescriptor)obj;
            if (_apsFlags != other._apsFlags)
            {
                return false;
            }
            if (BufferSize != other.BufferSize)
            {
                return false;
            }
            if (ComplexDescriptorAvailable != other.ComplexDescriptorAvailable)
            {
                return false;
            }
            if (IsextendedEndpointListAvailable != other.IsextendedEndpointListAvailable)
            {
                return false;
            }
            if (ExtendedSimpleDescriptorListAvailable != other.ExtendedSimpleDescriptorListAvailable)
            {
                return false;
            }
            if (FrequencyBands == null)
            {
                if (other.FrequencyBands != null)
                {
                    return false;
                }
            }
            else if (!FrequencyBands.Equals(other.FrequencyBands)) // TODO: SequenceEquals ??
            {
                return false;
            }
            if (IncomingTransferSize != other.IncomingTransferSize)
            {
                return false;
            }
            if (LogicalNodeType != other.LogicalNodeType)
            {
                return false;
            }
            if (MacCapabilities == null)
            {
                if (other.MacCapabilities != null)
                {
                    return false;
                }
            }
            else if (!MacCapabilities.Equals(other.MacCapabilities)) // TODO: SequenceEquals ??
            {
                return false;
            }
            if (ManufacturerCode != other.ManufacturerCode)
            {
                return false;
            }
            if (OutgoingTransferSize != other.OutgoingTransferSize)
            {
                return false;
            }
            if (ServerCapabilities == null)
            {
                if (other.ServerCapabilities != null)
                {
                    return false;
                }
            }
            else if (!ServerCapabilities.Equals(other.ServerCapabilities)) // TODO: SequenceEquals ??
            {
                return false;
            }
            if (IsuserDescriptorAvailable != other.IsuserDescriptorAvailable)
            {
                return false;
            }
            return true;
        }


        public override string ToString()
        {
            return "NodeDescriptor [apsFlags=" + _apsFlags + ", bufferSize=" + BufferSize + ", complexDescriptorAvailable="
                    + ComplexDescriptorAvailable + ", manufacturerCode=" + ManufacturerCode + ", logicalType=" + LogicalNodeType
                    + ", serverCapabilities=" + string.Join(", ", ServerCapabilities) + ", incomingTransferSize=" + IncomingTransferSize
                    + ", outgoingTransferSize=" + OutgoingTransferSize + ", userDescriptorAvailable="
                    + IsuserDescriptorAvailable + ", frequencyBands=" + string.Join(", ", FrequencyBands) + ", macCapabilities="
                    + string.Join(", ", MacCapabilities) + ", extendedEndpointListAvailable=" + IsextendedEndpointListAvailable
                    + ", extendedSimpleDescriptorListAvailable=" + ExtendedSimpleDescriptorListAvailable
                    + ", stackCompliance=" + StackCompliance + "]";
        }

    }
}
