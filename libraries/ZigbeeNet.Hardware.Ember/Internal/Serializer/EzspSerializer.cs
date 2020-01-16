using System;
using System.Collections.Generic;
using System.Threading;
using ZigBeeNet.Hardware.Ember.Ezsp.Structure;

namespace ZigBeeNet.Hardware.Ember.Internal.Serializer
{
    /// <summary>
    /// The EmberZNet Serial Protocol Data Representation
    /// 
    /// This class contains low level methods for serialising Ember data packets and
    /// structures to the array for sending
    /// 
    /// </summary>

    public class EzspSerializer
    {
        private int[] _buffer = new int[220];
        private int _length = 0;

        /**
         * Adds a uint8_t into the output stream
         *
         * @param val the value to serialize
         */
        public void SerializeUInt8(int val) 
        {
            _buffer[_length++] = val & 0xFF;
        }

        /**
         * Adds an int8s_t into the output stream
         *
         * @param val the value to serialize
         */
        public void SerializeInt8S(int val) 
        {
            _buffer[_length++] = val & 0xFF;
        }

        /**
         * Adds a uint16_t into the output stream
         *
         * @param val the value to serialize
         */
        public void SerializeUInt16(int val) 
        {
            _buffer[_length++] = val & 0xFF;
            _buffer[_length++] = (val >> 8) & 0xFF;
        }

        /**
         * Adds a uint32_t into the output stream
         *
         * @param val the value to serialize
         */
        public void SerializeUInt32(int val) 
        {
            _buffer[_length++] = val & 0xFF;
            _buffer[_length++] = (val >> 8) & 0xFF;
            _buffer[_length++] = (val >> 16) & 0xFF;
            _buffer[_length++] = (val >> 24) & 0xFF;
        }

        public void SerializeUInt16Array(int[] array) 
        {
            foreach (int val in array) 
            {
                SerializeUInt16(val);
            }
        }

        public void SerializeUInt8Array(int[] array) 
        {
            foreach (int val in array) 
            {
                SerializeUInt8(val);
            }
        }

        public void SerializeByteArray(byte[] array)
        {
            foreach (byte val in array)
            {
                SerializeUInt8(val);
            }
        }

        public void SerializeBool(bool val) 
        {
            _buffer[_length++] = val ? 1 : 0;
        }
        public void SerializeEmberKeyData(EmberKeyData keyData) 
        {
            // If we pass in null, then send a null key.
            // Note that ember will not accept this key, so this check is here to prevent the system
            // throwing an NPE - the application needs to resolve this so it sends a valid key.
            if (keyData == null || keyData.GetContents() == null) 
            {
                SerializeUInt8Array(new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });
                return;
            }
            SerializeUInt8Array(keyData.GetContents());
        }

        public void SerializeEmberEui64(IeeeAddress address) 
        {
            _buffer[_length++] = address.GetAddress()[0];
            _buffer[_length++] = address.GetAddress()[1];
            _buffer[_length++] = address.GetAddress()[2];
            _buffer[_length++] = address.GetAddress()[3];
            _buffer[_length++] = address.GetAddress()[4];
            _buffer[_length++] = address.GetAddress()[5];
            _buffer[_length++] = address.GetAddress()[6];
            _buffer[_length++] = address.GetAddress()[7];
        }

        public void SerializeEmberNetworkParameters(EmberNetworkParameters networkParameters) 
        {
            networkParameters.Serialize(this);
        }

        public void SerializeEmberApsFrame(EmberApsFrame apsFrame) 
        {
            apsFrame.Serialize(this);
        }

        public void SerializeEmberApsOption(HashSet<EmberApsOption> options) 
        {
            int val = 0;
            foreach (EmberApsOption option in options) 
            {
                val += (int)option;
            }
            _buffer[_length++] = val & 0xFF;
            _buffer[_length++] = (val >> 8) & 0xFF;
        }

        public void SerializeEzspNetworkScanType(EzspNetworkScanType scanType) 
        {
            _buffer[_length++] = (int)scanType;
        }
        public void SerializeEmberBindingTableEntry(EmberBindingTableEntry tableEntry) 
        {
            tableEntry.Serialize(this);
        }

        public void SerializeEmberInitialSecurityState(EmberInitialSecurityState securityState) 
        {
            securityState.Serialize(this);
        }

        public void SerializeEmberOutgoingMessageType(EmberOutgoingMessageType messageType) 
        {
            _buffer[_length++] = (int)messageType;
        }

        public void SerializeEmberConcentratorType(EmberConcentratorType concentratorType) 
        {
            SerializeUInt16((int)concentratorType);
        }

        public void SerializeEzspPolicyId(EzspPolicyId policyId) 
        {
            _buffer[_length++] = (int)policyId;
        }

        public void SerializeEmberNodeType(EmberNodeType nodeType) 
        {
            _buffer[_length++] = (int)nodeType;
        }

