using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Command;
using Serilog;

namespace ZigBeeNet.Internal
{
    /**
     * Class to respond to the <see cref="MatchDescriptorRequest"/>.
     *
     * Note that this class currently only supports clusters that are not manufacturer-specific.
     */
    public class ClusterMatcher : IZigBeeCommandListener
    {
        private ZigBeeNetworkManager _networkManager;
        private byte _localEndpointId;
        private int _profileId;

        private HashSet<ushort> _clientClusters = new HashSet<ushort>();
        private HashSet<ushort> _serverClusters = new HashSet<ushort>();

        public ClusterMatcher(ZigBeeNetworkManager networkManager, byte localEndpointId, int profileId)
        {
            Log.Debug("ClusterMatcher starting");
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
            Log.Debug($"ClusterMatcher adding client cluster {cluster}");
            _clientClusters.Add(cluster);
        }

        public void AddServerCluster(ushort cluster)
        {
            Log.Debug($"ClusterMatcher adding server cluster {cluster}");
            _serverClusters.Add(cluster);
        }

        public void CommandReceived(ZigBeeCommand command)
        {
            // If we have local servers matching the request, then we need to respond
            if (command is MatchDescriptorRequest matchRequest)
            {
                Log.Debug("{ExtPanId}: ClusterMatcher received request {Match}", _networkManager.ZigBeeExtendedPanId, matchRequest);
                if (matchRequest.ProfileId != _profileId)
                {
                    Log.Debug("{ExtPanId}: ClusterMatcher no match to profileId", _networkManager.ZigBeeExtendedPanId);
                    return;
                }

                if (matchRequest.NwkAddrOfInterest != _networkManager.LocalNwkAddress
                    && !ZigBeeBroadcastDestination.IsBroadcast(matchRequest.NwkAddrOfInterest))
                {
                    Log.Debug("{ExtPanId}: ClusterMatcher no match to local address", _networkManager.ZigBeeExtendedPanId);
                    return;
                }

                // We want to match any of our local servers (ie our input clusters) with any
                // requested clusters in the requests cluster list
                if (matchRequest.InClusterList.Intersect(_serverClusters).Count() == 0
                        && matchRequest.OutClusterList.Intersect(_clientClusters).Count() == 0)
                {
                    Log.Debug("{ExtPanId}: ClusterMatcher no match", _networkManager.ZigBeeExtendedPanId);
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
                Log.Debug("{ExtPanId}: ClusterMatcher sending match {Response}", _networkManager.ZigBeeExtendedPanId, matchResponse);
                _networkManager.SendCommand(matchResponse);
            }
        }
    }
}
