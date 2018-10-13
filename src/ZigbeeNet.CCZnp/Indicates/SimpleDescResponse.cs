using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class SimpleDescResponse : ZpiObject
    {
        public SimpleDescResponse(ZpiObject zpi)
            : base(ZDO.simpleDescRsp)
        {
            RequestArguments = zpi.RequestArguments;
        }

        public SimpleDescResponse()
            : base (ZDO.simpleDescRsp)
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

        public byte Length
        {
            get
            {
                return (byte)RequestArguments["len"];
            }
            set
            {
                RequestArguments["len"] = value;
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

        public ushort ProfileId
        {
            get
            {
                return (ushort)RequestArguments["profileid"];
            }
            set
            {
                RequestArguments["profileid"] = value;
            }
        }

        public ushort DeviceId
        {
            get
            {
                return (ushort)RequestArguments["deviceid"];
            }
            set
            {
                RequestArguments["deviceid"] = value;
            }
        }

        public byte DeviceVersion
        {
            get
            {
                return (byte)RequestArguments["deviceversion"];
            }
            set
            {
                RequestArguments["deviceversion"] = value;
            }
        }

        public byte[] ClusterIn
        {
            get
            {
                return (byte[])RequestArguments["inclusterlist"];
            }
            set
            {
                RequestArguments["inclusterlist"] = value;
            }
        }

        public byte[] ClusterOut
        {
            get
            {
                return (byte[])RequestArguments["outclusterlist"];
            }
            set
            {
                RequestArguments["outclusterlist"] = value;
            }
        }
    }
}
