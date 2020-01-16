using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.Ember.Ezsp;
using ZigBeeNet.Hardware.Ember.Ezsp.Structure;
using ZigBeeNet.Hardware.Ember.Transaction;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.Ember.Internal
{
    /// <summary>
    /// This class provides utility functions to configure, and read the configuration from the Ember stack.
    /// </summary>
    public class EmberStackConfiguration
    {
        /**
         * The {@link EmberNcp} used to send the EZSP frames to the NCP
         */
        private EmberNcp _ncp;

        /**
         * Constructor to set the {@link EmberNcp}
         *
         * @param ncp the {@link EmberNcp} used to communicate with the NCP
         */
        public EmberStackConfiguration(EmberNcp ncp) 
        {
            this._ncp = ncp;
        }

        /**
         * Configuration utility. Takes a {@link Map} of {@link EzspConfigId} to {@link Integer} and will work through
         * setting them before returning.
         *
         * @param configuration {@link Map} of {@link EzspConfigId} to {@link Integer} with configuration to set
         * @return true if all configuration were set successfully
         */
        public bool SetConfiguration(Dictionary<EzspConfigId, int> configuration) 
        {
            bool success = true;

            foreach (var config in configuration) 
            {
                if (_ncp.SetConfiguration(config.Key, config.Value) != EzspStatus.EZSP_SUCCESS) 
                {
                    success = false;
                }
            }
            return success;
        }

        /**
         * Configuration utility. Takes a {@link Set} of {@link EzspConfigId} and will work through
         * requesting them before returning.
         *
         * @param configuration {@link Set} of {@link EzspConfigId} to request
         * @return map of configuration data mapping {@link EzspConfigId} to {@link Integer}. Value will be null if error
         *         occurred.
         */
        public Dictionary<EzspConfigId, int?> GetConfiguration(IEnumerable<EzspConfigId> configuration) 
        {
            Dictionary<EzspConfigId, int?> response = new Dictionary<EzspConfigId, int?>();

            foreach (EzspConfigId configId in configuration) 
            {
                response.Add(configId, _ncp.GetConfiguration(configId));
            }

            return response;
        }

        /**
         * Configuration utility. Takes a {@link Map} of {@link EzspConfigId} to {@link EzspDecisionId} and will work
         * through setting them before returning.
         *
         * @param policies {@link Map} of {@link EzspPolicyId} to {@link EzspDecisionId} with configuration to set
         * @return true if all policies were set successfully
         */
        public bool SetPolicy(Dictionary<EzspPolicyId, EzspDecisionId> policies) 
        {
            bool success = true;

            foreach (var policy in policies) 
            {
                if (_ncp.SetPolicy(policy.Key, policy.Value) != EzspStatus.EZSP_SUCCESS) 
                {
                    success = false;
                }
            }
            return success;
        }

        /**
         * Configuration utility. Takes a {@link Set} of {@link EzspPolicyId} and will work through
         * requesting them before returning.
         *
         * @param policies {@link Set} of {@link EzspPolicyId} to request
         * @return map of configuration data mapping {@link EzspPolicyId} to {@link EzspDecisionId}. Value will be null if
         *         error occurred.
         */
        public Dictionary<EzspPolicyId, EzspDecisionId> GetPolicy(IEnumerable<EzspPolicyId> policies) 
        {
            Dictionary<EzspPolicyId, EzspDecisionId> response = new Dictionary<EzspPolicyId, EzspDecisionId>();

            foreach (EzspPolicyId policyId in policies) 
            {
                response.Add(policyId, _ncp.GetPolicy(policyId));
            }

            return response;
        }

    }
}
