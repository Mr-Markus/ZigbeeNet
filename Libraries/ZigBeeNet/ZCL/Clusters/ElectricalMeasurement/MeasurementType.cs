// License text here

using System;
using System.Collections.Generic;
using System.Text;


namespace ZigBeeNet.ZCL.Clusters.ElectricalMeasurement
{
   /// <summary>
   /// Enumeration of ElectricalMeasurement attribute MeasurementType options.
   ///
   /// Code is auto-generated. Modifications may be overwritten!
   ///
   /// </summary>
   public enum MeasurementType
   {
       AC_ACTIVE_MEASUREMENT = 0x0000,
       AC_REACTIVE_MEASUREMENT = 0x0001,
       AC_APPARENT_MEASUREMENT = 0x0002,
       PHASE_A_MEASUREMENT = 0x0004,
       PHASE_B_MEASUREMENT = 0x0008,
       PHASE_C_MEASUREMENT = 0x0010,
       DC_MEASUREMENT = 0x0020,
       HARMONICS_MEASUREMENT = 0x0040,
       POWER_QUALITY_MEASUREMENT = 0x0080
   }
}
