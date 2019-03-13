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

        /// <summary>
         /// The response command.
         /// </summary>
        public ZigBeeCommand Response { get; private set; }

        /// <summary>
         /// Constructor which sets the received response command or null if timeout occurs.
         ///
         /// <param name="response">the response command.</param>
         /// </summary>
        public CommandResult(ZigBeeCommand response)
        {
            this.Response = response;
        }

        /// <summary>
         /// Constructor for timeout situations.
         /// </summary>
        public CommandResult()
        {
            this.Response = null;
        }

        /// <summary>
         /// Checks whether command execution was successful.
         ///
         /// <returns>TRUE if command execution was successful.</returns>
         /// </summary>
        public bool IsSuccess()
        {
            return !(IsTimeout() || IsError());
        }

        /// <summary>
         /// Checks whether command timed out.
         ///
         /// <returns>TRUE if timeout occurred</returns>
         /// </summary>
        public bool IsTimeout()
        {
            return Response == null;
        }

        /// <summary>
         /// Checks if message status code was received in default response.
         ///
         /// <returns>the message status code</returns>
         /// </summary>
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

        /// <summary>
         /// Check if default response was received.
         ///
         /// <returns>TRUE if default response was received</returns>
         /// </summary>
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

        /// <summary>
         /// Gets status code received in default response.
         ///
         /// <returns>the status code</returns>
         /// </summary>
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

        /// <summary>
         /// Gets the received response.
         ///
         /// <returns>the received response <see cref="ZigBeeCommand"></returns>
         /// </summary>
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
