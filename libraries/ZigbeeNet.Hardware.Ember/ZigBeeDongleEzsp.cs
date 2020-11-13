using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Serilog;
using ZigBeeNet.Hardware.Ember.Ezsp;
using ZigBeeNet.Hardware.Ember.Ezsp.Command;
using ZigBeeNet.Hardware.Ember.Ezsp.Structure;
using ZigBeeNet.Hardware.Ember.Internal;
using ZigBeeNet.Hardware.Ember.Internal.Ash;
using ZigBeeNet.Hardware.Ember.Transaction;
using ZigBeeNet.Security;
using ZigBeeNet.Transport;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.Hardware.Ember
{
    public class ZigBeeDongleEzsp : IZigBeeTransportTransmit, IEzspFrameHandler
    {

        private const int POLL_FRAME_ID = EzspNetworkStateRequest.FRAME_ID;
        private const int WAIT_FOR_ONLINE = 5000;

        /**
         * Response to the getBootloaderVersion if no bootloader is available
         */
        private const int BOOTLOADER_INVALID_VERSION = 0xFFFF;

        /**
         * The serial port used to connect to the dongle
         */
        private IZigBeePort _serialPort;

        /**
         * The protocol handler used to send and receive EZSP packets
         */
        private IEzspProtocolHandler _frameHandler;

        /**
         * The Ember bootload handler
         */
        //Not implemented yet
        //private EmberFirmwareUpdateHandler bootloadHandler;

        /**
         * The stack configuration we need for the NCP
         */
        private Dictionary<EzspConfigId, int> _stackConfiguration;

        /**
         * The stack policies we need for the NCP
         */
        private Dictionary<EzspPolicyId, EzspDecisionId> _stackPolicies;

        /**
         * The reference to the receive interface
         */
        private IZigBeeTransportReceive _zigbeeTransportReceive;

        /**
         * The current link key as {@link ZigBeeKey}
         */
        private ZigBeeKey _linkKey = new ZigBeeKey();

        /**
         * The current network key as {@link ZigBeeKey}
         */
        private ZigBeeKey _networkKey = new ZigBeeKey();

        /**
         * The current network parameters as {@link EmberNetworkParameters}
         */
        private EmberNetworkParameters _networkParameters = new EmberNetworkParameters();

        /**
         * The IeeeAddress of the Ember NCP
         */
        public IeeeAddress IeeeAddress { get; set; }

        /**
         * The network address of the Ember NCP
         */
        public ushort NwkAddress { get; set; }

        /**
         * Defines the type of device we want to be - normally this should be COORDINATOR
         */
        private DeviceType _deviceType = DeviceType.COORDINATOR;

        /**
         * The low level protocol to use for this dongle
         */
        private EmberSerialProtocol _protocol;

        /**
         * The Ember version used in this system. Set during initialisation and saved in case the client is interested.
         */
        public string VersionString { get; set; }  = "Unknown";

        /**
         * Boolean that is true when the network is UP
         */
        private bool _networkStateUp = false;

        /**
         * Boolean to hold initialisation state. Set to true after {@link #startup()} completes.
         */
        private bool _initialised = false;

        /**
         * Flag to indicate if the framework should pass multicast and broadcast messages sent by the framework, and
         * returned from the NCP, back to the framework as a received frame.
         */
        private bool _passLoopbackMessages = true;

        /**
         * The default ProfileID to use
         */
        private int _defaultProfileId = ZigBeeProfileType.Get(ProfileType.ZIGBEE_HOME_AUTOMATION).Key;

        /**
         * The default DeviceID to use
         */
        private int _defaultDeviceId = (int)ZigBeeDeviceType.HomeGateway;

        private System.Timers.Timer _pollingTimer = null;

        /**
         * The rate at which we will do a status poll if we've not sent any other messages within this period
         */
        private int _pollRate = 1000;

        /**
         * The time the last command was sent from the {@link ZigBeeNetworkManager}. This is used by the dongle polling task
         * to not poll if commands are otherwise being sent so as to reduce unnecessary communications with the dongle.
         */
        private DateTime _lastSendCommand;

        /**
         * If the dongle is being used with the manufacturing library, then this records the listener to be called when
         * packets are received.
         */
         //TODO
        //private EmberMfglibListener mfglibListener;

        /**
         * The {@link EmberNcpResetProvider} used to perform a hardware reset. If not set, no hardware reset will be
         * attempted.
         */
         //TODO
        //private EmberNcpResetProvider resetProvider;

        /**
         * List of input clusters supported - this will be added to the endpoint definition
         */
        private int[] _inputClusters = new int[] { 0 };

        /**
         * List of output clusters supported - this will be added to the endpoint definition
         */
        private int[] _outputClusters = new int[] { 0 };

        /**
         * We need to retain the transaction ID returned by the NCP when we're sending fragments so that we can use this in
         * the sendReply message and also pass the ACK to the application. This maps the NCP transaction IDs to the
         * framework IDs.
         */
        //Fragmentation not implemented yet
        /*
        Dictionary<int, int> fragmentationApsCounters = new Dictionary<int, int>();
        */

        /**
         * Create a {@link ZigBeeDongleEzsp} with the default ASH2 frame handler
         *
         * @param serialPort the {@link ZigBeePort} to use for the connection
         */
        public ZigBeeDongleEzsp(IZigBeePort serialPort) 
            : this(serialPort, EmberSerialProtocol.ASH2)
        {
        }

        /**
         * Create a {@link ZigBeeDongleEzsp} with the default ASH frame handler
         *
         * @param serialPort the {@link ZigBeePort} to use for the connection
         * @param protocol the {@link EmberSerialProtocol} to use
         */
        public ZigBeeDongleEzsp(IZigBeePort serialPort, EmberSerialProtocol protocol) {
            this._serialPort = serialPort;
            this._protocol = protocol;

            // Define the default configuration
            _stackConfiguration = new Dictionary<EzspConfigId, int>();
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_SOURCE_ROUTE_TABLE_SIZE, 16);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_SECURITY_LEVEL, 5);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_ADDRESS_TABLE_SIZE, 8);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_TRUST_CENTER_ADDRESS_CACHE_SIZE, 2);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_STACK_PROFILE, 2);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_INDIRECT_TRANSMISSION_TIMEOUT, 7680);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_MAX_HOPS, 30);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_TX_POWER_MODE, 0);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_SUPPORTED_NETWORKS, 1);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_KEY_TABLE_SIZE, 4);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_APPLICATION_ZDO_FLAGS, (int)EmberZdoConfigurationFlags.EMBER_APP_RECEIVES_SUPPORTED_ZDO_REQUESTS);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_MAX_END_DEVICE_CHILDREN, 16);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_APS_UNICAST_MESSAGE_COUNT, 10);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_BROADCAST_TABLE_SIZE, 15);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_NEIGHBOR_TABLE_SIZE, 16);
            //Fragmentation not implemented yet
            //stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_FRAGMENT_WINDOW_SIZE, 1);
            //stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_FRAGMENT_DELAY_MS, 50);
            _stackConfiguration.Add(EzspConfigId.EZSP_CONFIG_PACKET_BUFFER_COUNT, 255);

            // Define the default policies
            _stackPolicies = new Dictionary<EzspPolicyId, EzspDecisionId>();
            _stackPolicies.Add(EzspPolicyId.EZSP_TC_KEY_REQUEST_POLICY, EzspDecisionId.EZSP_DENY_TC_KEY_REQUESTS);
            _stackPolicies.Add(EzspPolicyId.EZSP_TRUST_CENTER_POLICY, EzspDecisionId.EZSP_ALLOW_PRECONFIGURED_KEY_JOINS);
            _stackPolicies.Add(EzspPolicyId.EZSP_MESSAGE_CONTENTS_IN_CALLBACK_POLICY, EzspDecisionId.EZSP_MESSAGE_TAG_ONLY_IN_CALLBACK);
            _stackPolicies.Add(EzspPolicyId.EZSP_APP_KEY_REQUEST_POLICY, EzspDecisionId.EZSP_DENY_APP_KEY_REQUESTS);
            _stackPolicies.Add(EzspPolicyId.EZSP_BINDING_MODIFICATION_POLICY, EzspDecisionId.EZSP_CHECK_BINDING_MODIFICATIONS_ARE_VALID_ENDPOINT_CLUSTERS);

            _networkKey = new ZigBeeKey();
        }

        /**
         * Sets the hardware reset provider if the dongle supports a hardware reset. If this is not set, the dongle driver
         * will not attempt a hardware reset and will attempt to use other software methods to reset the dongle as may be
         * available by the low level protocol.
         *
         * @param resetProvider the {@link EmberNcpResetProvider} to be called to perform the reset
         */
         /*
        public void setEmberNcpResetProvider(EmberNcpResetProvider resetProvider) {
            this.resetProvider = resetProvider;
        }
        */

        /**
         * Update the Ember configuration that will be sent to the dongle during the initialisation.
         * <p>
         * Note that this must be called prior to {@link #initialize()} for the configuration to be effective.
         *
         * @param configId the {@link EzspConfigId} to be updated.
         * @param value the value to set (as {@link Integer}. Setting this to null will remove the configuration Id from the
         *            list of configuration to be sent during NCP initialisation.
         * @return the previously configured value, or null if no value was set for the {@link EzspConfigId}
         */
        public int? UpdateDefaultConfiguration(EzspConfigId configId, int? value) 
        {
            int? previousValue = _stackConfiguration.ContainsKey(configId) ? (int?)_stackConfiguration[configId] : null;
            if (value == null)
                _stackConfiguration.Remove(configId);
            else
                _stackConfiguration[configId] = value.Value;

            return previousValue;
        }

        /**
         * Update the Ember policies that will be sent to the dongle during the initialisation.
         * <p>
         * Note that this must be called prior to {@link #initialize()} for the configuration to be effective.
         *
         * @param policyId the {@link EzspPolicyId} to be updated
         * @param decisionId the (as {@link EzspDecisionId} to set. Setting this to null will remove the policy from
         *            the list of policies to be sent during NCP initialisation.
         * @return the previously configured {@link EzspDecisionId}, or null if no value was set for the
         *         {@link EzspPolicyId}
         */
        public EzspDecisionId? UpdateDefaultPolicy(EzspPolicyId policyId, EzspDecisionId? decisionId) 
        {
            EzspDecisionId? previousValue = _stackPolicies.ContainsKey(policyId) ? (EzspDecisionId?)_stackPolicies[policyId] : null;

            if (decisionId == null)
                _stackPolicies.Remove(policyId);
            else
                _stackPolicies[policyId] = decisionId.Value;

            return previousValue;
        }

        /**
         * Gets an {@link EmberMfglib} instance that can be used for low level testing of the Ember dongle.
         * <p>
         * This may only be used if the {@link ZigBeeDongleEmber} instance has not been initialized on a ZigBee network.
         *
         * @param mfglibListener a {@link EmberMfglibListener} to receive packets received. May be null.
         * @return the {@link EmberMfglib} instance, or null on error
         */
         /*
        public EmberMfglib getEmberMfglib(EmberMfglibListener mfglibListener) {
            if (frameHandler == null && !initialiseEzspProtocol()) {
                return null;
            }

            this.mfglibListener = mfglibListener;

            return new EmberMfglib(frameHandler);
        }
        */

        public void SetDefaultProfileId(int defaultProfileId) 
        {
            this._defaultProfileId = defaultProfileId;
        }

        public void SetDefaultDeviceId(int defaultDeviceId) 
        {
            this._defaultDeviceId = defaultDeviceId;
        }

        public ZigBeeStatus Initialize() 
        {
            Log.Debug("EZSP Dongle: Initialize with protocol {Protocol}.", _protocol);
            _zigbeeTransportReceive.SetTransportState(ZigBeeTransportState.INITIALISING);

            if (_protocol != EmberSerialProtocol.NONE && !InitialiseEzspProtocol()) 
                return ZigBeeStatus.COMMUNICATION_ERROR;

            // Perform any stack configuration
            EmberStackConfiguration stackConfigurer = new EmberStackConfiguration(GetEmberNcp());

            Dictionary<EzspConfigId, int?> configuration = stackConfigurer.GetConfiguration(_stackConfiguration.Keys);
            foreach (var config in configuration) 
            {
                Log.Debug("Configuration state {Key} = {Value}", config.Key, config.Value);
            }

            Dictionary<EzspPolicyId, EzspDecisionId> policies = stackConfigurer.GetPolicy(_stackPolicies.Keys);
            foreach (var policy in policies) 
            {
                Log.Debug("Policy state {Key} = {Value}", policy.Key, policy.Value);
            }

            stackConfigurer.SetConfiguration(_stackConfiguration);
            configuration = stackConfigurer.GetConfiguration(_stackConfiguration.Keys);
            foreach (var config in configuration)
            {
                Log.Debug("Configuration state {Key} = {Value}", config.Key, config.Value);
            }

            stackConfigurer.SetPolicy(_stackPolicies);
            policies = stackConfigurer.GetPolicy(_stackPolicies.Keys);
            foreach (var policy in policies)
            {
                Log.Debug("Policy state {Key} = {Value}", policy.Key, policy.Value);
            }

            EmberNcp ncp = GetEmberNcp();

            // Get the current network parameters so that any configuration updates start from here
            _networkParameters = ncp.GetNetworkParameters().GetParameters();
            Log.Debug("Ember initial network parameters are {NetworkParameters}", _networkParameters);

            IeeeAddress = ncp.GetIeeeAddress();
            Log.Debug("Ember local IEEE Address is {IeeeAddress}", IeeeAddress);

            ncp.GetNetworkParameters();

            Log.Debug("EZSP Dongle: initialize done");

            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus Startup(bool reinitialize) 
        {
            Log.Debug("EZSP Dongle: Startup - reinitialize={Reinitialize}", reinitialize);

            // If frameHandler is null then the serial port didn't initialise or startup has not been called
            if (_frameHandler == null) 
            {
                Log.Error("EZSP Dongle: Startup found low level handler is not initialised.");
                return ZigBeeStatus.INVALID_STATE;
            }

            EmberNcp ncp = GetEmberNcp();

            // Add the endpoint
            Log.Debug("EZSP Adding Endpoint: ProfileID={ProfileID}, DeviceID={DeviceID}", _defaultProfileId.ToString("X4"), _defaultDeviceId.ToString("X4"));
            Log.Debug("EZSP Adding Endpoint: Input Clusters   {InputClusters}", _inputClusters);
            Log.Debug("EZSP Adding Endpoint: Output Clusters  {OutputClusters}", _outputClusters);
            ncp.AddEndpoint(1, _defaultDeviceId, _defaultProfileId, _inputClusters, _outputClusters);

            // Now initialise the network
            EmberStatus initResponse = ncp.NetworkInit();
            if (initResponse == EmberStatus.EMBER_NOT_JOINED) 
            {
                Log.Debug("EZSP dongle initialize done - response {Response}", initResponse);
            }

            // Print current security state to debug logs
            ncp.getCurrentSecurityState();

            ScheduleNetworkStatePolling();

            // Check if the network is initialised
            EmberNetworkStatus networkState = ncp.GetNetworkState();
            Log.Debug("EZSP networkStateResponse {State}", networkState);

            // If we want to reinitialize the network, then go...
            EmberNetworkInitialisation netInitialiser = new EmberNetworkInitialisation(_frameHandler);
            if (reinitialize) 
            {
                Log.Debug("Reinitialising Ember NCP network as {DeviceType}", _deviceType);
                if (_deviceType == DeviceType.COORDINATOR)
                    netInitialiser.FormNetwork(_networkParameters, _linkKey, _networkKey);
                else
                    netInitialiser.JoinNetwork(_networkParameters, _linkKey);

            } 
            else if (_deviceType == DeviceType.ROUTER) 
            {
                netInitialiser.RejoinNetwork();
            }
            ncp.GetNetworkParameters();

            // Wait for the network to come up
            networkState = WaitNetworkStartup(ncp);
            Log.Debug("EZSP networkState after online wait {NetworkState}", networkState);

            // Get the security state - mainly for information
            EmberCurrentSecurityState currentSecurityState = ncp.getCurrentSecurityState();
            Log.Debug("EZSP Current Security State = {CurrentSecurityState}", currentSecurityState);

            EmberStatus txPowerResponse = ncp.SetRadioPower(_networkParameters.GetRadioTxPower());
            if (txPowerResponse != EmberStatus.EMBER_SUCCESS) 
            {
                Log.Debug("Setting TX Power to {TxPower} resulted in {Response}", _networkParameters.GetRadioTxPower(), txPowerResponse);
            }

            int address = ncp.GetNwkAddress();
            if (address != 0xFFFE)
                NwkAddress = (ushort) address;

            Log.Debug("EZSP Dongle: Startup complete. NWK Address = {NwkAddress}, State = {NetworkState}", NwkAddress.ToString("X4"), networkState);

            // At this stage, we will now take note of the EzspStackStatusHandler notifications
            bool joinedNetwork = (networkState == EmberNetworkStatus.EMBER_JOINED_NETWORK || networkState == EmberNetworkStatus.EMBER_JOINED_NETWORK_NO_PARENT);
            _initialised = true;
            HandleLinkStateChange(joinedNetwork);

            return joinedNetwork ? ZigBeeStatus.SUCCESS : ZigBeeStatus.BAD_RESPONSE;
        }

        /**
         * Waits for the network to start. This periodically polls the network state waiting for the network to come online.
         * If a terminal state is observed (eg EMBER_JOINED_NETWORK or EMBER_LEAVING_NETWORK) whereby the network cannot
         * start, then this method will return.
         * <p>
         * If the network start starts to join, but then shows EMBER_NO_NETWORK, it will return. Otherwise it will wait for
         * the timeout.
         *
         * @param ncp
         * @return
         */
        private EmberNetworkStatus WaitNetworkStartup(EmberNcp ncp) 
        {
            EmberNetworkStatus networkState;
            bool joinStarted = false;
            DateTime startTime = DateTime.Now;
            do 
            {
                networkState = ncp.GetNetworkState();
                switch (networkState) 
                {
                    case EmberNetworkStatus.EMBER_JOINING_NETWORK:
                        joinStarted = true;
                        break;
                    case EmberNetworkStatus.EMBER_NO_NETWORK:
                        if (joinStarted)
                            return networkState;
                        break;
                        
                    case EmberNetworkStatus.EMBER_JOINED_NETWORK:
                    case EmberNetworkStatus.EMBER_JOINED_NETWORK_NO_PARENT:
                    case EmberNetworkStatus.EMBER_LEAVING_NETWORK:
                        return networkState;
                    default:
                        break;
                }

                Thread.Sleep(250);
            } 
            while ((DateTime.Now - startTime).TotalMilliseconds < WAIT_FOR_ONLINE);

            return networkState;
        }

        /**
         * This method schedules sending a status request frame on the interval specified by pollRate. If the frameHandler
         * does not receive a response after a certain amount of retries, the state will be set to OFFLINE.
         * The poll will not be sent if other commands have been sent to the dongle within the pollRate period so as to
         * eliminate any unnecessary traffic with the dongle.
         */
        private void ScheduleNetworkStatePolling() 
        {
            if (_pollingTimer != null) 
            {
                _pollingTimer.Stop();
            }

            if (_pollRate == 0) {
                return;
            }

            _pollingTimer = new System.Timers.Timer();
            _pollingTimer.AutoReset = true;
            _pollingTimer.Interval = _pollRate;
            _pollingTimer.Elapsed += new ElapsedEventHandler(OnPollTimerElapsedEvent);
            _pollingTimer.Start();
        }

        private void OnPollTimerElapsedEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                // Don't poll the state if the network is down
                // or we've sent a command to the dongle within the pollRate
                if (!_networkStateUp || (DateTime.Now - _lastSendCommand).TotalMilliseconds < _pollRate)
                {
                    return;
                }
                // Don't wait for the response. This is running in a single thread scheduler
                _frameHandler.QueueFrame(new EzspNetworkStateRequest());
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "EZSP Dongle: error in poll timer elapsed event");
            }
        }

        /**
         * Set the polling rate at which the handler will poll the NCP to ensure it is still responding.
         * If the NCP fails to respond within a fixed time the driver will be set to OFFLINE.
         * <p>
         * Setting the rate to 0 will disable polling
         *
         * @param pollRate the polling rate in milliseconds. 0 will disable polling.
         */
        public void SetPollRate(int pollRate) 
        {
            this._pollRate = pollRate;
            ScheduleNetworkStatePolling();
        }

        public void Shutdown() 
        {
            Log.Debug("EZSP Dongle: Shutdown");
            if (_frameHandler == null) 
            {
                Log.Debug("EZSP Dongle: Shutdown frameHandler is null");
                return;
            }
            _frameHandler.SetClosing();

            /*
            if (mfglibListener != null) {
                mfglibListener = null;
            }
            */

            if (_pollingTimer != null) {
                _pollingTimer.Stop();
            }

            _frameHandler.Close();
            _serialPort.Close();
            _frameHandler = null;
        }

        /**
         * Configures the driver to pass or drop loopback messages received from the NCP. These are MUTICAST or BROADCAST
         * messages that were sent by the framework, and retransmitted by the NCP.
         * <p>
         * This defaults to passing the loopback frames back to the framework
         *
         * @param passLoopbackMessages true if the driver should pass loopback messages back as a received frame
         */
        public void PassLoopbackMessages(bool passLoopbackMessages) 
        {
            this._passLoopbackMessages = passLoopbackMessages;
        }

        /**
         * Returns an instance of the {@link EmberNcp}
         *
         * @return an instance of the {@link EmberNcp}
         */
        public EmberNcp GetEmberNcp() 
        {
            return new EmberNcp(_frameHandler);
        }

        /**
         * Returns an instance of the {@link EmberCbkeProvider}
         *
         * @return an instance of the {@link EmberCbkeProvider}
         */
         /*
        public EmberCbkeProvider getEmberCbkeProvider() {
            return new EmberCbkeProvider(this);
        }
        */

        public void SendCommand(/*int msgTag,*/ ZigBeeApsFrame apsFrame) 
        {
            if (_frameHandler == null)
                return;

            _lastSendCommand = DateTime.Now;

            IEzspTransaction transaction;

            EmberApsFrame emberApsFrame = new EmberApsFrame();
            emberApsFrame.SetClusterId(apsFrame.Cluster);
            emberApsFrame.SetProfileId(apsFrame.Profile);
            emberApsFrame.SetSourceEndpoint(apsFrame.SourceEndpoint);
            emberApsFrame.SetDestinationEndpoint(apsFrame.DestinationEndpoint);
            emberApsFrame.SetSequence(apsFrame.ApsCounter);
            emberApsFrame.AddOptions(EmberApsOption.EMBER_APS_OPTION_RETRY);
            emberApsFrame.AddOptions(EmberApsOption.EMBER_APS_OPTION_ENABLE_ROUTE_DISCOVERY);
            emberApsFrame.AddOptions(EmberApsOption.EMBER_APS_OPTION_ENABLE_ADDRESS_DISCOVERY);

            if (apsFrame.SecurityEnabled) 
            {
                emberApsFrame.AddOptions(EmberApsOption.EMBER_APS_OPTION_ENCRYPTION);
            }

            if (apsFrame.AddressMode == ZigBeeNwkAddressMode.Device && !ZigBeeBroadcastDestination.IsBroadcast(apsFrame.DestinationAddress)) 
            {
                EzspSendUnicastRequest emberUnicast = new EzspSendUnicastRequest();
                emberUnicast.SetIndexOrDestination(apsFrame.DestinationAddress);
                //emberUnicast.SetMessageTag(msgTag);
                emberUnicast.SetSequenceNumber(apsFrame.ApsCounter);
                emberUnicast.SetType(EmberOutgoingMessageType.EMBER_OUTGOING_DIRECT);
                emberUnicast.SetApsFrame(emberApsFrame);
                emberUnicast.SetMessageContents(Array.ConvertAll(apsFrame.Payload, c => (int)c));

                //Fragmentation not implemented yet
                /*
                if (apsFrame is ZigBeeApsFrameFragment) 
                {
                    ZigBeeApsFrameFragment fragment = (ZigBeeApsFrameFragment) apsFrame;
                    emberApsFrame.addOptions(EmberApsOption.EMBER_APS_OPTION_FRAGMENT);
                    emberApsFrame.setGroupId(fragment.getFragmentNumber() + (fragment.getFragmentTotal() << 8));
                    if (fragment.getFragmentNumber() != 0) {
                        emberApsFrame.setSequence(fragmentationApsCounters.get(msgTag));
                    }
                    if (fragment.getFragmentNumber() == fragment.getFragmentTotal() - 1) {
                        fragmentationApsCounters.remove(msgTag);
                    }
                }
                */

                transaction = new EzspSingleResponseTransaction(emberUnicast, typeof(EzspSendUnicastResponse));
            } 
            else if (apsFrame.AddressMode == ZigBeeNwkAddressMode.Device && ZigBeeBroadcastDestination.IsBroadcast(apsFrame.DestinationAddress)) 
            {
                EzspSendBroadcastRequest emberBroadcast = new EzspSendBroadcastRequest();
                emberBroadcast.SetDestination(apsFrame.DestinationAddress);
                //emberBroadcast.SetMessageTag(msgTag);
                emberBroadcast.SetSequenceNumber(apsFrame.ApsCounter);
                emberBroadcast.SetApsFrame(emberApsFrame);
                emberBroadcast.SetRadius(apsFrame.Radius);
                emberBroadcast.SetMessageContents(Array.ConvertAll(apsFrame.Payload, c => (int)c));

                transaction = new EzspSingleResponseTransaction(emberBroadcast, typeof(EzspSendBroadcastResponse));
            } 
            else if (apsFrame.AddressMode == ZigBeeNwkAddressMode.Group) 
            {
                emberApsFrame.SetGroupId(apsFrame.GroupAddress);

                EzspSendMulticastRequest emberMulticast = new EzspSendMulticastRequest();
                emberMulticast.SetApsFrame(emberApsFrame);
                emberMulticast.SetHops(apsFrame.Radius);
                emberMulticast.SetNonmemberRadius(apsFrame.NonMemberRadius);
                //emberMulticast.SetMessageTag(msgTag);
                emberMulticast.SetMessageContents(Array.ConvertAll(apsFrame.Payload, c => (int)c));

                transaction = new EzspSingleResponseTransaction(emberMulticast, typeof(EzspSendMulticastResponse));
            } 
            else 
            {
                Log.Debug("EZSP message not sent as unknown address mode: {ApsFrame}", apsFrame);
                return;
            }

            // The response from the SendXxxcast messages returns the network layer sequence number
            // We need to correlate this with the messageTag
            Task.Run(() =>
            {
                try
                {
                    _frameHandler.SendEzspTransaction(transaction);

                    EmberStatus status = EmberStatus.UNKNOWN;
                    if (transaction.GetResponse() is EzspSendUnicastResponse)
                    {
                        //Fragmentation not implemented yet        
                        //fragmentationApsCounters.put(msgTag, ((EzspSendUnicastResponse) transaction.getResponse()).getSequence());
                        status = ((EzspSendUnicastResponse)transaction.GetResponse()).GetStatus();
                    }
                    else if (transaction.GetResponse() is EzspSendBroadcastResponse)
                    {
                        status = ((EzspSendBroadcastResponse)transaction.GetResponse()).GetStatus();
                    }
                    else if (transaction.GetResponse() is EzspSendMulticastResponse)
                    {
                        status = ((EzspSendMulticastResponse)transaction.GetResponse()).GetStatus();
                    }
                    else
                    {
                        Log.Debug("Unable to get response from {Request} :: {Response}", transaction.GetRequest(), transaction.GetResponse());
                        return;
                    }

                    // If this is EMBER_SUCCESS, then do nothing as the command is still not transmitted.
                    // If there was an error, then we let the system know we've failed already!
                    if (status == EmberStatus.EMBER_SUCCESS)
                        return;

                    //Not implemented yet
                    //zigbeeTransportReceive.ReceiveCommandState(msgTag, ZigBeeTransportProgressState.TX_NAK);
                }
                catch (Exception ex)
                {
                    Log.Debug(ex, "EZSP Dongle: error in SendCommand");
                }
            });
        }

        public void SetZigBeeTransportReceive(IZigBeeTransportReceive zigbeeTransportReceive) 
        {
            this._zigbeeTransportReceive = zigbeeTransportReceive;
        }

        public void SetNodeDescriptor(IeeeAddress ieeeAddress, NodeDescriptor nodeDescriptor) 
        {
            // Update the extendedTimeout flag in the address table.
            // Users should ensure the address table is large enough to hold all nodes on the network.
            Log.Debug("{IeeeAddress}: NodeDescriptor passed to Ember NCP {NodeDescriptor}", ieeeAddress, nodeDescriptor);
            if (!nodeDescriptor.MacCapabilities.Contains(NodeDescriptor.MacCapabilitiesType.RECEIVER_ON_WHEN_IDLE)) 
            {
                EmberNcp ncp = GetEmberNcp();
                ncp.SetExtendedTimeout(ieeeAddress, true);
            }
        }

        public void HandlePacket(EzspFrame response) 
        {
            if (response.GetFrameId() != POLL_FRAME_ID) {
                Log.Debug("RX EZSP: {Response}", response);
            }

            if (response is EzspIncomingMessageHandler) 
            {
                if (!_initialised) {
                    Log.Debug("Ignoring received frame as stack is still initialising");
                    return;
                }
                EzspIncomingMessageHandler incomingMessage = (EzspIncomingMessageHandler) response;
                EmberApsFrame emberApsFrame = incomingMessage.GetApsFrame();
                ZigBeeApsFrame apsFrame = new ZigBeeApsFrame();
            //Fragmentation not implemented yet    
            /*
                if (emberApsFrame.getOptions().contains(EmberApsOption.EMBER_APS_OPTION_FRAGMENT)) {
                    ZigBeeApsFrameFragment fragment = new ZigBeeApsFrameFragment(emberApsFrame.getGroupId() & 0xFF);
                    if ((emberApsFrame.getGroupId() & 0xFF) == 0) {
                        fragment.setFragmentTotal((emberApsFrame.getGroupId() & 0xFF00) >> 8);
                    }
                    // We must respond to a fragment with a sendReply command
                    EzspSendReplyRequest sendReply = new EzspSendReplyRequest();
                    emberApsFrame.setGroupId(emberApsFrame.getGroupId() | 0xFF00);
                    sendReply.setApsFrame(emberApsFrame);
                    sendReply.setSender(incomingMessage.getSender());
                    sendReply.setMessageContents(new int[] {});
                    frameHandler.queueFrame(sendReply);
                    apsFrame = fragment;
                } else {
                    apsFrame = new ZigBeeApsFrame();
                }
                */

                switch (incomingMessage.GetType2()) 
                {
                    case EmberIncomingMessageType.EMBER_INCOMING_BROADCAST_LOOPBACK:
                        if (!_passLoopbackMessages)
                            return;
                        apsFrame.AddressMode = ZigBeeNwkAddressMode.Device;
                        break;
                    case EmberIncomingMessageType.EMBER_INCOMING_BROADCAST:
                    case EmberIncomingMessageType.EMBER_INCOMING_UNICAST:
                    case EmberIncomingMessageType.EMBER_INCOMING_UNICAST_REPLY:
                        apsFrame.AddressMode = ZigBeeNwkAddressMode.Device;
                        break;
                    case EmberIncomingMessageType.EMBER_INCOMING_MULTICAST_LOOPBACK:
                        if (!_passLoopbackMessages)
                            return;
                        apsFrame.AddressMode = ZigBeeNwkAddressMode.Group;
                        break;
                    case EmberIncomingMessageType.EMBER_INCOMING_MULTICAST:
                        apsFrame.AddressMode = ZigBeeNwkAddressMode.Group;
                        break;
                    case EmberIncomingMessageType.EMBER_INCOMING_MANY_TO_ONE_ROUTE_REQUEST:
                        return;
                    case EmberIncomingMessageType.UNKNOWN:
                        Log.Information("Ignoring unknown EZSP incoming message type");
                        return;
                }

                apsFrame.ApsCounter = (byte)emberApsFrame.GetSequence();
                apsFrame.Cluster = (ushort)emberApsFrame.GetClusterId();
                apsFrame.Profile = (ushort)emberApsFrame.GetProfileId();
                apsFrame.SecurityEnabled = emberApsFrame.GetOptions().Contains(EmberApsOption.EMBER_APS_OPTION_ENCRYPTION);

                apsFrame.DestinationAddress = NwkAddress;
                apsFrame.DestinationEndpoint = (byte) emberApsFrame.GetDestinationEndpoint();

                apsFrame.SourceAddress = (ushort)incomingMessage.GetSender();
                apsFrame.SourceEndpoint = (byte)emberApsFrame.GetSourceEndpoint();

                apsFrame.Payload = Array.ConvertAll(incomingMessage.GetMessageContents(), c => (byte)c);
                _zigbeeTransportReceive.ReceiveCommand(apsFrame);

                return;
            }

            // Message has been completed by the NCP
            //Not implemented yet
            /*
            if (response is EzspMessageSentHandler) 
            {
                Task.Run(() =>
                {
                    EzspMessageSentHandler sentHandler = (EzspMessageSentHandler) response;
                    ZigBeeTransportProgressState sentHandlerState;
                    if (sentHandler.GetStatus() == EmberStatus.EMBER_SUCCESS) {
                        sentHandlerState = ZigBeeTransportProgressState.RX_ACK;
                    } else {
                        sentHandlerState = ZigBeeTransportProgressState.RX_NAK;
                    }
                    zigbeeTransportReceive.ReceiveCommandState(sentHandler.GetMessageTag(), sentHandlerState);
                });
                return;
            }
            */

            if (response is EzspStackStatusHandler) 
            {
                switch (((EzspStackStatusHandler) response).GetStatus()) 
                {
                    case EmberStatus.EMBER_NETWORK_BUSY:
                        break;
                    case EmberStatus.EMBER_PRECONFIGURED_KEY_REQUIRED:
                    case EmberStatus.EMBER_NETWORK_DOWN:
                        HandleLinkStateChange(false);
                        break;
                    case EmberStatus.EMBER_NETWORK_UP:
                        HandleLinkStateChange(true);
                        break;
                    default:
                        break;
                }
                return;
            }

            if (response is EzspTrustCenterJoinHandler) 
            {
                EzspTrustCenterJoinHandler joinHandler = (EzspTrustCenterJoinHandler) response;

                ZigBeeNodeStatus status;
                switch (joinHandler.GetStatus()) 
                {
                    case EmberDeviceUpdate.EMBER_HIGH_SECURITY_UNSECURED_JOIN:
                    case EmberDeviceUpdate.EMBER_STANDARD_SECURITY_UNSECURED_JOIN:
                        status = ZigBeeNodeStatus.UNSECURED_JOIN;
                        break;
                    case EmberDeviceUpdate.EMBER_HIGH_SECURITY_UNSECURED_REJOIN:
                    case EmberDeviceUpdate.EMBER_STANDARD_SECURITY_UNSECURED_REJOIN:
                        status = ZigBeeNodeStatus.UNSECURED_REJOIN;
                        break;
                    case EmberDeviceUpdate.EMBER_HIGH_SECURITY_SECURED_REJOIN:
                    case EmberDeviceUpdate.EMBER_STANDARD_SECURITY_SECURED_REJOIN:
                        status = ZigBeeNodeStatus.SECURED_REJOIN;
                        break;
                    case EmberDeviceUpdate.EMBER_DEVICE_LEFT:
                        status = ZigBeeNodeStatus.DEVICE_LEFT;
                        break;
                    default:
                        Log.Debug("Unknown state in trust centre join handler {Status}", joinHandler.GetStatus());
                        return;
                }

                _zigbeeTransportReceive.NodeStatusUpdate(status, (ushort)joinHandler.GetNewNodeId(), joinHandler.GetNewNodeEui64());
                return;
            }

            if (response is EzspChildJoinHandler) 
            {
                EzspChildJoinHandler joinHandler = (EzspChildJoinHandler) response;
                _zigbeeTransportReceive.NodeStatusUpdate(
                        joinHandler.GetJoining() ? ZigBeeNodeStatus.UNSECURED_JOIN : ZigBeeNodeStatus.DEVICE_LEFT,
                        (ushort)joinHandler.GetChildId(), joinHandler.GetChildEui64());
                return;
            }
        
            /*
            if (response instanceof EzspMfglibRxHandler) {
                if (mfglibListener != null) {
                    EzspMfglibRxHandler mfglibHandler = (EzspMfglibRxHandler) response;
                    mfglibListener.emberMfgLibPacketReceived(mfglibHandler.getLinkQuality(), mfglibHandler.getLinkQuality(),
                            mfglibHandler.getPacketContents());
                }
                return;
            }
            */
        }

        public void HandleLinkStateChange(bool linkState) 
        {
            Log.Debug("Ember: Link State change to {LinkState}, initialised={Initialised}, networkStateUp={NetworkStateUp}", linkState, _initialised, _networkStateUp);

            // Only act on changes to OFFLINE once we have completed initialisation
            // changes to ONLINE have to work during init because they mark the end of the initialisation
            if (!_initialised || linkState == _networkStateUp) 
            {
                Log.Debug("Ember: Link State change to {LinkState} ignored.", linkState);
                return;
            }
            _networkStateUp = linkState;

            Task.Run(() =>
            {
                if (linkState)
                {
                    Log.Debug("Ember: Link State up running");

                    EmberNcp ncp = GetEmberNcp();
                    int addr = ncp.GetNwkAddress();
                    if (addr != 0xFFFE) {
                        NwkAddress = (ushort)addr;
                    }
                }
                // Handle link changes and notify framework
                _zigbeeTransportReceive.SetTransportState(linkState ? ZigBeeTransportState.ONLINE : ZigBeeTransportState.OFFLINE);
            });
        }

        public ZigBeeChannel ZigBeeChannel
        {
            get { return (ZigBeeChannel)(1 << _networkParameters.GetRadioChannel()); } 
        }

        public ZigBeeStatus SetZigBeeChannel(ZigBeeChannel channel) 
        {
            if ((ZigBeeChannelMask.CHANNEL_MASK_2GHZ & (int)channel) == 0) 
            {
                Log.Debug("Unable to set channel outside of 2.4GHz channels: {Channel}", channel);
                return ZigBeeStatus.INVALID_ARGUMENTS;
            }
            _networkParameters.SetRadioChannel(channel.GetChannelNum());
            return ZigBeeStatus.SUCCESS;
        }

        public ushort PanID
        {
            get { return (ushort) _networkParameters.GetPanId(); }
        }

        public ZigBeeStatus SetZigBeePanId(ushort panId) 
        {
            _networkParameters.SetPanId(panId);
            return ZigBeeStatus.SUCCESS;
        }

        public ExtendedPanId ExtendedPanId 
        {
            get { return _networkParameters.GetExtendedPanId(); }
        }

        public ZigBeeStatus SetZigBeeExtendedPanId(ExtendedPanId extendedPanId) 
        {
            _networkParameters.SetExtendedPanId(extendedPanId);
            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus SetZigBeeNetworkKey(ZigBeeKey key) 
        {
            _networkKey = key;
            if (_networkStateUp) 
                return ZigBeeStatus.INVALID_STATE;

            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeKey ZigBeeNetworkKey 
        {
            get
            {
                EmberNcp ncp = GetEmberNcp();
                EmberKeyStruct key = ncp.GetKey(EmberKeyType.EMBER_CURRENT_NETWORK_KEY);
                return EmberKeyToZigBeeKey(key);
            }
        }

        public ZigBeeStatus SetTcLinkKey(ZigBeeKey key) 
        {
            _linkKey = key;
            if (_networkStateUp)
                return ZigBeeStatus.INVALID_STATE;

            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeKey TcLinkKey 
        {
            get
            {
                EmberNcp ncp = GetEmberNcp();
                EmberKeyStruct key = ncp.GetKey(EmberKeyType.EMBER_TRUST_CENTER_LINK_KEY);
                return EmberKeyToZigBeeKey(key);
            }
        }

        public void UpdateTransportConfig(TransportConfig configuration) 
        {
            foreach (TransportConfigOption option in configuration.GetOptions()) 
            {
                try 
                {
                    switch (option) 
                    {
                        case TransportConfigOption.CONCENTRATOR_CONFIG:
                            configuration.SetResult(option, SetConcentrator((ConcentratorConfig) configuration.GetValue(option)));
                            break;

                        case TransportConfigOption.INSTALL_KEY:
                            EmberNcp ncp = GetEmberNcp();
                            ZigBeeKey nodeKey = (ZigBeeKey) configuration.GetValue(option);
                            if (!nodeKey.HasAddress()) 
                            {
                                Log.Debug("Attempt to set INSTALL_KEY without setting address");
                                configuration.SetResult(option, ZigBeeStatus.FAILURE);
                                break;
                            }
                            EmberStatus result = ncp.AddTransientLinkKey(nodeKey.address, nodeKey);

                            configuration.SetResult(option, result == EmberStatus.EMBER_SUCCESS ? ZigBeeStatus.SUCCESS : ZigBeeStatus.FAILURE);
                            break;

                        case TransportConfigOption.RADIO_TX_POWER:
                            configuration.SetResult(option, SetEmberTxPower((int) configuration.GetValue(option)));
                            break;

                        case TransportConfigOption.DEVICE_TYPE:
                            _deviceType = (DeviceType) configuration.GetValue(option);
                            configuration.SetResult(option, ZigBeeStatus.SUCCESS);
                            break;

                        case TransportConfigOption.TRUST_CENTRE_LINK_KEY:
                            SetTcLinkKey((ZigBeeKey) configuration.GetValue(option));
                            configuration.SetResult(option, ZigBeeStatus.SUCCESS);
                            break;

                        case TransportConfigOption.TRUST_CENTRE_JOIN_MODE:
                            configuration.SetResult(option, SetTcJoinMode((TrustCentreJoinMode) configuration.GetValue(option)));
                            break;

                        case TransportConfigOption.SUPPORTED_INPUT_CLUSTERS:
                            configuration.SetResult(option, SetSupportedInputClusters((ICollection<int>)configuration.GetValue(option)));
                            break;

                        case TransportConfigOption.SUPPORTED_OUTPUT_CLUSTERS:
                            configuration.SetResult(option, SetSupportedOutputClusters((ICollection<int>)configuration.GetValue(option)));
                            break;

                        default:
                            configuration.SetResult(option, ZigBeeStatus.UNSUPPORTED);
                            Log.Debug("Unsupported configuration option \"{Option}\" in EZSP dongle", option);
                            break;
                    }
                } 
                catch (Exception ex) 
                {
                    Log.Debug(ex, "EZSP Dongle: error in UpdateTransportConfig");
                    configuration.SetResult(option, ZigBeeStatus.INVALID_ARGUMENTS);
                }
            }
        }

        private int[] CopyClusters(ICollection<int> clusterList) 
        {
            int[] clusters = new int[clusterList.Count];
            int cnt = 0;
            foreach (int value in clusterList) 
            {
                clusters[cnt++] = value;
            }
            return clusters;
        }

        private ZigBeeStatus SetSupportedInputClusters(ICollection<int> supportedClusters) 
        {
            if (_initialised)
                return ZigBeeStatus.INVALID_STATE;

            _inputClusters = CopyClusters(supportedClusters);
            return ZigBeeStatus.SUCCESS;
        }

        private ZigBeeStatus SetSupportedOutputClusters(ICollection<int> supportedClusters) 
        {
            if (_initialised)
                return ZigBeeStatus.INVALID_STATE;
 
            _outputClusters = CopyClusters(supportedClusters);
            return ZigBeeStatus.SUCCESS;
        }

        private ZigBeeStatus SetTcJoinMode(TrustCentreJoinMode joinMode) 
        {
            EzspDecisionId emberJoinMode;
            switch (joinMode) 
            {
                case TrustCentreJoinMode.TC_JOIN_INSECURE:
                    emberJoinMode = EzspDecisionId.EZSP_ALLOW_JOINS;
                    break;
                case TrustCentreJoinMode.TC_JOIN_SECURE:
                    emberJoinMode = EzspDecisionId.EZSP_ALLOW_PRECONFIGURED_KEY_JOINS;
                    break;
                case TrustCentreJoinMode.TC_JOIN_DENY:
                    emberJoinMode = EzspDecisionId.EZSP_DISALLOW_ALL_JOINS_AND_REJOINS;
                    break;
                default:
                    return ZigBeeStatus.INVALID_ARGUMENTS;
            }
            return (GetEmberNcp().SetPolicy(EzspPolicyId.EZSP_TRUST_CENTER_POLICY, emberJoinMode) == EzspStatus.EZSP_SUCCESS) ? ZigBeeStatus.SUCCESS : ZigBeeStatus.FAILURE;
        }

        private bool InitialiseEzspProtocol() 
        {
            if (_frameHandler != null) 
            {
                Log.Error("EZSP Dongle: Attempt to initialise Ember dongle when already initialised");
                return false;
            }
            if (!_serialPort.Open()) 
            {
                Log.Error("EZSP Dongle: Unable to open serial port");
                return false;
            }

            switch (_protocol) {
                case EmberSerialProtocol.ASH2:
                    _frameHandler = new AshFrameHandler(this);
                    break;
                    //Not implemented yet
                    /*
                case EmberSerialProtocol.SPI:
                    frameHandler = new SpiFrameHandler(this);
                    break;
                    */
                case EmberSerialProtocol.NONE:
                    return true;
                default:
                    Log.Error("EZSP Dongle: Unknown serial protocol {Protocol}", _protocol);
                    return false;
            }

            // Connect to the ASH handler and NCP
            _frameHandler.Start(_serialPort);

            // If possible, perform a hardware reset of the NCP
            /*
            if (resetProvider != null) {
                resetProvider.emberNcpReset(serialPort);
            }
            */

            _frameHandler.Connect();

            EmberNcp ncp = GetEmberNcp();

            // We MUST send the version command first.
            // Any failure to respond here indicates a failure of the ASH or EZSP layers to initialise
            EzspVersionResponse version = ncp.GetVersion(4);
            if (version == null) 
            {
                Log.Debug("EZSP Dongle: Version returned null. ASH/EZSP not initialised.");
                return false;
            }

            if (version.GetProtocolVersion() != EzspFrame.GetEzspVersion()) 
            {
                // The device supports a different version that we current have set
                if (!EzspFrame.SetEzspVersion(version.GetProtocolVersion())) 
                {
                    Log.Error("EZSP Dongle: NCP requires unsupported version of EZSP (required = V{RequiredVersion}, supported = V{SupportedVersion})",
                            version.GetProtocolVersion(), EzspFrame.GetEzspVersion());
                    return false;
                }

                version = ncp.GetVersion(EzspFrame.GetEzspVersion());
                Log.Debug(version.ToString());
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("EZSP Version=");
            builder.Append(version.GetProtocolVersion());
            builder.Append(", Stack Type=");
            builder.Append(version.GetStackType());
            builder.Append(", Stack Version=");
            for (int cnt = 3; cnt >= 0; cnt--) 
            {
                builder.Append((version.GetStackVersion() >> (cnt * 4)) & 0x0F);
                if (cnt != 0) 
                {
                    builder.Append('.');
                }
            }

            int bootloaderVersion = ncp.GetBootloaderVersion();
            builder.Append(", Bootloader Version=");
            if (bootloaderVersion == BOOTLOADER_INVALID_VERSION) 
            {
                builder.Append("NONE");
            } 
            else 
            {
                builder.Append((bootloaderVersion >> 12) & 0x0F);
                builder.Append('.');
                builder.Append((bootloaderVersion >> 8) & 0x0F);

                builder.Append(" build ");
                builder.Append(bootloaderVersion & 0xFF);
            }

            VersionString = builder.ToString();

            return true;
        }

        // Callback from the bootload handler when the transfer is completed/aborted/failed
        //Not implemented yet
        /*
        public void bootloadComplete() {
            bootloadHandler = null;
        }
        */

        private ZigBeeStatus SetConcentrator(ConcentratorConfig concentratorConfig) 
        {
            EzspSetConcentratorRequest concentratorRequest = new EzspSetConcentratorRequest();
            concentratorRequest.SetMinTime(concentratorConfig.RefreshMinimum);
            concentratorRequest.SetMaxTime(concentratorConfig.RefreshMaximum);
            concentratorRequest.SetMaxHops(concentratorConfig.MaxHops);
            concentratorRequest.SetRouteErrorThreshold(concentratorConfig.MaxFailures);
            concentratorRequest.SetDeliveryFailureThreshold(concentratorConfig.MaxFailures);
            switch (concentratorConfig.Type) 
            {
                case ConcentratorType.DISABLED:
                    concentratorRequest.SetEnable(false);
                    break;
                case ConcentratorType.HIGH_RAM:
                    concentratorRequest.SetConcentratorType(EmberConcentratorType.EMBER_HIGH_RAM_CONCENTRATOR);
                    concentratorRequest.SetEnable(true);
                    break;
                case ConcentratorType.LOW_RAM:
                    concentratorRequest.SetConcentratorType(EmberConcentratorType.EMBER_LOW_RAM_CONCENTRATOR);
                    concentratorRequest.SetEnable(true);
                    break;
                default:
                    break;
            }

            IEzspTransaction concentratorTransaction = _frameHandler.SendEzspTransaction(new EzspSingleResponseTransaction(concentratorRequest, typeof(EzspSetConcentratorResponse)));
            EzspSetConcentratorResponse concentratorResponse = (EzspSetConcentratorResponse) concentratorTransaction.GetResponse();
            Log.Debug(concentratorResponse.ToString());

            if (concentratorResponse.GetStatus() == EzspStatus.EZSP_SUCCESS)
                return ZigBeeStatus.SUCCESS;
            
            return ZigBeeStatus.FAILURE;
        }

        /**
         * Set the Ember Radio transmitter power
         *
         * @param txPower the power in dBm
         * @return {@link ZigBeeStatus}
         */
        private ZigBeeStatus SetEmberTxPower(int txPower) 
        {
            _networkParameters.SetRadioTxPower(txPower);

            EmberNcp ncp = GetEmberNcp();
            return (ncp.SetRadioPower(txPower) == EmberStatus.EMBER_SUCCESS) ? ZigBeeStatus.SUCCESS : ZigBeeStatus.BAD_RESPONSE;
        }

        /**
         * Get a map of statistics counters from the dongle
         *
         * @return map of counters
         */
        public Dictionary<string, long> GetCounters() 
        {
            if (_frameHandler != null)
                return _frameHandler.GetCounters();
        
            return new Dictionary<string, long>();
        }

        /**
         * Converts from an {@link EmberKeyStruct} to {@link ZigBeeKey}
         *
         * @param emberKey the {@link EmberKeyStruct} read from the NCP
         * @return the {@link ZigBeeKey} used by the framework. May be null if the key is invalid.
         */
        private ZigBeeKey EmberKeyToZigBeeKey(EmberKeyStruct emberKey) 
        {
            if (emberKey == null)
                return null;
            
            ZigBeeKey key = new ZigBeeKey(Array.ConvertAll(emberKey.GetKey().GetContents(), c => (byte)c));

            if (emberKey.GetBitmask().Contains(EmberKeyStructBitmask.EMBER_KEY_HAS_PARTNER_EUI64))
                key.address = emberKey.GetPartnerEUI64();
            
            if (emberKey.GetBitmask().Contains(EmberKeyStructBitmask.EMBER_KEY_HAS_SEQUENCE_NUMBER))
                key.SequenceNumber = (byte)emberKey.GetSequenceNumber();
            
            if (emberKey.GetBitmask().Contains(EmberKeyStructBitmask.EMBER_KEY_HAS_OUTGOING_FRAME_COUNTER))
                key.OutgoingFrameCounter = (byte) emberKey.GetOutgoingFrameCounter();

            if (emberKey.GetBitmask().Contains(EmberKeyStructBitmask.EMBER_KEY_HAS_INCOMING_FRAME_COUNTER))
                key.IncomingFrameCounter = (byte) emberKey.GetIncomingFrameCounter();

            return key;
        }

        /**
         * Gets the {@link EzspProtocolHandler}
         *
         * @return the {@link EzspProtocolHandler}
         */
        protected IEzspProtocolHandler GetProtocolHandler() 
        {
            return _frameHandler;
        }
    }
}
