using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Commands
{
    public class PermitJoinRequest : ZpiObject
    {
        public PermitJoinRequest(byte duration = 0xff, AddressMode mode = AddressMode.All) 
            : base(SubSystem.ZDO, (byte)ZDO.mgmtPermitJoinReq)
        {
            Duration = duration;
            Mode = mode;
            TrustCenterSignificance = 0;
        }

        public AddressMode Mode
        {
            get
            {
                return (AddressMode)RequestArguments["addrmode"];
            }
            set
            {
                RequestArguments["addrmode"] = value;

                if (value == AddressMode.Coord)
                {
                    DestinationAddress = 0x0000; //Coord address
                } else
                {
                    DestinationAddress = 0xFFFC; //all coord and routers
                }
            }
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

        public byte Duration
        {
            get
            {
                return (byte)RequestArguments["duration"];
            }
            set
            {
                RequestArguments["duration"] = value;
            }
        }

        public byte TrustCenterSignificance
        {
            get
            {
                return (byte)RequestArguments["tcsignificance"];
            }
            set
            {
                RequestArguments["tcsignificance"] = value;
            }
        }
    }

    public enum AddressMode : byte
    {
        Coord = 0x02,
        All = 0xFF
    }
}
