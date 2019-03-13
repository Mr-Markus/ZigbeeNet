// License text here

using System;
using System.Collections.Generic;
using System.Text;


namespace ZigBeeNet.ZCL.Clusters.Basic
{
   /// <summary>
   /// Enumeration of Basic attribute PowerSource options.
   ///
   /// Code is auto-generated. Modifications may be overwritten!
   ///
   /// </summary>
   public enum PowerSource
   {
       UNKNOWN = 0x0000,
       MAINS_SINGLE_PHASE = 0x0001,
       MAINS_THREE_PHASE = 0x0002,
       BATTERY = 0x0003,
       DC_SOURCE = 0x0004,
       EMERGENCY_MAINS_CONSTANT = 0x0005,
       EMERGENCY_MAINS_CHANGEOVER = 0x0006
   }
}
