using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZDO;

namespace ZigBeeNet
{
    public class CommandResult
    {

        /**
         * The response command.
         */
        public ZigBeeCommand Response { get; private set; }

        /**
         * Constructor which sets the received response command or null if timeout occurs.
         *
         * @param response the response command.
         */
        public CommandResult(ZigBeeCommand response)
        {
            this.Response = response;
        }

        /**
         * Constructor for timeout situations.
         */
        public CommandResult()
        {
            this.Response = null;
        }

        /**
         * Checks whether command execution was successful.
         *
         * @return TRUE if command execution was successful.
         */
        public bool IsSuccess()
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
            return Response == null;
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
                return Response == null;
            }
        }

        /**
         * Check if default response was received.
         *
         * @return TRUE if default response was received
         */
        private bool HasStatusCode()
        {
            if (Response != null)
            {
                return Response is DefaultResponse || Response is ZdoResponse;
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
                if (Response is DefaultResponse resp)
                {
                    return (int)resp.StatusCode;
                }
                else
                {
                    return (int)((ZdoResponse)Response).Status;
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
            return Response;
        }

        public TCommand GetResponse<TCommand>() where TCommand : ZigBeeCommand
        {
            return (TCommand)Response;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("CommandResult [");

            if (IsSuccess())
            {
                builder.Append("SUCCESS, ")
                       .Append(Response.ToString());
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
                       .Append(string.Format(",0x{0}), ", ((int)status).ToString("X2")))
                       .Append(Response);
            }

            builder.Append(']');
            return builder.ToString();
        }
    }
}
