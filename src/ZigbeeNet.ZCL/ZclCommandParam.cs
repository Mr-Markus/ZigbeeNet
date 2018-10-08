using System;
using BinarySerialization;

namespace ZigbeeNet.ZCL
{
    public class ZclCommandParam
    {
        [Ignore()]
        public string Name { get; set; }

        [Ignore()]
        public DataType DataType { get; set; }

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
                        case DataType.Boolean:
                            _value = new ZclCommandParamValue<bool>(Convert.ToBoolean(value));
                            break;
                        case DataType.UInt8:
                        case DataType.Int8:
                        case DataType.Data8:
                        case DataType.Enum8:
                            _value = new ZclCommandParamValue<byte>(Convert.ToByte(value));
                            break;
                        case DataType.Int16:
                        case DataType.Data16:
                        case DataType.Enum16:
                        case DataType.Map16:
                        case DataType.String16:
                            _value = new ZclCommandParamValue<short>(Convert.ToInt16(value));
                            break;
                        case DataType.Int32:
                        case DataType.Data32:
                        case DataType.Map32:
                            _value = new ZclCommandParamValue<int>(Convert.ToInt32(value));
                            break;
                        case DataType.Int64:
                        case DataType.IeeeAddress:
                            _value = new ZclCommandParamValue<long>(Convert.ToInt64(value));
                            break;
                        case DataType.UInt16:
                            _value = new ZclCommandParamValue<ushort>(Convert.ToUInt16(value));
                            break;
                        case DataType.UInt32:
                        case DataType.UTC:
                            _value = new ZclCommandParamValue<uint>(Convert.ToUInt32(value));
                            break;
                        case DataType.Double:
                            _value = new ZclCommandParamValue<double>(Convert.ToDouble(value));
                            break;
                        case DataType.UInt64:
                            _value = new ZclCommandParamValue<ulong>(Convert.ToUInt64(value));
                            break;
                        case DataType.String:
                            _value = new ZclCommandParamValue<string>(Convert.ToString(value));
                            break;
                        case DataType.Date:
                            DateTime date = Convert.ToDateTime(value);
                            value = new ZclCommandParamValue<byte[]>(new byte[4] { (byte)date.Year, (byte)date.Month, (byte)date.Day, (byte)date.DayOfWeek });
                            break;
                        case DataType.TimeOfDay:
                            DateTime time = Convert.ToDateTime(value);
                            value = new ZclCommandParamValue<byte[]>(new byte[4] { (byte)time.Hour, (byte)time.Minute, (byte)time.Second, (byte)(time.Millisecond / 10) });
                            break;
                        default:

                            break;
                    }
                }
            }
        }
    }
}