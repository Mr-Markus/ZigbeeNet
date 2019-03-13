// License text here

using System;
using System.Collections.Generic;
using System.Text;


namespace ZigBeeNet.ZCL.Clusters.Powerconfiguration
{
   /// <summary>
   /// Enumeration of Powerconfiguration attribute BatterySize options.
   ///
   /// Code is auto-generated. Modifications may be overwritten!
   ///
   /// </summary>
   public enum BatterySize
   {
       NO_BATTERY = 0x0000,
       BUILD_IN = 0x0001,
       OTHER = 0x0002,
       AA__CELL = 0x0003,
       AAA_CELL = 0x0004,
       C_CELL = 0x0005,
       D_CELL = 0x0006,
       CR2_CELL = 0x0007,
       CR123A_CELL = 0x0008,
       UNKNOWN = 0x00FF
   }
}
