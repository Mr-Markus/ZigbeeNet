using System;
using BinarySerialization;

namespace ZigBeeNet.ZCL
{
    public class ZclCommandParam
    {
        [Ignore()]
        public string Name { get; set; }

        [Ignore()]
        public ZclDataType DataType { get; set; }

        [Ignore()]
        public string SpecialType { get; set; }

        [Ignore()]
        private object _value;

        [FieldOrder(0)]
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (string.IsNullOrEmpty(SpecialType))
                {
                    switch (DataType)
                    {
                        case ZclDataType.Boolean:
                            _value = Convert.ToBoolean(value);
                            break;
                        case ZclDataType.UInt8:
                        case ZclDataType.Int8:
                        case ZclDataType.Data8:
                        case ZclDataType.Enum8:
                            _value = Convert.ToByte(value);
                            break;
                        case ZclDataType.Int16:
                        case ZclDataType.Data16:
                        case ZclDataType.Enum16:
                        case ZclDataType.Map16:
                        case ZclDataType.String16:
                            _value = Convert.ToInt16(value);
                            break;
                        case ZclDataType.Int32:
                        case ZclDataType.Data32:
                        case ZclDataType.Map32:
                            _value = Convert.ToInt32(value);
                            break;
                        case ZclDataType.Int64:
                        case ZclDataType.IeeeAddress:
                            _value = Convert.ToInt64(value);
                            break;
                        case ZclDataType.UInt16:
                            _value = Convert.ToUInt16(value);
                            break;
                        case ZclDataType.UInt32:
                        case ZclDataType.UTC:
                            _value = Convert.ToUInt32(value);
                            break;
                        case ZclDataType.Double:
                            _value = Convert.ToDouble(value);
                            break;
                        case ZclDataType.UInt64:
                            _value = Convert.ToUInt64(value);
                            break;
                        case ZclDataType.String:
                            _value = Convert.ToString(value);
                            break;
                        case ZclDataType.Date:
                            DateTime date = Convert.ToDateTime(value);
                            value = new byte[4] { (byte)date.Year, (byte)date.Month, (byte)date.Day, (byte)date.DayOfWeek };
                            break;
                        case ZclDataType.TimeOfDay:
                            DateTime time = Convert.ToDateTime(value);
                            value = new byte[4] { (byte)time.Hour, (byte)time.Minute, (byte)time.Second, (byte)(time.Millisecond / 10) };
                            break;
                        default:

                            break;
                    }
                } else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}