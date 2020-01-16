using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    /// <summary>
    /// A class to hold a set of channels and methods to construct channel masks
    /// </summary>
    public class ZigBeeChannelMask
    {
        /// <summary>
        /// No channels selected
        /// </summary>
        public const int CHANNEL_MASK_NONE = 0x00000000;
        /// <summary>
        /// All currently defined ZigBee channels in all bands
        /// </summary>
        public const int CHANNEL_MASK_ALL = 0x07FFFFFF;
        /// <summary>
        /// All currently defined ZigBee channels in the 2.4GHz band
        /// </summary>
        public const int CHANNEL_MASK_2GHZ = 0x07FFF800;

        /// <summary>
        /// The channel mask
        /// </summary>
        public int ChannelMask { get; private set; } = CHANNEL_MASK_2GHZ;

        /// <summary>
        /// Constructor creating a mask with no channels set
        /// </summary>
        public ZigBeeChannelMask() 
        {
            ChannelMask = 0;
        }

        /// <summary>
        /// Constructor taking a channel mask
        /// </summary>
        /// <param name="channelMask">bitmask of the channels</param>
        public ZigBeeChannelMask(int channelMask) 
        {
            ChannelMask = channelMask;
        }

        /// <summary>
        /// Gets the channels in this channel mask
        /// </summary>
        /// <returns>the List of ZigBeeChannel  in this mask</returns>
        public List<ZigBeeChannel> GetChannels() 
        {
            List<ZigBeeChannel> channels = new List<ZigBeeChannel>();

            for (int channelCnt = 0; channelCnt < 32; channelCnt++) 
            {
                if ((ChannelMask & 1 << channelCnt) == 0) {
                    continue;
                }

                ZigBeeChannel channel = (ZigBeeChannel)(1 << channelCnt);
                if (channel != ZigBeeChannel.UNKNOWN)
                    channels.Add(channel);
            }
            return channels;
        }

        /// <summary>
        /// Adds a new channel to the channel mask
        /// </summary>
        /// <param name="channel">the channel to add to the mask</param>
        public void AddChannel(int channel)
        {
            if (channel < 0 || channel > 27) 
                return;

            ChannelMask |= 1 << channel;
        }
   
        /// <summary>
        /// Adds a new channel to the channel mask
        /// </summary>
        /// <param name="channel">ZigBeeChannel to add to the mask</param>
        public void AddChannel(ZigBeeChannel channel) 
        {
            ChannelMask |= (int)channel;
        }

        /// <summary>
        /// Tests of the specified channel number is included in the bitmask
        /// </summary>
        /// <param name="channel">the channel number</param>
        /// <returns>true if the mask contains the channel</returns>
        public bool ContainsChannel(int channel) 
        {
            return ((ChannelMask & (1 << channel)) != 0);
        }

        /// <summary>
        /// Tests of the specified channel number is included in the bitmask
        /// </summary>
        /// <param name="channel">the ZigBeeChannel number</param>
        /// <returns>true if the mask contains the channel</returns>
        public bool ContainsChannel(ZigBeeChannel channel) 
        {
            return ((ChannelMask & (int)channel) != 0);
        }

        public override string ToString() 
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Channels");
            builder.Append(' ');
            int mask = 1;
            int start = -1;
            int channel;
            bool first = true;
            for (channel = 0; channel < 31; channel++) 
            {
                if ((ChannelMask & mask) != 0) 
                {
                    if (start == -1) {
                        start = channel;
                    }
                } 
                else if (start != -1) 
                {
                    if (!first)
                        builder.Append(", ");
                    
                    first = false;
                    builder.Append(start.ToString());
                    if (start != channel - 1) 
                    {
                        builder.Append('-');
                        builder.Append((channel - 1).ToString());
                    }

                    start = -1;
                }

                mask = mask << 1;
            }

            if (start != -1) 
            {
                if (!first)
                    builder.Append(", ");
                
                builder.Append(start.ToString());
                if (start != channel - 1) 
                {
                    builder.Append('-');
                    builder.Append((channel - 1).ToString());
                }
            }

            return builder.ToString();
        }
    }
}
