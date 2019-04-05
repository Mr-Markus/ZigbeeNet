using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZigBeeNet.Transport
{
    public class TransportConfig
    {
        private Dictionary<TransportConfigOption, object> _request;
        private Dictionary<TransportConfigOption, ZigBeeStatus> _response;

        public TransportConfig()
        {
            _request = new Dictionary<TransportConfigOption, object>();
            _response = new Dictionary<TransportConfigOption, ZigBeeStatus>();
        }

        /// <summary>
         /// Creates a configuration and directly adds the option
         ///
         /// @param option
         /// @param value
         /// </summary>
        public TransportConfig(TransportConfigOption option, object value)
        {
            _request[option] = value;
        }

        /// <summary>
         /// Adds a <see cref="TransportConfigOption"> and its value. The same option can't be added to the configuration twice.
         ///
         /// <param name="option">the <see cref="TransportConfigOption"> to set</param>
         /// <returns>true if the option was added, false if the option already existed</returns>
         /// </summary>
        public bool AddOption(TransportConfigOption option, object value)
        {
            if (_request.ContainsKey(option))
            {
                return false;
            }
            _request.Add(option, value);
            return true;
        }

        /// <summary>
         /// Gets the a <see cref="TransportConfigOption"> if it is configured
         ///
         /// <param name="option">the <see cref="TransportConfigOption"> to retrieve</param>
         /// <returns>the requested <see cref="TransportConfigOption"> value or null if it is not set</returns>
         /// </summary>
        public object GetOption(TransportConfigOption option)
        {
            return _request[option];
        }

        /// <summary>
         /// Gets the <see cref="Set"> of <see cref="TransportConfigOption">s
         ///
         /// <returns>the <see cref="Set"> of <see cref="TransportConfigOption">s</returns>
         /// </summary>
        public List<TransportConfigOption> GetOptions()
        {
            return _request.Keys.ToList();
        }

        /// <summary>
         /// Gets a value for the specified <see cref="TransportConfigOption">
         ///
         /// <param name="option">the <see cref="TransportConfigOption"> to retrieve</param>
         /// <returns>the <see cref="Object"></returns>
         /// </summary>
        public object GetValue(TransportConfigOption option)
        {
            return _request[option];
        }

        /// <summary>
         /// Sets the <see cref="ZigBeeStatus"> for a configuration setting
         ///
         /// <param name="option">the <see cref="TransportConfigOption"> to set the result</param>
         /// <param name="value">the <see cref="ZigBeeStatus"></param>
         /// <returns>true if the result was set, false if the option did not exist or the result was already set</returns>
         /// </summary>
        public bool SetResult(TransportConfigOption option, ZigBeeStatus value)
        {
            if (_request.ContainsKey(option) == false || _request[option] == null || _response.ContainsKey(option))
            {
                return false;
            }
            _response[option] = value;

            return true;
        }

        /// <summary>
         /// Gets the the <see cref="TransportConfigResult"> for a <see cref="TransportConfigOption"> if it is configured
         ///
         /// <param name="option">the <see cref="TransportConfigOption"> to retrieve the result</param>
         /// <returns>the result <see cref="ZigBeeStatus"> for the requested <see cref="TransportConfigOption"></returns>
         /// </summary>
        public ZigBeeStatus GetResult(TransportConfigOption option)
        {
            if (_request.ContainsKey(option) == false)
            {
                return ZigBeeStatus.INVALID_ARGUMENTS;
            }
            if (_response.ContainsKey(option) == false)
            {
                return ZigBeeStatus.BAD_RESPONSE;
            }
            return _response[option];
        }
    }
}
