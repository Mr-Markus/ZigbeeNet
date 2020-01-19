
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.GreenPower;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Green Power cluster implementation (Cluster ID 0x0021).
    ///
    /// The Green Power cluster defines the format of the commands exchanged when handling
    /// GPDs.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclGreenPowerCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0021;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Green Power";

        // Attribute constants

        /// <summary>
        /// Maximum number of Proxy Table entries this node can hold. Any proxy type shall
        /// support at least five Proxy Table entries. The recommended number of the Proxy
        /// Table entries for a Basic Proxy is twenty.
        /// </summary>
        public const ushort ATTR_GPPMAXPROXYTABLEENTRIES = 0x0010;

        /// <summary>
        /// The Proxy Table attribute contains the information on GPDs active in the system and
        /// the corresponding sinks. Proxy Table is a read-only attribute. Generic ZCL
        /// commands cannot be used to create/modify or remove Proxy Table entries. If
        /// required, e.g. for CT-based commissioning, the GP Pairing command of the Green
        /// Power cluster can be used for that purpose.
        /// The Proxy Table shall be persistently stored across restarts, OTA upgrades and
        /// power cycles.
        /// </summary>
        public const ushort ATTR_PROXYTABLE = 0x0011;

        /// <summary>
        /// This attribute defines the maximum number of retransmissions in case a GP
        /// Notification Response command is not received from a particular sink for full
        /// unicast GP Notification command.
        /// </summary>
        public const ushort ATTR_GPPNOTIFICATIONRETRYNUMBER = 0x0012;

        /// <summary>
        /// This attribute defines the time to wait for GP Notification Response command after
        /// sending full unicast GP Notification command.
        /// </summary>
        public const ushort ATTR_GPPNOTIFICATIONRETRYTIMER = 0x0013;

        /// <summary>
        /// This attribute defines the maximum value the Search Counter can take, before it
        /// rolls over.
        /// </summary>
        public const ushort ATTR_GPPMAXSEARCHCOUNTER = 0x0014;

        /// <summary>
        /// The gppBlockedGPDID attribute contains the information on GPDs active in the
        /// vicinity of the network node, but not belonging to the system.
        /// </summary>
        public const ushort ATTR_GPPBLOCKEDGPDID = 0x0015;

        /// <summary>
        /// The gppFunctionality attribute indicates support of the GP functionality by this
        /// device. Any 1-bit sub-field set to 0b1 indicates that this functionality is
        /// supported; set to 0b0 indicates that this functionality is not implemented. The
        /// reserved sub-fields and sub-fields for any non-applicable functionality shall
        /// also be set to 0b0.
        /// </summary>
        public const ushort ATTR_GPPFUNCTIONALITY = 0x0016;

        /// <summary>
        /// The gppActiveFunctionality attribute indicates which GP functionality
        /// supported by this device is currently enabled. Any 1-bit sub-field set to 0b1
        /// indicates that this functionality is supported and enabled; set to 0b0 indicates
        /// that this functionality is disabled or not implemented.
        /// </summary>
        public const ushort ATTR_GPPACTIVEFUNCTIONALITY = 0x0017;

        /// <summary>
        /// The gpSharedSecurityKeyType attribute stores the key type of the shared security
        /// key. The gpSharedSecurityKeyType attribute can take the following values: 0b000
        /// (no key), 0b001 (NWK key), 0b010 (GP group key), 0b011 (NWK-key derived GP group
        /// key) and 0b111 (Derived individual GPD key).
        /// </summary>
        public const ushort ATTR_GPCLIENTSHAREDSECURITYKEYTYPE = 0x0020;

        /// <summary>
        /// The gpSharedSecurityKey attribute stores the shared security key of the key type
        /// as indicated in the gpSecurityKeyType attribute. It can take any value.
        /// </summary>
        public const ushort ATTR_GPCLIENTSHAREDSECURITYKEY = 0x0021;

        /// <summary>
        /// The gpLinkKey attribute stores the Link Key, used to encrypt the key transmitted in
        /// the Commissioning GPDF and Commissioning Reply GPDF. By default, it has the value
        /// of the default ZigBee Trust Center Link Key (TC-LK), 'ZigbeeAlliance09'. Then,
        /// storing of the gpLinkKey may be omitted.
        /// </summary>
        public const ushort ATTR_GPCLIENTLINKKEY = 0x0022;

        /// <summary>
        /// The gpsMaxSinkTableEntries attribute is one octet in length, and it contains the
        /// maximum number of Sink Table entries that can be stored by this sink.
        /// </summary>
        public const ushort ATTR_GPSMAXSINKTABLEENTRIES = 0x0000;

        /// <summary>
        /// The Sink Table attribute contains the pairings configured for this sink. Sink
        /// Table is a read-only attribute. Generic ZCL commands cannot be used to
        /// create/modify or remove Sink Table entries. If required, e.g. for CT-based
        /// commissioning, the GP Pairing Configuration command of the Green Power cluster
        /// can be used for that purpose.
        /// </summary>
        public const ushort ATTR_SINKTABLE = 0x0001;

        /// <summary>
        /// The gpsCommunicationMode attribute contains the communication mode required by
        /// this sink.
        /// </summary>
        public const ushort ATTR_GPSCOMMUNICATIONMODE = 0x0002;

        /// <summary>
        /// The gpsCommissioningExitMode attribute contains the information on
        /// commissioning mode exit requirements of this sink.
        /// </summary>
        public const ushort ATTR_GPSCOMMISSIONINGEXITMODE = 0x0003;

        /// <summary>
        /// The gpsCommissioningWindow attribute contains the information on the time, in
        /// seconds, during which this sink accepts pairing changes (additions/removals).
        /// The default value is 180 seconds.
        /// </summary>
        public const ushort ATTR_GPSCOMMISSIONINGWINDOW = 0x0004;

        /// <summary>
        /// The gpsSecurityLevel attribute contains the minimum security level this sink
        /// requires the paired GPDs to support.
        /// </summary>
        public const ushort ATTR_GPSSECURITYLEVEL = 0x0005;

        /// <summary>
        /// The gpsFunctionality attribute indicates support of the GP functionality by this
        /// device. Any 1-bit subfield set to 0b1 indicates that this functionality is
        /// supported; set to 0b0 indicates that this functionality is not implemented.
        /// </summary>
        public const ushort ATTR_GPSFUNCTIONALITY = 0x0006;

        /// <summary>
        /// The gpsActiveFunctionality attribute indicates which GP functionality
        /// supported by this device is currently enabled. Any 1-bit sub-field set to 0b1
        /// indicates that this functionality is supported and enabled; set to 0b0 indicates
        /// that this functionality is disabled or not implemented.
        /// </summary>
        public const ushort ATTR_GPSACTIVEFUNCTIONALITY = 0x0007;

        /// <summary>
        /// The gpSharedSecurityKeyType attribute stores the key type of the shared security
        /// key. The gpSharedSecurityKeyType attribute can take the following values: 0b000
        /// (no key), 0b001 (NWK key), 0b010 (GP group key), 0b011 (NWK-key derived GP group
        /// key) and 0b111 (Derived individual GPD key).
        /// </summary>
        public const ushort ATTR_GPSERVERSHAREDSECURITYKEYTYPE = 0x0020;

        /// <summary>
        /// The gpSharedSecurityKey attribute stores the shared security key of the key type
        /// as indicated in the gpSecurityKeyType attribute. It can take any value.
        /// </summary>
        public const ushort ATTR_GPSERVERSHAREDSECURITYKEY = 0x0021;

        /// <summary>
        /// The gpLinkKey attribute stores the Link Key, used to encrypt the key transmitted in
        /// the Commissioning GPDF and Commissioning Reply GPDF. By default, it has the value
        /// of the default ZigBee Trust Center Link Key (TC-LK), 'ZigbeeAlliance09'. Then,
        /// storing of the gpLinkKey may be omitted.
        /// </summary>
        public const ushort ATTR_GPSERVERLINKKEY = 0x0022;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(11);

            attributeMap.Add(ATTR_GPPMAXPROXYTABLEENTRIES, new ZclAttribute(this, ATTR_GPPMAXPROXYTABLEENTRIES, "Gpp Max Proxy Table Entries", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PROXYTABLE, new ZclAttribute(this, ATTR_PROXYTABLE, "Proxy Table", ZclDataType.Get(DataType.LONG_OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_GPPNOTIFICATIONRETRYNUMBER, new ZclAttribute(this, ATTR_GPPNOTIFICATIONRETRYNUMBER, "Gpp Notification Retry Number", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_GPPNOTIFICATIONRETRYTIMER, new ZclAttribute(this, ATTR_GPPNOTIFICATIONRETRYTIMER, "Gpp Notification Retry Timer", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_GPPMAXSEARCHCOUNTER, new ZclAttribute(this, ATTR_GPPMAXSEARCHCOUNTER, "Gpp Max Search Counter", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_GPPBLOCKEDGPDID, new ZclAttribute(this, ATTR_GPPBLOCKEDGPDID, "Gpp Blocked Gpd ID", ZclDataType.Get(DataType.LONG_OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_GPPFUNCTIONALITY, new ZclAttribute(this, ATTR_GPPFUNCTIONALITY, "Gpp Functionality", ZclDataType.Get(DataType.BITMAP_24_BIT), true, true, false, false));
            attributeMap.Add(ATTR_GPPACTIVEFUNCTIONALITY, new ZclAttribute(this, ATTR_GPPACTIVEFUNCTIONALITY, "Gpp Active Functionality", ZclDataType.Get(DataType.BITMAP_24_BIT), true, true, false, false));
            attributeMap.Add(ATTR_GPCLIENTSHAREDSECURITYKEYTYPE, new ZclAttribute(this, ATTR_GPCLIENTSHAREDSECURITYKEYTYPE, "Gp Client Shared Security Key Type", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, true, true));
            attributeMap.Add(ATTR_GPCLIENTSHAREDSECURITYKEY, new ZclAttribute(this, ATTR_GPCLIENTSHAREDSECURITYKEY, "Gp Client Shared Security Key", ZclDataType.Get(DataType.SECURITY_KEY), false, true, true, true));
            attributeMap.Add(ATTR_GPCLIENTLINKKEY, new ZclAttribute(this, ATTR_GPCLIENTLINKKEY, "Gp Client Link Key", ZclDataType.Get(DataType.SECURITY_KEY), false, true, true, true));

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(11);

            attributeMap.Add(ATTR_GPSMAXSINKTABLEENTRIES, new ZclAttribute(this, ATTR_GPSMAXSINKTABLEENTRIES, "Gps Max Sink Table Entries", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_SINKTABLE, new ZclAttribute(this, ATTR_SINKTABLE, "Sink Table", ZclDataType.Get(DataType.LONG_OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_GPSCOMMUNICATIONMODE, new ZclAttribute(this, ATTR_GPSCOMMUNICATIONMODE, "Gps Communication Mode", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, true, true));
            attributeMap.Add(ATTR_GPSCOMMISSIONINGEXITMODE, new ZclAttribute(this, ATTR_GPSCOMMISSIONINGEXITMODE, "Gps Commissioning Exit Mode", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, true, true));
            attributeMap.Add(ATTR_GPSCOMMISSIONINGWINDOW, new ZclAttribute(this, ATTR_GPSCOMMISSIONINGWINDOW, "Gps Commissioning Window", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_GPSSECURITYLEVEL, new ZclAttribute(this, ATTR_GPSSECURITYLEVEL, "Gps Security Level", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, true, true));
            attributeMap.Add(ATTR_GPSFUNCTIONALITY, new ZclAttribute(this, ATTR_GPSFUNCTIONALITY, "Gps Functionality", ZclDataType.Get(DataType.BITMAP_24_BIT), true, true, false, false));
            attributeMap.Add(ATTR_GPSACTIVEFUNCTIONALITY, new ZclAttribute(this, ATTR_GPSACTIVEFUNCTIONALITY, "Gps Active Functionality", ZclDataType.Get(DataType.BITMAP_24_BIT), true, true, false, false));
            attributeMap.Add(ATTR_GPSERVERSHAREDSECURITYKEYTYPE, new ZclAttribute(this, ATTR_GPSERVERSHAREDSECURITYKEYTYPE, "Gp Server Shared Security Key Type", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, true, true));
            attributeMap.Add(ATTR_GPSERVERSHAREDSECURITYKEY, new ZclAttribute(this, ATTR_GPSERVERSHAREDSECURITYKEY, "Gp server Shared Security Key", ZclDataType.Get(DataType.SECURITY_KEY), false, true, true, true));
            attributeMap.Add(ATTR_GPSERVERLINKKEY, new ZclAttribute(this, ATTR_GPSERVERLINKKEY, "Gp Server Link Key", ZclDataType.Get(DataType.SECURITY_KEY), false, true, true, true));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(6);

            commandMap.Add(0x0000, () => new GpNotificationResponse());
            commandMap.Add(0x0001, () => new GpPairing());
            commandMap.Add(0x0002, () => new GpProxyCommissioningMode());
            commandMap.Add(0x0006, () => new GpResponse());
            commandMap.Add(0x000A, () => new GpSinkTableResponse());
            commandMap.Add(0x000B, () => new GpProxyTableRequest());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(10);

            commandMap.Add(0x0000, () => new GpNotification());
            commandMap.Add(0x0001, () => new GpPairingSearch());
            commandMap.Add(0x0003, () => new GpTunnelingStop());
            commandMap.Add(0x0004, () => new GpCommissioningNotification());
            commandMap.Add(0x0005, () => new GpSinkCommissioningMode());
            commandMap.Add(0x0007, () => new GpTranslationTableUpdate());
            commandMap.Add(0x0008, () => new GpTranslationTableRequest());
            commandMap.Add(0x0009, () => new GpPairingConfiguration());
            commandMap.Add(0x000A, () => new GpSinkTableRequest());
            commandMap.Add(0x000B, () => new GpProxyTableResponse());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Green Power cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclGreenPowerCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Gp Notification
        ///
        /// The GP Notification command is generated by the proxy (or a sink capable of Sink
        /// Table-based forwarding) to forward the received Data GPDF to the paired sinks.
        /// On receipt of the GP Notification command, a device is informed about a GPDF
        /// forwarded by a proxy. Also the device which received this frame is informed of
        /// bidirectional communication capability of the sender.
        ///
        /// <param name="options" <see cref="ushort"> Options</ param >
        /// <param name="gpdSrcId" <see cref="uint"> Gpd Src ID</ param >
        /// <param name="gpdIeee" <see cref="IeeeAddress"> Gpd IEEE</ param >
        /// <param name="gpdEndpoint" <see cref="byte"> Gpd Endpoint</ param >
        /// <param name="gpdSecurityFrameCounter" <see cref="uint"> Gpd Security Frame Counter</ param >
        /// <param name="gpdCommandId" <see cref="byte"> Gpd Command ID</ param >
        /// <param name="gpdCommandPayload" <see cref="ByteArray"> Gpd Command Payload</ param >
        /// <param name="gppShortAddress" <see cref="ushort"> Gpp Short Address</ param >
        /// <param name="gppDistance" <see cref="byte"> Gpp Distance</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpNotification(ushort options, uint gpdSrcId, IeeeAddress gpdIeee, byte gpdEndpoint, uint gpdSecurityFrameCounter, byte gpdCommandId, ByteArray gpdCommandPayload, ushort gppShortAddress, byte gppDistance)
        {
            GpNotification command = new GpNotification();

            // Set the fields
            command.Options = options;
            command.GpdSrcId = gpdSrcId;
            command.GpdIeee = gpdIeee;
            command.GpdEndpoint = gpdEndpoint;
            command.GpdSecurityFrameCounter = gpdSecurityFrameCounter;
            command.GpdCommandId = gpdCommandId;
            command.GpdCommandPayload = gpdCommandPayload;
            command.GppShortAddress = gppShortAddress;
            command.GppDistance = gppDistance;

            return Send(command);
        }

        /// <summary>
        /// The Gp Pairing Search
        ///
        /// The GP Pairing Search command is generated when the proxy needs to discover pairing
        /// information for a particular GPD.
        /// On receipt of this command, the device is informed about a proxy requesting pairing
        /// information on particular GPD.
        ///
        /// <param name="options" <see cref="ushort"> Options</ param >
        /// <param name="gpdSrcId" <see cref="uint"> Gpd Src ID</ param >
        /// <param name="gpdIeee" <see cref="IeeeAddress"> Gpd IEEE</ param >
        /// <param name="endpoint" <see cref="byte"> Endpoint</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpPairingSearch(ushort options, uint gpdSrcId, IeeeAddress gpdIeee, byte endpoint)
        {
            GpPairingSearch command = new GpPairingSearch();

            // Set the fields
            command.Options = options;
            command.GpdSrcId = gpdSrcId;
            command.GpdIeee = gpdIeee;
            command.Endpoint = endpoint;

            return Send(command);
        }

        /// <summary>
        /// The Gp Tunneling Stop
        ///
        /// This command is sent to prevent other proxies from also forwarding GP
        /// Notifications to the sinks requiring full unicast communication mode.
        ///
        /// <param name="options" <see cref="byte"> Options</ param >
        /// <param name="gpdSrcId" <see cref="uint"> Gpd Src ID</ param >
        /// <param name="gpdIeee" <see cref="IeeeAddress"> Gpd IEEE</ param >
        /// <param name="endpoint" <see cref="byte"> Endpoint</ param >
        /// <param name="gpdSecurityFrameCounter" <see cref="uint"> Gpd Security Frame Counter</ param >
        /// <param name="gppShortAddress" <see cref="ushort"> Gpp Short Address</ param >
        /// <param name="gppDistance" <see cref="sbyte"> Gpp Distance</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpTunnelingStop(byte options, uint gpdSrcId, IeeeAddress gpdIeee, byte endpoint, uint gpdSecurityFrameCounter, ushort gppShortAddress, sbyte gppDistance)
        {
            GpTunnelingStop command = new GpTunnelingStop();

            // Set the fields
            command.Options = options;
            command.GpdSrcId = gpdSrcId;
            command.GpdIeee = gpdIeee;
            command.Endpoint = endpoint;
            command.GpdSecurityFrameCounter = gpdSecurityFrameCounter;
            command.GppShortAddress = gppShortAddress;
            command.GppDistance = gppDistance;

            return Send(command);
        }

        /// <summary>
        /// The Gp Commissioning Notification
        ///
        /// The GP Commissioning Notification command is used by the proxy in commissioning
        /// mode to forward commissioning data to the sink(s).
        /// On receipt of the GP Commissioning Notification command, a device is informed
        /// about a GPD device seeking to manage a pairing. Also the device which received this
        /// frame is informed of bidirectional commissioning capability of the sender.
        ///
        /// <param name="options" <see cref="ushort"> Options</ param >
        /// <param name="gpdSrcId" <see cref="uint"> Gpd Src ID</ param >
        /// <param name="gpdIeee" <see cref="IeeeAddress"> Gpd IEEE</ param >
        /// <param name="endpoint" <see cref="byte"> Endpoint</ param >
        /// <param name="gpdSecurityFrameCounter" <see cref="uint"> Gpd Security Frame Counter</ param >
        /// <param name="gpdCommandId" <see cref="byte"> Gpd Command ID</ param >
        /// <param name="gpdCommandPayload" <see cref="ByteArray"> Gpd Command Payload</ param >
        /// <param name="gppShortAddress" <see cref="ushort"> Gpp Short Address</ param >
        /// <param name="gppLink" <see cref="byte"> Gpp Link</ param >
        /// <param name="mic" <see cref="uint"> Mic</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpCommissioningNotification(ushort options, uint gpdSrcId, IeeeAddress gpdIeee, byte endpoint, uint gpdSecurityFrameCounter, byte gpdCommandId, ByteArray gpdCommandPayload, ushort gppShortAddress, byte gppLink, uint mic)
        {
            GpCommissioningNotification command = new GpCommissioningNotification();

            // Set the fields
            command.Options = options;
            command.GpdSrcId = gpdSrcId;
            command.GpdIeee = gpdIeee;
            command.Endpoint = endpoint;
            command.GpdSecurityFrameCounter = gpdSecurityFrameCounter;
            command.GpdCommandId = gpdCommandId;
            command.GpdCommandPayload = gpdCommandPayload;
            command.GppShortAddress = gppShortAddress;
            command.GppLink = gppLink;
            command.Mic = mic;

            return Send(command);
        }

        /// <summary>
        /// The Gp Sink Commissioning Mode
        ///
        /// The GP Sink Commissioning Mode command is generated by a remote device, e.g. a
        /// Commissioning Tool, to request a sink to perform a commissioning action in a
        /// particular way.
        ///
        /// <param name="options" <see cref="byte"> Options</ param >
        /// <param name="gpmAddrForSecurity" <see cref="ushort"> Gpm Addr For Security</ param >
        /// <param name="gpmAddrForPairing" <see cref="ushort"> Gpm Addr For Pairing</ param >
        /// <param name="sinkEndpoint" <see cref="byte"> Sink Endpoint</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpSinkCommissioningMode(byte options, ushort gpmAddrForSecurity, ushort gpmAddrForPairing, byte sinkEndpoint)
        {
            GpSinkCommissioningMode command = new GpSinkCommissioningMode();

            // Set the fields
            command.Options = options;
            command.GpmAddrForSecurity = gpmAddrForSecurity;
            command.GpmAddrForPairing = gpmAddrForPairing;
            command.SinkEndpoint = sinkEndpoint;

            return Send(command);
        }

        /// <summary>
        /// The Gp Translation Table Update
        ///
        /// This command is generated to configure the GPD Command Translation Table.
        ///
        /// <param name="options" <see cref="ushort"> Options</ param >
        /// <param name="gpdSrcId" <see cref="uint"> Gpd Src ID</ param >
        /// <param name="gpdIeee" <see cref="IeeeAddress"> Gpd IEEE</ param >
        /// <param name="endpoint" <see cref="byte"> Endpoint</ param >
        /// <param name="translations" <see cref="GpTranslationTableUpdateTranslation"> Translations</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpTranslationTableUpdate(ushort options, uint gpdSrcId, IeeeAddress gpdIeee, byte endpoint, GpTranslationTableUpdateTranslation translations)
        {
            GpTranslationTableUpdate command = new GpTranslationTableUpdate();

            // Set the fields
            command.Options = options;
            command.GpdSrcId = gpdSrcId;
            command.GpdIeee = gpdIeee;
            command.Endpoint = endpoint;
            command.Translations = translations;

            return Send(command);
        }

        /// <summary>
        /// The Gp Translation Table Request
        ///
        /// The GP Translation Table Request is generated to request information from the GPD
        /// Command Translation Table of remote device(s).
        ///
        /// <param name="startIndex" <see cref="byte"> Start Index</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpTranslationTableRequest(byte startIndex)
        {
            GpTranslationTableRequest command = new GpTranslationTableRequest();

            // Set the fields
            command.StartIndex = startIndex;

            return Send(command);
        }

        /// <summary>
        /// The Gp Pairing Configuration
        ///
        /// The command is generated to configure the Sink Table of a sink, to
        /// create/update/replace/remove a pairing to a GPD and/or trigger the sending of GP
        /// Pairing command.
        /// In the current version of the specification, a device shall only send GP Pairing
        /// Configuration command with the Number of paired endpoints field set to 0xfe, if the
        /// CommunicationMode is equal to Pre-Commissioned Groupcast.
        ///
        /// <param name="actions" <see cref="byte"> Actions</ param >
        /// <param name="options" <see cref="ushort"> Options</ param >
        /// <param name="gpdSrcId" <see cref="uint"> Gpd Src ID</ param >
        /// <param name="gpdIeee" <see cref="IeeeAddress"> Gpd IEEE</ param >
        /// <param name="endpoint" <see cref="byte"> Endpoint</ param >
        /// <param name="deviceId" <see cref="byte"> Device ID</ param >
        /// <param name="groupListCount" <see cref="byte"> Group List Count</ param >
        /// <param name="groupList" <see cref="GpPairingConfigurationGroupList"> Group List</ param >
        /// <param name="gpdAssignedAlias" <see cref="ushort"> Gpd Assigned Alias</ param >
        /// <param name="forwardingRadius" <see cref="byte"> Forwarding Radius</ param >
        /// <param name="securityOptions" <see cref="byte"> Security Options</ param >
        /// <param name="gpdSecurityFrameCounter" <see cref="uint"> Gpd Security Frame Counter</ param >
        /// <param name="gpdSecurityKey" <see cref="ZigBeeKey"> Gpd Security Key</ param >
        /// <param name="numberOfPairedEndpoints" <see cref="byte"> Number Of Paired Endpoints</ param >
        /// <param name="pairedEndpoints" <see cref="byte"> Paired Endpoints</ param >
        /// <param name="applicationInformation" <see cref="byte"> Application Information</ param >
        /// <param name="manufacturerId" <see cref="ushort"> Manufacturer ID</ param >
        /// <param name="modeId" <see cref="ushort"> Mode ID</ param >
        /// <param name="numberOfGpdCommands" <see cref="byte"> Number Of Gpd Commands</ param >
        /// <param name="gpdCommandIdList" <see cref="byte"> Gpd Command ID List</ param >
        /// <param name="clusterIdListCount" <see cref="byte"> Cluster ID List Count</ param >
        /// <param name="clusterListServer" <see cref="ushort"> Cluster List Server</ param >
        /// <param name="clusterListClient" <see cref="ushort"> Cluster List Client</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpPairingConfiguration(byte actions, ushort options, uint gpdSrcId, IeeeAddress gpdIeee, byte endpoint, byte deviceId, byte groupListCount, GpPairingConfigurationGroupList groupList, ushort gpdAssignedAlias, byte forwardingRadius, byte securityOptions, uint gpdSecurityFrameCounter, ZigBeeKey gpdSecurityKey, byte numberOfPairedEndpoints, byte pairedEndpoints, byte applicationInformation, ushort manufacturerId, ushort modeId, byte numberOfGpdCommands, byte gpdCommandIdList, byte clusterIdListCount, ushort clusterListServer, ushort clusterListClient)
        {
            GpPairingConfiguration command = new GpPairingConfiguration();

            // Set the fields
            command.Actions = actions;
            command.Options = options;
            command.GpdSrcId = gpdSrcId;
            command.GpdIeee = gpdIeee;
            command.Endpoint = endpoint;
            command.DeviceId = deviceId;
            command.GroupListCount = groupListCount;
            command.GroupList = groupList;
            command.GpdAssignedAlias = gpdAssignedAlias;
            command.ForwardingRadius = forwardingRadius;
            command.SecurityOptions = securityOptions;
            command.GpdSecurityFrameCounter = gpdSecurityFrameCounter;
            command.GpdSecurityKey = gpdSecurityKey;
            command.NumberOfPairedEndpoints = numberOfPairedEndpoints;
            command.PairedEndpoints = pairedEndpoints;
            command.ApplicationInformation = applicationInformation;
            command.ManufacturerId = manufacturerId;
            command.ModeId = modeId;
            command.NumberOfGpdCommands = numberOfGpdCommands;
            command.GpdCommandIdList = gpdCommandIdList;
            command.ClusterIdListCount = clusterIdListCount;
            command.ClusterListServer = clusterListServer;
            command.ClusterListClient = clusterListClient;

            return Send(command);
        }

        /// <summary>
        /// The Gp Sink Table Request
        ///
        /// The GP Sink Table Request command is generated to read out selected Sink Table
        /// entry(s), by index or by GPD ID
        ///
        /// <param name="options" <see cref="byte"> Options</ param >
        /// <param name="gpdSrcId" <see cref="uint"> Gpd Src ID</ param >
        /// <param name="gpdIeee" <see cref="IeeeAddress"> Gpd IEEE</ param >
        /// <param name="endpoint" <see cref="byte"> Endpoint</ param >
        /// <param name="index" <see cref="byte"> Index</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpSinkTableRequest(byte options, uint gpdSrcId, IeeeAddress gpdIeee, byte endpoint, byte index)
        {
            GpSinkTableRequest command = new GpSinkTableRequest();

            // Set the fields
            command.Options = options;
            command.GpdSrcId = gpdSrcId;
            command.GpdIeee = gpdIeee;
            command.Endpoint = endpoint;
            command.Index = index;

            return Send(command);
        }

        /// <summary>
        /// The Gp Proxy Table Response
        ///
        /// To reply with read-out Proxy Table entries, by index or by GPD ID.
        /// Upon reception of the GP Proxy Table Request command, the device shall check if it
        /// implements a Proxy Table. If not, it shall generate a ZCL Default Response command,
        /// with the Status code field carrying UNSUP_CLUSTER_COMMAND. If the device
        /// implements the Proxy Table, it shall prepare a GP Proxy Table Response.
        ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="totalNumberOfNonEmptyProxyTableEntries" <see cref="byte"> Total Number Of Non Empty Proxy Table Entries</ param >
        /// <param name="startIndex" <see cref="byte"> Start Index</ param >
        /// <param name="entriesCount" <see cref="byte"> Entries Count</ param >
        /// <param name="proxyTableEntries" <see cref="byte"> Proxy Table Entries</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpProxyTableResponse(byte status, byte totalNumberOfNonEmptyProxyTableEntries, byte startIndex, byte entriesCount, byte proxyTableEntries)
        {
            GpProxyTableResponse command = new GpProxyTableResponse();

            // Set the fields
            command.Status = status;
            command.TotalNumberOfNonEmptyProxyTableEntries = totalNumberOfNonEmptyProxyTableEntries;
            command.StartIndex = startIndex;
            command.EntriesCount = entriesCount;
            command.ProxyTableEntries = proxyTableEntries;

            return Send(command);
        }

        /// <summary>
        /// The Gp Notification Response
        ///
        /// This command is generated when the sink acknowledges the reception of full unicast
        /// GP Notification command. The GP Notification Response command is sent in unicast
        /// to the originating proxy.
        ///
        /// <param name="options" <see cref="byte"> Options</ param >
        /// <param name="gpdSrcId" <see cref="uint"> Gpd Src ID</ param >
        /// <param name="gpdIeee" <see cref="IeeeAddress"> Gpd IEEE</ param >
        /// <param name="gpdSecurityFrameCounter" <see cref="uint"> Gpd Security Frame Counter</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpNotificationResponse(byte options, uint gpdSrcId, IeeeAddress gpdIeee, uint gpdSecurityFrameCounter)
        {
            GpNotificationResponse command = new GpNotificationResponse();

            // Set the fields
            command.Options = options;
            command.GpdSrcId = gpdSrcId;
            command.GpdIeee = gpdIeee;
            command.GpdSecurityFrameCounter = gpdSecurityFrameCounter;

            return Send(command);
        }

        /// <summary>
        /// The Gp Pairing
        ///
        /// The GP Pairing command is generated by the sink to manage pairing information. The
        /// GP Pairing command is typically sent using network-wide broadcast. If the
        /// CommunicationMode sub-field is set to 0b11, GP Pairing command may be sent in
        /// unicast to the selected proxy.
        ///
        /// <param name="options" <see cref="int"> Options</ param >
        /// <param name="gpdSrcId" <see cref="uint"> Gpd Src ID</ param >
        /// <param name="gpdIeee" <see cref="IeeeAddress"> Gpd IEEE</ param >
        /// <param name="endpoint" <see cref="byte"> Endpoint</ param >
        /// <param name="sinkIeeeAddress" <see cref="IeeeAddress"> Sink IEEE Address</ param >
        /// <param name="sinkNwkAddress" <see cref="ushort"> Sink NWK Address</ param >
        /// <param name="sinkGroupId" <see cref="ushort"> Sink Group ID</ param >
        /// <param name="deviceId" <see cref="byte"> Device ID</ param >
        /// <param name="gpdSecurityFrameCounter" <see cref="uint"> Gpd Security Frame Counter</ param >
        /// <param name="gpdKey" <see cref="ZigBeeKey"> Gpd Key</ param >
        /// <param name="assignedAlias" <see cref="ushort"> Assigned Alias</ param >
        /// <param name="forwardingRadius" <see cref="byte"> Forwarding Radius</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpPairing(int options, uint gpdSrcId, IeeeAddress gpdIeee, byte endpoint, IeeeAddress sinkIeeeAddress, ushort sinkNwkAddress, ushort sinkGroupId, byte deviceId, uint gpdSecurityFrameCounter, ZigBeeKey gpdKey, ushort assignedAlias, byte forwardingRadius)
        {
            GpPairing command = new GpPairing();

            // Set the fields
            command.Options = options;
            command.GpdSrcId = gpdSrcId;
            command.GpdIeee = gpdIeee;
            command.Endpoint = endpoint;
            command.SinkIeeeAddress = sinkIeeeAddress;
            command.SinkNwkAddress = sinkNwkAddress;
            command.SinkGroupId = sinkGroupId;
            command.DeviceId = deviceId;
            command.GpdSecurityFrameCounter = gpdSecurityFrameCounter;
            command.GpdKey = gpdKey;
            command.AssignedAlias = assignedAlias;
            command.ForwardingRadius = forwardingRadius;

            return Send(command);
        }

        /// <summary>
        /// The Gp Proxy Commissioning Mode
        ///
        /// This command is generated when the sink wishes to instruct the proxies to
        /// enter/exit commissioning mode. The GP Proxy Commissioning Mode command is
        /// typically sent using network-wide broadcast.
        ///
        /// <param name="options" <see cref="byte"> Options</ param >
        /// <param name="commissioningWindow" <see cref="ushort"> Commissioning Window</ param >
        /// <param name="channel" <see cref="byte"> Channel</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpProxyCommissioningMode(byte options, ushort commissioningWindow, byte channel)
        {
            GpProxyCommissioningMode command = new GpProxyCommissioningMode();

            // Set the fields
            command.Options = options;
            command.CommissioningWindow = commissioningWindow;
            command.Channel = channel;

            return Send(command);
        }

        /// <summary>
        /// The Gp Response
        ///
        /// This command is generated when sink requests to send any information to a specific
        /// GPD with Rx capability.
        ///
        /// <param name="options" <see cref="byte"> Options</ param >
        /// <param name="tempMasterShortAddress" <see cref="ushort"> Temp Master Short Address</ param >
        /// <param name="tempMasterTxChannel" <see cref="byte"> Temp Master Tx Channel</ param >
        /// <param name="gpdSrcId" <see cref="uint"> Gpd Src ID</ param >
        /// <param name="gpdIeee" <see cref="IeeeAddress"> Gpd IEEE</ param >
        /// <param name="endpoint" <see cref="byte"> Endpoint</ param >
        /// <param name="gpdCommandId" <see cref="byte"> Gpd Command ID</ param >
        /// <param name="gpdCommandPayload" <see cref="ByteArray"> Gpd Command Payload</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpResponse(byte options, ushort tempMasterShortAddress, byte tempMasterTxChannel, uint gpdSrcId, IeeeAddress gpdIeee, byte endpoint, byte gpdCommandId, ByteArray gpdCommandPayload)
        {
            GpResponse command = new GpResponse();

            // Set the fields
            command.Options = options;
            command.TempMasterShortAddress = tempMasterShortAddress;
            command.TempMasterTxChannel = tempMasterTxChannel;
            command.GpdSrcId = gpdSrcId;
            command.GpdIeee = gpdIeee;
            command.Endpoint = endpoint;
            command.GpdCommandId = gpdCommandId;
            command.GpdCommandPayload = gpdCommandPayload;

            return Send(command);
        }

        /// <summary>
        /// The Gp Sink Table Response
        ///
        /// To selected Proxy Table entries, by index or by GPD ID.
        ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="totalNumberofNonEmptySinkTableEntries" <see cref="byte"> Total Numberof Non Empty Sink Table Entries</ param >
        /// <param name="startIndex" <see cref="byte"> Start Index</ param >
        /// <param name="sinkTableEntriesCount" <see cref="byte"> Sink Table Entries Count</ param >
        /// <param name="sinkTableEntries" <see cref="byte"> Sink Table Entries</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpSinkTableResponse(byte status, byte totalNumberofNonEmptySinkTableEntries, byte startIndex, byte sinkTableEntriesCount, byte sinkTableEntries)
        {
            GpSinkTableResponse command = new GpSinkTableResponse();

            // Set the fields
            command.Status = status;
            command.TotalNumberofNonEmptySinkTableEntries = totalNumberofNonEmptySinkTableEntries;
            command.StartIndex = startIndex;
            command.SinkTableEntriesCount = sinkTableEntriesCount;
            command.SinkTableEntries = sinkTableEntries;

            return Send(command);
        }

        /// <summary>
        /// The Gp Proxy Table Request
        ///
        /// To request selected Proxy Table entries, by index or by GPD ID.
        ///
        /// <param name="options" <see cref="byte"> Options</ param >
        /// <param name="gpdSrcId" <see cref="uint"> Gpd Src ID</ param >
        /// <param name="gpdIeee" <see cref="IeeeAddress"> Gpd IEEE</ param >
        /// <param name="endpoint" <see cref="byte"> Endpoint</ param >
        /// <param name="index" <see cref="byte"> Index</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GpProxyTableRequest(byte options, uint gpdSrcId, IeeeAddress gpdIeee, byte endpoint, byte index)
        {
            GpProxyTableRequest command = new GpProxyTableRequest();

            // Set the fields
            command.Options = options;
            command.GpdSrcId = gpdSrcId;
            command.GpdIeee = gpdIeee;
            command.Endpoint = endpoint;
            command.Index = index;

            return Send(command);
        }
    }
}
