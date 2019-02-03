using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL
{
    public abstract class ZclCommand : ZigBeeCommand
    {

        /**
         * True if this is a generic command
         */
        public bool GenericCommand { get; set; }

        /**
         * The command ID
         */
        public byte CommandId { get; set; }

        /**
         * The command direction for this command.
         * <p>
         * If this command is to be sent <b>to</b> the server, this will return <i>true</i>.
         * If this command is to be sent <b>from</b> the server, this will return <i>false</i>.
         */
        public ZclCommandDirection CommandDirection { get; set; }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append(ZclClusterType.GetValueById(ClusterId).Label)
                .Append(": ")
                .Append(base.ToString());

            return builder.ToString();
        }
    }
}
