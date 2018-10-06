using BinarySerialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public class ZclCommandParamValue<TDataType>
    {
        [FieldOrder(0)]
        [SerializeWhen(nameof(SerializeLength), true)]
        public byte Length { get; set; }

        [FieldOrder(1)]
        [SerializeWhen(nameof(SerializeCount), true)]
        public byte Count { get; set; }

        [FieldOrder(2)]
        [FieldLength(nameof(Length))]
        public TDataType Value { get; set; }

        [Ignore()]
        public bool SerializeLength { get; set; }

        [Ignore()]
        public bool SerializeCount { get; set; }

        public ZclCommandParamValue(TDataType value, DataType dataType = DataType.Unknown)
        {
            Value = value;

            if (typeof(TDataType) == typeof(string))
            {
                SerializeLength = true;
            }
        }
    }
}
