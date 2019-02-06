using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL
{
    /**
    * The attribute normalizer allows attribute type conversion to ensure that attribute data stored in the
    * {@link ZclAttribute} class is always of the type defined in the library. This ensures that any devices not conforming
    * to the ZCL definition of the attribute type can be normalized before updating the {@link ZclAttribute}. This in turn
    * guarantees that applications can rely on the data type.
    */
    public class ZclAttributeNormalizer
    {
        /**
         * The logger
         */
        //private Logger logger = LoggerFactory.getLogger(ZclAttributeNormalizer.class);

        /**
         * Normalize ZCL data
         *
         * @param dataType The {@link ZclDataType} used for the normalised output
         * @param data the input data
         * @return the normalised output data
         */
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
