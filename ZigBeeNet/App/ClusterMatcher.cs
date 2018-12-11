using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Logging;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Command;

namespace ZigBeeNet.App
{
    public class ClusterMatcher : IZigBeeCommandListener
    {
        private readonly ILog _logger = LogProvider.For<ClusterMatcher>();

        private ZigBeeNetworkManager _networkManager;

        private List<ushort> _clusters = new List<ushort>();

        public ClusterMatcher(ZigBeeNetworkManager networkManager)
        {
            _logger.Debug("ClusterMatcher starting");
            _networkManager = networkManager;

            _networkManager.AddCommandListener(this);
        }

        public void AddCluster(ushort cluster)
        {
            _logger.Debug($"ClusterMatcher adding cluster {cluster}");
            _clusters.Add(cluster);
        }

        public void CommandReceived(ZigBeeCommand command)
        {
            // If we have local servers matching the request, then we need to respond
            if (command is MatchDescriptorRequest matchRequest) {
                _logger.Debug("{ExtPanId}: ClusterMatcher received request {Match}", _networkManager.ZigBeeExtendedPanId, matchRequest);
                if (matchRequest.ProfileId != 0x104)
                {
                    // TODO: Do we need to restrict the profile? Remove this check?
                    return;
                }

                // We want to match any of our local servers (ie our input clusters) with any
                // requested clusters in the requests cluster list
                if (matchRequest.InClusterList.Intersect(_clusters).Count() == 0
                        && matchRequest.OutClusterList.Intersect(_clusters).Count() == 0)
                {
                    _logger.Debug("{ExtPanId}: ClusterMatcher no match", _networkManager.ZigBeeExtendedPanId);
                    return;
                }

                MatchDescriptorResponse matchResponse = new MatchDescriptorResponse();
                matchResponse.Status = ZdoStatus.SUCCESS;
                List<ushort> matchList = new List<ushort>();
                matchList.Add(1);
                matchResponse.MatchList = matchList;

                matchResponse.DestinationAddress = command.SourceAddress;
                matchResponse.NwkAddrOfInterest = matchRequest.NwkAddrOfInterest;
                _logger.Debug("{ExtPanId}: ClusterMatcher sending match {Response}", _networkManager.ZigBeeExtendedPanId, matchResponse);
                _networkManager.SendCommand(matchResponse);
            }
        }
    }
}
