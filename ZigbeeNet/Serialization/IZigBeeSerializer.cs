using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;
using ZigbeeNet.ZCL.Protocol;

namespace ZigbeeNet.Serialization
{
    /// <summary>
    /// The interface for serialization of a ZCL frame to array of bytes
    /// </summary>
    public interface IZigBeeSerializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">Object containing the value to append</param>
        /// <param name="type">ZclDataType to select of data has to be appended</param>
        void AppendZigBeeType(object data, ZclDataType type);

        /// <summary>
        /// Returnss a copy of the payload
        /// </summary>
        /// <returns></returns>
        int[] Payload { get; }
    }
}
