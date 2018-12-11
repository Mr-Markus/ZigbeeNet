using System;
using System.Collections.Generic;
using System.Text;
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
        public Task<CommandResult> SendTransaction(ZigBeeCommand command, IZigBeeTransactionMatcher responseMatcher)
        {
            return Task.Run<CommandResult>(
            () =>
            {
                this._command = command;
                this._responseMatcher = responseMatcher;

                //lock (_command)
                //{
                //    // Schedule a task to timeout the transaction
                //    _timeoutTask = _networkManager.ScheduleTask(new Runnable() {
                //    @Override
                //    public void run()
                //    {
                //        timeoutTransaction();
                //    }
                //}

                _networkManager.AddCommandListener(this);

                int transactionId = _networkManager.SendCommand(command);

                if (command is ZclCommand cmd)
                {
                    cmd.TransactionId = (byte)transactionId;
                    return new CommandResult(cmd);
                }

                return new CommandResult(command);
            });
        }

        public void CommandReceived(ZigBeeCommand receivedCommand)
        {
            // Ensure that received command is not processed before command is sent
            // and hence transaction ID for the command set.
            //lock(_command)
            //{
            //    if (_responseMatcher.IsTransactionMatch(_command, receivedCommand))
            //    {
                   

            //        //timeoutTask.cancel(false);

            //        lock(transactionFuture) {
            //            transactionFuture.set(new CommandResult(receivedCommand));
            //            transactionFuture.notify();
            //        }
                    
            //    }
            //}

            _logger.Debug("Transaction complete: {}", _command);
            _networkManager.RemoveCommandListener(this);
        }

        //private void timeoutTransaction()
        //{
        //    _logger.Debug("Transaction timeout: {}",_command);
        //    //lock(_command) {
        //        synchronized(transactionFuture) {
        //            transactionFuture.cancel(false);
        //            transactionFuture.notify();
        //        }
        //        _networkManager.RemoveCommandListener(this);
        //    //}
        //}
    }
}
