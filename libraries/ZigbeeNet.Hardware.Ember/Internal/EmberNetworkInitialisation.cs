using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.Ember.Ezsp;
using ZigBeeNet.Hardware.Ember.Ezsp.Command;
using ZigBeeNet.Hardware.Ember.Ezsp.Structure;
using ZigBeeNet.Hardware.Ember.Internal.Serializer;
using ZigBeeNet.Hardware.Ember.Transaction;
using ZigBeeNet.Security;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.Ember.Internal
{
    /// <summary>
    /// This class provides utility functions to establish an Ember ZigBee network
    /// </summary>
    public class EmberNetworkInitialisation 
    {
        /**
         * The frame handler used to send the EZSP frames to the NCP
         */
        private IEzspProtocolHandler _protocolHandler;

        /**
         * Scan duration used for scans
         */
        private int _scanDuration = 1;

        /**
         * @param protocolHandler the {@link EzspProtocolHandler} used to communicate with the NCP
         */
        public EmberNetworkInitialisation(IEzspProtocolHandler protocolHandler) 
        {
            this._protocolHandler = protocolHandler;
        }

        /**
         * Sets the scan duration used when performing scans.
         *
         * @param scanDuration the scan duration. Sets the exponent of the number of scan periods, where a scan period is
         *            960 symbols. The scan will occur for ((2^duration) + 1) scan periods.
         */
        public void SetScanDuration(int scanDuration) 
        {
            this._scanDuration = scanDuration;
        }

        /**
         * This utility function uses emberStartScan, emberStopScan, emberScanCompleteHandler, emberEnergyScanResultHandler,
         * and emberNetworkFoundHandler to discover other networks or determine the background noise level. It then uses
         * emberFormNetwork to create a new network with a unique PAN-ID on a channel with low background noise.
         * <p>
         * Setting the PAN-ID or Extended PAN-ID to 0 will set these values to a random value.
         * <p>
         * If channel is set to 0, the quietest channel will be used.
         *
         * @param networkParameters the required {@link EmberNetworkParameters}
         * @param linkKey the {@link ZigBeeKey} with the link key. This can not be set to all 00 or all FF.
         * @param networkKey the {@link ZigBeeKey} with the network key. This can not be set to all 00 or all FF.
         */
        public void FormNetwork(EmberNetworkParameters networkParameters, ZigBeeKey linkKey, ZigBeeKey networkKey) 
        {
            if (networkParameters.GetExtendedPanId() == null) 
            {
                networkParameters.SetExtendedPanId(new ExtendedPanId());
            }

            Log.Debug("Initialising Ember network with configuration {NetworkParameters}", networkParameters);

            EmberNcp ncp = new EmberNcp(_protocolHandler);

            // Leave the current network so we can initialise a new network
            if (CheckNetworkJoined()) 
            {
                ncp.LeaveNetwork();
            }

            ncp.ClearKeyTable();

            // Perform an energy scan to find a clear channel
            int? quietestChannel = DoEnergyScan(ncp, _scanDuration);
            Log.Debug("Energy scan reports quietest channel is {QuietestChannel}", quietestChannel);

            // Check if any current networks were found and avoid those channels, PAN ID and especially Extended PAN ID
            ncp.DoActiveScan(ZigBeeChannelMask.CHANNEL_MASK_2GHZ, _scanDuration);

            // Read the current network parameters
            GetNetworkParameters();

            // Create a random PAN ID and Extended PAN ID
            if (networkParameters.GetPanId() == 0 || networkParameters.GetExtendedPanId().Equals(new ExtendedPanId())) 
            {
                Random random = new Random();
                int panId = random.Next(65535);
                networkParameters.SetPanId(panId);
                Log.Debug("Created random PAN ID: {PanId}", panId);

                byte[] extendedPanIdBytes = new byte[8];
                random.NextBytes(extendedPanIdBytes);
                ExtendedPanId extendedPanId = new ExtendedPanId(extendedPanIdBytes);
                networkParameters.SetExtendedPanId(extendedPanId);
                Log.Debug("Created random Extended PAN ID: {ExtendedPanId}", extendedPanId.ToString());
            }

            if (networkParameters.GetRadioChannel() == 0 && quietestChannel.HasValue) 
            {
                networkParameters.SetRadioChannel(quietestChannel.Value);
            }

            // If the channel set is empty, use the single channel defined above
            if (networkParameters.GetChannels() == 0) 
            {
                networkParameters.SetChannels(1 << networkParameters.GetRadioChannel());
            }

            // Initialise security
            SetSecurityState(linkKey, networkKey);

            // And now form the network
            DoFormNetwork(networkParameters);
        }

        /**
         * Utility function to join an existing network as a Router
         *
         * @param networkParameters the required {@link EmberNetworkParameters}
         * @param linkKey the {@link ZigBeeKey} with the initial link key. This cannot be set to all 00 or all FF.
         */
        public void JoinNetwork(EmberNetworkParameters networkParameters, ZigBeeKey linkKey) 
        {
            Log.Debug("Joining Ember network with configuration {Parameters}", networkParameters);

            // Leave the current network so we can initialise a new network
            EmberNcp ncp = new EmberNcp(_protocolHandler);
            if (CheckNetworkJoined())
                ncp.LeaveNetwork();

            ncp.ClearKeyTable();

            // Initialise security - no network key as we'll get that from the coordinator
            SetSecurityState(linkKey, null);

            DoJoinNetwork(networkParameters);
        }

        /**
         * Searches for the current network, assuming that we know the network key.
         * This will search all current channels.
         */
        public void RejoinNetwork() 
        {
            DoRejoinNetwork(true, new ZigBeeChannelMask(0));
        }

        private bool CheckNetworkJoined() 
        {
            // Check if the network is initialised
            EzspNetworkStateRequest networkStateRequest = new EzspNetworkStateRequest();
            IEzspTransaction networkStateTransaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(networkStateRequest, typeof(EzspNetworkStateResponse)));
            EzspNetworkStateResponse networkStateResponse = (EzspNetworkStateResponse) networkStateTransaction.GetResponse();
            Log.Debug(networkStateResponse.ToString());
            Log.Debug("EZSP networkStateResponse {Status}", networkStateResponse.GetStatus());

            return networkStateResponse.GetStatus() == EmberNetworkStatus.EMBER_JOINED_NETWORK;
        }

        /**
         * Performs an energy scan and returns the quietest channel
         *
         * @param ncp {@link EmberNcp}
         * @param scanDuration duration of the scan on each channel
         * @return the quietest channel, or null on error
         */
        private int? DoEnergyScan(EmberNcp ncp, int scanDuration) 
        {
            List<EzspEnergyScanResultHandler> channels = ncp.DoEnergyScan(ZigBeeChannelMask.CHANNEL_MASK_2GHZ, scanDuration);

            if (channels == null) {
                Log.Debug("Error during energy scan: {Status}", ncp.GetLastStatus());
                return null;
            }

            int lowestRSSI = 999;
            int lowestChannel = 11;
            foreach (EzspEnergyScanResultHandler channel in channels) 
            {
                if (channel.GetMaxRssiValue() < lowestRSSI) {
                    lowestRSSI = channel.GetMaxRssiValue();
                    lowestChannel = channel.GetChannel();
                }
            }

            return lowestChannel;
        }

        /**
         * Get the current network parameters
         *
         * @return the {@link EmberNetworkParameters} or null on error
         */
        private EmberNetworkParameters GetNetworkParameters() 
        {
            EzspGetNetworkParametersRequest networkParms = new EzspGetNetworkParametersRequest();
            EzspSingleResponseTransaction transaction = new EzspSingleResponseTransaction(networkParms, typeof(EzspGetNetworkParametersResponse));
            _protocolHandler.SendEzspTransaction(transaction);
            EzspGetNetworkParametersResponse getNetworkParametersResponse = (EzspGetNetworkParametersResponse) transaction.GetResponse();
            Log.Debug(getNetworkParametersResponse.ToString());
            if (getNetworkParametersResponse.GetStatus() != EmberStatus.EMBER_SUCCESS) 
            {
                Log.Debug("Error during retrieval of network parameters: {Response}", getNetworkParametersResponse);
                return null;
            }
            return getNetworkParametersResponse.GetParameters();
        }

        /**
         * Sets the initial security state
         *
         * @param linkKey the initial {@link ZigBeeKey}
         * @param networkKey the initial {@link ZigBeeKey}
         * @return true if the security state was set successfully
         */
        private bool SetSecurityState(ZigBeeKey linkKey, ZigBeeKey networkKey) 
        {
            EzspSetInitialSecurityStateRequest securityState = new EzspSetInitialSecurityStateRequest();
            EmberInitialSecurityState state = new EmberInitialSecurityState();
            state.AddBitmask(EmberInitialSecurityBitmask.EMBER_TRUST_CENTER_GLOBAL_LINK_KEY);

            EmberKeyData networkKeyData = new EmberKeyData();
            if (networkKey != null) 
            {
                networkKeyData.SetContents(Array.ConvertAll(networkKey.Key, c => (int)c));
                state.AddBitmask(EmberInitialSecurityBitmask.EMBER_HAVE_NETWORK_KEY);
                if (networkKey.SequenceNumber.HasValue) 
                {
                    state.SetNetworkKeySequenceNumber(networkKey.SequenceNumber.Value);
                }
            }
            state.SetNetworkKey(networkKeyData);

            EmberKeyData linkKeyData = new EmberKeyData();
            if (linkKey != null) 
            {
                linkKeyData.SetContents(Array.ConvertAll(linkKey.Key, c => (int)c));
                state.AddBitmask(EmberInitialSecurityBitmask.EMBER_HAVE_PRECONFIGURED_KEY);
                state.AddBitmask(EmberInitialSecurityBitmask.EMBER_REQUIRE_ENCRYPTED_KEY);
            }
            state.SetPreconfiguredKey(linkKeyData);

            state.SetPreconfiguredTrustCenterEui64(new IeeeAddress());

            securityState.SetState(state);
            EzspSingleResponseTransaction transaction = new EzspSingleResponseTransaction(securityState, typeof(EzspSetInitialSecurityStateResponse));
            _protocolHandler.SendEzspTransaction(transaction);
            EzspSetInitialSecurityStateResponse securityStateResponse = (EzspSetInitialSecurityStateResponse) transaction.GetResponse();
            Log.Debug(securityStateResponse.ToString());
            if (securityStateResponse.GetStatus() != EmberStatus.EMBER_SUCCESS) 
            {
                Log.Debug("Error during retrieval of network parameters: {Response}", securityStateResponse);
                return false;
            }

            EmberNcp ncp = new EmberNcp(_protocolHandler);
            if (networkKey != null && networkKey.OutgoingFrameCounter.HasValue) 
            {
                EzspSerializer serializer = new EzspSerializer();
                serializer.SerializeUInt32(networkKey.OutgoingFrameCounter.Value);
                if (ncp.SetValue(EzspValueId.EZSP_VALUE_NWK_FRAME_COUNTER, serializer.GetPayload()) != EzspStatus.EZSP_SUCCESS)
                    return false;
            }
            if (linkKey != null && linkKey.OutgoingFrameCounter.HasValue) 
            {
                EzspSerializer serializer = new EzspSerializer();
                serializer.SerializeUInt32(linkKey.OutgoingFrameCounter.Value);
                if (ncp.SetValue(EzspValueId.EZSP_VALUE_APS_FRAME_COUNTER, serializer.GetPayload()) != EzspStatus.EZSP_SUCCESS)
                    return false;
            }

            return true;
        }

        /**
         * Forms the ZigBee network as a coordinator
         *
         * @param networkParameters the {@link EmberNetworkParameters}
         * @return true if the network was formed successfully
         */
        private bool DoFormNetwork(EmberNetworkParameters networkParameters) 
        {
            networkParameters.SetJoinMethod(EmberJoinMethod.EMBER_USE_MAC_ASSOCIATION);

            EzspFormNetworkRequest formNetwork = new EzspFormNetworkRequest();
            formNetwork.SetParameters(networkParameters);
            EzspSingleResponseTransaction transaction = new EzspSingleResponseTransaction(formNetwork, typeof(EzspFormNetworkResponse));
            _protocolHandler.SendEzspTransaction(transaction);
            EzspFormNetworkResponse formNetworkResponse = (EzspFormNetworkResponse) transaction.GetResponse();
            Log.Debug(formNetworkResponse.ToString());
            if (formNetworkResponse.GetStatus() != EmberStatus.EMBER_SUCCESS) 
            {
                Log.Debug("Error forming network: {Response}", formNetworkResponse);
                return false;
            }

            return true;
        }

        /**
         * Joins an existing ZigBee network as a router
         *
         * @param networkParameters the {@link EmberNetworkParameters}
         * @return true if the network was joined successfully
         */
        private bool DoJoinNetwork(EmberNetworkParameters networkParameters) 
        {
            networkParameters.SetJoinMethod(EmberJoinMethod.EMBER_USE_MAC_ASSOCIATION);

            EzspJoinNetworkRequest joinNetwork = new EzspJoinNetworkRequest();
            joinNetwork.SetNodeType(EmberNodeType.EMBER_ROUTER);
            joinNetwork.SetParameters(networkParameters);
            EzspSingleResponseTransaction transaction = new EzspSingleResponseTransaction(joinNetwork, typeof(EzspJoinNetworkResponse));
            _protocolHandler.SendEzspTransaction(transaction);

            EzspJoinNetworkResponse joinNetworkResponse = (EzspJoinNetworkResponse) transaction.GetResponse();
            Log.Debug(joinNetworkResponse.ToString());
            if (joinNetworkResponse.GetStatus() != EmberStatus.EMBER_SUCCESS) 
            {
                Log.Debug("Error joining network: {Response}", joinNetworkResponse);
                return false;
            }
            return true;
        }

        /**
         * Rejoins an existing ZigBee network as a router.
         *
         * @param haveCurrentNetworkKey true if we already know the network key
         * @param channelMask the channel mask to scan.
         * @return true if the network was joined successfully
         */
        private bool DoRejoinNetwork(bool haveCurrentNetworkKey, ZigBeeChannelMask channelMask) 
        {
            EzspFindAndRejoinNetworkRequest rejoinNetwork = new EzspFindAndRejoinNetworkRequest();
            rejoinNetwork.SetHaveCurrentNetworkKey(haveCurrentNetworkKey);
            rejoinNetwork.SetChannelMask(channelMask.ChannelMask);
            EzspSingleResponseTransaction transaction = new EzspSingleResponseTransaction(rejoinNetwork, typeof(EzspFindAndRejoinNetworkResponse));
            _protocolHandler.SendEzspTransaction(transaction);

            EzspFindAndRejoinNetworkResponse rejoinNetworkResponse = (EzspFindAndRejoinNetworkResponse) transaction.GetResponse();
            Log.Debug(rejoinNetworkResponse.ToString());
            if (rejoinNetworkResponse.GetStatus() != EmberStatus.EMBER_SUCCESS) 
            {
                Log.Debug("Error rejoining network: {Response}", rejoinNetworkResponse);
                return false;
            }

            return true;
        }

    }
}
