using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.ZDO
{
    public class EndDeviceAnnouncedInd : ZpiObject
    {
        public EndDeviceAnnouncedInd(ZpiObject zpi)
            : base(ZdoCommand.endDeviceAnnceInd)
        {
            RequestArguments = zpi.RequestArguments;
        }
        public EndDeviceAnnouncedInd(ushort srcAddr, ushort nwkAddr, ulong ieeeAddr, byte capabilities)
            :base(ZdoCommand.endDeviceAnnceInd)
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

        public ulong IeeeAddress
        {
            get
            {
                return (ulong)RequestArguments["ieeeaddr"];
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
