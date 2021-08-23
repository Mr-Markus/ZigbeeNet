using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Command;
using ZigBeeNet.Util;
using Microsoft.Extensions.Logging;

namespace ZigBeeNet.Internal
{
    /**
     * Class to respond to the <see cref="MatchDescriptorRequest"/>.
     *
     * Note that this class currently only supports clusters that are not manufacturer-specific.
     */
    public class ClusterMatcher : IZigBeeCommandListener
    {
        /// <summary>
        /// ILogger for logging events for this class
        /// </summary>
        private static ILogger _logger = LogManager.GetLog<ClusterMatcher>();

        private ZigBeeNetworkManager _networkManager;
        private byte _localEndpointId;
        private int _profileId;

        private HashSet<ushort> _clientClusters = new HashSet<ushort>();
        private HashSet<ushort> _serverClusters = new HashSet<ushort>();

        public ClusterMatcher(ZigBeeNetworkManager networkManager, byte localEndpointId, int profileId)
        {
            _logger.LogDebug("ClusterMatcher starting");
            _networkManager = networkManager;
            _localEndpointId = localEndpointId;
            _profileId = profileId;

            _networkManager.AddCommandListener(this);
        }

        public void Shutdown()
        {
            _networkManager.RemoveCommandListener(this);
        }

        public void AddClientCluster(ushort cluster)
        {
            _logger.LogDebug($"ClusterMatcher adding client cluster {cluster}");
            _clientClusters.Add(cluster);
        }

        public void AddServerCluster(ushort cluster)
        {
            _logger.LogDebug($"ClusterMatcher adding server cluster {cluster}");
            _serverClusters.Add(cluster);
        }

        public void CommandReceived(ZigBeeCommand command)
        {
            // If we have local servers matching the request, then we need to respond
            if (command is MatchDescriptorRequest matchRequest)
            {
                _logger.LogDebug("{ExtPanId}: ClusterMatcher received request {Match}", _networkManager.ZigBeeExtendedPanId, matchRequest);
                if (matchRequest.ProfileId != _profileId)
                {
                    _logger.LogDebug("{ExtPanId}: ClusterMatcher no match to profileId", _networkManager.ZigBeeExtendedPanId);
                    return;
                }

                if (matchRequest.NwkAddrOfInterest != _networkManager.LocalNwkAddress
                    && !ZigBeeBroadcastDestinationHelper.IsBroadcast(matchRequest.NwkAddrOfInterest))
                {
                    _logger.LogDebug("{ExtPanId}: ClusterMatcher no match to local address", _networkManager.ZigBeeExtendedPanId);
                    return;
                }

                // We want to match any of our local servers (ie our input clusters) with any
                // requested clusters in the requests cluster list
                if (matchRequest.InClusterList.Intersect(_serverClusters).Count() == 0
                        && matchRequest.OutClusterList.Intersect(_clientClusters).Count() == 0)
                {
                    _logger.LogDebug("{ExtPanId}: ClusterMatcher no match", _networkManager.ZigBeeExtendedPanId);
                    return;
                }

                MatchDescriptorResponse matchResponse = new MatchDescriptorResponse();
                matchResponse.Status = ZdoStatus.SUCCESS;
                List<byte> matchList = new List<byte>();
                matchList.Add(_localEndpointId);
                matchResponse.MatchList = matchList;
                matchResponse.NwkAddrOfInterest = _networkManager.LocalNwkAddress;
                matchResponse.TransactionId = matchRequest.TransactionId;
                matchResponse.DestinationAddress = command.SourceAddress;
                _logger.LogDebug("{ExtPanId}: ClusterMatcher sending match {Response}", _networkManager.ZigBeeExtendedPanId, matchResponse);
                _networkManager.SendCommand(matchResponse);
            }
        }
    }
}
