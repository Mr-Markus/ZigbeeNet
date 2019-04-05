// License text here

using System;
using System.Collections.Generic;
using System.Text;


namespace ZigBeeNet.ZCL.Clusters.IASZone
{
   /// <summary>
   /// Enumeration of IASZone attribute ZoneType options.
   ///
   /// Code is auto-generated. Modifications may be overwritten!
   ///
   /// </summary>
   public enum ZoneType
   {
       STANDARD_CIE = 0x0000,
       MOTION_SENSOR = 0x000D,
       CONTACT_SWITCH = 0x0015,
       FIRE_SENSOR = 0x0028,
       WATER_SENSOR = 0x002A,
       CO_SENSOR = 0x002B,
       PERSONAL_EMERGENCY_DEVICE = 0x002C,
       VIBRATION_MOVEMENT_SENSOR = 0x002D,
       REMOTE_CONTROL = 0x010F,
       KEY_FOB = 0x0115,
       KEY_PAD = 0x021D,
       STANDARD_WARNING_DEVICE = 0x0225,
       GLASS_BREAK_SENSOR = 0x0226,
       SECURITY_REPEATER = 0x0229
   }
}
