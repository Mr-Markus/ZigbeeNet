// ReSharper disable InconsistentNaming
namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    // Source: https://github.com/zsmartsystems/com.zsmartsystems.zigbee/blob/master/com.zsmartsystems.zigbee.dongle.cc2531/src/main/java/com/zsmartsystems/zigbee/dongle/cc2531/network/packet/ZToolCMD.java
    public enum ZToolCMD : ushort
    {
        /// <summary>
        /// AF Data confirm.
        /// </summary>
        AF_DATA_CONFIRM = 0x4480,

        /// <summary>
         /// This command is used by tester to build and send a data request message
         /// </summary>
        AF_DATA_REQUEST = 0x2401,

        /// <summary>
         /// This command is used by tester to build and send a data request message
         /// </summary>
        AF_DATA_REQUEST_EXT = 0x2402,

        /// <summary>
         /// Response for AF_DATA_REQUEST
         /// </summary>
        AF_DATA_SRSP = 0x6401,

        /// <summary>
         /// Response for AF_DATA_REQUEST
         /// </summary>
        AF_DATA_SRSP_EXT = 0x6402,

        /// <summary>
         /// Incoming AF data.
         /// </summary>
        AF_INCOMING_MSG = 0x4481,

        /// <summary>
         /// This command enables the tester to register an application's endpoint
         /// description
         /// </summary>
        AF_REGISTER = 0x2400,

        /// <summary>
         /// Response for AF_REGISTER
         /// </summary>
        AF_REGISTER_SRSP = 0x6400,

        /// <summary>
         /// Use this message to send raw data to an application.
         /// </summary>
        APP_MSG = 0x2900,

        /// <summary>
         /// Status for APP_MSG
         /// </summary>
        APP_MSG_RESPONSE = 0x6900,

        /// <summary>
         /// Response for APP_MSG
         /// </summary>
        APP_MSG_RSP = 0x6980,

        /// <summary>
         /// This command is used by the tester to set the debug threshold level for a
         /// particular software component in the target.
         /// </summary>
        APP_USER_TEST = 0x2901,

        /// <summary>
         /// Response for APP_USER_TEST
         /// </summary>
        APP_USER_TEST_RESPONSE = 0x6901,

        /// <summary>
         /// This message is issued by the target APS to the tester to report the
         /// results of a request to transfer a data PDU from a local NHLE (Next
         /// Higher Layer Entity) to a single peer NHLE.
         /// </summary>
        APSDE_DATA_CONFIRMATION = 0x880,

        /// <summary>
         /// Response for APSME_BIND
         /// </summary>
        APSME_BIND_RESPONSE = 0x1804,

        /// <summary>
         /// This command is used by the tester to set the debug threshold level for a
         /// particular software component in the target.
         /// </summary>
        DEBUG_SET_DEBUG_THRESHOLD = 0x2800,

        /// <summary>
         /// Response for SYS_SET_DEBUG_THRESHOLD
         /// </summary>
        DEBUG_SET_DEBUG_THRESHOLD_RESPONSE = 0x6800,

        /// <summary>
         /// Debug message sent by device
         /// </summary>
        DEBUG_STRING = 0x4880,

        /// <summary>
         /// This message is issued by the target NWK to the tester to report the
         /// results of a request to transfer a data PDU from a local APS sub-layer
         /// entity to a single peer APS sub-layer entity.
         /// </summary>
        NLDE_DATA_CONFIRMATION = 0x4380,

        /// <summary>
         /// This message is issued by the target NWK to the tester to indicate the
         /// transfer of a data PDU from the NWK layer to the local APS sub-layer
         /// entity.
         /// </summary>
        NLDE_DATA_INDICATION = 0x4381,

        /// <summary>
         /// This command enables the tester to request the transfer of data from the
         /// local APS sub-layer to a peer APS sublayer entity.
         /// </summary>
        NLDE_DATA_REQUEST = 0x2301,

        /// <summary>
         /// Response for NLDE_DATA_REQUEST
         /// </summary>
        NLDE_DATA_RESPONSE = 0x6301,

        /// <summary>
         /// This function will initialize the nwk with the NWK_TASKID.
         /// </summary>
        NLDE_NWK_INIT = 0x4300,

        /// <summary>
         /// NLME direct join request used by tester
         /// </summary>
        NLME_DIRECTJOIN_REQUEST = 0x230b,

        /// <summary>
         /// Response for NLME_DIRECTJOIN_REQUEST
         /// </summary>
        NLME_DIRECTJOIN_RESPONSE = 0x630b,

        /// <summary>
         /// This command is used by tester to make a request (on behalf of the next
         /// higher layer) to read the value of an attribute from the NWK information
         /// base (NIB).
         /// </summary>
        NLME_GET_REQUEST = 0x2307,

        /// <summary>
         /// Response for NLME_GET_REQUEST
         /// </summary>
        NLME_GET_RESPONSE = 0x6307,

        /// <summary>
         /// This command is issued by the target NWK (to tester) to announce the next
         /// higher layer of the results of its request to join itself or another
         /// device to a network.
         /// </summary>
        NLME_JOIN_CONFIRMATION = 0x4383,

        /// <summary>
         /// This message is sent by the target to announce the next higher layer of a
         /// remote join request.
         /// </summary>
        NLME_JOIN_INDICATION = 0x4384,

        /// <summary>
         /// This command is used by tester to make a request (on behalf of the next
         /// higher layer) to join the device itself or another device to a network.
         /// </summary>
        NLME_JOIN_REQUEST = 0x2304,

        /// <summary>
         /// Response for NLME_JOIN_REQUEST
         /// </summary>
        NLME_JOIN_RESPONSE = 0x6304,

        /// <summary>
         /// This message is sent by the target to indicate to the next higher layer
         /// that the device itself or another device is leaving the network.
         /// </summary>
        NLME_LEAVE_CONFIRMATION = 0x4385,

        /// <summary>
         /// This message is sent by the target to indicate a remote leave request to
         /// the next higher layer of a coordinator
         /// </summary>
        NLME_LEAVE_INDICATION = 0x4386,

        /// <summary>
         /// This command is used by tester to make a request (on behalf of the next
         /// higher layer) that the device itself or another device leave the network.
         /// </summary>
        NLME_LEAVE_REQUEST = 0x2305,

        /// <summary>
         /// Response for NLME_LEAVE_REQUEST
         /// </summary>
        NLME_LEAVE_RESPONSE = 0x6305,

        /// <summary>
         /// This message is used by the target NWK to inform the tester of the result
         /// of a previous association request command
         /// </summary>
        NLME_NETWORK_FORMATION_CONFIRMATION = 0x4382,

        /// <summary>
         /// This command is used by tester to request (on behalf of the next higher
         /// layer) that the device be initiated as a coordinator.
         /// </summary>
        NLME_NETWORK_FORMATION_REQUEST = 0x2302,

        /// <summary>
         /// Response for NLME_NETWORK_FORMATION_REQUEST
         /// </summary>
        NLME_NETWORK_FORMATION_RESPONSE = 0x6302,

        /// <summary>
         /// This message is sent by the target to indicate network discovery
         /// confirmation
         /// </summary>
        NLME_NETWORKDISCOVERY_CONFIRMATION = 0x4389,

        /// <summary>
         /// NLME Network discovery request used by tester
         /// </summary>
        NLME_NETWORKDISCOVERY_REQUEST = 0x2309,

        /// <summary>
         /// Response for NLME_NETWORKDISCOVERY_REQUEST
         /// </summary>
        NLME_NETWORKDISCOVERY_RESPONSE = 0x6309,

        /// <summary>
         /// NLME orphan join request used by tester
         /// </summary>
        NLME_ORPHANJOIN_REQUEST = 0x230c,

        /// <summary>
         /// Response for NLME_ORPHANJOIN_REQUEST
         /// </summary>
        NLME_ORPHANJOIN_RESPONSE = 0x630c,

        /// <summary>
         /// This command is used by the tester to define how the next higher layer of
         /// a coordinator device would permit devices to join its network for a fixed
         /// period.
         /// </summary>
        NLME_PERMITJOINING_REQUEST = 0x2303,

        /// <summary>
         /// Response for NLME_PERMITJOINING_REQUEST
         /// </summary>
        NLME_PERMITJOINING_RESPONSE = 0x6303,

        /// <summary>
         /// This function reports the results of a polling attempt.
         /// </summary>
        NLME_POLL_CONFIRMATION = 0x4387,

        /// <summary>
         /// This command is used by tester to make a request (on behalf of the next
         /// higher layer) that the NWK layer perform a reset operation
         /// </summary>
        NLME_RESET_REQUEST = 0x2306,

        /// <summary>
         /// Response for NLME_RESET_REQUEST
         /// </summary>
        NLME_RESET_RESPONSE = 0x6306,

        /// <summary>
         /// NLME route discovery request used by tester
         /// </summary>
        NLME_ROUTEDISCOVERY_REQUEST = 0x230a,

        /// <summary>
         /// Response for NLME_ROUTEDISCOVERY_REQUEST
         /// </summary>
        NLME_ROUTEDISCOVERY_RESPONSE = 0x630a,

        /// <summary>
         /// This command is used by tester to make a request (on behalf of the next
         /// higher layer) to set the value of an attribute in the NWK information
         /// base (NIB).
         /// </summary>
        NLME_SET_REQUEST = 0x2308,

        /// <summary>
         /// Response for NLME_SET_REQUEST
         /// </summary>
        NLME_SET_RESPONSE = 0x6308,

        /// <summary>
         /// This message is sent by the target to the next higher layer of the
         /// results of its request to start a router
         /// </summary>
        NLME_STARTROUTER_CONFIRMATION = 0x438a,

        /// <summary>
         /// NLME Start router request used by tester
         /// </summary>
        NLME_STARTROUTER_REQUEST = 0x230d,

        /// <summary>
         /// Response for NLME_STARTROUTER_REQUEST
         /// </summary>
        NLME_STARTROUTER_RESPONSE = 0x630d,

        /// <summary>
         /// This message is sent by the target to indicate a sync request to the next
         /// higher layer of a coordinator
         /// </summary>
        NLME_SYNC_INDICATION = 0x4388,

        /// <summary>
         /// This function is used to inform the upper layers of the initiating device
         /// whether its request to associate was successful or unsuccessful.
         /// </summary>
        NWK_ASSOCIATE_CONFIRMATION = 0x2382,

        /// <summary>
         /// This function is used to indicate the reception of an association request
         /// command.
         /// </summary>
        NWK_ASSOCIATE_INDICATION = 0x2381,

        /// <summary>
         /// This function is used to send parameters contained within a beacon frame
         /// received by the MAC sublayer to the next higher layer. The function also
         /// sends a measure of the link quality and the time the beacon was received.
         ////// </summary>
        NWK_BEACON_NOTIFY_INDICATION = 0x2383,

        /// <summary>
         /// This function reports a comm status error
         /// </summary>
        NWK_COMM_STATUS_INDICATION = 0x238d,

        /// <summary>
         /// This function is used to send the results of a request to transfer a data
         /// SPDU (MSDU) from a local SSCS entity to a single peer SSCS entity, or
         /// multiple peer SSCS entities.
         /// </summary>
        NWK_DATA_CONFIRMATION = 0x2384,

        /// <summary>
         /// This function indicates the transfer of a data SPDU (MSDU) from the MAC
         /// sublayer to the local SSCS entity.
         /// </summary>
        NWK_DATA_INDICATION = 0x2385,

        /// <summary>
         /// This function is sent as the result of a disassociation request.
         /// </summary>
        NWK_DISASSOCIATE_CONFIRMATION = 0x2387,

        /// <summary>
         /// This function is used to indicate the reception of a disassociation
         /// notification command.
         /// </summary>
        NWK_DISASSOCIATE_INDICATION = 0x2386,

        /// <summary>
         /// This function allows the MLME to announce the next higher layer of an
         /// orphaned device
         /// </summary>
        NWK_ORPHAN_INDICATION = 0x238a,

        /// <summary>
         /// This function reports the results of a polling attempt.
         /// </summary>
        NWK_POLL_CONFIRMATION = 0x238b,

        /// <summary>
         /// This function reports the results of a purge attempt.
         /// </summary>
        NWK_PURGE_CONFIRMATION = 0x2390,

        /// <summary>
         /// This function reports the results of an RX enable attempt.
         /// </summary>
        NWK_RX_ENABLE_CONFIRMATION = 0x238f,

        /// <summary>
         /// This function reports the results of a Channel scan request.
         /// </summary>
        NWK_SCAN_CONFIRMATION = 0x238c,

        /// <summary>
         /// This function reports the success of the start request.
         /// </summary>
        NWK_START_CONFIRMATION = 0x238e,

        /// <summary>
         /// This function indicates the loss of synchronization of a network beacon
         /// </summary>
        NWK_SYNCHRONIZATION_LOSS_INDICATION = 0x2380,

        /// <summary>
         /// Stop timer.
         /// </summary>
        SYS_ADC_READ = 0x210d,

        /// <summary>
         /// Response for SYS_ADC_READ
         /// </summary>
        SYS_ADC_READ_SRSP = 0x610d,

        /// <summary>
         /// Configure the accessible GPIO pins
         /// </summary>
        SYS_GPIO = 0x210e,

        /// <summary>
         /// Configure the device RF test modes
         /// </summary>
        SYS_TEST_RF = 0x4140,

        /// <summary>
         /// Test the physical interface
         /// </summary>
        SYS_TEST_LOOPBACK = 0x2141,

        /// <summary>
         /// Response to SYS_GPIO
         /// </summary>
        SYS_GPIO_SRSP = 0x610e,

        /// <summary>
         /// Response to SYS_TEST_LOOPBACK
         /// </summary>
        SYS_TEST_LOOPBACK_SRSP = 0x6141,

        /// <summary>
         /// This command is used by the tester to read a single memory location in
         /// the target non-volatile memory. The command accepts an address value and
         /// returns the memory value present in the target at that address.
         /// </summary>
        SYS_OSAL_NV_READ = 0x2108,

        /// <summary>
         /// Response for SYS_OSAL_NV_READ
         /// </summary>
        SYS_OSAL_NV_READ_SRSP = 0x6108,

        /// <summary>
         /// This command is used by the tester to write to a particular location in
         /// non-volatile memory. The command accepts an address location and a memory
         /// value. The memory value is written to the address location in the target.
         /// </summary>
        SYS_OSAL_NV_WRITE = 0x2109,

        /// <summary>
         /// Response for SYS_OSAL_NV_WRITE
         /// </summary>
        SYS_OSAL_NV_WRITE_SRSP = 0x6109,

        /// <summary>
         /// Start timer.
         /// </summary>
        SYS_OSAL_START_TIMER = 0x210a,

        /// <summary>
         /// Response for SYS_OSAL_START_TIMER
         /// </summary>
        SYS_OSAL_START_TIMER_SRSP = 0x610a,

        /// <summary>
         /// Stop timer.
         /// </summary>
        SYS_OSAL_STOP_TIMER = 0x210b,

        /// <summary>
         /// Response for SYS_OSAL_STOP_TIMER
         /// </summary>
        SYS_OSAL_STOP_TIMER_SRSP = 0x610b,

        /// <summary>
         /// OSAL timer expired
         /// </summary>
        SYS_OSAL_TIMER_EXPIRED_IND = 0x4181,

        /// <summary>
         /// This command is used to check for a device
         /// </summary>
        SYS_PING = 0x2101,

        /// <summary>
         /// Response for SYS_PING
         /// </summary>
        SYS_PING_RESPONSE = 0x6101,

        /// <summary>
         /// Generate random number.
         /// </summary>
        SYS_RANDOM = 0x210c,

        /// <summary>
         /// Response for SYS_RANDOM
         /// </summary>
        SYS_RANDOM_SRSP = 0x610c,

        /// <summary>
         /// This command is sent by the tester to the target to reset it
         /// </summary>
        SYS_RESET = 0x4100,

        /// <summary>
         /// Indicates a device has reset.
         /// </summary>
        SYS_RESET_RESPONSE = 0x4180,

        /// <summary>
         /// RPC transport layer error.
         /// </summary>
        SYS_RPC_ERROR = 0x6000,

        /// <summary>
         /// Ask for the device's version string.
         /// </summary>
        SYS_VERSION = 0x2102,

        /// <summary>
         /// Response for SYS_VERSION
         /// </summary>
        SYS_VERSION_RESPONSE = 0x6102,

        /// <summary>
         /// This message is sent to the target in order to test the functions defined
         /// for individual applications (which internally use attributes and cluster
         /// IDs from various device descriptions).
         /// </summary>
        USERTEST_REQUEST = 0xb51,

        /// <summary>
         /// Response for USERTEST_REQUEST
         /// </summary>
        USERTEST_RESPONSE = 0x1b51,

        /// <summary>
         /// This command subscribes/unsubscribes to layer callbacks.
         /// </summary>
        UTIL_CALLBACK_SUBSCRIBE = 0x2706,

        /// <summary>
         /// Response for UTIL_CALLBACK_SUBSCRIBE
         /// </summary>
        UTIL_CALLBACK_SUBSCRIBE_RESPONSE = 0x6706,

        /// <summary>
         /// This command is used by the tester to read a single memory location in
         /// the target non-volatile memory. The command accepts an address value and
         /// returns the memory value present in the target at that address.
         /// </summary>
        UTIL_GET_DEVICE_INFO = 0x2700,

        /// <summary>
         /// Response for UTIL_GET_DEVICE_INFO
         /// </summary>
        UTIL_GET_DEVICE_INFO_RESPONSE = 0x6700,

        /// <summary>
         /// Use this message to get the NV information.
         /// </summary>
        UTIL_GET_NV_INFO = 0x2701,

        /// <summary>
         /// Response for UTIL_GET_NV_INFO
         /// </summary>
        UTIL_GET_NV_INFO_RESPONSE = 0x6701,

        /// <summary>
         /// Use this message to get board's time alive.
         /// </summary>
        UTIL_GET_TIME_ALIVE = 0x2709,

        /// <summary>
         /// Response for UTIL_GET_TIME_ALIVE
         /// </summary>
        UTIL_GET_TIME_ALIVE_RESPONSE = 0x6709,

        /// <summary>
         /// Sends a key event to the device registered application. The device
         /// register application means that the application registered for key events
         /// with OnBoard. Not all application support all keys, so you must know what
         /// keys the application supports.
         /// </summary>
        UTIL_KEY_EVENT = 0x2707,

        /// <summary>
         /// Response for UTIL_KEY_EVENT
         /// </summary>
        UTIL_KEY_EVENT_RESPONSE = 0x6707,

        /// <summary>
         /// Use this message to control LEDs on the board.
         /// </summary>
        UTIL_LED_CONTROL = 0x270a,

        /// <summary>
         /// Response for UTIL_LED_CONTROL
         /// </summary>
        UTIL_LED_CONTROL_RESPONSE = 0x670a,

        /// <summary>
         /// Use this message to set the channels.
         /// </summary>
        UTIL_SET_CHANNELS = 0x2703,

        /// <summary>
         /// Response for UTIL_SET_CHANNELS
         /// </summary>
        UTIL_SET_CHANNELS_RESPONSE = 0x6703,

        /// <summary>
         /// Use this message to set PANID.
         /// </summary>
        UTIL_SET_PANID = 0x2702,

        /// <summary>
         /// Response for UTIL_SET_PANID
         /// </summary>
        UTIL_SET_PANID_RESPONSE = 0x6702,

        /// <summary>
         /// Use this message to set the preconfig key.
         /// </summary>
        UTIL_SET_PRECONFIG_KEY = 0x2705,

        /// <summary>
         /// Response for UTIL_SET_PRECONFIG_KEY
         /// </summary>
        UTIL_SET_PRECONFIG_KEY_RESPONSE = 0x6705,

        /// <summary>
         /// Use this message to set the security level.
         /// </summary>
        UTIL_SET_SECURITY_LEVEL = 0x2704,

        /// <summary>
         /// Response for UTIL_SET_SECURITY_LEVEL
         /// </summary>
        UTIL_SET_SECURITY_LEVEL_RESPONSE = 0x6704,

        /// <summary>
         /// Puts the device into the Allow Bind mode (zb_AllowBind).
         /// </summary>
        ZB_ALLOW_BIND = 0x2602,

        /// <summary>
         /// Response for ZB_ALLOW_BIND
         /// </summary>
        ZB_ALLOW_BIND_CONFIRM = 0x4682,

        /// <summary>
         /// Response for ZB_ALLOW_BIND
         /// </summary>
        ZB_ALLOW_BIND_RSP = 0x6602,

        /// <summary>
         /// This command register the device descriptor
         /// </summary>
        ZB_APP_REGISTER_REQUEST = 0x260a,

        /// <summary>
         /// Response for ZB_APP_REGISTER_REQUEST
         /// </summary>
        ZB_APP_REGISTER_RSP = 0x660a,

        /// <summary>
         /// Response for ZB_BIND_DEVICE
         /// </summary>
        ZB_BIND_CONFIRM = 0x4681,

        /// <summary>
         /// Create or remove a binding entry (zb_BindDevice).
         /// </summary>
        ZB_BIND_DEVICE = 0x2601,

        /// <summary>
         /// Response for ZB_BIND_DEVICE
         /// </summary>
        ZB_BIND_DEVICE_RSP = 0x6601,

        /// <summary>
         /// (zb_FindDeviceConfirm)
         /// </summary>
        ZB_FIND_DEVICE_CONFIRM = 0x4685,

        /// <summary>
         /// Search for a device's short address given its IEEE address.
         /// </summary>
        ZB_FIND_DEVICE_REQUEST = 0x2607,

        /// <summary>
         /// Response for ZB_FIND_DEVICE_REQUEST
         /// </summary>
        ZB_FIND_DEVICE_REQUEST_RSP = 0x6607,

        /// <summary>
         /// Reads current device information.
         /// </summary>
        ZB_GET_DEVICE_INFO = 0x2606,

        /// <summary>
         /// Response for ZB_GET_DEVICE_INFO
         /// </summary>
        ZB_GET_DEVICE_INFO_RSP = 0x6606,

        /// <summary>
         /// Enables or disables the joining permissions on the destination device
         /// thus controlling the ability of new devices to join the network.
         /// </summary>
        ZB_PERMIT_JOINING_REQUEST = 0x2608,

        /// <summary>
         /// Response for ZB_PERMIT_JOINING_REQUEST
         /// </summary>
        ZB_PERMIT_JOINING_REQUEST_RSP = 0x6608,

        /// <summary>
         /// Reads a configuration property from nonvolatile memory
         /// (zb_ReadConfiguration).
         /// </summary>
        ZB_READ_CONFIGURATION = 0x2604,

        /// <summary>
         ///
         /// </summary>
        ZB_READ_CONFIGURATION_RSP = 0x6604,

        /// <summary>
         /// (zb_ReceiveDataIndication)
         /// </summary>
        ZB_RECEIVE_DATA_INDICATION = 0x4687,

        /// <summary>
         /// Response for ZB_SEND_DATA_REQUEST
         /// </summary>
        ZB_SEND_DATA_CONFIRM = 0x4683,

        /// <summary>
         /// Send a data packet to another device (zb_SendDataRequest).
         /// </summary>
        ZB_SEND_DATA_REQUEST = 0x2603,

        /// <summary>
         /// Response for ZB_SEND_DATA_REQUEST
         /// </summary>
        ZB_SEND_DATA_REQUEST_RSP = 0x6603,

        /// <summary>
         /// Response for ZB_START_REQUEST
         /// </summary>
        ZB_START_CONFIRM = 0x4680,

        /// <summary>
         /// Starts the ZigBee stack (zb_StartRequest).
         /// </summary>
        ZB_START_REQUEST = 0x2600,

        /// <summary>
         /// Response for ZB_START_REQUEST
         /// </summary>
        ZB_START_REQUEST_RSP = 0x6600,

        /// <summary>
         /// Reboot the device (zb_SystemReset)
         /// </summary>
        ZB_SYSTEM_RESET = 0x4609,

        /// <summary>
         /// Writes a configuration property to nonvolatile memory
         /// (zb_WriteConfiguration).
         /// </summary>
        ZB_WRITE_CONFIGURATION = 0x2605,

        /// <summary>
         /// Response for ZB_WRITE_CONFIGURATION
         /// </summary>
        ZB_WRITE_CONFIGURATION_RSP = 0x6605,

        /// <summary>
         /// This command is generated to request a list of active endpoint from the
         /// destination device.
         /// </summary>
        ZDO_ACTIVE_EP_REQ = 0x2505,

        /// <summary>
         /// Response for ZDO_ACTIVE_EP_REQ
         /// </summary>
        ZDO_ACTIVE_EP_REQ_SRSP = 0x6505,

        /// <summary>
         /// This callback message is in response to the ZDO Active Endpoint Request.
         /// </summary>
        ZDO_ACTIVE_EP_RSP = 0x4585,

        /// <summary>
         /// This function will issue a Match Description Request for the requested
         /// endpoint outputs. This message will generate a broadcast message.
         /// </summary>
        ZDO_AUTO_FIND_DESTINATION = 0x4541,

        /// <summary>
         /// This command is generated to request a Bind
         /// </summary>
        ZDO_BIND_REQ = 0x2521,

        /// <summary>
         /// Response for ZDO_BIND_REQ
         /// </summary>
        ZDO_BIND_REQ_SRSP = 0x6521,

        /// <summary>
         /// This callback message is in response to the ZDO Bind Request
         /// </summary>
        ZDO_BIND_RSP = 0x45a1,

        /// <summary>
         /// This command is generated to request for the destination device's complex
         /// descriptor
         /// </summary>
        ZDO_COMPLEX_DESC_REQ = 0x2507,

        /// <summary>
         /// Response for ZDO_COMPLEX_DESC_REQ
         /// </summary>
        ZDO_COMPLEX_DESC_REQ_SRSP = 0x6507,

        /// <summary>
         /// Response for ZDO_COMPLEX_DESC_REQ
         /// </summary>
        ZDO_COMPLEX_DESC_RSP = 0x4587,

        /// <summary>
         /// This command is generated to request an End Device Announce.
         /// </summary>
        ZDO_END_DEVICE_ANNCE = 0x250a,

        /// <summary>
         /// ZDO end device announce indication.
         /// </summary>
        ZDO_END_DEVICE_ANNCE_IND = 0x45c1,

        /// <summary>
         /// Response for ZDO_END_DEVICE_ANNCE
         /// </summary>
        ZDO_END_DEVICE_ANNCE_SRSP = 0x650a,

        /// <summary>
         /// This command is generated to request an End Device Bind with the
         /// destination device
         /// </summary>
        ZDO_END_DEVICE_BIND_REQ = 0x2520,

        /// <summary>
         /// Response for ZDO_END_DEVICE_BIND_REQ
         /// </summary>
        ZDO_END_DEVICE_BIND_REQ_SRSP = 0x6520,

        /// <summary>
         /// This callback message is in response to the ZDO End Device Bind Request
         /// </summary>
        ZDO_END_DEVICE_BIND_RSP = 0x45a0,

        /// <summary>
         /// This command will request a device's IEEE 64-bit address. You must
         /// subscribe to 'ZDO IEEE Address Response' to receive the data response to
         /// this message. The response message listed below only indicates whether or
         /// not the message was received properly.
         /// </summary>
        ZDO_IEEE_ADDR_REQ = 0x2501,

        /// <summary>
         /// Response for ZDO_IEEE_ADDR_REQ
         /// </summary>
        ZDO_IEEE_ADDR_REQ_SRSP = 0x6501,

        /// <summary>
         /// This callback message is in response to the ZDO IEEE Address Request.
         /// </summary>
        ZDO_IEEE_ADDR_RSP = 0x4581,

        /// <summary>
         /// This command is generated to request a list of active endpoint from the
         /// destination device
         /// </summary>
        ZDO_MATCH_DESC_REQ = 0x2506,

        /// <summary>
         /// Response for ZDO_MATCH_DESC_REQ
         /// </summary>
        ZDO_MATCH_DESC_REQ_SRSP = 0x6506,

        /// <summary>
         /// This callback message is in response to the ZDO Match Description Request
         /// </summary>
        ZDO_MATCH_DESC_RSP = 0x4586,

        /// <summary>
         /// This command is generated to request a Management Binding Table Request.
         /// </summary>
        ZDO_MGMT_BIND_REQ = 0x2533,

        /// <summary>
         /// Response for ZDO_MGMT_BIND_REQ
         /// </summary>
        ZDO_MGMT_BIND_REQ_SRSP = 0x6533,

        /// <summary>
         /// This callback message is in response to the ZDO Management Binding Table
         /// Request
         /// </summary>
        ZDO_MGMT_BIND_RSP = 0x45b3,

        /// <summary>
         /// This command is generated to request a Management Direct Join Request
         /// </summary>
        ZDO_MGMT_DIRECT_JOIN_REQ = 0x2535,

        /// <summary>
         /// Response for ZDO_MGMT_DIRECT_JOIN_REQ
         /// </summary>
        ZDO_MGMT_DIRECT_JOIN_REQ_SRSP = 0x6535,

        /// <summary>
         /// This callback message is in response to the ZDO Management Direct Join
         /// Request.
         /// </summary>
        ZDO_MGMT_DIRECT_JOIN_RSP = 0x45b5,

        /// <summary>
         /// This command is generated to request a Management Leave Request
         /// </summary>
        ZDO_MGMT_LEAVE_REQ = 0x2534,

        /// <summary>
         /// Response for ZDO_MGMT_LEAVE_REQ
         /// </summary>
        ZDO_MGMT_LEAVE_REQ_SRSP = 0x6534,

        /// <summary>
         /// This callback message is in response to the ZDO Management Leave Request.
         /// </summary>
        ZDO_MGMT_LEAVE_RSP = 0x45b4,

        /// <summary>
         /// This command is generated to request a Management LQI Request.
         /// </summary>
        ZDO_MGMT_LQI_REQ = 0x2531,

        /// <summary>
         /// Response for ZDO_MGMT_LQI_REQ
         /// </summary>
        ZDO_MGMT_LQI_REQ_SRSP = 0x6531,

        /// <summary>
         /// This callback message is in response to the ZDO Management LQI Request.
         /// </summary>
        ZDO_MGMT_LQI_RSP = 0x45b1,

        /// <summary>
         /// This command is generated to request a Management Network Discovery
         /// Request
         /// </summary>
        ZDO_MGMT_NWK_DISC_REQ = 0x2530,

        /// <summary>
         /// Response for ZDO_MGMT_NWK_DISC_REQ
         /// </summary>
        ZDO_MGMT_NWK_DISC_REQ_SRSP = 0x6530,

        /// <summary>
         /// This callback message is in response to the ZDO Management Network
         /// Discovery Request
         /// </summary>
        ZDO_MGMT_NWK_DISC_RSP = 0x45b0,

        /// <summary>
         /// This command is provided to allow updating of network configuration
         /// parameters or to request information from devices on network conditions
         /// in the local operating environment.
         /// </summary>
        ZDO_MGMT_NWK_UPDATE_REQ = 0x2537,

        /// <summary>
         /// Response for ZDO_MGMT_NWK_UPDATE_REQ
         /// </summary>
        ZDO_MGMT_NWK_UPDATE_REQ_SRSP = 0x6537,

        /// <summary>
         /// Response for ZDO_MGMT_PERMIT_JOIN_REQ
         /// </summary>
        ZDO_MGMT_PERMIT_JOIN_REQ_SRSP = 0x6536,

        /// <summary>
         /// This command is generated to request a Management Join Request
         /// </summary>
        ZDO_MGMT_PERMIT_JOIN_REQ = 0x2536,

        /// <summary>
         /// This callback message is in response to the ZDO Management Permit Join
         /// Request
         /// </summary>
        ZDO_MGMT_PERMIT_JOIN_RSP = 0x45b6,

        /// <summary>
         /// This command is generated to request a Management Routing Table Request.
         /// </summary>
        ZDO_MGMT_RTG_REQ = 0x2532,

        /// <summary>
         /// Response for ZDO_MGMT_RTG_REQ
         /// </summary>
        ZDO_MGMT_RTG_REQ_SRSP = 0x6532,

        /// <summary>
         /// This callback message is in response to the ZDO Management Routing Table
         /// Request.
         /// </summary>
        ZDO_MGMT_RTG_RSP = 0x45b2,

        /// <summary>
         /// This command registers for a ZDO callback.
         /// </summary>
        ZDO_MSG_CB_REGISTER = 0x253e,

        /// <summary>
         /// Response for ZDO_MSG_CB_REGISTER.
         /// </summary>
        ZDO_MSG_CB_REGISTER_SRSP = 0x653e,

        /// <summary>
         /// This callback message contains a ZDO cluster response.
         /// </summary>
        ZDO_MSG_CB_INCOMING = 0x45ff,

        /// <summary>
         /// This command is generated to inquire as to the Node Descriptor of the
         /// destination device
         /// </summary>
        ZDO_NODE_DESC_REQ = 0x2502,

        /// <summary>
         /// Response for ZDO_NODE_DESC_REQ
         /// </summary>
        ZDO_NODE_DESC_REQ_SRSP = 0x6502,

        /// <summary>
         /// This callback message is in response to the ZDO Node Descriptor Request.
         /// </summary>
        ZDO_NODE_DESC_RSP = 0x4582,

        /// <summary>
         /// This message will request the device to send a "Network Address Request".
         /// This message sends a broadcast message looking for a 16 bit address with
         /// a 64 bit address as bait. You must subscribe to
         /// "ZDO Network Address Response" to receive the response to this message.
         /// The response message listed below only indicates whether or not the
         /// message was received properly.
         /// </summary>
        ZDO_NWK_ADDR_REQ = 0x2500,

        /// <summary>
         /// Response for ZDO_NWK_ADDR_REQ
         /// </summary>
        ZDO_NWK_ADDR_REQ_SRSP = 0x6500,

        /// <summary>
         /// This callback message is in response to the ZDO Network Address Request.
         /// </summary>
        ZDO_NWK_ADDR_RSP = 0x4580,

        /// <summary>
         /// This command is generated to inquire as to the Power Descriptor of the
         /// destination
         /// </summary>
        ZDO_POWER_DESC_REQ = 0x2503,

        /// <summary>
         /// Response for ZDO_POWER_DESC_REQ
         /// </summary>
        ZDO_POWER_DESC_REQ_SRSP = 0x6503,

        /// <summary>
         /// This callback message is in response to the ZDO Power Descriptor Request.
         /// </summary>
        ZDO_POWER_DESC_RSP = 0x4583,

        /// <summary>
         /// The command is used for local device to discover the location of a
         /// particular system server or servers as indicated by the ServerMask
         /// parameter. The destination addressing on this request is 'broadcast to
         /// all RxOnWhenIdle devices'.
         /// </summary>
        ZDO_SERVER_DISC_REQ = 0x250c,

        /// <summary>
         /// Response for ZDO_SERVER_DISC_REQ
         /// </summary>
        ZDO_SERVER_DISC_REQ_SRSP = 0x650c,

        /// <summary>
         /// This callback message is
         /// </summary>
        ZDO_SERVER_DISC_RSP = 0x458a,

        /// <summary>
         /// This command is generated to inquire as to the Simple Descriptor of the
         /// destination device's Endpoint
         /// </summary>
        ZDO_SIMPLE_DESC_REQ = 0x2504,

        /// <summary>
         /// Response for ZDO_SIMPLEDESCRIPTOR_REQUEST
         /// </summary>
        ZDO_SIMPLE_DESC_REQ_SRSP = 0x6504,

        /// <summary>
         /// This callback message is in response to the ZDO Simple Descriptor
         /// Request.
         /// </summary>
        ZDO_SIMPLE_DESC_RSP = 0x4584,

        /// <summary>
         /// In the case where compiler flag HOLD_AUTO_START is defined by default,
         /// device will start from HOLD state. Issuing this command will trigger the
         /// device to leave HOLD state to form or join a network.
         /// </summary>
        ZDO_STARTUP_FROM_APP = 0x2540,

        /// <summary>
         /// Response for ZDO_STARTUP_FROM_APP
         /// </summary>
        ZDO_STARTUP_FROM_APP_SRSP = 0x6540,

        /// <summary>
         /// ZDO state change indication.
         /// </summary>
        ZDO_STATE_CHANGE_IND = 0x45c0,

        /// <summary>
         /// This message is the default message for error status.
         /// </summary>
        ZDO_STATUS_ERROR_RSP = 0x45c3,

        /// <summary>
         /// ZDO Trust Center end device announce indication.
         /// </summary>
        ZDO_TC_DEVICE_IND = 0x45ca,

        /// <summary>
         /// This command is generated to request an UnBind
         /// </summary>
        ZDO_UNBIND_REQ = 0x2522,

        /// <summary>
         /// Response for ZDO_UNBIND_REQ
         /// </summary>
        ZDO_UNBIND_REQ_SRSP = 0x6522,

        /// <summary>
         /// This callback message is in response to the ZDO UnBind Request
         /// </summary>
        ZDO_UNBIND_RSP = 0x45a2,

        /// <summary>
         /// This callback message is in response to the ZDO User Descriptor Set
         /// Request.
         /// </summary>
        ZDO_USER_DESC_CONF = 0x4589,

        /// <summary>
         /// This command is generated to request for the destination device's user
         /// descriptor
         /// </summary>
        ZDO_USER_DESC_REQ = 0x2508,

        /// <summary>
         /// Response for ZDO_USER_DESC_REQ
         /// </summary>
        ZDO_USER_DESC_REQ_SRSP = 0x6508,

        /// <summary>
         /// This callback message is in response to the ZDO User Description Request.
         /// </summary>
        ZDO_USER_DESC_RSP = 0x4588,

        /// <summary>
         /// This command is generated to request a User Descriptor Set Request
         /// </summary>
        ZDO_USER_DESC_SET = 0x250b,

        /// <summary>
         /// Response for ZDO_USER_DESC_SET
         /// </summary>
        ZDO_USER_DESC_SET_SRSP = 0x650b,

        /// <summary>
         /// This sends an associate confirm command
         /// </summary>
        ZMAC_ASSOCIATE_CNF = 0x4282,

        /// <summary>
         /// This command is used to send (on behalf of the next higher layer) an
         /// association indication message
         /// </summary>
        ZMAC_ASSOCIATE_IND = 0x4281,

        /// <summary>
         /// This command is used to request (on behalf of the next higher layer) an
         /// association with a coordinator
         /// </summary>
        ZMAC_ASSOCIATE_REQUEST = 0x2206,

        /// <summary>
         /// Response for ZMAC_ASSOCIATE_REQUEST
         /// </summary>
        ZMAC_ASSOCIATE_RESPONSE = 0x6206,

        /// <summary>
         /// Beacon Notify Indication
         /// </summary>
        ZMAC_BEACON_NOTIFY_IND = 0x4283,

        /// <summary>
         /// Communication status indication
         /// </summary>
        ZMAC_COMM_STATUS_IND = 0x428d,

        /// <summary>
         /// Data Request Confirmation
         /// </summary>
        ZMAC_DATA_CNF = 0x4284,

        /// <summary>
         /// Data Request Confirmation
         /// </summary>
        ZMAC_DATA_IND = 0x4285,

        /// <summary>
         /// This command is used to send (on behalf of the next higher layer) MAC
         /// Data Frame packet.
         /// </summary>
        ZMAC_DATA_REQUEST = 0x2205,

        /// <summary>
         /// Response for ZMAC_DATA_REQUEST
         /// </summary>
        ZMAC_DATA_RESPONSE = 0x6205,

        /// <summary>
         /// Disassociate Indication
         /// </summary>
        ZMAC_DISASSOCIATE_CNF = 0x4287,

        /// <summary>
         /// Disassociate Indication
         /// </summary>
        ZMAC_DISASSOCIATE_IND = 0x4286,

        /// <summary>
         /// This command is used to request (on behalf of the next higher layer) a
         /// disassociation of the device from the coordinator.
         /// </summary>
        ZMAC_DISASSOCIATE_REQUEST = 0x2207,

        /// <summary>
         /// Response for ZMAC_DISASSOCIATE_REQUEST
         /// </summary>
        ZMAC_DISASSOCIATE_RESPONSE = 0x6207,

        /// <summary>
         /// This command is used to read (on behalf of the next higher layer) a MAC
         /// PIB attribute.
         /// </summary>
        ZMAC_GET_REQUEST = 0x2208,

        /// <summary>
         /// Response for ZMAC_GET_REQUEST
         /// </summary>
        ZMAC_GET_RESPONSE = 0x6208,

        /// <summary>
         /// This command is used to initialize the ZMAC on the current device (on
         /// behalf of the next higher layer).
         /// </summary>
        ZMAC_INIT_REQUEST = 0x2202,

        /// <summary>
         /// >Response for ZMAC_INIT_REQUEST
         /// </summary>
        ZMAC_INIT_RESPONSE = 0x6202,

        /// <summary>
         /// Orphan Indication
         /// </summary>
        ZMAC_ORPHAN_IND = 0x428a,

        /// <summary>
         /// Mac Poll Confirmation
         /// </summary>
        ZMAC_POLL_CNF = 0x428b,

        /// <summary>
         /// This command is used to send a MAC data request poll
         /// </summary>
        ZMAC_POLL_REQUEST = 0x220d,

        /// <summary>
         /// Response for ZMAC_POLL_REQUEST
         /// </summary>
        ZMAC_POLL_RESPONSE = 0x620d,

        /// <summary>
         /// Mac RX enable Confirmation
         /// </summary>
        ZMAC_PURGE_CNF = 0x4290,

        /// <summary>
         /// This command is used to send a request to the device to purge a data
         /// frame
         /// </summary>
        ZMAC_PURGE_REQUEST = 0x220e,

        /// <summary>
         /// Response for ZMAC_PURGE_REQUEST
         /// </summary>
        ZMAC_PURGE_RESPONSE = 0x620e,

        /// <summary>
         /// This command is used to send a MAC Reset command to reset MAC state
         /// machine
         /// </summary>
        ZMAC_RESET_REQUEST = 0x2201,

        /// <summary>
         /// Response for ZMAC_RESET_REQUEST
         /// </summary>
        ZMAC_RESET_RESPONSE = 0x6201,

        /// <summary>
         /// Mac RX enable Confirmation
         /// </summary>
        ZMAC_RX_ENABLE_CNF = 0x428f,

        /// <summary>
         /// This command contains timing information that tells the device when to
         /// enable or disable its receiver, in order to schedule a data transfer
         /// between itself and another device. The information is sent from the upper
         /// layers directly to the MAC sublayer.
         /// </summary>
        ZMAC_RX_ENABLE_REQUEST = 0x220b,

        /// <summary>
         /// Response for ZMAC_RX_ENABLE_REQUEST
         /// </summary>
        ZMAC_RX_ENABLE_RESPONSE = 0x620b,

        /// <summary>
         /// Scan Confirmation
         /// </summary>
        ZMAC_SCAN_CNF = 0x428c,

        /// <summary>
         /// This command is used to send a request to the device to perform a network
         /// scan.
         /// </summary>
        ZMAC_SCAN_REQUEST = 0x220c,

        /// <summary>
         /// Response for ZMAC_SCAN_REQUEST
         /// </summary>
        ZMAC_SCAN_RESPONSE = 0x620c,

        /// <summary>
         /// This command is used to request the device to write a MAC PIB value.
         /// </summary>
        ZMAC_SET_REQUEST = 0x2209,

        /// <summary>
         /// Response for ZMAC_SET_REQUEST
         /// </summary>
        ZMAC_SET_RESPONSE = 0x6209,

        /// <summary>
         /// This command is used to send a request to the device to set Rx gain
         /// </summary>
        ZMAC_SET_RX_GAIN_REQUEST = 0x220f,

        /// <summary>
         /// Response for ZMAC_SET_RX_GAIN_REQUEST
         /// </summary>
        ZMAC_SET_RX_GAIN_RESPONSE = 0x620f,

        /// <summary>
         /// Mac Start Confirmation
         /// </summary>
        ZMAC_START_CNF = 0x428e,

        /// <summary>
         /// This command is used to request the MAC to transmit beacons and become a
         /// coordinator
         /// </summary>
        ZMAC_START_REQUEST = 0x2203,

        /// <summary>
         /// Response for ZMAC_START_REQUEST
         /// </summary>
        ZMAC_START_RESPONSE = 0x6203,

        /// <summary>
         /// Indication for sync loss
         /// </summary>
        ZMAC_SYNC_LOSS_IND = 0x4280,

        /// <summary>
         /// This command is used to request synchronization to the current network
         /// beacon
         /// </summary>
        ZMAC_SYNCHRONIZE_REQUEST = 0x2204,

        /// <summary>
         /// Response for ZMAC_SYNCHRONIZE_REQUEST
         /// </summary>
        ZMAC_SYNCHRONIZE_RESPONSE = 0x6204,

    }
}