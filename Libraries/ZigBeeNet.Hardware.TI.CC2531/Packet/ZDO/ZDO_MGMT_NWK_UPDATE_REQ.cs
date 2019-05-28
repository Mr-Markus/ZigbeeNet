using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    /// <summary>
 /// This command is provided to allow updating of network configuration parameters or
 /// to request information from devices on network conditions in the local operating
 /// environment. Upon receipt, the remote device shall determine from the contents of
 /// the ScanDuration parameter whether this request is an update to the ChannelMask and
 /// NwkManagerAddr parameters, a channel change command, or a request to scan channels
 /// and report the results.
 /// </summary>
    public class ZDO_MGMT_NWK_UPDATE_REQ : ZToolPacket
    {
        public ushort DestinationAddress { get; private set; }
        public byte DestinationAddressMode { get; private set; }
        public int ChannelMask { get; private set; }
        public byte ScanDuration { get; private set; }
        public byte ScanCount { get; private set; }
        public ushort NetworkManagerAddress { get; private set; }

        public static int CHANNEL_MASK_NONE = 0x00000000;
        public static int CHANNEL_MASK_ALL = 0x07FFF800;
        public static int CHANNEL_MASK_11 = 0x00000800;
        public static int CHANNEL_MASK_12 = 0x00001000;
        public static int CHANNEL_MASK_13 = 0x00002000;
        public static int CHANNEL_MASK_14 = 0x00004000;
        public static int CHANNEL_MASK_15 = 0x00008000;
        public static int CHANNEL_MASK_16 = 0x00010000;
        public static int CHANNEL_MASK_17 = 0x00020000;
        public static int CHANNEL_MASK_18 = 0x00040000;
        public static int CHANNEL_MASK_19 = 0x00080000;
        public static int CHANNEL_MASK_20 = 0x00100000;
        public static int CHANNEL_MASK_21 = 0x00200000;
        public static int CHANNEL_MASK_22 = 0x00400000;
        public static int CHANNEL_MASK_23 = 0x00800000;
        public static int CHANNEL_MASK_24 = 0x01000000;
        public static int CHANNEL_MASK_25 = 0x02000000;
        public static int CHANNEL_MASK_26 = 0x04000000;

        public ZDO_MGMT_NWK_UPDATE_REQ(ushort destinationAddress, byte destinationAddressMode,
                int channelMask, byte scanDuration, byte scanCount, ushort networkManagerAddress)
        {

            this.DestinationAddress = destinationAddress;
            this.DestinationAddressMode = destinationAddressMode;
            this.ChannelMask = channelMask;
            this.ScanDuration = scanDuration;
            this.ScanCount = scanCount;
            this.NetworkManagerAddress = networkManagerAddress;

            byte[] framedata = new byte[11];

            framedata[0] = this.DestinationAddress.GetByte(0);
            framedata[1] = this.DestinationAddress.GetByte(1);

            framedata[2] = this.DestinationAddressMode;

            framedata[3] = this.ChannelMask.GetByte(0);
            framedata[4] = this.ChannelMask.GetByte(1);
            framedata[5] = this.ChannelMask.GetByte(2);
            framedata[6] = this.ChannelMask.GetByte(3);

            framedata[7] = this.ScanDuration;
            framedata[8] = this.ScanCount;

            framedata[9] = this.NetworkManagerAddress.GetByte(0);
            framedata[10] = this.NetworkManagerAddress.GetByte(1);

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_MGMT_NWK_UPDATE_REQ), framedata);
        }

        public override string ToString()
        {
            return "ZDO_MGMT_NWK_UPDATE_REQ{destinationAddress=" + DestinationAddress + ", destinationAddressMode="
                    + DestinationAddressMode + ", channelMask=" + ChannelMask + ", scanDuration=" + ScanDuration
                    + ", scanCount=" + ScanCount + ", networkManagerAddress=" + NetworkManagerAddress + '}';
        }
    }
}
