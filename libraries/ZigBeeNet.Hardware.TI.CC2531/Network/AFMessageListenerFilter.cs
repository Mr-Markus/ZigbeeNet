using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.Hardware.TI.CC2531.Packet.AF;
using Serilog;

namespace ZigBeeNet.Hardware.TI.CC2531.Network
{
    internal class AFMessageListenerFilter : IAsynchronousCommandListener
    {
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
                    Log.Warning($"Received AF_INCOMING_MSG but no listeners. Message was from {msg.SrcAddr} and cluster {msg.ClusterId} to endpoint {msg.DstEndpoint}. Data: {msg}");
                } else
                {
                    Log.Verbose($"Received AF_INCOMING_MSG from {msg.SrcAddr} and cluster {msg.ClusterId} to endpoint {msg.DstEndpoint}. Data: {msg}");
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
                        Log.Error(e, "Error AF message listener notify");
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
