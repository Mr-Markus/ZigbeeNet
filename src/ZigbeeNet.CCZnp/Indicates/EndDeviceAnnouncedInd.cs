using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class EndDeviceAnnouncedInd : ZpiObject
    {
        public EndDeviceAnnouncedInd(ZpiObject zpi)
            : base(SubSystem.ZDO, (byte)ZDO.endDeviceAnnceInd)
        {
            RequestArguments = zpi.RequestArguments;
        }
        public EndDeviceAnnouncedInd(ushort srcAddr, ushort nwkAddr, long ieeeAddr, byte capabilities)
            :base(SubSystem.ZDO, (byte)ZDO.endDeviceAnnceInd)
        {
            SourceAddress = srcAddr;
            NetworkAddress = nwkAddr;
            IeeeAddress = ieeeAddr;
            Capabilities = capabilities;
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

        public ushort NetworkAddress
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

        public long IeeeAddress
        {
            get
            {
                return (long)RequestArguments["ieeeaddr"];
            }
            set
            {
                RequestArguments["ieeeaddr"] = value;
            }
        }

        public byte Capabilities
        {
            get
            {
                return (byte)RequestArguments["capabilities"];
            }
            set
            {
                RequestArguments["capabilities"] = value;
            }
        }
    }
}
