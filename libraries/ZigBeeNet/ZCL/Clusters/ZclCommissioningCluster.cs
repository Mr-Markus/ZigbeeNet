
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Commissioning;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Commissioning cluster implementation (Cluster ID 0x0015).
    ///
    /// This cluster provides attributes and commands pertaining to the commissioning and
    /// management of ZigBee devices operating in a network.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclCommissioningCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0015;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Commissioning";

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(4);

            commandMap.Add(0x0000, () => new RestartDeviceResponseResponse());
            commandMap.Add(0x0001, () => new SaveStartupParametersResponse());
            commandMap.Add(0x0002, () => new RestoreStartupParametersResponse());
            commandMap.Add(0x0003, () => new ResetStartupParametersResponse());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(4);

            commandMap.Add(0x0000, () => new RestartDeviceCommand());
            commandMap.Add(0x0001, () => new SaveStartupParametersCommand());
            commandMap.Add(0x0002, () => new RestoreStartupParametersCommand());
            commandMap.Add(0x0003, () => new ResetStartupParametersCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Commissioning cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclCommissioningCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Restart Device Command
        ///
        ///         ///
        /// <param name="option" <see cref="byte"> Option</ param >
        /// <param name="delay" <see cref="byte"> Delay</ param >
        /// <param name="jitter" <see cref="byte"> Jitter</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RestartDeviceCommand(byte option, byte delay, byte jitter)
        {
            RestartDeviceCommand command = new RestartDeviceCommand();

            // Set the fields
            command.Option = option;
            command.Delay = delay;
            command.Jitter = jitter;

            return Send(command);
        }

        /// <summary>
        /// The Save Startup Parameters Command
        ///
        ///         ///
        /// <param name="option" <see cref="byte"> Option</ param >
        /// <param name="index" <see cref="byte"> Index</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SaveStartupParametersCommand(byte option, byte index)
        {
            SaveStartupParametersCommand command = new SaveStartupParametersCommand();

            // Set the fields
            command.Option = option;
            command.Index = index;

            return Send(command);
        }

        /// <summary>
        /// The Restore Startup Parameters Command
        ///
        ///         ///
        /// <param name="option" <see cref="byte"> Option</ param >
        /// <param name="index" <see cref="byte"> Index</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RestoreStartupParametersCommand(byte option, byte index)
        {
            RestoreStartupParametersCommand command = new RestoreStartupParametersCommand();

            // Set the fields
            command.Option = option;
            command.Index = index;

            return Send(command);
        }

        /// <summary>
        /// The Reset Startup Parameters Command
        ///
        ///         ///
        /// <param name="option" <see cref="byte"> Option</ param >
        /// <param name="index" <see cref="byte"> Index</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ResetStartupParametersCommand(byte option, byte index)
        {
            ResetStartupParametersCommand command = new ResetStartupParametersCommand();

            // Set the fields
            command.Option = option;
            command.Index = index;

            return Send(command);
        }

        /// <summary>
        /// The Restart Device Response Response
        ///
        ///         ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RestartDeviceResponseResponse(byte status)
        {
            RestartDeviceResponseResponse command = new RestartDeviceResponseResponse();

            // Set the fields
            command.Status = status;

            return Send(command);
        }

        /// <summary>
        /// The Save Startup Parameters Response
        ///
        ///         ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SaveStartupParametersResponse(byte status)
        {
            SaveStartupParametersResponse command = new SaveStartupParametersResponse();

            // Set the fields
            command.Status = status;

            return Send(command);
        }

        /// <summary>
        /// The Restore Startup Parameters Response
        ///
        ///         ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RestoreStartupParametersResponse(byte status)
        {
            RestoreStartupParametersResponse command = new RestoreStartupParametersResponse();

            // Set the fields
            command.Status = status;

            return Send(command);
        }

        /// <summary>
        /// The Reset Startup Parameters Response
        ///
        ///         ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ResetStartupParametersResponse(byte status)
        {
            ResetStartupParametersResponse command = new ResetStartupParametersResponse();

            // Set the fields
            command.Status = status;

            return Send(command);
        }
    }
}
