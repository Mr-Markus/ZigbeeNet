// License text here

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Commissioning;

namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Commissioningcluster implementation (Cluster ID 0x0015).
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

        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Commissioning cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclCommissioningCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// The Restart Device Command
        ///
        /// <param name="option"><see cref="byte"/> Option</param>
        /// <param name="delay"><see cref="byte"/> Delay</param>
        /// <param name="jitter"><see cref="byte"/> Jitter</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
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
        /// <param name="option"><see cref="byte"/> Option</param>
        /// <param name="index"><see cref="byte"/> Index</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
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
        /// <param name="option"><see cref="byte"/> Option</param>
        /// <param name="index"><see cref="byte"/> Index</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
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
        /// <param name="option"><see cref="byte"/> Option</param>
        /// <param name="index"><see cref="byte"/> Index</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
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
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
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
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
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
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
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
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ResetStartupParametersResponse(byte status)
        {
            ResetStartupParametersResponse command = new ResetStartupParametersResponse();

            // Set the fields
            command.Status = status;

            return Send(command);
        }

        public override ZclCommand GetCommandFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // RESTART_DEVICE_COMMAND
                    return new RestartDeviceCommand();
                case 1: // SAVE_STARTUP_PARAMETERS_COMMAND
                    return new SaveStartupParametersCommand();
                case 2: // RESTORE_STARTUP_PARAMETERS_COMMAND
                    return new RestoreStartupParametersCommand();
                case 3: // RESET_STARTUP_PARAMETERS_COMMAND
                    return new ResetStartupParametersCommand();
                    default:
                        return null;
            }
        }

        public ZclCommand getResponseFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // RESTART_DEVICE_RESPONSE_RESPONSE
                    return new RestartDeviceResponseResponse();
                case 1: // SAVE_STARTUP_PARAMETERS_RESPONSE
                    return new SaveStartupParametersResponse();
                case 2: // RESTORE_STARTUP_PARAMETERS_RESPONSE
                    return new RestoreStartupParametersResponse();
                case 3: // RESET_STARTUP_PARAMETERS_RESPONSE
                    return new ResetStartupParametersResponse();
                    default:
                        return null;
            }
        }
    }
}
