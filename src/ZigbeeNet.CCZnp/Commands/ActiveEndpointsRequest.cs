using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class ActiveEndpointsRequest : ZpiSREQ
    {
        public ActiveEndpointsRequest()
            : base(ZDO.activeEpReq)
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
