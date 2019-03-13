// License text here

using System;
using System.Collections.Generic;
using System.Text;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
   /// <summary>
   /// Enumeration of IASACE attribute Arm Notification options.
   ///
   /// Code is auto-generated. Modifications may be overwritten!
   ///
   /// </summary>
   public enum ArmNotification
   {
       ALL_ZONES_DISARMED = 0x0000,
       DAY_ZONES_ARMED = 0x0001,
       NIGHT_ZONES_ARMED = 0x0002,
       ALL_ZONES_ARMED = 0x0003,
       INVALID_ARM_CODE = 0x0004,
       NOT_READY_TO_ARM = 0x0005,
       ALREADY_DISARMED = 0x0006
   }
}
