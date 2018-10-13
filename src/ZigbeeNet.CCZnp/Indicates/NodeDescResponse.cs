using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class NodeDescResponse : ZpiObject
    {
        public NodeDescResponse(ZpiObject zpi)
            : base(ZDO.nodeDescRsp)
        {
            RequestArguments = zpi.RequestArguments;
        }

        public NodeDescResponse()
            : base (ZDO.nodeDescRsp)
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

        public byte LogicaltypeCmplxdescavaiUserdescavai
        {
            get
            {
                return (byte)RequestArguments["logicaltype_cmplxdescavai_userdescavai"];
            }
            set
            {
                RequestArguments["logicaltype_cmplxdescavai_userdescavai"] = value;
            }
        }

        public byte APSFlagsFrequencyBand 
        {
            get
            {
                return (byte)RequestArguments["apsflags_freqband"];
            }
            set
            {
                RequestArguments["apsflags_freqband"] = value;
            }
        }

        //TODO: Enum
        public byte MacCapabilitiesFlags
        {
            get
            {
                return (byte)RequestArguments["maccapflags"];
            }
            set
            {
                RequestArguments["maccapflags"] = value;
            }
        }

        public ushort ManufacturerCode
        {
            get
            {
                return (ushort)RequestArguments["manufacturercode"];
            }
            set
            {
                RequestArguments["manufacturercode"] = value;
            }
        }

        public byte MaxBufferSize
        {
            get
            {
                return (byte)RequestArguments["maxbuffersize"];
            }
            set
            {
                RequestArguments["maxbuffersize"] = value;
            }
        }

        public ushort MaxInTransferSize
        {
            get
            {
                return (ushort)RequestArguments["maxintransfersize"];
            }
            set
            {
                RequestArguments["maxintransfersize"] = value;
            }
        }

        public ushort ServerMask
        {
            get
            {
                return (ushort)RequestArguments["servermask"];
            }
            set
            {
                RequestArguments["servermask"] = value;
            }
        }

        public ushort MaxOutTransferSize
        {
            get
            {
                return (ushort)RequestArguments["maxouttransfersize"];
            }
            set
            {
                RequestArguments["maxouttransfersize"] = value;
            }
        }

        public byte DescriptorCapabilities
        {
            get
            {
                return (byte)RequestArguments["descriptorcap"];
            }
            set
            {
                RequestArguments["descriptorcap"] = value;
            }
        }
    }
}
