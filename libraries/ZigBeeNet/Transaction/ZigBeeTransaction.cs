using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet;
using ZigBeeNet.ZCL;
using Serilog;

namespace ZigBeeNet.Transaction
{
    /// <summary>
    /// Transaction class to handle the sending of commands and timeout in the event there is no response.
    ///
    /// </summary>
    public class ZigBeeTransaction : IZigBeeCommandListener
    {
        private ZigBeeNetworkManager _networkManager;
        private IZigBeeTransactionMatcher _responseMatcher;
        private ZigBeeCommand _command;
        private TaskCompletionSource<CommandResult> _sendTransactionTask;
        private readonly object _objLock = new object();
        private const int DEFAULT_TIMEOUT_MILLISECONDS = 8000;

        public int Timeout { get; set; } = DEFAULT_TIMEOUT_MILLISECONDS;

        /// <summary>
        /// Transaction constructor
        /// 
        /// <param name="networkManager">the <see cref="ZigBeeNetworkManager"> to which the transaction is being sent</param>
        /// </summary>
        public ZigBeeTransaction(ZigBeeNetworkManager networkManager)
        {
            this._networkManager = networkManager;
        }

        /// <summary>
        /// Sends ZigBeeCommand command and uses the ZigBeeTransactionMatcher to match the response.
        /// The task will be timed out if there is no response.
        ///
        /// <param name="command">the ZigBeeCommand</param>
        /// <param name="responseMatcher">the ZigBeeTransactionMatcher</param>
        /// </summary>
        public async Task<CommandResult> SendTransaction(ZigBeeCommand command, IZigBeeTransactionMatcher responseMatcher)
        {
            CommandResult commandResult;

            lock (_objLock)
            {
                _command = command;
                _responseMatcher = responseMatcher;
                _networkManager.AddCommandListener(this);

                int transactionId = _networkManager.SendCommand(command);

                if (command is ZclCommand cmd)
                {
                    cmd.TransactionId = (byte)transactionId;
                }
                //Without RunContinuationsAsynchronously, calling SetResult can block the calling thread, because the continuation is run synchronously
                //see https://stackoverflow.com/a/37492081
                _sendTransactionTask = new TaskCompletionSource<CommandResult>(TaskCreationOptions.RunContinuationsAsynchronously);
            }
            
            var t = _sendTransactionTask.Task;
            var cancel = new CancellationTokenSource();
            var timeoutTask = Task.Delay(Timeout, cancel.Token);

            if (t == await Task.WhenAny(t, timeoutTask).ConfigureAwait(false))
            {
                cancel.Cancel(); //Cancel the timeout task
                commandResult = t.Result;
            }
            else
            {
                /* Timeout */
                Log.Debug("Transaction timeout: {Command}", _command);
                commandResult = new CommandResult();
            }
            _networkManager.RemoveCommandListener(this);

            return commandResult;
        }

        public void CommandReceived(ZigBeeCommand receivedCommand)
        {
            // Ensure that received command is not processed before command is sent
            // and hence transaction ID for the command set.
            lock (_objLock)
            {
                if (_sendTransactionTask == null || _sendTransactionTask.Task.IsCompleted)
                    return;

                if (_responseMatcher.IsTransactionMatch(_command, receivedCommand))
                {
                    _sendTransactionTask.SetResult(new CommandResult(receivedCommand));

                    Log.Debug("Transaction complete: {Command}", _command);
                }
            }
        }
    }
}
