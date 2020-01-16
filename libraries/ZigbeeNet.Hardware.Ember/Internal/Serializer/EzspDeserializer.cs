using System;
using System.Collections.Generic;
using System.Threading;
using ZigBeeNet.Hardware.Ember.Ezsp.Structure;

namespace ZigBeeNet.Hardware.Ember.Internal.Serializer
{
    /// <summary>
    /// The EmberZNet Serial Protocol Data Representation
    /// 
    /// This class contains low level methods for deserialising Ember data packets and
    /// structures from the incoming received array
    /// 
    /// </summary>
    public class EzspDeserializer
    {
        private int[] _buffer = new int[220];
        private int _position = 0;

        public EzspDeserializer(int[] inputBuffer)
        {
            _buffer = inputBuffer;
        }

        /**
         * Reads a uint8_t from the output stream
         *
         * @return value read from input
         */
        public int DeserializeUInt8()
        {
            return _buffer[_position++];
        }

        /**
         * Reads a int8s from the output stream
         *
         * @return value read from input
         */
        public int DeserializeInt8S()
        {
            return _buffer[_position] > 127 ? _buffer[_position++] - 256 : _buffer[_position++];
        }

        /**
         * Reads a boolean from the output stream
         *
         * @return value read from input
         */
        public bool DeserializeBool()
        {
            return _buffer[_position++] == 0 ? false : true;
        }

        /**
         * Reads an Eui64 address from the stream
         *
         * @return value read from input
         */
        public IeeeAddress DeserializeEmberEui64()
        {
            byte[] address = new byte[8];
            for (int cnt = 0; cnt < 8; cnt++)
            {
                address[cnt] = (byte)_buffer[_position++];
            }
            return new IeeeAddress(address);
        }

        /**
         * Reads a uint16_t from the output stream
         *
         * @return value read from input
         */
        public int DeserializeUInt16()
        {
            return _buffer[_position++] + (_buffer[_position++] << 8);
        }

        public int[] DeserializeUInt8Array(int length)
        {
            int[] val = new int[length];

            for (int cnt = 0; cnt < length; cnt++)
            {
                val[cnt] = DeserializeUInt8();
            }

            return val;
        }

        public int[] DeserializeUInt16Array(int length)
        {
            int[] val = new int[length];

            for (int cnt = 0; cnt < length; cnt++)
            {
                val[cnt] = DeserializeUInt16();
            }

            return val;
        }

        public EmberKeyData DeserializeEmberKeyData() 
        {
            return new EmberKeyData(this);
        }

