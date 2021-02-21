
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Groups;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Groups cluster implementation (Cluster ID 0x0004).
    ///
    /// The ZigBee specification provides the capability for group addressing. That is, any
    /// endpoint on any device may be assigned to one or more groups, each labeled with a 16-bit
    /// identifier (0x0001 – 0xfff7), which acts for all intents and purposes like a network
    /// address. Once a group is established, frames, sent using the APSDE-DATA.request
    /// primitive and having a DstAddrMode of 0x01, denoting group addressing, will be
    /// delivered to every endpoint assigned to the group address named in the DstAddr
    /// parameter of the outgoing APSDE-DATA.request primitive on every device in the network
    /// for which there are such endpoints.
    /// Management of group membership on each device and endpoint is implemented by the APS,
    /// but the over-the-air messages that allow for remote management and commissioning of
    /// groups are defined here in the cluster library on the theory that, while the basic group
    /// addressing facilities are integral to the operation of the stack, not every device will
    /// need or want to implement this management cluster. Furthermore, the placement of the
    /// management commands here allows developers of proprietary profiles to avoid
    /// implementing the library cluster but still exploit group addressing
    /// In order to ensure that only authorized devices are able to set up groups (particularly
    /// if application link keys are to be used) the following approach should be employed. The
    /// security Permissions Configuration Table provides a mechanism by which certain
    /// commands can be restricted to specified authorized devices. Configuration of groups
    /// via the Groups cluster should use the ApplicationSettings permissions entry of this
    /// table to specify from which devices group configuration commands may be received, and
    /// whether a link key is required.
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

        // Attribute constants

        /// <summary>
        /// The most significant bit of the NameSupport attribute indicates whether or not
        /// group names are supported. A value of 1 indicates that they are supported, and a
        /// value of 0 indicates that they are not supported.
        /// </summary>
        public const ushort ATTR_NAMESUPPORT = 0x0000;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(1);

            attributeMap.Add(ATTR_NAMESUPPORT, new ZclAttribute(this, ATTR_NAMESUPPORT, "Name Support", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(4);

            commandMap.Add(0x0000, () => new AddGroupResponse());
            commandMap.Add(0x0001, () => new ViewGroupResponse());
            commandMap.Add(0x0002, () => new GetGroupMembershipResponse());
            commandMap.Add(0x0003, () => new RemoveGroupResponse());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(6);

            commandMap.Add(0x0000, () => new AddGroupCommand());
            commandMap.Add(0x0001, () => new ViewGroupCommand());
            commandMap.Add(0x0002, () => new GetGroupMembershipCommand());
            commandMap.Add(0x0003, () => new RemoveGroupCommand());
            commandMap.Add(0x0004, () => new RemoveAllGroupsCommand());
            commandMap.Add(0x0005, () => new AddGroupIfIdentifyingCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Groups cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclGroupsCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Add Group Command
        ///
        /// The Add Group command allows the sending device to add group membership in a
        /// particular group for one or more endpoints on the receiving device.
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="groupName" <see cref="string"> Group Name</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> AddGroupCommand(ushort groupId, string groupName)
        {
            AddGroupCommand command = new AddGroupCommand();

            // Set the fields
            command.GroupId = groupId;
            command.GroupName = groupName;

            return Send(command);
        }

        /// <summary>
        /// The View Group Command
        ///
        /// The view group command allows the sending device to request that the receiving
        /// entity or entities respond with a view group response command containing the
        /// application name string for a particular group.
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ViewGroupCommand(ushort groupId)
        {
            ViewGroupCommand command = new ViewGroupCommand();

            // Set the fields
            command.GroupId = groupId;

            return Send(command);
        }

        /// <summary>
        /// The Get Group Membership Command
        ///
        /// The get group membership command allows the sending device to inquire about the
        /// group membership of the receiving device and endpoint in a number of ways.
        ///
        /// <param name="groupList" <see cref="List<ushort>"> Group List</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetGroupMembershipCommand(List<ushort> groupList)
        {
            GetGroupMembershipCommand command = new GetGroupMembershipCommand();

            // Set the fields
            command.GroupList = groupList;

            return Send(command);
        }

        /// <summary>
        /// The Remove Group Command
        ///
        /// The remove group command allows the sender to request that the receiving entity or
        /// entities remove their membership, if any, in a particular group.
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RemoveGroupCommand(ushort groupId)
        {
            RemoveGroupCommand command = new RemoveGroupCommand();

            // Set the fields
            command.GroupId = groupId;

            return Send(command);
        }

        /// <summary>
        /// The Remove All Groups Command
        ///
        /// The remove all groups command allows the sending device to direct the receiving
        /// entity or entities to remove all group associations.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RemoveAllGroupsCommand()
        {
            return Send(new RemoveAllGroupsCommand());
        }

        /// <summary>
        /// The Add Group If Identifying Command
        ///
        /// The add group if identifying command allows the sending device to add group
        /// membership in a particular group for one or more endpoints on the receiving device,
        /// on condition that it is identifying itself. Identifying functionality is
        /// controlled using the identify cluster.
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="groupName" <see cref="string"> Group Name</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> AddGroupIfIdentifyingCommand(ushort groupId, string groupName)
        {
            AddGroupIfIdentifyingCommand command = new AddGroupIfIdentifyingCommand();

            // Set the fields
            command.GroupId = groupId;
            command.GroupName = groupName;

            return Send(command);
        }

        /// <summary>
        /// The Add Group Response
        ///
        /// The add group response is sent by the groups cluster server in response to an add
        /// group command.
        ///
        /// <param name="status" <see cref="ZclStatus"> Status</ param >
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> AddGroupResponse(ZclStatus status, ushort groupId)
        {
            AddGroupResponse command = new AddGroupResponse();

            // Set the fields
            command.Status = status;
            command.GroupId = groupId;

            return Send(command);
        }

        /// <summary>
        /// The View Group Response
        ///
        /// The view group response command is sent by the groups cluster server in response to a
        /// view group command.
        ///
        /// <param name="status" <see cref="ZclStatus"> Status</ param >
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="groupName" <see cref="string"> Group Name</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ViewGroupResponse(ZclStatus status, ushort groupId, string groupName)
        {
            ViewGroupResponse command = new ViewGroupResponse();

            // Set the fields
            command.Status = status;
            command.GroupId = groupId;
            command.GroupName = groupName;

            return Send(command);
        }

        /// <summary>
        /// The Get Group Membership Response
        ///
        /// The get group membership response command is sent by the groups cluster server in
        /// response to a get group membership command.
        ///
        /// <param name="capacity" <see cref="byte"> Capacity</ param >
        /// <param name="groupList" <see cref="List<ushort>"> Group List</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetGroupMembershipResponse(byte capacity, List<ushort> groupList)
        {
            GetGroupMembershipResponse command = new GetGroupMembershipResponse();

            // Set the fields
            command.Capacity = capacity;
            command.GroupList = groupList;

            return Send(command);
        }

        /// <summary>
        /// The Remove Group Response
        ///
        /// The remove group response command is generated by an application entity in
        /// response to the receipt of a remove group command.
        ///
        /// <param name="status" <see cref="ZclStatus"> Status</ param >
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RemoveGroupResponse(ZclStatus status, ushort groupId)
        {
            RemoveGroupResponse command = new RemoveGroupResponse();

            // Set the fields
            command.Status = status;
            command.GroupId = groupId;

            return Send(command);
        }
    }
}
