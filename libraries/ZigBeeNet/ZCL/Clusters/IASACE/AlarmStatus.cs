// License text here

using System;
using System.Collections.Generic;
using System.Text;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
   /// <summary>
   /// Enumeration of IASACE attribute Alarm Status options.
   ///
   /// Code is auto-generated. Modifications may be overwritten!
   ///
   /// </summary>
   public enum AlarmStatus
   {
       NO_ALARM = 0x0000,
       BURGLAR = 0x0001,
       FIRE = 0x0002,
       EMERGENCY = 0x0003,
       POLICE_PANIC = 0x0004,
       FIRE_PANIC = 0x0005,
       EMERGENCY_PANIC = 0x0006
   }
}
