using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
   /// Bind Register value object class.
   /// 
   /// The Bind_Register_req is generated from a Local Device and sent to a primary
   /// binding table cache device to register that the local device wishes to hold its own
   /// binding table entries. The destination addressing mode for this request is unicast.
   /// </summary>
    public class BindRegister : ZdoResponse
    {
        /// <summary>
         /// Default constructor.
         /// </summary>
        public BindRegister()
        {
            ClusterId = 0x0023;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("BindRegister [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }

    }
}