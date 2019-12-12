
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.KeyEstablishment;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Key Establishment cluster implementation (Cluster ID 0x0800).
    ///
    /// This cluster provides attributes and commands to perform mutual authentication and
    /// establish keys between two ZigBee devices.
    /// All Key Establishment messages should be sent with APS retries enabled. A failure to
    /// receive an ACK in a timely manner can be seen as a failure of key establishment. No
    /// Terminate Key Establishment should be sent to the partner of device that has timed out
    /// the operation.
    /// The initiator can initiate the key establishment with any active endpoint on the
    /// responder device that supports the key establishment cluster. The endpoint can be
    /// either preconfigured or discovered, for example, by using ZDO Match_Desc_req. A link
    /// key successfully established using key establishment is valid for all endpoints on a
    /// particular device. The responder shall respond to the initiator using the source
    /// endpoint of the initiator's messages as the destination endpoint of the responder's
    /// messages.
    /// It is expected that the time it takes to perform the various cryptographic computations
    /// of the key establishment cluster may vary greatly based on the device. Therefore rather
    /// than set static timeouts, the Initiate Key Establishment Request and Response
    /// messages will contain approximate values for how long the device will take to generate
    /// the ephemeral data and how long the device will take to generate confirm key message. A
    /// device performing key establishment can use this information in order to choose a
    /// reasonable timeout for its partner during those operations. The timeout should also
    /// take into consideration the time it takes for a message to traverse the network
    /// including APS retries. A minimum transmission time of 2 seconds is recommended.
    /// For the Initiate Key Establishment Response message, it is recommended the initiator
    /// wait at least 2 seconds before timing out the operation. It is not expected that
    /// generating an Initiate Key Establishment Response will take significant time
    /// compared to generating the Ephemeral Data and Confirm Key messages.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclKeyEstablishmentCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0800;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Key Establishment";

        // Attribute constants

        /// <summary>
        /// The KeyEstablishmentSuite attribute is 16-bits in length and specifies all the
        /// cryptographic schemes for key establishment on the device. A device shall set the
        /// corresponding bit to 1 for every cryptographic scheme that is supports. All other
        /// cryptographic schemes and reserved bits shall be set to 0.
        /// </summary>
        public const ushort ATTR_CLIENTKEYESTABLISHMENTSUITE = 0x0000;

        /// <summary>
        /// The KeyEstablishmentSuite attribute is 16-bits in length and specifies all the
        /// cryptographic schemes for key establishment on the device. A device shall set the
        /// corresponding bit to 1 for every cryptographic scheme that is supports. All other
        /// cryptographic schemes and reserved bits shall be set to 0.
        /// </summary>
        public const ushort ATTR_SERVERKEYESTABLISHMENTSUITE = 0x0000;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(1);

            attributeMap.Add(ATTR_CLIENTKEYESTABLISHMENTSUITE, new ZclAttribute(this, ATTR_CLIENTKEYESTABLISHMENTSUITE, "Client Key Establishment Suite", ZclDataType.Get(DataType.ENUMERATION_16_BIT), true, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(1);

            attributeMap.Add(ATTR_SERVERKEYESTABLISHMENTSUITE, new ZclAttribute(this, ATTR_SERVERKEYESTABLISHMENTSUITE, "Server Key Establishment Suite", ZclDataType.Get(DataType.ENUMERATION_16_BIT), true, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(4);

            commandMap.Add(0x0000, () => new InitiateKeyEstablishmentResponse());
            commandMap.Add(0x0001, () => new EphemeralDataResponse());
            commandMap.Add(0x0002, () => new ConfirmKeyResponse());
            commandMap.Add(0x0003, () => new TerminateKeyEstablishment());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(3);

            commandMap.Add(0x0000, () => new InitiateKeyEstablishmentRequestCommand());
            commandMap.Add(0x0001, () => new EphemeralDataRequestCommand());
            commandMap.Add(0x0002, () => new ConfirmKeyDataRequestCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Key Establishment cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclKeyEstablishmentCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Initiate Key Establishment Request Command
        ///
        /// The Initiate Key Establishment Request command allows a device to initiate key
        /// establishment with another device. The sender will transmit its identity
        /// information and key establishment protocol information to the receiving device.
        /// If the device does not currently have the resources to respond to a key
        /// establishment request it shall send a Terminate Key Establishment command with
        /// the result value set to NO_RESOURCES and the Wait Time field shall be set to an
        /// approximation of the time that must pass before the device will have the resources
        /// to process a new Key Establishment Request.
        /// If the device can process this request, it shall check the Issuer field of the
        /// device's implicit certificate. If the Issuer field does not contain a value that
        /// corresponds to a known Certificate Authority, the device shall send a Terminate
        /// Key Establishment command with the result set to UNKNOWN_ISSUER.
        /// If the device accepts the request it shall send an Initiate Key Establishment
        /// Response command containing its own identity information. The device should
        /// verify the certificate belongs to the address that the device is communicating
        /// with. The binding between the identity of the communicating device and its address
        /// is verifiable using out-of-band method.
        ///
        /// <param name="keyEstablishmentSuite" <see cref="ushort"> Key Establishment Suite</ param >
        /// <param name="ephemeralDataGenerateTime" <see cref="byte"> Ephemeral Data Generate Time</ param >
        /// <param name="confirmKeyGenerateTime" <see cref="byte"> Confirm Key Generate Time</ param >
        /// <param name="identity" <see cref="ByteArray"> Identity</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> InitiateKeyEstablishmentRequestCommand(ushort keyEstablishmentSuite, byte ephemeralDataGenerateTime, byte confirmKeyGenerateTime, ByteArray identity)
        {
            InitiateKeyEstablishmentRequestCommand command = new InitiateKeyEstablishmentRequestCommand();

            // Set the fields
            command.KeyEstablishmentSuite = keyEstablishmentSuite;
            command.EphemeralDataGenerateTime = ephemeralDataGenerateTime;
            command.ConfirmKeyGenerateTime = confirmKeyGenerateTime;
            command.Identity = identity;

            return Send(command);
        }

        /// <summary>
        /// The Ephemeral Data Request Command
        ///
        /// The Ephemeral Data Request command allows a device to communicate its ephemeral
        /// data to another device and request that the device send back its own ephemeral data.
        ///
        /// <param name="ephemeralData" <see cref="ByteArray"> Ephemeral Data</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> EphemeralDataRequestCommand(ByteArray ephemeralData)
        {
            EphemeralDataRequestCommand command = new EphemeralDataRequestCommand();

            // Set the fields
            command.EphemeralData = ephemeralData;

            return Send(command);
        }

        /// <summary>
        /// The Confirm Key Data Request Command
        ///
        /// The Confirm Key Request command allows the initiator sending device to confirm the
        /// key established with the responder receiving device based on performing a
        /// cryptographic hash using part of the generated keying material and the identities
        /// and ephemeral data of both parties.
        ///
        /// <param name="secureMessageAuthenticationCode" <see cref="ByteArray"> Secure Message Authentication Code</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ConfirmKeyDataRequestCommand(ByteArray secureMessageAuthenticationCode)
        {
            ConfirmKeyDataRequestCommand command = new ConfirmKeyDataRequestCommand();

            // Set the fields
            command.SecureMessageAuthenticationCode = secureMessageAuthenticationCode;

            return Send(command);
        }

        /// <summary>
        /// The Initiate Key Establishment Response
        ///
        /// The Initiate Key Establishment Response command allows a device to respond to a
        /// device requesting the initiation of key establishment with it. The sender will
        /// transmit its identity information and key establishment protocol information to
        /// the receiving device.
        ///
        /// <param name="requestedKeyEstablishmentSuite" <see cref="ushort"> Requested Key Establishment Suite</ param >
        /// <param name="ephemeralDataGenerateTime" <see cref="byte"> Ephemeral Data Generate Time</ param >
        /// <param name="confirmKeyGenerateTime" <see cref="byte"> Confirm Key Generate Time</ param >
        /// <param name="identity" <see cref="ByteArray"> Identity</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> InitiateKeyEstablishmentResponse(ushort requestedKeyEstablishmentSuite, byte ephemeralDataGenerateTime, byte confirmKeyGenerateTime, ByteArray identity)
        {
            InitiateKeyEstablishmentResponse command = new InitiateKeyEstablishmentResponse();

            // Set the fields
            command.RequestedKeyEstablishmentSuite = requestedKeyEstablishmentSuite;
            command.EphemeralDataGenerateTime = ephemeralDataGenerateTime;
            command.ConfirmKeyGenerateTime = confirmKeyGenerateTime;
            command.Identity = identity;

            return Send(command);
        }

        /// <summary>
        /// The Ephemeral Data Response
        ///
        /// The Ephemeral Data Response command allows a device to communicate its ephemeral
        /// data to another device that previously requested it.
        ///
        /// <param name="ephemeralData" <see cref="ByteArray"> Ephemeral Data</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> EphemeralDataResponse(ByteArray ephemeralData)
        {
            EphemeralDataResponse command = new EphemeralDataResponse();

            // Set the fields
            command.EphemeralData = ephemeralData;

            return Send(command);
        }

        /// <summary>
        /// The Confirm Key Response
        ///
        /// The Confirm Key Response command allows the responder to verify the initiator has
        /// derived the same secret key. This is done by sending the initiator a cryptographic
        /// hash generated using the keying material and the identities and ephemeral data of
        /// both parties.
        ///
        /// <param name="secureMessageAuthenticationCode" <see cref="ByteArray"> Secure Message Authentication Code</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ConfirmKeyResponse(ByteArray secureMessageAuthenticationCode)
        {
            ConfirmKeyResponse command = new ConfirmKeyResponse();

            // Set the fields
            command.SecureMessageAuthenticationCode = secureMessageAuthenticationCode;

            return Send(command);
        }

        /// <summary>
        /// The Terminate Key Establishment
        ///
        /// The Terminate Key Establishment command may be sent by either the initiator or
        /// responder to indicate a failure in the key establishment exchange.
        ///
        /// <param name="statusCode" <see cref="byte"> Status Code</ param >
        /// <param name="waitTime" <see cref="byte"> Wait Time</ param >
        /// <param name="keyEstablishmentSuite" <see cref="ushort"> Key Establishment Suite</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> TerminateKeyEstablishment(byte statusCode, byte waitTime, ushort keyEstablishmentSuite)
        {
            TerminateKeyEstablishment command = new TerminateKeyEstablishment();

            // Set the fields
            command.StatusCode = statusCode;
            command.WaitTime = waitTime;
            command.KeyEstablishmentSuite = keyEstablishmentSuite;

            return Send(command);
        }
    }
}
