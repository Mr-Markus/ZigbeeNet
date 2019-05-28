// License text here

using System;
using System.Collections.Generic;
using System.Text;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
   /// <summary>
   /// Enumeration of IASACE attribute Panel Status options.
   ///
   /// Code is auto-generated. Modifications may be overwritten!
   ///
   /// </summary>
   public enum PanelStatus
   {
       PANEL_DISARMED = 0x0000,
       ARMED_STAY = 0x0001,
       ARMED_NIGHT = 0x0002,
       ARMED_AWAY = 0x0003,
       EXIT_DELAY = 0x0004,
       ENTRY_DELAY = 0x0005,
       NOT_READY_TO_ARM = 0x0006,
       IN_ALARM = 0x0007,
       ARMING_STAY = 0x0008,
       ARMING_NIGHT = 0x0009,
       ARMING_AWAY = 0x000A
   }
}
