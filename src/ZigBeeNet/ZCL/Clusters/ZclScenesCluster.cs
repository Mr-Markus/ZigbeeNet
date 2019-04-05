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
using ZigBeeNet.ZCL.Clusters.Scenes;

namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Scenescluster implementation (Cluster ID 0x0005).
    ///
    /// The scenes cluster provides attributes and commands for setting up and recalling
    /// scenes. Each scene corresponds to a set of stored values of specified attributes for
    /// one or more clusters on the same end point as the scenes cluster.
    /// <p>
    /// In most cases scenes are associated with a particular group ID. Scenes may also
    /// exist without a group, in which case the value 0x0000 replaces the group ID. Note
    /// that extra care is required in these cases to avoid a scene ID collision, and that
    /// commands related to scenes without a group may only be unicast, i.e.: they may
    /// not be multicast or broadcast.
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

        /* Attribute constants */

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
        /// The CurrentGroup attribute holds the Group ID of the scene last invoked, or
        /// 0x0000 if the scene last invoked is not associated with a group.
        /// </summary>
        public const ushort ATTR_CURRENTGROUP = 0x0002;

        /// <summary>
        /// The SceneValid attribute indicates whether the state of the device corresponds to
        /// that associated with the CurrentScene and CurrentGroup attributes. TRUE
        /// indicates that these attributes are valid, FALSE indicates that they are not valid.
        /// 
        /// Before a scene has been stored or recalled, this attribute is set to FALSE. After a
        /// successful Store Scene or Recall Scene command it is set to TRUE. If, after a
        /// scene is stored or recalled, the state of the device is modified, this attribute is set to
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
        /// The LastConfiguredBy attribute is 64-bits in length and specifies the IEEE address
        /// of the device that last configured the scene table.
        /// 
        /// The value 0xffffffffffffffff indicates that the device has not been configured, or
        /// that the address of the device that last configured the scenes cluster is not known.
        /// </summary>
        public const ushort ATTR_LASTCONFIGUREDBY = 0x0005;


        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(6);

            ZclClusterType scenes = ZclClusterType.GetValueById(ClusterType.SCENES);

            attributeMap.Add(ATTR_SCENECOUNT, new ZclAttribute(scenes, ATTR_SCENECOUNT, "SceneCount", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTSCENE, new ZclAttribute(scenes, ATTR_CURRENTSCENE, "CurrentScene", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTGROUP, new ZclAttribute(scenes, ATTR_CURRENTGROUP, "CurrentGroup", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_SCENEVALID, new ZclAttribute(scenes, ATTR_SCENEVALID, "SceneValid", ZclDataType.Get(DataType.BOOLEAN), true, true, false, false));
            attributeMap.Add(ATTR_NAMESUPPORT, new ZclAttribute(scenes, ATTR_NAMESUPPORT, "NameSupport", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_LASTCONFIGUREDBY, new ZclAttribute(scenes, ATTR_LASTCONFIGUREDBY, "LastConfiguredBy", ZclDataType.Get(DataType.IEEE_ADDRESS), false, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Scenes cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclScenesCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// Get the SceneCount attribute [attribute ID0].
        ///
        /// The SceneCount attribute specifies the number of scenes currently in the device's
        /// scene table.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetSceneCountAsync()
        {
            return Read(_attributes[ATTR_SCENECOUNT]);
        }

        /// <summary>
        /// Synchronously Get the SceneCount attribute [attribute ID0].
        ///
        /// The SceneCount attribute specifies the number of scenes currently in the device's
        /// scene table.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetSceneCount(long refreshPeriod)
        {
            if (_attributes[ATTR_SCENECOUNT].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_SCENECOUNT].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_SCENECOUNT]);
        }


        /// <summary>
        /// Get the CurrentScene attribute [attribute ID1].
        ///
        /// The CurrentScene attribute holds the Scene ID of the scene last invoked.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetCurrentSceneAsync()
        {
            return Read(_attributes[ATTR_CURRENTSCENE]);
        }

        /// <summary>
        /// Synchronously Get the CurrentScene attribute [attribute ID1].
        ///
        /// The CurrentScene attribute holds the Scene ID of the scene last invoked.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetCurrentScene(long refreshPeriod)
        {
            if (_attributes[ATTR_CURRENTSCENE].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_CURRENTSCENE].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_CURRENTSCENE]);
        }


        /// <summary>
        /// Get the CurrentGroup attribute [attribute ID2].
        ///
        /// The CurrentGroup attribute holds the Group ID of the scene last invoked, or
        /// 0x0000 if the scene last invoked is not associated with a group.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetCurrentGroupAsync()
        {
            return Read(_attributes[ATTR_CURRENTGROUP]);
        }

        /// <summary>
        /// Synchronously Get the CurrentGroup attribute [attribute ID2].
        ///
        /// The CurrentGroup attribute holds the Group ID of the scene last invoked, or
        /// 0x0000 if the scene last invoked is not associated with a group.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetCurrentGroup(long refreshPeriod)
        {
            if (_attributes[ATTR_CURRENTGROUP].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_CURRENTGROUP].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_CURRENTGROUP]);
        }


        /// <summary>
        /// Get the SceneValid attribute [attribute ID3].
        ///
        /// The SceneValid attribute indicates whether the state of the device corresponds to
        /// that associated with the CurrentScene and CurrentGroup attributes. TRUE
        /// indicates that these attributes are valid, FALSE indicates that they are not valid.
        /// 
        /// Before a scene has been stored or recalled, this attribute is set to FALSE. After a
        /// successful Store Scene or Recall Scene command it is set to TRUE. If, after a
        /// scene is stored or recalled, the state of the device is modified, this attribute is set to
        /// FALSE.
        ///
        /// The attribute is of type bool.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetSceneValidAsync()
        {
            return Read(_attributes[ATTR_SCENEVALID]);
        }

        /// <summary>
        /// Synchronously Get the SceneValid attribute [attribute ID3].
        ///
        /// The SceneValid attribute indicates whether the state of the device corresponds to
        /// that associated with the CurrentScene and CurrentGroup attributes. TRUE
        /// indicates that these attributes are valid, FALSE indicates that they are not valid.
        /// 
        /// Before a scene has been stored or recalled, this attribute is set to FALSE. After a
        /// successful Store Scene or Recall Scene command it is set to TRUE. If, after a
        /// scene is stored or recalled, the state of the device is modified, this attribute is set to
        /// FALSE.
        ///
        /// The attribute is of type bool.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public bool GetSceneValid(long refreshPeriod)
        {
            if (_attributes[ATTR_SCENEVALID].IsLastValueCurrent(refreshPeriod))
            {
                return (bool)_attributes[ATTR_SCENEVALID].LastValue;
            }

            return (bool)ReadSync(_attributes[ATTR_SCENEVALID]);
        }


        /// <summary>
        /// Get the NameSupport attribute [attribute ID4].
        ///
        /// The most significant bit of the NameSupport attribute indicates whether or not
        /// scene names are supported. A value of 1 indicates that they are supported, and a
        /// value of 0 indicates that they are not supported.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetNameSupportAsync()
        {
            return Read(_attributes[ATTR_NAMESUPPORT]);
        }

        /// <summary>
        /// Synchronously Get the NameSupport attribute [attribute ID4].
        ///
        /// The most significant bit of the NameSupport attribute indicates whether or not
        /// scene names are supported. A value of 1 indicates that they are supported, and a
        /// value of 0 indicates that they are not supported.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetNameSupport(long refreshPeriod)
        {
            if (_attributes[ATTR_NAMESUPPORT].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_NAMESUPPORT].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_NAMESUPPORT]);
        }


        /// <summary>
        /// Get the LastConfiguredBy attribute [attribute ID5].
        ///
        /// The LastConfiguredBy attribute is 64-bits in length and specifies the IEEE address
        /// of the device that last configured the scene table.
        /// 
        /// The value 0xffffffffffffffff indicates that the device has not been configured, or
        /// that the address of the device that last configured the scenes cluster is not known.
        ///
        /// The attribute is of type IeeeAddress.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetLastConfiguredByAsync()
        {
            return Read(_attributes[ATTR_LASTCONFIGUREDBY]);
        }

        /// <summary>
        /// Synchronously Get the LastConfiguredBy attribute [attribute ID5].
        ///
        /// The LastConfiguredBy attribute is 64-bits in length and specifies the IEEE address
        /// of the device that last configured the scene table.
        /// 
        /// The value 0xffffffffffffffff indicates that the device has not been configured, or
        /// that the address of the device that last configured the scenes cluster is not known.
        ///
        /// The attribute is of type IeeeAddress.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public IeeeAddress GetLastConfiguredBy(long refreshPeriod)
        {
            if (_attributes[ATTR_LASTCONFIGUREDBY].IsLastValueCurrent(refreshPeriod))
            {
                return (IeeeAddress)_attributes[ATTR_LASTCONFIGUREDBY].LastValue;
            }

            return (IeeeAddress)ReadSync(_attributes[ATTR_LASTCONFIGUREDBY]);
        }


        /// <summary>
        /// The Add Scene Command
        ///
        /// The Add Scene command shall be addressed to a single device (not a group).
        ///
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="sceneID"><see cref="byte"/> Scene ID</param>
        /// <param name="transitionTime"><see cref="ushort"/> Transition time</param>
        /// <param name="sceneName"><see cref="string"/> Scene Name</param>
        /// <param name="extensionFieldSets"><see cref="List<ExtensionFieldSet>"/> Extension field sets</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> AddSceneCommand(ushort groupID, byte sceneID, ushort transitionTime, string sceneName, List<ExtensionFieldSet> extensionFieldSets)
        {
            AddSceneCommand command = new AddSceneCommand();

            // Set the fields
            command.GroupID = groupID;
            command.SceneID = sceneID;
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
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="sceneID"><see cref="byte"/> Scene ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ViewSceneCommand(ushort groupID, byte sceneID)
        {
            ViewSceneCommand command = new ViewSceneCommand();

            // Set the fields
            command.GroupID = groupID;
            command.SceneID = sceneID;

            return Send(command);
        }

        /// <summary>
        /// The Remove Scene Command
        ///
        /// The Remove All Scenes may be addressed to a single device or to a group.
        ///
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="sceneID"><see cref="byte"/> Scene ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> RemoveSceneCommand(ushort groupID, byte sceneID)
        {
            RemoveSceneCommand command = new RemoveSceneCommand();

            // Set the fields
            command.GroupID = groupID;
            command.SceneID = sceneID;

            return Send(command);
        }

        /// <summary>
        /// The Remove All Scenes Command
        ///
        /// The Remove All Scenes may be addressed to a single device or to a group.
        ///
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> RemoveAllScenesCommand(ushort groupID)
        {
            RemoveAllScenesCommand command = new RemoveAllScenesCommand();

            // Set the fields
            command.GroupID = groupID;

            return Send(command);
        }

        /// <summary>
        /// The Store Scene Command
        ///
        /// The Store Scene command may be addressed to a single device or to a group.
        ///
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="sceneID"><see cref="byte"/> Scene ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> StoreSceneCommand(ushort groupID, byte sceneID)
        {
            StoreSceneCommand command = new StoreSceneCommand();

            // Set the fields
            command.GroupID = groupID;
            command.SceneID = sceneID;

            return Send(command);
        }

        /// <summary>
        /// The Recall Scene Command
        ///
        /// The Recall Scene command may be addressed to a single device or to a group.
        ///
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="sceneID"><see cref="byte"/> Scene ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> RecallSceneCommand(ushort groupID, byte sceneID)
        {
            RecallSceneCommand command = new RecallSceneCommand();

            // Set the fields
            command.GroupID = groupID;
            command.SceneID = sceneID;

            return Send(command);
        }

        /// <summary>
        /// The Get Scene Membership Command
        ///
        /// The Get Scene Membership command can be used to find an unused scene
        /// number within the group when no commissioning tool is in the network, or for a
        /// commissioning tool to get used scenes for a group on a single device or on all
        /// devices in the group.
        ///
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetSceneMembershipCommand(ushort groupID)
        {
            GetSceneMembershipCommand command = new GetSceneMembershipCommand();

            // Set the fields
            command.GroupID = groupID;

            return Send(command);
        }

        /// <summary>
        /// The Add Scene Response
        ///
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="sceneID"><see cref="byte"/> Scene ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> AddSceneResponse(byte status, ushort groupID, byte sceneID)
        {
            AddSceneResponse command = new AddSceneResponse();

            // Set the fields
            command.Status = status;
            command.GroupID = groupID;
            command.SceneID = sceneID;

            return Send(command);
        }

        /// <summary>
        /// The View Scene Response
        ///
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="sceneID"><see cref="byte"/> Scene ID</param>
        /// <param name="transitionTime"><see cref="ushort"/> Transition time</param>
        /// <param name="sceneName"><see cref="string"/> Scene Name</param>
        /// <param name="extensionFieldSets"><see cref="List<ExtensionFieldSet>"/> Extension field sets</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ViewSceneResponse(byte status, ushort groupID, byte sceneID, ushort transitionTime, string sceneName, List<ExtensionFieldSet> extensionFieldSets)
        {
            ViewSceneResponse command = new ViewSceneResponse();

            // Set the fields
            command.Status = status;
            command.GroupID = groupID;
            command.SceneID = sceneID;
            command.TransitionTime = transitionTime;
            command.SceneName = sceneName;
            command.ExtensionFieldSets = extensionFieldSets;

            return Send(command);
        }

        /// <summary>
        /// The Remove Scene Response
        ///
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="sceneID"><see cref="byte"/> Scene ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> RemoveSceneResponse(byte status, ushort groupID, byte sceneID)
        {
            RemoveSceneResponse command = new RemoveSceneResponse();

            // Set the fields
            command.Status = status;
            command.GroupID = groupID;
            command.SceneID = sceneID;

            return Send(command);
        }

        /// <summary>
        /// The Remove All Scenes Response
        ///
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> RemoveAllScenesResponse(byte status, ushort groupID)
        {
            RemoveAllScenesResponse command = new RemoveAllScenesResponse();

            // Set the fields
            command.Status = status;
            command.GroupID = groupID;

            return Send(command);
        }

        /// <summary>
        /// The Store Scene Response
        ///
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="sceneID"><see cref="byte"/> Scene ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> StoreSceneResponse(byte status, ushort groupID, byte sceneID)
        {
            StoreSceneResponse command = new StoreSceneResponse();

            // Set the fields
            command.Status = status;
            command.GroupID = groupID;
            command.SceneID = sceneID;

            return Send(command);
        }

        /// <summary>
        /// The Get Scene Membership Response
        ///
        /// <param name="status"><see cref="byte"/> Status</param>
        /// <param name="capacity"><see cref="byte"/> Capacity</param>
        /// <param name="groupID"><see cref="ushort"/> Group ID</param>
        /// <param name="sceneCount"><see cref="byte"/> Scene count</param>
        /// <param name="sceneList"><see cref="List<byte>"/> Scene list</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetSceneMembershipResponse(byte status, byte capacity, ushort groupID, byte sceneCount, List<byte> sceneList)
        {
            GetSceneMembershipResponse command = new GetSceneMembershipResponse();

            // Set the fields
            command.Status = status;
            command.Capacity = capacity;
            command.GroupID = groupID;
            command.SceneCount = sceneCount;
            command.SceneList = sceneList;

            return Send(command);
        }

        public override ZclCommand GetCommandFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // ADD_SCENE_COMMAND
                    return new AddSceneCommand();
                case 1: // VIEW_SCENE_COMMAND
                    return new ViewSceneCommand();
                case 2: // REMOVE_SCENE_COMMAND
                    return new RemoveSceneCommand();
                case 3: // REMOVE_ALL_SCENES_COMMAND
                    return new RemoveAllScenesCommand();
                case 4: // STORE_SCENE_COMMAND
                    return new StoreSceneCommand();
                case 5: // RECALL_SCENE_COMMAND
                    return new RecallSceneCommand();
                case 6: // GET_SCENE_MEMBERSHIP_COMMAND
                    return new GetSceneMembershipCommand();
                    default:
                        return null;
            }
        }

        public ZclCommand getResponseFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // ADD_SCENE_RESPONSE
                    return new AddSceneResponse();
                case 1: // VIEW_SCENE_RESPONSE
                    return new ViewSceneResponse();
                case 2: // REMOVE_SCENE_RESPONSE
                    return new RemoveSceneResponse();
                case 3: // REMOVE_ALL_SCENES_RESPONSE
                    return new RemoveAllScenesResponse();
                case 4: // STORE_SCENE_RESPONSE
                    return new StoreSceneResponse();
                case 5: // GET_SCENE_MEMBERSHIP_RESPONSE
                    return new GetSceneMembershipResponse();
                    default:
                        return null;
            }
        }
    }
}
