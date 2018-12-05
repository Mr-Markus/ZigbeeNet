using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    /// <summary>
    /// Provides a list of channel mask values used for channel scans.
    /// <p>
    /// The 868MHz frequency band has only one channel and is used in Europe with a data rate of 20kbps.
    /// <p>
    /// The 915MHz frequency band band has 10 channels ranging from channel-1 to channel-10. It delivers data rate of 40 Kbps
    /// and used in Americas.
    /// The 2.4GHz frequency band is used worldwide and has total of 16 channels from channel-11 to channel-26 delivering a
    /// data rate of 250kbps.
    /// <ol>
    /// <li>channel-0 868 MHz
    /// <li>channel-1 906 MHz
    /// <li>channel-2 908 MHz
    /// <li>channel-3 910 MHz
    /// <li>channel-4 912 MHz
    /// <li>channel-5 914 MHz
    /// <li>channel-6 916 MHz
    /// <li>channel-7 918 MHz
    /// <li>channel-8 920 MHz
    /// <li>channel-9 922 MHz
    /// <li>channel-10 924 MHz
    /// <li>channel-11 2405 MHz
    /// <li>channel-12 2410 MHz
    /// <li>channel-13 2415 MHz
    /// <li>channel-14 2420 MHz
    /// <li>channel-15 2425 MHz
    /// <li>channel-16 2430 MHz
    /// <li>channel-17 2435 MHz
    /// <li>channel-18 2440 MHz
    /// <li>channel-19 2445 MHz
    /// <li>channel-20 2450 MHz
    /// <li>channel-21 2455 MHz
    /// <li>channel-22 2460 MHz
    /// <li>channel-23 2465 MHz
    /// <li>channel-24 2470 MHz
    /// <li>channel-25 2475 MHz
    /// <li>channel-26 2480 MHz
    /// </ol>
    /// </summary>
    public enum ZigBeeChannel
    {
        UNKNOWN = 0,
        CHANNEL_00 = 0x00000001,
        CHANNEL_01 = 0x00000002,
        CHANNEL_02 = 0x00000004,
        CHANNEL_03 = 0x00000008,
        CHANNEL_04 = 0x00000010,
        CHANNEL_05 = 0x00000020,
        CHANNEL_06 = 0x00000040,
        CHANNEL_07 = 0x00000080,
        CHANNEL_08 = 0x00000100,
        CHANNEL_09 = 0x00000200,
        CHANNEL_10 = 0x00000400,
        CHANNEL_11 = 0x00000800,
        CHANNEL_12 = 0x00001000,
        CHANNEL_13 = 0x00002000,
        CHANNEL_14 = 0x00004000,
        CHANNEL_15 = 0x00008000,
        CHANNEL_16 = 0x00010000,
        CHANNEL_17 = 0x00020000,
        CHANNEL_18 = 0x00040000,
        CHANNEL_19 = 0x00080000,
        CHANNEL_20 = 0x00100000,
        CHANNEL_21 = 0x00200000,
        CHANNEL_22 = 0x00400000,
        CHANNEL_23 = 0x00800000,
        CHANNEL_24 = 0x01000000,
        CHANNEL_25 = 0x02000000,
        CHANNEL_26 = 0x04000000
    }
}
