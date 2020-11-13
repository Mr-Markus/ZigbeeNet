using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL
{
    /// <summary>
    /// The attribute normalizer allows attribute type conversion to ensure that attribute data stored in the
    /// <see cref="ZclAttribute"> class is always of the type defined in the library. This ensures that any devices not conforming
    /// to the ZCL definition of the attribute type can be normalized before updating the <see cref="ZclAttribute">. This in turn
    /// guarantees that applications can rely on the data type.
    /// </summary>
    public class ZclAttributeNormalizer
    {
        /// <summary>
         /// Normalize ZCL data
         ///
         /// <param name="dataType">The <see cref="ZclDataType"> used for the normalised output</param>
         /// <param name="data">the input data</param>
         /// <returns>the normalised output data</returns>
         /// </summary>
        public object NormalizeZclData(ZclDataType zclDataType, object data)
        {
            try
            {
                switch (zclDataType.DataType)
                {
                    case DataType.BOOLEAN:
                        return Convert.ToBoolean(data);

                    case DataType.DATA_8_BIT:
                    case DataType.BITMAP_8_BIT:
                    case DataType.UNSIGNED_8_BIT_INTEGER:
                    case DataType.ENUMERATION_8_BIT:
                        return Convert.ToByte(data);

                    case DataType.BITMAP_16_BIT:
                    case DataType.UNSIGNED_16_BIT_INTEGER:
                    case DataType.ENUMERATION_16_BIT:
                        return Convert.ToUInt16(data);

                    case DataType.BITMAP_24_BIT:
                    case DataType.BITMAP_32_BIT:
                    case DataType.UNSIGNED_24_BIT_INTEGER:
                    case DataType.UNSIGNED_32_BIT_INTEGER:
                    case DataType.ENUMERATION_32_BIT:
                        return Convert.ToUInt32(data);

                    case DataType.BITMAP_40_BIT:
                    case DataType.BITMAP_48_BIT:
                    case DataType.BITMAP_56_BIT:
                    case DataType.BITMAP_64_BIT:
                    case DataType.UNSIGNED_40_BIT_INTEGER:
                    case DataType.UNSIGNED_48_BIT_INTEGER:
                    case DataType.UNSIGNED_56_BIT_INTEGER:
                    case DataType.UNSIGNED_64_BIT_INTEGER:
                        return Convert.ToUInt64(data);

                    case DataType.SIGNED_8_BIT_INTEGER:
                        return Convert.ToSByte(data);

                    case DataType.SIGNED_16_BIT_INTEGER:
                        return Convert.ToInt16(data);

                    case DataType.SIGNED_24_BIT_INTEGER:
                    case DataType.SIGNED_32_BIT_INTEGER:
                        return Convert.ToInt32(data);

                    case DataType.SIGNED_40_BIT_INTEGER:
                    case DataType.SIGNED_48_BIT_INTEGER:
                    case DataType.SIGNED_56_BIT_INTEGER:
                    case DataType.SIGNED_64_BIT_INTEGER:
                        return Convert.ToInt64(data);

                    case DataType.FLOAT_16_BIT:
                    case DataType.FLOAT_32_BIT:
                        return Convert.ToSingle(data);

                    case DataType.FLOAT_64_BIT:
                        return Convert.ToDouble(data);

                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                Log.Warning("Exception normalizing data: {Exception}", ex);
            }
            return data;
        }
    }
}
