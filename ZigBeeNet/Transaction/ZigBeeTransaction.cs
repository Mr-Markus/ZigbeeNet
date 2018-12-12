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
        private CancellationTokenSource _timeoutCancel;
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
            this._command = command;
            this._responseMatcher = responseMatcher;

            lock (_command)
            {
                _task = new TaskCompletionSource<CommandResult>();
                // Schedule a task to timeout the transaction
                _timeoutCancel = new CancellationTokenSource();
                //_timeoutTask = _networkManager.ScheduleTask(new Task(() => TimeoutTransaction()), Timeout, _timeoutCancel);

                _networkManager.AddCommandListener(this);

                int transactionId = _networkManager.SendCommand(command);

                if (command is ZclCommand cmd)
                {
                    cmd.TransactionId = (byte)transactionId;
                    _task.SetResult(new CommandResult(cmd));
                }
            }


            if (await Task.WhenAny(_task.Task, Task.Delay(Timeout)) == _task.Task)
            {
                return _task.Task.Result;
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
                    _timeoutCancel.Cancel();
                    _task.SetResult(new CommandResult(receivedCommand));
                }
            }

            _logger.Debug("Transaction complete: {Command}", _command);
            _networkManager.RemoveCommandListener(this);
        }

        private void TimeoutTransaction()
        {
            _logger.Debug("Transaction timeout: {Command}", _command);
            lock (_command)
            {
                _task.SetCanceled();
                _networkManager.RemoveCommandListener(this);
            }
        }
    }
}
