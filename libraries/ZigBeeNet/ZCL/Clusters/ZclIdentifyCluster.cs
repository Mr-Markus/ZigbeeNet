
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Identify;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Identify cluster implementation (Cluster ID 0x0003).
    ///
    /// Attributes and commands to put a device into an Identification mode (e.g. flashing a
    /// light), that indicates to an observer – e.g. an installer - which of several devices it
    /// is, also to request any device that is identifying itself to respond to the initiator.
    /// Note that this cluster cannot be disabled, and remains functional regardless of the
    /// setting of the DeviceEnable attribute in the Basic cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclIdentifyCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0003;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Identify";

        // Attribute constants

        /// <summary>
        /// The IdentifyTime attribute specifies the remaining length of time, in seconds,
        /// that the device will continue to identify itself.
        /// If this attribute is set to a value other than 0x0000 then the device shall enter its
        /// identification procedure, in order to indicate to an observer which of several
        /// devices it is. It is recommended that this procedure consists of flashing a light
        /// with a period of 0.5 seconds. The IdentifyTime attribute shall be decremented
        /// every second.
        /// If this attribute reaches or is set to the value 0x0000 then the device shall
        /// terminate its identification procedure.
        /// </summary>
        public const ushort ATTR_IDENTIFYTIME = 0x0000;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(1);

            attributeMap.Add(ATTR_IDENTIFYTIME, new ZclAttribute(this, ATTR_IDENTIFYTIME, "Identify Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, true, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(1);

            commandMap.Add(0x0000, () => new IdentifyQueryResponse());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(2);

            commandMap.Add(0x0000, () => new IdentifyCommand());
            commandMap.Add(0x0001, () => new IdentifyQueryCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Identify cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclIdentifyCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Identify Command
        ///
        /// The identify command starts or stops the receiving device identifying itself.
        ///
        /// <param name="identifyTime" <see cref="ushort"> Identify Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> IdentifyCommand(ushort identifyTime)
        {
            IdentifyCommand command = new IdentifyCommand();

            // Set the fields
            command.IdentifyTime = identifyTime;

            return Send(command);
        }

        /// <summary>
        /// The Identify Query Command
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> IdentifyQueryCommand()
        {
            return Send(new IdentifyQueryCommand());
        }

        /// <summary>
        /// The Identify Query Response
        ///
        /// The identify query response command is generated in response to receiving an
        /// Identify Query command in the case that the device is currently identifying
        /// itself.
        ///
        /// <param name="identifyTime" <see cref="ushort"> Identify Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> IdentifyQueryResponse(ushort identifyTime)
        {
            IdentifyQueryResponse command = new IdentifyQueryResponse();

            // Set the fields
            command.IdentifyTime = identifyTime;

            return Send(command);
        }
    }
}
