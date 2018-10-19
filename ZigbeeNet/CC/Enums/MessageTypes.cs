using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    /// <summary>
    /// A one byte value which specifies the message type
    /// 
    /// Source: http://processors.wiki.ti.com/index.php/NPI_Type_SubSystem
    /// </summary>
    public enum MessageType : byte
    {
        POLL = 0x00,
        /// <summary>
        /// Synchronous Messages A Synchronous Request (SREQ) is a frame, defined by data content instead of 
        /// the ordering of events of the physical interface, which is sent from the Host to NP where the 
        /// next frame sent from NP to Host must be the Synchronous Response (SRESP) to that SREQ. 
        /// 
        /// Note that once a SREQ is sent, the NPI interface blocks until a corresponding response(SRESP) is received.
        /// </summary>
        SREQ = 0x01,
        /// <summary>
        /// Asynchronous Messages Asynchronous Request – transfer initiated by Host Asynchronous Indication – transfer initiated by NP. 
        /// </summary>
        AREQ = 0x02,
        /// <summary>
        /// Synchronous Response
        /// </summary>
        SRSP = 0x03,
        RES0 = 0x04,
        RES1 = 0x05,
        RES2 = 0x06,
        RES3 = 0x07
    }
}
