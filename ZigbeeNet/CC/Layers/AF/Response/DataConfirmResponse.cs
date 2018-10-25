using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.AF
{
    public class DataConfirmResponse : ZpiObject
    {
        public DataConfirmResponse(ZpiObject zpi)
            : base(CommandType.AF_DATA_CONFIRM)
        {
            RequestArguments = zpi.RequestArguments;
        }

        public DataConfirmResponse()
            : base (CommandType.AF_DATA_CONFIRM)
        {

        }

        /// <summary>
        /// Status is either Success (0) or Failure (1).
        /// </summary>
        public ZpiStatus Status
        {
            get
            {
                return (ZpiStatus)RequestArguments["status"];
            }
            set
            {
                RequestArguments["status"] = value;
            }
        }

        /// <summary>
        /// Endpoint of the device 
        /// </summary>
        public byte Endpoint
        {
            get
            {
                return (byte)RequestArguments["activeepcount"];
            }
            set
            {
                RequestArguments["activeepcount"] = value;
            }
        }

        /// <summary>
        /// Specified the transaction sequence number of the message 
        /// </summary>
        public byte TransId
        {
            get
            {
                return (byte)RequestArguments["activeepcount"];
            }
            set
            {
                RequestArguments["activeepcount"] = value;
            }
        }
    }
}
