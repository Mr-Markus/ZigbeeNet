using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.Hardware.TI.CC2531.Packet.AF;
using ZigBeeNet.Util;
using Microsoft.Extensions.Logging;

namespace ZigBeeNet.Hardware.TI.CC2531.Network
{
    internal class AFMessageListenerFilter : IAsynchronousCommandListener
    {
        static private readonly ILogger _logger = LogManager.GetLog<AFMessageListenerFilter>();

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
        public void ReceivedAsynchronousCommand(ZToolPacket packet)
        {
            if(packet is AF_INCOMING_MSG msg)
            {
                if(_listeners.Count == 0)
                {
                    _logger.LogWarning($"Received AF_INCOMING_MSG but no listeners. Message was from {msg.SrcAddr} and cluster {msg.ClusterId} to endpoint {msg.DstEndpoint}. Data: {msg}");
                } else
                {
                    _logger.LogTrace($"Received AF_INCOMING_MSG from {msg.SrcAddr} and cluster {msg.ClusterId} to endpoint {msg.DstEndpoint}. Data: {msg}");
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
                        _logger.LogError(e, "Error AF message listener notify: {Exception}", e.Message);
                    }
                }
            }
        }

        public void ReceivedUnclaimedSynchronousCommandResponse(ZToolPacket packet)
        {
            // No need to handle unclaimed responses here
        }
    }
}
