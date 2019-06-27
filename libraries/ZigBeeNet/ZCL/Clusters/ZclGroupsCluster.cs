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
using ZigBeeNet.ZCL.Clusters.Groups;

namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Groupscluster implementation (Cluster ID 0x0004).
    ///
    /// The ZigBee specification provides the capability for group addressing. That is,
    /// any endpoint on any device may be assigned to one or more groups, each labeled
    /// with a 16-bit identifier (0x0001 – 0xfff7), which acts for all intents and purposes
    /// like a network address. Once a group is established, frames, sent using the
    /// APSDE-DATA.request primitive and having a DstAddrMode of 0x01, denoting
    /// group addressing, will be delivered to every endpoint assigned to the group
    /// address named in the DstAddr parameter of the outgoing APSDE-DATA.request
    /// primitive on every device in the network for which there are such endpoints.
    /// <p>
    /// Management of group membership on each device and endpoint is implemented
    /// by the APS, but the over-the-air messages that allow for remote management and
    /// commissioning of groups are defined here in the cluster library on the theory that,
    /// while the basic group addressing facilities are integral to the operation of the
    /// stack, not every device will need or want to implement this management cluster.
    /// Furthermore, the placement of the management commands here allows developers
    /// of proprietary profiles to avoid implementing the library cluster but still exploit
    /// group addressing
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclGroupsCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0004;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Groups";

        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Groups cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclGroupsCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// The Add Group Command
        ///
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="groupName"><see cref="string"/> Group Name</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> AddGroupCommand(ushort groupID, string groupName)
        {
            AddGroupCommand command = new AddGroupCommand();

            // Set the fields
            command.GroupID = groupID;
            command.GroupName = groupName;

            return Send(command);
        }

        /// <summary>
        /// The View Group Command
        ///
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ViewGroupCommand(ushort groupID)
        {
            ViewGroupCommand command = new ViewGroupCommand();

            // Set the fields
            command.GroupID = groupID;

            return Send(command);
        }

        /// <summary>
        /// The Get Group Membership Command
        ///
        /// <param name="groupCount"><see cref="byte"/> Group count</param>
        /// <param name="groupList"><see cref="List<ushort>"/> Group list</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetGroupMembershipCommand(byte groupCount, List<ushort> groupList)
        {
            GetGroupMembershipCommand command = new GetGroupMembershipCommand();

            // Set the fields
            command.GroupCount = groupCount;
            command.GroupList = groupList;

            return Send(command);
        }

        /// <summary>
        /// The Remove Group Command
        ///
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> RemoveGroupCommand(ushort groupID)
        {
            RemoveGroupCommand command = new RemoveGroupCommand();

            // Set the fields
            command.GroupID = groupID;

            return Send(command);
        }

        /// <summary>
        /// The Remove All Groups Command
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> RemoveAllGroupsCommand()
        {
            RemoveAllGroupsCommand command = new RemoveAllGroupsCommand();

            return Send(command);
        }

        /// <summary>
        /// The Add Group If Identifying Command
        ///
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="groupName"><see cref="string"/> Group Name</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> AddGroupIfIdentifyingCommand(ushort groupID, string groupName)
        {
            AddGroupIfIdentifyingCommand command = new AddGroupIfIdentifyingCommand();

            // Set the fields
            command.GroupID = groupID;
            command.GroupName = groupName;

            return Send(command);
        }

        /// <summary>
        /// The Add Group Response
        ///
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> AddGroupResponse(byte status, ushort groupID)
        {
            AddGroupResponse command = new AddGroupResponse();

            // Set the fields
            command.Status = status;
            command.GroupID = groupID;

            return Send(command);
        }

        /// <summary>
        /// The View Group Response
        ///
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="groupName"><see cref="string"/> Group Name</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ViewGroupResponse(byte status, ushort groupID, string groupName)
        {
            ViewGroupResponse command = new ViewGroupResponse();

            // Set the fields
            command.Status = status;
            command.GroupID = groupID;
            command.GroupName = groupName;

            return Send(command);
        }

        /// <summary>
        /// The Get Group Membership Response
        ///
        /// <param name="capacity"><see cref="byte"/> Capacity</param>
        /// <param name="groupCount"><see cref="byte"/> Group count</param>
        /// <param name="groupList"><see cref="List<ushort>"/> Group list</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetGroupMembershipResponse(byte capacity, byte groupCount, List<ushort> groupList)
        {
            GetGroupMembershipResponse command = new GetGroupMembershipResponse();

            // Set the fields
            command.Capacity = capacity;
            command.GroupCount = groupCount;
            command.GroupList = groupList;

            return Send(command);
        }

        /// <summary>
        /// The Remove Group Response
        ///
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> RemoveGroupResponse(byte status, ushort groupID)
        {
            RemoveGroupResponse command = new RemoveGroupResponse();

            // Set the fields
            command.Status = status;
            command.GroupID = groupID;

            return Send(command);
        }

        public override ZclCommand GetCommandFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // ADD_GROUP_COMMAND
                    return new AddGroupCommand();
                case 1: // VIEW_GROUP_COMMAND
                    return new ViewGroupCommand();
                case 2: // GET_GROUP_MEMBERSHIP_COMMAND
                    return new GetGroupMembershipCommand();
                case 3: // REMOVE_GROUP_COMMAND
                    return new RemoveGroupCommand();
                case 4: // REMOVE_ALL_GROUPS_COMMAND
                    return new RemoveAllGroupsCommand();
                case 5: // ADD_GROUP_IF_IDENTIFYING_COMMAND
                    return new AddGroupIfIdentifyingCommand();
                    default:
                        return null;
            }
        }

        public ZclCommand getResponseFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // ADD_GROUP_RESPONSE
                    return new AddGroupResponse();
                case 1: // VIEW_GROUP_RESPONSE
                    return new ViewGroupResponse();
                case 2: // GET_GROUP_MEMBERSHIP_RESPONSE
                    return new GetGroupMembershipResponse();
                case 3: // REMOVE_GROUP_RESPONSE
                    return new RemoveGroupResponse();
                    default:
                        return null;
            }
        }
    }
}
