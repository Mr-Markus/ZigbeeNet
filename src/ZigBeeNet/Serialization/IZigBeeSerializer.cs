using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.Serialization
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
        void AppendZigBeeType(object data, DataType type);

        /// <summary>
        /// Returnss a copy of the payload
        /// </summary>
        /// <returns></returns>
        byte[] Payload { get; }
    }
}