        public void SerializeEzspConfigId(EzspConfigId configId) 
        {
            _buffer[_length++] = (int)configId;
        }

        public void SerializeEzspDecisionId(EzspDecisionId decisionId) 
        {
            _buffer[_length++] = (int)decisionId;
        }

        public void SerializeEmberBindingType(EmberBindingType bindingType) 
        {
            _buffer[_length++] = (int)bindingType;
        }
        public void SerializeEmberCurrentSecurityBitmask(HashSet<EmberCurrentSecurityBitmask> securityBitmask) 
        {
            int value = 0;
            foreach (EmberCurrentSecurityBitmask bitmask in securityBitmask) 
            {
                value |= (int)bitmask;
            }
            _buffer[_length++] = value & 0xFF;
            _buffer[_length++] = (value >> 8) & 0xFF;
        }

        public void SerializeEmberInitialSecurityBitmask(HashSet<EmberInitialSecurityBitmask> securityBitmask) 
        {
            int value = 0;
            foreach (EmberInitialSecurityBitmask bitmask in securityBitmask) 
            {
                value |= (int)bitmask;
            }
            _buffer[_length++] = value & 0xFF;
            _buffer[_length++] = (value >> 8) & 0xFF;
        }

        public void SerializeEmberJoinMethod(EmberJoinMethod joinMethod) 
        {
            _buffer[_length++] = (int)joinMethod;
        }

        public void SerializeExtendedPanId(ExtendedPanId extendedPanId) 
        {
            SerializeByteArray(extendedPanId.PanId);
        }

        /**
         * Returns the data to be sent to the NCP
         *
         * @return integer array of data to be sent
         */
        public int[] GetPayload() 
        {
            int[] payload = new int[_length];
            Array.Copy(_buffer, 0, payload, 0, _length);
            return payload;
        }

        public void SerializeEmberKeyType(EmberKeyType keyType) 
        {
            _buffer[_length++] = (int)keyType;
        }

        public void SerializeEzspValueId(EzspValueId valueId) 
        {
            _buffer[_length++] = (int)valueId;
        }

        public void SerializeEmberKeyStructBitmask(HashSet<EmberKeyStructBitmask> keyBitmask) 
        {
            int value = 0;
            foreach (EmberKeyStructBitmask bitmask in keyBitmask) 
            {
                value |= (int)bitmask;
            }
            _buffer[_length++] = value & 0xFF;
            _buffer[_length++] = (value >> 8) & 0xFF;
        }

        public void SerializeEmberPowerMode(EmberPowerMode powerMode) 
        {
            SerializeUInt16((int)powerMode);
        }

        public void SerializeEmberGpAddress(EmberGpAddress address) 
        {
            address.Serialize(this);
        }

        public void SerializeEmberGpProxyTableEntryStatus(EmberGpProxyTableEntryStatus status) 
        {
            _buffer[_length++] = (int)status;
        }

        public void SerializeEmberAesMmoHashContext(EmberAesMmoHashContext context) 
        {
            context.Serialize(this);
        }

        public void SerializeEmberCertificateData(EmberCertificateData certificate) 
        {
            certificate.Serialize(this);
        }

        public void SerializeEmberCertificate283k1Data(EmberCertificate283k1Data certificate) 
        {
            certificate.Serialize(this);
        }

        public void SerializeEmberPublicKeyData(EmberPublicKeyData publicKey) 
        {
            publicKey.Serialize(this);
        }

        public void SerializeEmberPublicKey283k1Data(EmberPublicKey283k1Data publicKey) 
        {
            publicKey.Serialize(this);
        }

        public void SerializeEmberPrivateKey283k1Data(EmberPrivateKey283k1Data privateKey) 
        {
            privateKey.Serialize(this);
        }

        public void SerializeEmberPrivateKeyData(EmberPrivateKeyData privateKey) 
        {
            privateKey.Serialize(this);
        }

        public void SerializeEmberLibraryId(EmberLibraryId libraryId) 
        {
            _buffer[_length++] = (int)libraryId;
        }

        public void SerializeEzspMfgTokenId(EzspMfgTokenId tokenId) 
        {
            _buffer[_length++] = (int)tokenId;
        }

        public void SerializeEmberGpSinkListEntry(EmberGpSinkListEntry[] sinkList) 
        {
            foreach (EmberGpSinkListEntry sink in sinkList) 
            {
                sink.Serialize(this);
            }
        }

        public void SerializeEmberGpSinkTableEntryStatus(EmberGpSinkTableEntryStatus status) 
        {
            _buffer[_length++] = (int)status;
        }

        public void SerializeEmberGpSinkTableEntry(EmberGpSinkTableEntry entry) 
        {
            entry.Serialize(this);
        }

        public void SerializeEmberGpApplicationId(EmberGpApplicationId applicationId) 
        {
            _buffer[_length++] = (int)applicationId;
        }
    }
}
