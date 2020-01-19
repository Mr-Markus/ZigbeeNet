
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Scenes;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Scenes cluster implementation (Cluster ID 0x0005).
    ///
    /// The scenes cluster provides attributes and commands for setting up and recalling
    /// scenes. Each scene corresponds to a set of stored values of specified attributes for one
    /// or more clusters on the same end point as the scenes cluster.
    /// In most cases scenes are associated with a particular group ID. Scenes may also exist
    /// without a group, in which case the value 0x0000 replaces the group ID. Note that extra
    /// care is required in these cases to avoid a scene ID collision, and that commands related
    /// to scenes without a group may only be unicast, i.e.: they may not be multicast or
    /// broadcast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclScenesCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0005;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Scenes";

        // Attribute constants

        /// <summary>
        /// The SceneCount attribute specifies the number of scenes currently in the device's
        /// scene table.
        /// </summary>
        public const ushort ATTR_SCENECOUNT = 0x0000;

        /// <summary>
        /// The CurrentScene attribute holds the Scene ID of the scene last invoked.
        /// </summary>
        public const ushort ATTR_CURRENTSCENE = 0x0001;

        /// <summary>
        /// The CurrentGroup attribute holds the Group ID of the scene last invoked, or 0x0000
        /// if the scene last invoked is not associated with a group.
        /// </summary>
        public const ushort ATTR_CURRENTGROUP = 0x0002;

        /// <summary>
        /// The SceneValid attribute indicates whether the state of the device corresponds to
        /// that associated with the CurrentScene and CurrentGroup attributes. TRUE
        /// indicates that these attributes are valid, FALSE indicates that they are not
        /// valid.
        /// Before a scene has been stored or recalled, this attribute is set to FALSE. After a
        /// successful Store Scene or Recall Scene command it is set to TRUE. If, after a scene is
        /// stored or recalled, the state of the device is modified, this attribute is set to
        /// FALSE.
        /// </summary>
        public const ushort ATTR_SCENEVALID = 0x0003;

        /// <summary>
        /// The most significant bit of the NameSupport attribute indicates whether or not
        /// scene names are supported. A value of 1 indicates that they are supported, and a
        /// value of 0 indicates that they are not supported.
        /// </summary>
        public const ushort ATTR_NAMESUPPORT = 0x0004;

        /// <summary>
        /// The LastConfiguredBy attribute is 64-bits in length and specifies the IEEE
        /// address of the device that last configured the scene table.
        /// The value 0xffffffffffffffff indicates that the device has not been configured,
        /// or that the address of the device that last configured the scenes cluster is not
        /// known.
        /// </summary>
        public const ushort ATTR_LASTCONFIGUREDBY = 0x0005;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(6);

            attributeMap.Add(ATTR_SCENECOUNT, new ZclAttribute(this, ATTR_SCENECOUNT, "Scene Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTSCENE, new ZclAttribute(this, ATTR_CURRENTSCENE, "Current Scene", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTGROUP, new ZclAttribute(this, ATTR_CURRENTGROUP, "Current Group", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_SCENEVALID, new ZclAttribute(this, ATTR_SCENEVALID, "Scene Valid", ZclDataType.Get(DataType.BOOLEAN), true, true, false, false));
            attributeMap.Add(ATTR_NAMESUPPORT, new ZclAttribute(this, ATTR_NAMESUPPORT, "Name Support", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_LASTCONFIGUREDBY, new ZclAttribute(this, ATTR_LASTCONFIGUREDBY, "Last Configured By", ZclDataType.Get(DataType.IEEE_ADDRESS), false, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(9);

            commandMap.Add(0x0000, () => new AddSceneResponse());
            commandMap.Add(0x0001, () => new ViewSceneResponse());
            commandMap.Add(0x0002, () => new RemoveSceneResponse());
            commandMap.Add(0x0003, () => new RemoveAllScenesResponse());
            commandMap.Add(0x0004, () => new StoreSceneResponse());
            commandMap.Add(0x0006, () => new GetSceneMembershipResponse());
            commandMap.Add(0x0040, () => new EnhancedAddSceneResponse());
            commandMap.Add(0x0041, () => new EnhancedViewSceneResponse());
            commandMap.Add(0x0042, () => new CopySceneResponse());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(10);

            commandMap.Add(0x0000, () => new AddSceneCommand());
            commandMap.Add(0x0001, () => new ViewSceneCommand());
            commandMap.Add(0x0002, () => new RemoveSceneCommand());
            commandMap.Add(0x0003, () => new RemoveAllScenesCommand());
            commandMap.Add(0x0004, () => new StoreSceneCommand());
            commandMap.Add(0x0005, () => new RecallSceneCommand());
            commandMap.Add(0x0006, () => new GetSceneMembershipCommand());
            commandMap.Add(0x0040, () => new EnhancedAddSceneCommand());
            commandMap.Add(0x0041, () => new EnhancedViewSceneCommand());
            commandMap.Add(0x0042, () => new CopySceneCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Scenes cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclScenesCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Add Scene Command
        ///
        /// The Add Scene command shall be addressed to a single device (not a group).
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <param name="sceneName" <see cref="string"> Scene Name</ param >
        /// <param name="extensionFieldSets" <see cref="List<ExtensionFieldSet>"> Extension Field Sets</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> AddSceneCommand(ushort groupId, byte sceneId, ushort transitionTime, string sceneName, List<ExtensionFieldSet> extensionFieldSets)
        {
            AddSceneCommand command = new AddSceneCommand();

            // Set the fields
            command.GroupId = groupId;
            command.SceneId = sceneId;
            command.TransitionTime = transitionTime;
            command.SceneName = sceneName;
            command.ExtensionFieldSets = extensionFieldSets;

            return Send(command);
        }

        /// <summary>
        /// The View Scene Command
        ///
        /// The View Scene command shall be addressed to a single device (not a group).
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ViewSceneCommand(ushort groupId, byte sceneId)
        {
            ViewSceneCommand command = new ViewSceneCommand();

            // Set the fields
            command.GroupId = groupId;
            command.SceneId = sceneId;

            return Send(command);
        }

        /// <summary>
        /// The Remove Scene Command
        ///
        /// The Remove All Scenes may be addressed to a single device or to a group.
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RemoveSceneCommand(ushort groupId, byte sceneId)
        {
            RemoveSceneCommand command = new RemoveSceneCommand();

            // Set the fields
            command.GroupId = groupId;
            command.SceneId = sceneId;

            return Send(command);
        }

        /// <summary>
        /// The Remove All Scenes Command
        ///
        /// The Remove All Scenes may be addressed to a single device or to a group.
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RemoveAllScenesCommand(ushort groupId)
        {
            RemoveAllScenesCommand command = new RemoveAllScenesCommand();

            // Set the fields
            command.GroupId = groupId;

            return Send(command);
        }

        /// <summary>
        /// The Store Scene Command
        ///
        /// The Store Scene command may be addressed to a single device or to a group.
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> StoreSceneCommand(ushort groupId, byte sceneId)
        {
            StoreSceneCommand command = new StoreSceneCommand();

            // Set the fields
            command.GroupId = groupId;
            command.SceneId = sceneId;

            return Send(command);
        }

        /// <summary>
        /// The Recall Scene Command
        ///
        /// The Recall Scene command may be addressed to a single device or to a group.
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RecallSceneCommand(ushort groupId, byte sceneId)
        {
            RecallSceneCommand command = new RecallSceneCommand();

            // Set the fields
            command.GroupId = groupId;
            command.SceneId = sceneId;

            return Send(command);
        }

        /// <summary>
        /// The Get Scene Membership Command
        ///
        /// The Get Scene Membership command can be used to find an unused scene number within
        /// the group when no commissioning tool is in the network, or for a commissioning tool
        /// to get used scenes for a group on a single device or on all devices in the group.
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetSceneMembershipCommand(ushort groupId)
        {
            GetSceneMembershipCommand command = new GetSceneMembershipCommand();

            // Set the fields
            command.GroupId = groupId;

            return Send(command);
        }

        /// <summary>
        /// The Enhanced Add Scene Command
        ///
        /// The Enhanced Add Scene command allows a scene to be added using a finer scene
        /// transition time than the Add Scene command.
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <param name="sceneName" <see cref="string"> Scene Name</ param >
        /// <param name="extensionFieldSets" <see cref="List<ExtensionFieldSet>"> Extension Field Sets</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> EnhancedAddSceneCommand(ushort groupId, byte sceneId, ushort transitionTime, string sceneName, List<ExtensionFieldSet> extensionFieldSets)
        {
            EnhancedAddSceneCommand command = new EnhancedAddSceneCommand();

            // Set the fields
            command.GroupId = groupId;
            command.SceneId = sceneId;
            command.TransitionTime = transitionTime;
            command.SceneName = sceneName;
            command.ExtensionFieldSets = extensionFieldSets;

            return Send(command);
        }

        /// <summary>
        /// The Enhanced View Scene Command
        ///
        /// The Enhanced View Scene command allows a scene to be retrieved using a finer scene
        /// transition time than the View Scene command.
        ///
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> EnhancedViewSceneCommand(ushort groupId, byte sceneId)
        {
            EnhancedViewSceneCommand command = new EnhancedViewSceneCommand();

            // Set the fields
            command.GroupId = groupId;
            command.SceneId = sceneId;

            return Send(command);
        }

        /// <summary>
        /// The Copy Scene Command
        ///
        /// The Copy Scene command allows a device to efficiently copy scenes from one
        /// group/scene identifier pair to another group/scene identifier pair.
        ///
        /// <param name="mode" <see cref="byte"> Mode</ param >
        /// <param name="groupIdFrom" <see cref="ushort"> Group ID From</ param >
        /// <param name="sceneIdFrom" <see cref="byte"> Scene ID From</ param >
        /// <param name="groupIdTo" <see cref="ushort"> Group ID To</ param >
        /// <param name="sceneIdTo" <see cref="byte"> Scene ID To</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> CopySceneCommand(byte mode, ushort groupIdFrom, byte sceneIdFrom, ushort groupIdTo, byte sceneIdTo)
        {
            CopySceneCommand command = new CopySceneCommand();

            // Set the fields
            command.Mode = mode;
            command.GroupIdFrom = groupIdFrom;
            command.SceneIdFrom = sceneIdFrom;
            command.GroupIdTo = groupIdTo;
            command.SceneIdTo = sceneIdTo;

            return Send(command);
        }

        /// <summary>
        /// The Add Scene Response
        ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> AddSceneResponse(byte status, ushort groupId, byte sceneId)
        {
            AddSceneResponse command = new AddSceneResponse();

            // Set the fields
            command.Status = status;
            command.GroupId = groupId;
            command.SceneId = sceneId;

            return Send(command);
        }

        /// <summary>
        /// The View Scene Response
        ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <param name="sceneName" <see cref="string"> Scene Name</ param >
        /// <param name="extensionFieldSets" <see cref="List<ExtensionFieldSet>"> Extension Field Sets</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ViewSceneResponse(byte status, ushort groupId, byte sceneId, ushort transitionTime, string sceneName, List<ExtensionFieldSet> extensionFieldSets)
        {
            ViewSceneResponse command = new ViewSceneResponse();

            // Set the fields
            command.Status = status;
            command.GroupId = groupId;
            command.SceneId = sceneId;
            command.TransitionTime = transitionTime;
            command.SceneName = sceneName;
            command.ExtensionFieldSets = extensionFieldSets;

            return Send(command);
        }

        /// <summary>
        /// The Remove Scene Response
        ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RemoveSceneResponse(byte status, ushort groupId, byte sceneId)
        {
            RemoveSceneResponse command = new RemoveSceneResponse();

            // Set the fields
            command.Status = status;
            command.GroupId = groupId;
            command.SceneId = sceneId;

            return Send(command);
        }

        /// <summary>
        /// The Remove All Scenes Response
        ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RemoveAllScenesResponse(byte status, ushort groupId)
        {
            RemoveAllScenesResponse command = new RemoveAllScenesResponse();

            // Set the fields
            command.Status = status;
            command.GroupId = groupId;

            return Send(command);
        }

        /// <summary>
        /// The Store Scene Response
        ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> StoreSceneResponse(byte status, ushort groupId, byte sceneId)
        {
            StoreSceneResponse command = new StoreSceneResponse();

            // Set the fields
            command.Status = status;
            command.GroupId = groupId;
            command.SceneId = sceneId;

            return Send(command);
        }

        /// <summary>
        /// The Get Scene Membership Response
        ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="capacity" <see cref="byte"> Capacity</ param >
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneCount" <see cref="byte"> Scene Count</ param >
        /// <param name="sceneList" <see cref="List<byte>"> Scene List</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetSceneMembershipResponse(byte status, byte capacity, ushort groupId, byte sceneCount, List<byte> sceneList)
        {
            GetSceneMembershipResponse command = new GetSceneMembershipResponse();

            // Set the fields
            command.Status = status;
            command.Capacity = capacity;
            command.GroupId = groupId;
            command.SceneCount = sceneCount;
            command.SceneList = sceneList;

            return Send(command);
        }

        /// <summary>
        /// The Enhanced Add Scene Response
        ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> EnhancedAddSceneResponse(byte status, ushort groupId, byte sceneId)
        {
            EnhancedAddSceneResponse command = new EnhancedAddSceneResponse();

            // Set the fields
            command.Status = status;
            command.GroupId = groupId;
            command.SceneId = sceneId;

            return Send(command);
        }

        /// <summary>
        /// The Enhanced View Scene Response
        ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <param name="transitionTime" <see cref="ushort"> Transition Time</ param >
        /// <param name="sceneName" <see cref="string"> Scene Name</ param >
        /// <param name="extensionFieldSets" <see cref="List<ExtensionFieldSet>"> Extension Field Sets</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> EnhancedViewSceneResponse(byte status, ushort groupId, byte sceneId, ushort transitionTime, string sceneName, List<ExtensionFieldSet> extensionFieldSets)
        {
            EnhancedViewSceneResponse command = new EnhancedViewSceneResponse();

            // Set the fields
            command.Status = status;
            command.GroupId = groupId;
            command.SceneId = sceneId;
            command.TransitionTime = transitionTime;
            command.SceneName = sceneName;
            command.ExtensionFieldSets = extensionFieldSets;

            return Send(command);
        }

        /// <summary>
        /// The Copy Scene Response
        ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="groupId" <see cref="ushort"> Group ID</ param >
        /// <param name="sceneId" <see cref="byte"> Scene ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> CopySceneResponse(byte status, ushort groupId, byte sceneId)
        {
            CopySceneResponse command = new CopySceneResponse();

            // Set the fields
            command.Status = status;
            command.GroupId = groupId;
            command.SceneId = sceneId;

            return Send(command);
        }
    }
}
