using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.Ember.Ezsp;
using ZigBeeNet.Hardware.Ember.Ezsp.Command;
using ZigBeeNet.Hardware.Ember.Ezsp.Structure;
using ZigBeeNet.Hardware.Ember.Internal;
using ZigBeeNet.Hardware.Ember.Transaction;
using ZigBeeNet.Security;

namespace ZigBeeNet.Hardware.Ember
{
    /// <summary>
    /// This class provides utility methods for accessing the Ember NCP.
    /// </summary>
    public class EmberNcp
    {

        /**
         * The protocol handler used to send and receive EZSP packets
         */
        private IEzspProtocolHandler _protocolHandler;

        /**
         * The status value from the last request
         */
        private EmberStatus _lastStatus;

        /**
         * Create the NCP instance
         *
         * @param protocolHandler the {@link EzspFrameHandler} used for communicating with the NCP
         */
        public EmberNcp(IEzspProtocolHandler protocolHandler)
        {
            this._protocolHandler = protocolHandler;
        }

        /**
         * Returns the {@link EmberStatus} from the last request. If the request did not provide a status, null is returned.
         *
         * @return {@link EmberStatus}
         */
        public EmberStatus GetLastStatus()
        {
            return _lastStatus;
        }

        /**
         * The command allows the Host to specify the desired EZSP version and must be sent before any other command. The
         * response provides information about the firmware running on the NCP.
         *
         * @param desiredVersion the requested version we support
         * @return the {@link EzspVersionResponse}
         */
        public EzspVersionResponse GetVersion(int desiredVersion)
        {
            EzspVersionRequest request = new EzspVersionRequest();
            request.SetDesiredProtocolVersion(EzspFrame.GetEzspVersion());
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspVersionResponse)));
            EzspVersionResponse response = (EzspVersionResponse)transaction.GetResponse();
            if (response == null)
            {
                Log.Debug("No response from ezspVersion command");
                return null;
            }
            Log.Debug(response.ToString());
            _lastStatus = EmberStatus.UNKNOWN;

            return response;
        }

        /**
         * Resume network operation after a reboot. The node retains its original type. This should be called on startup
         * whether or not the node was previously part of a network. EMBER_NOT_JOINED is returned if the node is not part of
         * a network.
         *
         * @return {@link EmberStatus} if success or failure
         */
        public EmberStatus NetworkInit()
        {
            EzspNetworkInitRequest request = new EzspNetworkInitRequest();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspNetworkInitResponse)));
            EzspNetworkInitResponse response = (EzspNetworkInitResponse)transaction.GetResponse();
            Log.Debug(response.ToString());

            return response.GetStatus();
        }

        /**
         * Causes the stack to leave the current network. This generates a stackStatusHandler callback to indicate that the
         * network is down. The radio will not be used until after sending a formNetwork or joinNetwork command.
         * <p>
         * Note that the user must wait for the network state to change to {@link EmberNetworkStatus#EMBER_NO_NETWORK}
         * otherwise the state may remain in {@link EmberNetworkStatus#EMBER_LEAVING_NETWORK} and attempts to reinitialise
         * the NCP will result in {@link EmberStatus#EMBER_INVALID_CALL}.
         *
         * @return {@link EmberStatus} if success or failure
         */
        public EmberStatus LeaveNetwork()
        {
            EzspLeaveNetworkRequest request = new EzspLeaveNetworkRequest();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspLeaveNetworkResponse)));
            EzspLeaveNetworkResponse response = (EzspLeaveNetworkResponse)transaction.GetResponse();
            Log.Debug(response.ToString());

            return response.GetStatus();
        }

        /**
         * Gets the current security state that is being used by a device that is joined in the network.
         *
         * @return the {@link EmberNetworkParameters} or null on error
         */
        public EmberCurrentSecurityState getCurrentSecurityState()
        {
            EzspGetCurrentSecurityStateRequest request = new EzspGetCurrentSecurityStateRequest();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetCurrentSecurityStateResponse)));
            EzspGetCurrentSecurityStateResponse response = (EzspGetCurrentSecurityStateResponse)transaction.GetResponse();
            Log.Debug(response.ToString());
            _lastStatus = response.GetStatus();
            return response.GetState();
        }

        /**
         * Gets the current network parameters, or an empty parameters class if there's an error
         *
         * @return {@link EzspGetNetworkParametersResponse}
         */
        public EzspGetNetworkParametersResponse GetNetworkParameters()
        {
            EzspGetNetworkParametersRequest request = new EzspGetNetworkParametersRequest();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetNetworkParametersResponse)));
            return (EzspGetNetworkParametersResponse)transaction.GetResponse();
        }

        /**
         * Returns a value indicating whether the node is joining, joined to, or leaving a network.
         *
         * @return the {@link EmberNetworkStatus}
         */
        public EmberNetworkStatus GetNetworkState()
        {
            EzspNetworkStateRequest request = new EzspNetworkStateRequest();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspNetworkStateResponse)));
            EzspNetworkStateResponse response = (EzspNetworkStateResponse)transaction.GetResponse();
            _lastStatus = EmberStatus.UNKNOWN;

            return response.GetStatus();
        }

        /**
         * Returns information about the children of the local node and the parent of the local node.
         *
         * @return the {@link EzspGetParentChildParametersResponse}
         */
        public EzspGetParentChildParametersResponse GetChildParameters()
        {
            EzspGetParentChildParametersRequest request = new EzspGetParentChildParametersRequest();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetParentChildParametersResponse)));
            EzspGetParentChildParametersResponse response = (EzspGetParentChildParametersResponse)transaction.GetResponse();
            _lastStatus = EmberStatus.UNKNOWN;

            return response;
        }

        /**
         * Returns information about a child of the local node.
         *
         * @param childId the ID of the child to get information on
         * @return the {@link EzspGetChildDataResponse} of the requested childId or null on error
         */
        public EzspGetChildDataResponse GetChildInformation(int childId)
        {
            EzspGetChildDataRequest request = new EzspGetChildDataRequest();
            request.SetIndex(childId);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetChildDataResponse)));
            EzspGetChildDataResponse response = (EzspGetChildDataResponse)transaction.GetResponse();
            Log.Debug(response.ToString());
            _lastStatus = response.GetStatus();
            if (_lastStatus != EmberStatus.EMBER_SUCCESS)
                return null;

            return response;
        }

        /**
         * Configures endpoint information on the NCP. The NCP does not remember these settings after a reset. Endpoints can
         * be added by the Host after the NCP has reset. Once the status of the stack changes to EMBER_NETWORK_UP, endpoints
         * can no longer be added and this command will respond with EZSP_ERROR_INVALID_CALL.
         *
         * @param endpointId the endpoint number to add
         * @param deviceId the device id for the endpoint
         * @param profileId the profile id
         * @param inputClusters an array of input clusters supported by the endpoint
         * @param outputClusters an array of output clusters supported by the endpoint
         * @return the {@link EzspStatus} of the response
         */
        public EzspStatus AddEndpoint(int endpointId, int deviceId, int profileId, int[] inputClusters, int[] outputClusters)
        {
            EzspAddEndpointRequest request = new EzspAddEndpointRequest();
            request.SetEndpoint(endpointId);
            request.SetDeviceId(deviceId);
            request.SetProfileId(profileId);
            request.SetInputClusterList(inputClusters);
            request.SetOutputClusterList(outputClusters);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspAddEndpointResponse)));
            EzspAddEndpointResponse response = (EzspAddEndpointResponse)transaction.GetResponse();

            Log.Debug(response.ToString());
            _lastStatus = EmberStatus.UNKNOWN;

            return response.GetStatus();
        }

        /**
         * Retrieves Ember counters. See the EmberCounterType enumeration for the counter types.
         *
         * @return the array of counters
         */
        public int[] GetCounters()
        {
            EzspReadCountersRequest request = new EzspReadCountersRequest();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspReadCountersResponse)));
            EzspReadCountersResponse response = (EzspReadCountersResponse)transaction.GetResponse();
            Log.Debug(response.ToString());
            _lastStatus = EmberStatus.UNKNOWN;
            return response.GetValues();
        }

        /**
         * Clears the key table on the NCP
         *
         * @return the {@link EmberStatus} or null on error
         */
        public EmberStatus ClearKeyTable()
        {
            EzspClearKeyTableRequest request = new EzspClearKeyTableRequest();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspClearKeyTableResponse)));
            EzspClearKeyTableResponse response = (EzspClearKeyTableResponse)transaction.GetResponse();
            Log.Debug(response.ToString());
            _lastStatus = response.GetStatus();
            return _lastStatus;
        }

        /**
         * Gets a Security Key based on the passed key type.
         *
         * @param keyType the {@link EmberKeyType} of the key to get
         * @return the {@link EmberKeyStruct} or null on error
         */
        public EmberKeyStruct GetKey(EmberKeyType keyType)
        {
            EzspGetKeyRequest request = new EzspGetKeyRequest();
            request.SetKeyType(keyType);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetKeyResponse)));
            EzspGetKeyResponse response = (EzspGetKeyResponse)transaction.GetResponse();
            Log.Debug(response.ToString());
            _lastStatus = response.GetStatus();
            if (_lastStatus != EmberStatus.EMBER_SUCCESS)
                return null;

            return response.GetKeyStruct();
        }

        /**
         * Gets a Security Key based on the passed key type.
         *
         * @param index the index of the key to get
         * @return the {@link EmberKeyStruct} of the requested key or null on error
         */
        public EmberKeyStruct GetKeyTableEntry(int index)
        {
            EzspGetKeyTableEntryRequest request = new EzspGetKeyTableEntryRequest();
            request.SetIndex(index);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetKeyTableEntryResponse)));
            EzspGetKeyTableEntryResponse response = (EzspGetKeyTableEntryResponse)transaction.GetResponse();
            Log.Debug(response.ToString());
            _lastStatus = response.GetStatus();
            if (_lastStatus != EmberStatus.EMBER_SUCCESS)
                return null;

            return response.GetKeyStruct();
        }

        /**
         * Get a configuration value
         *
         * @param configId the {@link EzspConfigId} to set
         * @return the configuration value as {@link Integer} or null on error
         */
        public int? GetConfiguration(EzspConfigId configId)
        {
            EzspGetConfigurationValueRequest request = new EzspGetConfigurationValueRequest();
            request.SetConfigId(configId);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetConfigurationValueResponse)));
            EzspGetConfigurationValueResponse response = (EzspGetConfigurationValueResponse)transaction.GetResponse();
            _lastStatus = EmberStatus.UNKNOWN;
            Log.Debug(response.ToString());

            if (response.GetStatus() != EzspStatus.EZSP_SUCCESS)
                return null;

            return response.GetValue();
        }

        /**
         * Set a configuration value
         *
         * @param configId the {@link EzspConfigId} to set
         * @param value the value to set
         * @return the {@link EzspStatus} of the response
         */
        public EzspStatus SetConfiguration(EzspConfigId configId, int value)
        {
            EzspSetConfigurationValueRequest request = new EzspSetConfigurationValueRequest();
            request.SetConfigId(configId);
            request.SetValue(value);
            Log.Debug(request.ToString());

            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspSetConfigurationValueResponse)));
            EzspSetConfigurationValueResponse response = (EzspSetConfigurationValueResponse)transaction.GetResponse();
            _lastStatus = EmberStatus.UNKNOWN;
            Log.Debug(response.ToString());

            return response.GetStatus();
        }

        /**
         * Set a policy used by the NCP to make fast decisions.
         *
         * @param policyId the {@link EzspPolicyId} to set
         * @param decisionId the {@link EzspDecisionId} to set to
         * @return the {@link EzspStatus} of the response
         */
        public EzspStatus SetPolicy(EzspPolicyId policyId, EzspDecisionId decisionId)
        {
            EzspSetPolicyRequest setPolicyRequest = new EzspSetPolicyRequest();
            setPolicyRequest.SetPolicyId(policyId);
            setPolicyRequest.SetDecisionId(decisionId);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(setPolicyRequest, typeof(EzspSetPolicyResponse)));
            EzspSetPolicyResponse setPolicyResponse = (EzspSetPolicyResponse)transaction.GetResponse();
            _lastStatus = EmberStatus.UNKNOWN;
            Log.Debug(setPolicyResponse.ToString());
            if (setPolicyResponse.GetStatus() != EzspStatus.EZSP_SUCCESS)
            {
                Log.Debug("Error during setting policy: {}", setPolicyResponse);
            }

            return setPolicyResponse.GetStatus();
        }

        /**
         * Get a policy used by the NCP to make fast decisions.
         *
         * @param policyId the {@link EzspPolicyId} to set
         * @return the returned {@link EzspDecisionId} if the policy was retrieved successfully or null if there was an
         *         error
         */
        public EzspDecisionId GetPolicy(EzspPolicyId policyId)
        {
            EzspGetPolicyRequest getPolicyRequest = new EzspGetPolicyRequest();
            getPolicyRequest.SetPolicyId(policyId);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(getPolicyRequest, typeof(EzspGetPolicyResponse)));
            EzspGetPolicyResponse getPolicyResponse = (EzspGetPolicyResponse)transaction.GetResponse();
            _lastStatus = EmberStatus.UNKNOWN;
            Log.Debug(getPolicyResponse.ToString());
            if (getPolicyResponse.GetStatus() != EzspStatus.EZSP_SUCCESS)
            {
                Log.Debug("Error getting policy: {}", getPolicyResponse);
                return EzspDecisionId.UNKNOWN;
            }

            return getPolicyResponse.GetDecisionId();
        }

        /**
         * Set a memory value used by the NCP.
         *
         * @param valueId the {@link EzspValueId} to set
         * @param value the value to set to
         * @return the {@link EzspStatus} of the response
         */
        public EzspStatus SetValue(EzspValueId valueId, int[] value)
        {
            EzspSetValueRequest request = new EzspSetValueRequest();
            request.SetValueId(valueId);
            request.SetValue(value);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspSetValueResponse)));
            EzspSetValueResponse response = (EzspSetValueResponse)transaction.GetResponse();
            _lastStatus = EmberStatus.UNKNOWN;
            Log.Debug(response.ToString());
            if (response.GetStatus() != EzspStatus.EZSP_SUCCESS)
                Log.Debug("Error setting value: {}", response);

            return response.GetStatus();
        }

        /**
         * Get a memory value from the NCP
         *
         * @param valueId the {@link EzspValueId} to set
         * @return the returned value as int[]
         */
        public int[] GetValue(EzspValueId valueId)
        {
            EzspGetValueRequest request = new EzspGetValueRequest();
            request.SetValueId(valueId);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetValueResponse)));
            EzspGetValueResponse response = (EzspGetValueResponse)transaction.GetResponse();
            _lastStatus = EmberStatus.UNKNOWN;
            Log.Debug(response.ToString());
            if (response.GetStatus() != EzspStatus.EZSP_SUCCESS)
            {
                Log.Debug("Error getting value: {}", response);
                return null;
            }

            return response.GetValue();
        }

        /**
         * Adds a transient link key to the NCP
         *
         * @param partner the {@link IeeeAddress} to set
         * @param transientKey the {@link ZigBeeKey} to set
         * @return the {@link EmberStatus} of the response
         */
        public EmberStatus AddTransientLinkKey(IeeeAddress partner, ZigBeeKey transientKey)
        {
            EmberKeyData emberKey = new EmberKeyData();
            emberKey.SetContents(Array.ConvertAll(transientKey.Key, c => (int)c));
            EzspAddTransientLinkKeyRequest request = new EzspAddTransientLinkKeyRequest();
            request.SetPartner(partner);
            request.SetTransientKey(emberKey);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspAddTransientLinkKeyResponse)));
            EzspAddTransientLinkKeyResponse response = (EzspAddTransientLinkKeyResponse)transaction.GetResponse();
            _lastStatus = response.GetStatus();
            Log.Debug(response.ToString());
            if (response.GetStatus() != EmberStatus.EMBER_SUCCESS)
                Log.Debug("Error setting transient key: {}", response);

            return response.GetStatus();
        }

        /**
         * Gets the {@link EmberCertificateData} certificate currently stored in the node.
         * <p>
         * This is the 163k1 certificate used in
         *
         * @return the {@link EmberCertificateData} certificate
         */
        public EmberCertificateData GetCertificateData()
        {
            EzspGetCertificateRequest request = new EzspGetCertificateRequest();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetCertificateResponse)));
            EzspGetCertificateResponse response = (EzspGetCertificateResponse)transaction.GetResponse();
            _lastStatus = response.GetStatus();
            if (response.GetStatus() != EmberStatus.EMBER_SUCCESS)
            {
                Log.Debug("Error getting 163k1 certificate: {Response}", response);
                return null;
            }

            return response.GetLocalCert();
        }

        /**
         * Gets the {@link EmberCertificate283k1Data} certificate currently stored in the node
         *
         * @return the {@link EmberCertificate283k1Data} certificate
         */
        public EmberCertificate283k1Data GetCertificate283k1Data()
        {
            EzspGetCertificate283k1Request request = new EzspGetCertificate283k1Request();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetCertificate283k1Response)));
            EzspGetCertificate283k1Response response = (EzspGetCertificate283k1Response)transaction.GetResponse();
            _lastStatus = response.GetStatus();
            if (response.GetStatus() != EmberStatus.EMBER_SUCCESS)
            {
                Log.Debug("Error getting 283k1 certificate: {}", response);
                return null;
            }

            return response.GetLocalCert();
        }

        /**
         * This routine processes the passed chunk of data and updates the hash context based on it. If the 'finalize'
         * parameter is not set, then the length of the data passed in must be a multiple of 16. If the 'finalize' parameter
         * is set then the length can be any value up 1-16, and the final hash value will be calculated.
         *
         * @param code the integer array to hash
         * @return the resulting {@link EmberAesMmoHashContext}
         */
        public EmberAesMmoHashContext MmoHash(int[] code)
        {
            EmberAesMmoHashContext hashContext = new EmberAesMmoHashContext();
            hashContext.SetResult(new int[16]);
            hashContext.SetLength(0);
            EzspAesMmoHashRequest request = new EzspAesMmoHashRequest();
            request.SetContext(hashContext);
            request.SetData(code);
            request.SetFinalize(true);
            request.SetLength(code.Length);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspAesMmoHashResponse)));
            EzspAesMmoHashResponse response = (EzspAesMmoHashResponse)transaction.GetResponse();
            _lastStatus = response.GetStatus();
            Log.Debug(response.ToString());
            if (response.GetStatus() != EmberStatus.EMBER_SUCCESS)
                Log.Debug("Error performing AES MMO hash: {}", response);

            return response.GetReturnContext();
        }

        /**
         * Gets the {@link IeeeAddress} of the local node
         *
         * @return the {@link IeeeAddress} of the local node
         */
        public IeeeAddress GetIeeeAddress()
        {
            EzspGetEui64Request request = new EzspGetEui64Request();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetEui64Response)));
            EzspGetEui64Response response = (EzspGetEui64Response)transaction.GetResponse();
            return response.GetEui64();
        }

        /**
         * Gets the 16 bit network node id of the local node
         *
         * @return the network address of the local node or 0xFFFE if the network address is not known
         */
        public int GetNwkAddress()
        {
            EzspGetNodeIdRequest request = new EzspGetNodeIdRequest();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetNodeIdResponse)));
            EzspGetNodeIdResponse response = (EzspGetNodeIdResponse)transaction.GetResponse();
            if (response == null) {
                return 0xFFFE;
            }
            return response.GetNodeId();
        }

        /**
         * Sets the radio output power at which a node is operating. Ember radios have discrete power settings. For a list
         * of available power settings, see the technical specification for the RF communication module in your Developer
         * Kit. Note: Care should be taken when using this API on a running network, as it will directly impact the
         * established link qualities neighboring nodes have with the node on which it is called. This can lead to
         * disruption of existing routes and erratic network behavior.
         *
         * @param power Desired radio output power, in dBm.
         * @return the response {@link EmberStatus} of the request
         */
        public EmberStatus SetRadioPower(int power)
        {
            EzspSetRadioPowerRequest request = new EzspSetRadioPowerRequest();
            request.SetPower(power);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspSetRadioPowerResponse)));
            EzspSetRadioPowerResponse response = (EzspSetRadioPowerResponse)transaction.GetResponse();
            return response.GetStatus();
        }

        /**
         * Gets the {@link EmberLibraryStatus} of the requested {@link EmberLibraryId}
         *
         * @return the {@link EmberLibraryStatus} of the local node
         */
        public EmberLibraryStatus GetLibraryStatus(EmberLibraryId libraryId)
        {
            EzspGetLibraryStatusRequest request = new EzspGetLibraryStatusRequest();
            request.SetLibraryId(libraryId);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspGetLibraryStatusResponse)));
            EzspGetLibraryStatusResponse response = (EzspGetLibraryStatusResponse)transaction.GetResponse();
            return response.GetStatus();
        }

        /**
         * This function updates an existing entry in the key table or adds a new one. It first searches the table for an
         * existing entry that matches the passed EUI64 address. If no entry is found, it searches for the first free entry.
         * If successful, it updates the key data and resets the associated incoming frame counter. If it fails to find an
         * existing entry and no free one exists, it returns a failure.
         *
         * @param address the {@link IeeeAddress}
         * @param key the {@link ZigBeeKey}
         * @param linkKey This indicates whether the key is a Link or a Master Key
         * @return the returned {@link EmberStatus} of the request
         */
        public EmberStatus AddOrUpdateKeyTableEntry(IeeeAddress address, ZigBeeKey key, bool linkKey)
        {
            EmberKeyData keyData = new EmberKeyData();
            keyData.SetContents(Array.ConvertAll(key.Key, c => (int)c));

            EzspAddOrUpdateKeyTableEntryRequest request = new EzspAddOrUpdateKeyTableEntryRequest();
            request.SetAddress(address);
            request.SetKeyData(keyData);
            request.SetLinkKey(linkKey);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspAddOrUpdateKeyTableEntryResponse)));
            EzspAddOrUpdateKeyTableEntryResponse response = (EzspAddOrUpdateKeyTableEntryResponse)transaction.GetResponse();
            return response.GetStatus();
        }

        /**
         * Sets the power descriptor to the specified value. The power descriptor is a dynamic value, therefore you should
         * call this function whenever the value changes.
         *
         * @param descriptor the descriptor to set as int
         */
        public void SetPowerDescriptor(int descriptor)
        {
            EzspSetPowerDescriptorRequest request = new EzspSetPowerDescriptorRequest();
            request.SetDescriptor(descriptor);
            _protocolHandler.QueueFrame(request);
            _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspSetPowerDescriptorResponse)));
        }

        /**
         * Sets the manufacturer code to the specified value. The manufacturer code is one of the fields of the node
         * descriptor.
         *
         * @param code the code to set as int
         */
        public void SetManufacturerCode(int code)
        {
            EzspSetManufacturerCodeRequest request = new EzspSetManufacturerCodeRequest();
            request.SetCode(code);
            _protocolHandler.QueueFrame(request);
            _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspSetManufacturerCodeResponse)));
        }



        /**
         * Perform an active scan. The radio will try to send beacon request on each channel.
         * <p>
         * During the scan, the CSMA/CA mechanism will be used to detect whether the radio channel is free at that moment.
         * If some of the radio channels in the environment are too busy for the device to perform the scan, the NCP returns
         * EMBER_MAC_COMMAND_TRANSMIT_FAILURE. A clearer RF environment might mitigate this issue.
         *
         * @param channelMask the channel mask on which to perform the scan.
         * @param scanDuration Sets the exponent of the number of scan periods, where a scan period is 960 symbols. The scan
         *            will occur for ((2^duration) + 1) scan periods.
         * @return a List of {@link EzspNetworkFoundHandler} on success. If there was an error during the scan, null is
         *         returned.
         */
        public ICollection<EzspNetworkFoundHandler> DoActiveScan(int channelMask, int scanDuration)
        {
            EzspStartScanRequest activeScan = new EzspStartScanRequest();
            activeScan.SetChannelMask(channelMask);
            activeScan.SetDuration(scanDuration);
            activeScan.SetScanType(EzspNetworkScanType.EZSP_ACTIVE_SCAN);

            HashSet<Type> relatedResponses = new HashSet<Type>() { typeof(EzspStartScanResponse), typeof(EzspNetworkFoundHandler)};
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspMultiResponseTransaction(activeScan, typeof(EzspScanCompleteHandler), relatedResponses));
            EzspScanCompleteHandler activeScanCompleteResponse = (EzspScanCompleteHandler) transaction.GetResponse();
            Log.Debug(activeScanCompleteResponse.ToString());

            if (activeScanCompleteResponse.GetStatus() != EmberStatus.EMBER_SUCCESS) 
            {
                _lastStatus = activeScanCompleteResponse.GetStatus();
                Log.Debug("Error during active scan: {}", activeScanCompleteResponse);
                return null;
            }

            Dictionary<ExtendedPanId, EzspNetworkFoundHandler> networksFound = new Dictionary<ExtendedPanId, EzspNetworkFoundHandler>();
            foreach (EzspFrameResponse response in transaction.GetResponses()) 
            {
                if (response is EzspNetworkFoundHandler) 
                {
                    EzspNetworkFoundHandler network = (EzspNetworkFoundHandler) response;
                    networksFound[network.GetNetworkFound().GetExtendedPanId()] = network;
                }
            }

            return networksFound.Values;
        }

        /**
         * Performs an energy scan and returns the quietest channel
         *
         * @param channelMask the channel mask on which to perform the scan.
         * @param scanDuration Sets the exponent of the number of scan periods, where a scan period is 960 symbols. The scan
         *            will occur for ((2^duration) + 1) scan periods.
         * @return a List of {@link EzspNetworkFoundHandler} on success. If there was an error during the scan, null is
         *         returned.
         */
        public List<EzspEnergyScanResultHandler> DoEnergyScan(int channelMask, int scanDuration) 
        {
            EzspStartScanRequest energyScan = new EzspStartScanRequest();
            energyScan.SetChannelMask(channelMask);
            energyScan.SetDuration(scanDuration);
            energyScan.SetScanType(EzspNetworkScanType.EZSP_ENERGY_SCAN);

            HashSet<Type> relatedResponses = new HashSet<Type>() { typeof(EzspStartScanResponse), typeof(EzspEnergyScanResultHandler) };
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspMultiResponseTransaction(energyScan, typeof(EzspScanCompleteHandler), relatedResponses));

            EzspScanCompleteHandler scanCompleteResponse = (EzspScanCompleteHandler) transaction.GetResponse();
            Log.Debug(scanCompleteResponse.ToString());

            List<EzspEnergyScanResultHandler> channels = new List<EzspEnergyScanResultHandler>();
            foreach (EzspFrameResponse network in transaction.GetResponses()) 
            {
                if (network is EzspEnergyScanResultHandler)
                    channels.Add((EzspEnergyScanResultHandler) network);
            }

            _lastStatus = scanCompleteResponse.GetStatus();

            return channels;
        }

        /**
         * Tells the stack whether or not the normal interval between retransmissions of a retried unicast message should be
         * increased by EMBER_INDIRECT_TRANSMISSION_TIMEOUT. The interval needs to be increased when sending to a sleepy
         * node so that the message is not retransmitted until the destination has had time to wake up and poll its parent.
         * The stack will automatically extend the timeout:
         * <ul>
         * <li>For our own sleepy children.
         * <li>When an address response is received from a parent on behalf of its child.
         * <li>When an indirect transaction expiry route error is received.
         * <li>When an end device announcement is received from a sleepy node.
         * </ul>
         *
         * @param remoteEui64 the {@link IeeeAddress} of the remote node
         * @param extendedTimeout true if the node should be set with an extended timeout
         * @return the {@link ZigBeeStatus} of the request
         */
        public ZigBeeStatus SetExtendedTimeout(IeeeAddress remoteEui64, bool extendedTimeout) 
        {
            EzspSetExtendedTimeoutRequest request = new EzspSetExtendedTimeoutRequest();
            request.SetRemoteEui64(remoteEui64);
            request.SetExtendedTimeout(extendedTimeout);
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspSetExtendedTimeoutResponse)));
            EzspSetExtendedTimeoutResponse response = (EzspSetExtendedTimeoutResponse) transaction.GetResponse();
            return (response == null) ? ZigBeeStatus.FAILURE : ZigBeeStatus.SUCCESS;
        }

        /**
         * Detects if the standalone bootloader is installed, and if so returns the installed version. If not return 0xffff.
         * A returned version of 0x1234 would indicate version 1.2 build 34.
         *
         * @return the bootloader version. A returned version of 0x1234 would indicate version 1.2 build 34.
         */
        public int GetBootloaderVersion() 
        {
            EzspGetStandaloneBootloaderVersionPlatMicroPhyRequest request = new EzspGetStandaloneBootloaderVersionPlatMicroPhyRequest();
            IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request,typeof(EzspGetStandaloneBootloaderVersionPlatMicroPhyResponse)));
            EzspGetStandaloneBootloaderVersionPlatMicroPhyResponse response = (EzspGetStandaloneBootloaderVersionPlatMicroPhyResponse) transaction.GetResponse();
            return (response == null) ? 0xFFFF : response.GetBootloaderVersion();
        }
    }
}
