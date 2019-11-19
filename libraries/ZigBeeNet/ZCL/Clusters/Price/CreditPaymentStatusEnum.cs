using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Price
{
    /// <summary>
    /// Credit Payment Status value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum CreditPaymentStatusEnum
    {
        // Pending
        PENDING = 0x0000,
        // Received/Paid
        RECEIVED_PAID = 0x0001,
        // Overdue
        OVERDUE = 0x0002,
        // Two Payments Overdue
        TWO_PAYMENTS_OVERDUE = 0x0003,
        // Three Payments Overdue
        THREE_PAYMENTS_OVERDUE = 0x0004
    }
}
