using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.ZDO
{
    public class NodeDescRequest : ZpiObject
    {
        public NodeDescRequest()
            : base(CommandType.ZDO_NODE_DESC_REQ)
        {

        }

        public ushort DestinationAddress
        {
            get
            {
                return (ushort)RequestArguments["dstaddr"];
            }
            set
            {
                RequestArguments["dstaddr"] = value;
            }
        }

        public ushort NetworkAddressOfInteresst
        {
            get
            {
                return (ushort)RequestArguments["nwkaddrofinterest"];
            }
            set
            {
                RequestArguments["nwkaddrofinterest"] = value;
            }
        }
    }
}
