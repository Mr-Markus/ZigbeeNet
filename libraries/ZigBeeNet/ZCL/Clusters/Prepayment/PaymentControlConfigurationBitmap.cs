using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Prepayment
{
    /// <summary>
    /// Payment Control Configuration value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum PaymentControlConfigurationBitmap
    {
        // Disconnection Enabled
        DISCONNECTION_ENABLED = 0x0001,
        // Prepayment Enabled
        PREPAYMENT_ENABLED = 0x0002,
        // Credit Management Enabled
        CREDIT_MANAGEMENT_ENABLED = 0x0004,
        // Credit Display Enabled
        CREDIT_DISPLAY_ENABLED = 0x0010,
        // Account Base
        ACCOUNT_BASE = 0x0040,
        // Contactor Fitted
        CONTACTOR_FITTED = 0x0080,
        // Standing Charge Configuration
        STANDING_CHARGE_CONFIGURATION = 0x0100,
        // Emergency Standing Charge Configuration
        EMERGENCY_STANDING_CHARGE_CONFIGURATION = 0x0200,
        // Debt Configuration
        DEBT_CONFIGURATION = 0x0400,
        // Emergency Debt Configuration
        EMERGENCY_DEBT_CONFIGURATION = 0x0800
    }
}