        public EzspStatus DeserializeEzspStatus()
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EzspStatus), value) ? (EzspStatus)value : EzspStatus.UNKNOWN;
        }

        /**
         * Reads a uint32_t from the output stream
         *
         * @return value read from input
         */
        public int DeserializeUInt32() 
        {
            return _buffer[_position++] + (_buffer[_position++] << 8) + (_buffer[_position++] << 16) + (_buffer[_position++] << 24);
        }

        public EmberStatus DeserializeEmberStatus()
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberStatus), value) ? (EmberStatus)value : EmberStatus.UNKNOWN;
        }

        public EmberConcentratorType DeserializeEmberConcentratorType() 
        {
            int value = DeserializeUInt16();

            return Enum.IsDefined(typeof(EmberConcentratorType), value) ? (EmberConcentratorType)value : EmberConcentratorType.UNKNOWN;
        }

        public EmberNodeType DeserializeEmberNodeType()
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberNodeType), value) ? (EmberNodeType)value : EmberNodeType.UNKNOWN;
        }

        public EmberBindingType DeserializeEmberBindingType()
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberBindingType), value) ? (EmberBindingType)value : EmberBindingType.UNKNOWN;
        }

        public EmberBindingTableEntry DeserializeEmberBindingTableEntry() 
        {
            return new EmberBindingTableEntry(this);
        }

        public EmberCurrentSecurityState DeserializeEmberCurrentSecurityState() 
        {
            return new EmberCurrentSecurityState(this);
        }

        public EmberNeighborTableEntry DeserializeEmberNeighborTableEntry() 
        {
            return new EmberNeighborTableEntry(this);
        }

        public EmberNetworkParameters DeserializeEmberNetworkParameters() 
        {
            return new EmberNetworkParameters(this);
        }

        public EzspDecisionId DeserializeEzspDecisionId()
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EzspDecisionId), value) ? (EzspDecisionId)value : EzspDecisionId.UNKNOWN;
        }

        public EzspConfigId DeserializeEmberConfigId()
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EzspConfigId), value) ? (EzspConfigId)value : EzspConfigId.UNKNOWN;
        }

        public EmberRouteTableEntry DeserializeEmberRouteTableEntry() 
        {
            return new EmberRouteTableEntry(this);
        }
        
        public EmberIncomingMessageType DeserializeEmberIncomingMessageType() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberIncomingMessageType), value) ? (EmberIncomingMessageType)value : EmberIncomingMessageType.UNKNOWN;
        }

        public EmberOutgoingMessageType DeserializeEmberOutgoingMessageType() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberOutgoingMessageType), value) ? (EmberOutgoingMessageType)value : EmberOutgoingMessageType.UNKNOWN;
        }

        public EmberApsFrame DeserializeEmberApsFrame() 
        {
            return new EmberApsFrame(this);
        }        

        public HashSet<EmberApsOption> DeserializeEmberApsOption() 
        {
            int val = DeserializeUInt16();
            HashSet<EmberApsOption> options = new HashSet<EmberApsOption>();
            foreach (EmberApsOption option in Enum.GetValues(typeof(EmberApsOption))) 
            {
                if (option == EmberApsOption.UNKNOWN) 
                {
                    continue;
                }

                if (((int)option & val) != 0) 
                {
                    options.Add(option);
                }
            }
            return options;
        }

        public EmberZigbeeNetwork DeserializeEmberZigbeeNetwork() 
        {
            return new EmberZigbeeNetwork(this);
        }

        public HashSet<EmberCurrentSecurityBitmask> DeserializeEmberCurrentSecurityBitmask() 
        {
            HashSet<EmberCurrentSecurityBitmask> list = new HashSet<EmberCurrentSecurityBitmask>();
            int value = DeserializeUInt16();
            foreach (EmberCurrentSecurityBitmask bitmask in Enum.GetValues(typeof(EmberCurrentSecurityBitmask)))
            {
                // Ignore UNKNOWN
                if (bitmask == EmberCurrentSecurityBitmask.UNKNOWN) 
                {
                    continue;
                }
                if ((value & (int)bitmask) != 0) 
                {
                    list.Add(bitmask);
                }
            }

            return list;
        }

        public EmberMacPassthroughType DeserializeEmberMacPassthroughType() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberMacPassthroughType), value) ? (EmberMacPassthroughType)value : EmberMacPassthroughType.UNKNOWN;
        }

        public EmberJoinMethod DeserializeEmberJoinMethod() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberJoinMethod), value) ? (EmberJoinMethod)value : EmberJoinMethod.UNKNOWN;
        }

        public EmberNetworkStatus DeserializeEmberNetworkStatus() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberNetworkStatus), value) ? (EmberNetworkStatus)value : EmberNetworkStatus.UNKNOWN;
        }

        public EmberDeviceUpdate DeserializeEmberDeviceUpdate() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberDeviceUpdate), value) ? (EmberDeviceUpdate)value : EmberDeviceUpdate.UNKNOWN;
        }

        public EmberJoinDecision DeserializeEmberJoinDecision() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberJoinDecision), value) ? (EmberJoinDecision)value : EmberJoinDecision.UNKNOWN;
        }

        public HashSet<EmberInitialSecurityBitmask> DeserializeEmberInitialSecurityBitmask() 
        {
            HashSet<EmberInitialSecurityBitmask> list = new HashSet<EmberInitialSecurityBitmask>();
            int value = DeserializeUInt16();
            foreach (EmberInitialSecurityBitmask bitmask in Enum.GetValues(typeof(EmberInitialSecurityBitmask)))
            {
                // Ignore UNKNOWN
                if (bitmask == EmberInitialSecurityBitmask.UNKNOWN) 
                {
                    continue;
                }
                if ((value & (int)bitmask) != 0) 
                {
                    list.Add(bitmask);
                }
            }

            return list;
        }

        public ExtendedPanId DeserializeExtendedPanId() 
        {
            return new ExtendedPanId(Array.ConvertAll(DeserializeUInt8Array(8), c => (byte)c));
        }

        public EmberKeyType DeserializeEmberKeyType() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberKeyType), value) ? (EmberKeyType)value : EmberKeyType.UNKNOWN;
        }
        public HashSet<EmberKeyStructBitmask> DeserializeEmberKeyStructBitmask() 
        {
            HashSet<EmberKeyStructBitmask> list = new HashSet<EmberKeyStructBitmask>();
            int value = DeserializeUInt16();
            foreach (EmberKeyStructBitmask bitmask in Enum.GetValues(typeof(EmberKeyStructBitmask)))
            {
                // Ignore UNKNOWN
                if (bitmask == EmberKeyStructBitmask.UNKNOWN) 
                {
                    continue;
                }
                if ((value & (int)bitmask) != 0) 
                {
                    list.Add(bitmask);
                }
            }

            return list;
        }

        public EmberKeyStruct DeserializeEmberKeyStruct() 
        {
            return new EmberKeyStruct(this);
        }

        // public EmberGpProxyTableEntry deserializeEmberGpProxyTableEntry() {
        // return new EmberGpProxyTableEntry(this);
        // }

        public EmberGpAddress DeserializeEmberGpAddress() 
        {
            return new EmberGpAddress(this);
        }

        public EmberGpSecurityLevel DeserializeEmberGpSecurityLevel() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberGpSecurityLevel), value) ? (EmberGpSecurityLevel)value : EmberGpSecurityLevel.UNKNOWN;
        }

        public EmberGpKeyType DeserializeEmberGpKeyType() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberGpKeyType), value) ? (EmberGpKeyType)value : EmberGpKeyType.UNKNOWN;
        }

        public EmberAesMmoHashContext DeserializeEmberAesMmoHashContext() 
        {
            return new EmberAesMmoHashContext(this);
        }

        public EmberCertificateData DeserializeEmberCertificateData() 
        {
            return new EmberCertificateData(this);
        }

        public EmberCertificate283k1Data DeserializeEmberCertificate283k1Data() 
        {
            return new EmberCertificate283k1Data(this);
        }

        public EmberSmacData DeserializeEmberSmacData() 
        {
            return new EmberSmacData(this);
        }

        public EmberPublicKeyData DeserializeEmberPublicKeyData() 
        {
            return new EmberPublicKeyData(this);
        }

        public EmberPublicKey283k1Data DeserializeEmberPublicKey283k1Data() 
        {
            return new EmberPublicKey283k1Data(this);
        }

        public EmberLibraryStatus DeserializeEmberLibraryStatus() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberLibraryStatus), value) ? (EmberLibraryStatus)value : EmberLibraryStatus.UNKNOWN;
        }
        public EmberTransientKeyData DeserializeEmberTransientKeyData() 
        {
            return new EmberTransientKeyData(this);
        }

        public EmberGpProxyTableEntryStatus DeserializeEmberGpProxyTableEntryStatus() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberGpProxyTableEntryStatus), value) ? (EmberGpProxyTableEntryStatus)value : EmberGpProxyTableEntryStatus.UNKNOWN;
        }
        public EmberGpSinkListEntry[] DeserializeEmberGpSinkListEntry(int length) 
        {
            EmberGpSinkListEntry[] array = new EmberGpSinkListEntry[length];
            for (int cnt = 0; cnt < length; cnt++) 
            {
                array[cnt] = new EmberGpSinkListEntry(this);
            }
            return array;
        }

        public EmberGpSinkTableEntry DeserializeEmberGpSinkTableEntry() 
        {
            return new EmberGpSinkTableEntry(this);
        }

        public EmberGpSinkTableEntryStatus DeserializeEmberGpSinkTableEntryStatus() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberGpSinkTableEntryStatus), value) ? (EmberGpSinkTableEntryStatus)value : EmberGpSinkTableEntryStatus.UNKNOWN;
        }
        public EmberGpProxyTableEntry DeserializeEmberGpProxyTableEntry() 
        {
            return new EmberGpProxyTableEntry(this);
        }

        public EmberGpApplicationId DeserializeEmberGpApplicationId() 
        {
            int value = DeserializeUInt8();

            return Enum.IsDefined(typeof(EmberGpApplicationId), value) ? (EmberGpApplicationId)value : EmberGpApplicationId.UNKNOWN;
        }
    }
}
