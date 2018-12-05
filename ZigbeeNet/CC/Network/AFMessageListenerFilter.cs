using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.CC.Packet;
using ZigBeeNet.CC.Packet.AF;
using ZigBeeNet.Logging;

namespace ZigBeeNet.CC.Network
{
    internal class AFMessageListenerFilter : IAsynchronousCommandListener
    {
        private readonly ILog _logger = LogProvider.For<AFMessageListenerFilter>();

        private List<IApplicationFrameworkMessageListener> _listeners;

        public AFMessageListenerFilter(List<IApplicationFrameworkMessageListener> list)
        {
            _listeners = list;
        }

        /// <summary>
        /// An asynchronous command is received from the network. If it is an AF_INCOMING_MSG then
        /// pass the message on to any listeners.
        /// </summary>
        /// <param name="packet"></param>
        public void ReceivedAsynchronousCommand(AsynchronousRequest packet)
        {
            if(packet is AF_INCOMING_MSG msg)
            {
                if(_listeners.Count == 0)
                {
                    _logger.Warn($"Received AF_INCOMING_MSG but no listeners. Message was from {msg.SrcAddr} and cluster {msg.ClusterId} to endpoint {msg.DstEndpoint}. Data: {msg}");
                } else
                {
                    _logger.Trace($"Received AF_INCOMING_MSG from {msg.SrcAddr} and cluster {msg.ClusterId} to endpoint {msg.DstEndpoint}. Data: {msg}");
                }
                List<IApplicationFrameworkMessageListener> localCopy;
                lock(_listeners)
                {
                    localCopy = new List<IApplicationFrameworkMessageListener>();
                    localCopy.AddRange(_listeners);
                }

                foreach (var listener in localCopy)
                {
                    try
                    {
                        listener.Notify(msg);
                    } catch (Exception e)
                    {
                        _logger.Error(e, "Error AF message listener notify");
                    }
                }
            }
        }

        public void ReceivedUnclaimedSynchronousCommandResponse(SerialPacket packet)
        {
            // No need to handle unclaimed responses here
            throw new NotImplementedException();
        }
    }
}
