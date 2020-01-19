using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Metering
{
    /// <summary>
    /// Snapshot Payload Type value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum SnapshotPayloadTypeEnum
    {
        // Tou Information Set Delivered Registers
        TOU_INFORMATION_SET_DELIVERED_REGISTERS = 0x0000,
        // Tou Information Set Received Registers
        TOU_INFORMATION_SET_RECEIVED_REGISTERS = 0x0001,
        // Block Tier Information Set Delivered
        BLOCK_TIER_INFORMATION_SET_DELIVERED = 0x0002,
        // Block Tier Information Set Received
        BLOCK_TIER_INFORMATION_SET_RECEIVED = 0x0003,
        // Tou Information Set Delivered Registers No Billing
        TOU_INFORMATION_SET_DELIVERED_REGISTERS_NO_BILLING = 0x0004,
        // Tou Information Set Received Register No Billings
        TOU_INFORMATION_SET_RECEIVED_REGISTER_NO_BILLINGS = 0x0005,
        // Block Tier Information Set Delivered No Billing
        BLOCK_TIER_INFORMATION_SET_DELIVERED_NO_BILLING = 0x0006,
        // Block Tier Information Set Received No Billing
        BLOCK_TIER_INFORMATION_SET_RECEIVED_NO_BILLING = 0x0007,
        // Data Unavailable
        DATA_UNAVAILABLE = 0x0080
    }
}
