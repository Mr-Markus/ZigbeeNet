using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL
{
    public abstract class ZclCommand : ZigBeeCommand
    {
        /// <summary>
        /// True if this is a generic command
        /// </summary>
        internal bool GenericCommand { get; set; }

        /// <summary>
        /// The command ID
        /// </summary>
        internal byte CommandId { get; set; }

        /// <summary>
        /// The command direction for this command.
        /// <p>
        /// If this command is to be sent <b>to</b> the server, this will return <i>true</i>.
        /// If this command is to be sent <b>from</b> the server, this will return <i>false</i>.
        /// </summary>
        internal ZclCommandDirection CommandDirection { get; set; }

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
