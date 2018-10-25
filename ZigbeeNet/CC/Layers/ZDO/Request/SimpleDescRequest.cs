using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.ZDO
{
    public class SimpleDescRequest : ZpiObject
    {
        public SimpleDescRequest()
            : base(CommandType.ZDO_SIMPLE_DESC_REQ)
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

        public byte Endpoint
        {
            get
            {
                return (byte)RequestArguments["endpoint"];
            }
            set
            {
                RequestArguments["endpoint"] = value;
            }
        }
    }
}
