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
         /// The logger
         /// </summary>
        //private Logger logger = LoggerFactory.getLogger(ZclAttributeNormalizer.class);

        /// <summary>
         /// Normalize ZCL data
         ///
         /// <param name="dataType">The <see cref="ZclDataType"> used for the normalised output</param>
         /// <param name="data">the input data</param>
         /// <returns>the normalised output data</returns>
         /// </summary>
        public object NormalizeZclData(ZclDataType zclDataType, object data)
        {
            switch (zclDataType.DataType)
            {
                case DataType.BOOLEAN:
                    if (data is int intValue)
                    {
                        //logger.debug("Normalizing data Integer {} to BOOLEAN", data);

                        return !(intValue == 0);
                    }
                    break;
                case DataType.UNSIGNED_8_BIT_INTEGER:
                    if (data is string stringValue)
                    {
                        //logger.debug("Normalizing data String {} to UNSIGNED_8_BIT_INTEGER", data);

                        return int.Parse(stringValue);
                    }
                    break;
                default:
                    break;
            }
            return data;
        }
    }
}
