using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.DemandResponseAndLoadControl
{
    /// <summary>
    /// Event Status value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum EventStatusEnum
    {
        // Load Control Event Command Rx
        LOAD_CONTROL_EVENT_COMMAND_RX = 0x0001,
        // Event Started
        EVENT_STARTED = 0x0002,
        // Event Completed
        EVENT_COMPLETED = 0x0003,
        // User Has Choose To Opt Out
        USER_HAS_CHOOSE_TO_OPT_OUT = 0x0004,
        // User Has Choose To Opt In
        USER_HAS_CHOOSE_TO_OPT_IN = 0x0005,
        // The Event Has Been Canceled
        THE_EVENT_HAS_BEEN_CANCELED = 0x0006,
        // The Event Has Been Superseded
        THE_EVENT_HAS_BEEN_SUPERSEDED = 0x0007,
        // Event Partially Completed With User Opt Out
        EVENT_PARTIALLY_COMPLETED_WITH_USER_OPT_OUT = 0x0008,
        // Event Partially Completed Due To User Opt In
        EVENT_PARTIALLY_COMPLETED_DUE_TO_USER_OPT_IN = 0x0009,
        // Event Completed No User Participation Previous Opt Out
        EVENT_COMPLETED_NO_USER_PARTICIPATION_PREVIOUS_OPT_OUT = 0x000A,
        // Invalid Opt Out
        INVALID_OPT_OUT = 0x00F6,
        // Event Not Found
        EVENT_NOT_FOUND = 0x00F7,
        // Rejected Invalid Cancel Command
        REJECTED_INVALID_CANCEL_COMMAND = 0x00F8,
        // Rejected Invalid Cancel Command Invalid Effective Time
        REJECTED_INVALID_CANCEL_COMMAND_INVALID_EFFECTIVE_TIME = 0x00F9,
        // Rejected Event Expired
        REJECTED_EVENT_EXPIRED = 0x00FB,
        // Rejected Invalid Cancel Undefined Event
        REJECTED_INVALID_CANCEL_UNDEFINED_EVENT = 0x00FD,
        // Load Control Event Command Rejected
        LOAD_CONTROL_EVENT_COMMAND_REJECTED = 0x00FE
    }
}
