using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /**
    * System Server Discovery Request value object class.
    * 
    * The System_Server_Discovery_req is generated from a Local Device wishing to
    * discover the location of a particular system server or servers as indicated by the
    * ServerMask parameter. The destination addressing on this request is "broadcast to
    * all devices for which macRxOnWhenIdle = TRUE".
    * 
    */

    public class SystemServerDiscoveryRequest : ZdoRequest
    {
        /**
        * Default constructor.
        */
        public SystemServerDiscoveryRequest()
        {
            ClusterId = 0x0015;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("SystemServerDiscoveryRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }
    }
}
