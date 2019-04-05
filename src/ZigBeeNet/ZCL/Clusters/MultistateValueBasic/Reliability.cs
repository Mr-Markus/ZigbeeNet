// License text here

using System;
using System.Collections.Generic;
using System.Text;


namespace ZigBeeNet.ZCL.Clusters.MultistateValueBasic
{
   /// <summary>
   /// Enumeration of MultistateValueBasic attribute Reliability options.
   ///
   /// Code is auto-generated. Modifications may be overwritten!
   ///
   /// </summary>
   public enum Reliability
   {
       NO_FAULT_DETECTED = 0x0000,
       OVER_RANGE = 0x0002,
       UNDER_RANGE = 0x0003,
       OPEN_LOOP = 0x0004,
       SHORTED_LOOP = 0x0005,
       UNRELIABLE_OTHER = 0x0007,
       PROCESS_ERROR = 0x0008,
       MULTI_STATE_FAULT = 0x0009,
       CONFIGURATION_ERROR = 0x000A
   }
}
