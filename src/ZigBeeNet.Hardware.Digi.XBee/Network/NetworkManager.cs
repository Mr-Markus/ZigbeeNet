using System;
using System.Numerics;
using ZigBeeNet.Hardware.Digi.XBee.Packet;
using ZigBeeNet.Security;

namespace ZigBeeNet.Hardware.Digi.XBee.Network
{
    public class NetworkManager
    {
        public NetworkManager()
        {
            throw new NotImplementedException();
        }

        internal ZigBeeChannel GetCurrentChannel()
        {
            throw new NotImplementedException();
        }

        internal ushort GetCurrentPanId()
        {
            throw new NotImplementedException();
        }

        internal ExtendedPanId GetCurrentExtendedPanId()
        {
            throw new NotImplementedException();
        }

        internal ZigBeeKey GetZigBeeNetworkKey()
        {
            throw new NotImplementedException();
        }

        internal void SetMagicNumber(byte magicNumber)
        {
            throw new NotImplementedException();
        }

        internal string Startup()
        {
            throw new NotImplementedException();
        }

        internal BigInteger GetIeeeAddress()
        {
            throw new NotImplementedException();
        }

        /**
        * Sends a command without waiting for the response
        *
        * @param request {@link ZToolPacket}
        */
        internal void SendCommand(ZToolPacket request)
        {
            throw new NotImplementedException();
        }
    }
}
