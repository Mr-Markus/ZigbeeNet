using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;

namespace ZigBeeNet.ZCL
{
    /// <summary>
    /// ZclSerializableField class for non primitive field types.
    /// </summary>
    public interface IZclListItemField
    {
        /// <summary>
        /// Serializes the field.
        /// </summary>
        /// <param name="serializer"></param>
        void Serialize(IZigBeeSerializer serializer);

        /// <summary>
        /// Deserializes the field.
        /// </summary>
        /// <param name="serializer"></param>
        void Deserialize(IZigBeeDeserializer deserializer);
    }
}
