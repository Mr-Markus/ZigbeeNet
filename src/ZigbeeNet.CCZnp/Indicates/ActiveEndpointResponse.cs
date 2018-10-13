using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class ActiveEndpointResponse : ZpiObject
    {
        public ActiveEndpointResponse(ZpiObject zpi)
            : base(ZDO.activeEpRsp)
        {
            RequestArguments = zpi.RequestArguments;
        }

        public ActiveEndpointResponse()
            : base (ZDO.activeEpRsp)
        {

        }

        public ushort SourceAddress
        {
            get
            {
                return (ushort)RequestArguments["srcaddr"];
            }
            set
            {
                RequestArguments["srcaddr"] = value;
            }
        }

        public ZpiStatus Status
        {
            get
            {
                return (ZpiStatus)RequestArguments["status"];
            }
            set
            {
                RequestArguments["status"] = value;
            }
        }

        public ushort NetworkAdressOfInterest
        {
            get
            {
                return (ushort)RequestArguments["nwkaddr"];
            }
            set
            {
                RequestArguments["nwkaddr"] = value;
            }
        }

        public byte EndpointCount
        {
            get
            {
                return (byte)RequestArguments["activeepcount"];
            }
            set
            {
                RequestArguments["activeepcount"] = value;
            }
        }

        public byte[] Endpoints
        {
            get
            {
                return (byte[])RequestArguments["activeeplist"];
            }
            set
            {
                RequestArguments["activeeplist"] = value;
            }
        }
    }
}
