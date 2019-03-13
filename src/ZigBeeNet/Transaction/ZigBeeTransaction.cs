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
    /// <summary>
     /// Transaction class to handle the sending of commands and timeout in the event there is no response.
     ///
     /// </summary>
    public class ZigBeeTransaction : IZigBeeCommandListener
    {

        /// <summary>
         /// The logger.
         /// </summary>
        private ILog _logger = LogProvider.For<ZigBeeTransaction>();

        private ZigBeeNetworkManager _networkManager;
        private IZigBeeTransactionMatcher _responseMatcher;
        private ZigBeeCommand _command;
        private CommandResult _result;
        private DateTime _timeout;

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
            _command = command;
            _responseMatcher = responseMatcher;

            _timeout = DateTime.Now.AddMilliseconds(DEFAULT_TIMEOUT_MILLISECONDS);

            lock (_command)
            {
                _networkManager.AddCommandListener(this);

                int transactionId = _networkManager.SendCommand(command);

                if (command is ZclCommand cmd)
                {
                    cmd.TransactionId = (byte)transactionId;
                }
            }

            return await Task.Run(() =>
            {
                while (true)
                {
                    if (DateTime.Now < _timeout)
                    {
                        if (_result != null)
                        {
                            return _result;
                        }
                    }
                    else
                    {
                        _logger.Debug("Transaction timeout: {Command}", _command);
                        lock (_command)
                        {
                            _networkManager.RemoveCommandListener(this);
                            return new CommandResult();
                        }
                    }
                }
            });
        }

        public bool IsTransactionMatch = false;
        public void CommandReceived(ZigBeeCommand receivedCommand)
        {
            if (DateTime.Now < _timeout)
            {
                // Ensure that received command is not processed before command is sent
                // and hence transaction ID for the command set.
                lock (_command)
                {
                    if (_responseMatcher.IsTransactionMatch(_command, receivedCommand))
                    {
                        _result = new CommandResult(receivedCommand);

                        IsTransactionMatch = true;

                        _networkManager.RemoveCommandListener(this);

                        _logger.Debug("Transaction complete: {Command}", _command);
                    }
                }
            }
        }
    }
}
