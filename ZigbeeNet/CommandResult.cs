using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;
using ZigbeeNet.ZCL.Clusters.General;
using ZigbeeNet.ZDO;

namespace ZigbeeNet
{
    public class CommandResult
    {

        /**
         * The response command.
         */
        private readonly ZigBeeCommand response;

        /**
         * Constructor which sets the received response command or null if timeout occurs.
         *
         * @param response the response command.
         */
        public CommandResult(ZigBeeCommand response)
        {
            this.response = response;
        }

        /**
         * Constructor for timeout situations.
         */
        public CommandResult()
        {
            this.response = null;
        }

        /**
         * Checks whether command execution was successful.
         *
         * @return TRUE if command execution was successful.
         */
        public bool isSuccess()
        {
            return !(IsTimeout() || IsError());
        }

        /**
         * Checks whether command timed out.
         *
         * @return TRUE if timeout occurred
         */
        public bool IsTimeout()
        {
            return response == null;
        }

        /**
         * Checks if message status code was received in default response.
         *
         * @return the message status code
         */
        public bool IsError()
        {
            if (HasStatusCode())
            {
                return GetStatusCode() != 0;
            }
            else
            {
                return response == null;
            }
        }

        /**
         * Check if default response was received.
         *
         * @return TRUE if default response was received
         */
        private bool HasStatusCode()
        {
            if (response != null)
            {
                return response is DefaultResponse || response is ZdoResponse;
            }
            else
            {
                return false;
            }
        }

        /**
         * Gets status code received in default response.
         *
         * @return the status code
         */
        public int GetStatusCode()
        {
            if (HasStatusCode())
            {
                if (response is DefaultResponse resp)
                {
                    return (int)resp.StatusCode;
                }
                else
                {
                    return (int)((ZdoResponse)response).GetStatus();
                }
            }
            else
            {
                return 0xffff;
            }
        }

        /**
         * Gets the received response.
         *
         * @return the received response {@link ZigBeeCommand}
         */
        public ZigBeeCommand GetResponse()
        {
            return response;
        }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("CommandResult [");
            if (isSuccess())
            {
                builder.Append("SUCCESS, ")
                       .Append(response.ToString());
            }
            else if (IsTimeout())
            {
                builder.Append("TIMEOUT");
            }
            else
            {
                ZclStatus status = (ZclStatus)GetStatusCode();
                builder.Append("ERROR (")
                       .Append(status.ToString())
                       .Append(String.Format(",0x{0}2X), ", (int)status))
                       .Append(response);
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}
