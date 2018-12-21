using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet;
using ZigBeeNet.Logging;
using ZigBeeNet.ZCL;

namespace ZigBeeNet.Transaction
{
    /**
     * Transaction class to handle the sending of commands and timeout in the event there is no response.
     *
     */
    public class ZigBeeTransaction : IZigBeeCommandListener
    {

        /**
         * The logger.
         */
        private ILog _logger = LogProvider.For<ZigBeeTransaction>();

        private ZigBeeNetworkManager _networkManager;
        //private ZigBeeTransactionFuture transactionFuture;
        private IZigBeeTransactionMatcher _responseMatcher;
        private ZigBeeCommand _command;
        private Task _timeoutTask;
        private TaskCompletionSource<CommandResult> _task;
        private CancellationTokenSource _timeoutCancellationTokenSource;
        private const int DEFAULT_TIMEOUT_MILLISECONDS = 8000;

        public int Timeout { get; set; } = DEFAULT_TIMEOUT_MILLISECONDS;

        /**
         * Transaction constructor
         * 
         * @param networkManager the {@link ZigBeeNetworkManager} to which the transaction is being sent
         */
        public ZigBeeTransaction(ZigBeeNetworkManager networkManager)
        {
            this._networkManager = networkManager;
        }

        /**
         * Sends {@link ZigBeeCommand} command and uses the {@link ZigBeeTransactionMatcher} to match the response.
         * The task will be timed out if there is no response.
         *
         * @param command the {@link ZigBeeCommand}
         * @param responseMatcher the {@link ZigBeeTransactionMatcher}
         * @return the {@link CommandResult} future.
         */
        public async Task<CommandResult> SendTransaction(ZigBeeCommand command, IZigBeeTransactionMatcher responseMatcher)
        {
            _command = command;
            _responseMatcher = responseMatcher;
            _task = new TaskCompletionSource<CommandResult>();
            _networkManager.AddCommandListener(this);

            // Schedule a task to timeout the transaction
            _timeoutCancellationTokenSource = new CancellationTokenSource();
            _timeoutTask = Task.Delay(Timeout, _timeoutCancellationTokenSource.Token);

            int transactionId = _networkManager.SendCommand(command);

            if (command is ZclCommand cmd)
            {
                cmd.TransactionId = (byte)transactionId;
            }

            if (await Task.WhenAny(_task.Task, _timeoutTask) == _task.Task)
            {
                _timeoutCancellationTokenSource.Cancel();

                return await _task.Task;
            }
            else
            {
                // Timeout
                TimeoutTransaction();
                return new CommandResult();
            }
        }

        public void CommandReceived(ZigBeeCommand receivedCommand)
        {
            // Ensure that received command is not processed before command is sent
            // and hence transaction ID for the command set.
            lock (_command)
            {
                if (_responseMatcher.IsTransactionMatch(_command, receivedCommand))
                {
                    _networkManager.RemoveCommandListener(this);

                    _task.SetResult(new CommandResult(receivedCommand));
                    _timeoutCancellationTokenSource.Cancel();

                    _logger.Debug("Transaction complete: {Command}", _command);
                }
            }
        }

        private void TimeoutTransaction()
        {

            if (_timeoutCancellationTokenSource.IsCancellationRequested)
            {
                return;
            }
            _logger.Debug("Transaction timeout: {Command}", _command);
            _networkManager.RemoveCommandListener(this);
            lock (_command)
            {
                _task.SetCanceled();
            }
        }
    }
}
