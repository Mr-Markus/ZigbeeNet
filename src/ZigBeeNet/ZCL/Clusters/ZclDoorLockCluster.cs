﻿// License text here

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
using ZigBeeNet.ZCL.Clusters.DoorLock;

<summary>
Door Lockcluster implementation (Cluster ID 0x0101).
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>
namespace ZigBeeNet.ZCL.Clusters
{
   public class ZclDoorLockCluster : ZclCluster
   {
       <summary>
        The ZigBee Cluster Library Cluster ID
       </summary>
       public const ushort CLUSTER_ID = 0x0101;

       <summary>
        The ZigBee Cluster Library Cluster Name
       </summary>
       public const string CLUSTER_NAME = "Door Lock";

       // Attribute initialisation
       protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
       {
           Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

           return attributeMap;
       }

        Default constructor to create a Door Lock cluster.
       
       <param name= zigbeeEndpoint the {@link ZigBeeEndpoint}
       </param>
       public ZclDoorLockCluster(ZigBeeEndpoint zigbeeEndpoint)
           : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
       {
       }


       <summary>
        The Lock Door Command
       </summary>
       <param name= pinCode {@link ByteArray} Pin code</param>
       <returns the Task<CommandResult> command result Task
       </returns>
       public Task<CommandResult> LockDoorCommand(ByteArray pinCode)
       {
           LockDoorCommand command = new LockDoorCommand();

           // Set the fields
           command.PinCode = pinCode;

           return Send(command);
       }

       <summary>
        The Unlock Door Command
       </summary>
       <param name= pinCode {@link ByteArray} Pin code</param>
       <returns the Task<CommandResult> command result Task
       </returns>
       public Task<CommandResult> UnlockDoorCommand(ByteArray pinCode)
       {
           UnlockDoorCommand command = new UnlockDoorCommand();

           // Set the fields
           command.PinCode = pinCode;

           return Send(command);
       }

       <summary>
        The Lock Door Response
       </summary>
       <param name= status {@link byte} Status</param>
       <returns the Task<CommandResult> command result Task
       </returns>
       public Task<CommandResult> LockDoorResponse(byte status)
       {
           LockDoorResponse command = new LockDoorResponse();

           // Set the fields
           command.Status = status;

           return Send(command);
       }

       <summary>
        The Unlock Door Response
       </summary>
       <param name= status {@link byte} Status</param>
       <returns the Task<CommandResult> command result Task
       </returns>
       public Task<CommandResult> UnlockDoorResponse(byte status)
       {
           UnlockDoorResponse command = new UnlockDoorResponse();

           // Set the fields
           command.Status = status;

           return Send(command);
       }

       public override ZclCommand GetCommandFromId(int commandId)
       {
           switch (commandId)
           {
               case 0: // LOCK_DOOR_COMMAND
                   return new LockDoorCommand();
               case 1: // UNLOCK_DOOR_COMMAND
                   return new UnlockDoorCommand();
                   default:
                       return null;
           }
       }

       public ZclCommand getResponseFromId(int commandId)
       {
           switch (commandId)
           {
               case 0: // LOCK_DOOR_RESPONSE
                   return new LockDoorResponse();
               case 1: // UNLOCK_DOOR_RESPONSE
                   return new UnlockDoorResponse();
                   default:
                       return null;
           }
       }
   }
}
