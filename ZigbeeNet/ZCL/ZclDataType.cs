using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    /// <summary>
    /// Cluster specifications MAY use the unique data type short name to reduce the text size of the specification.
    /// 
    /// Table 2-10
    /// </summary>
    public enum ZclDataType
    {
        NoData = 0,
        Data8 = 8,
        Data16 = 9,
        Data24 = 10,
        Data32 = 11,
        Data40 = 12,
        Data48 = 13,
        Data56 = 14,
        Data64 = 15,
        Boolean = 16,
        Map8 = 24,
        Map16 = 25,
        Map24 = 26,
        Map32 = 27,
        Map40 = 28,
        Map48 = 29,
        Map56 = 30,
        Map64 = 31,
        UInt8 = 32,
        UInt16 = 33,
        UInt24 = 34,
        UInt32 = 35,
        UInt40 = 36,
        UInt48 = 37,
        UInt56 = 38,
        UInt64 = 39,
        Int8 = 40,
        Int16 = 41,
        Int24 = 42,
        Int32 = 43,
        Int40 = 44,
        Int48 = 45,
        Int56 = 46,
        Int64 = 47,
        Enum8 = 48,
        Enum16 = 49,
        Semi = 56,
        Single = 57,
        Double = 58,
        Octstr = 65,
        String = 66,
        Octstr16 = 67,
        String16 = 68,
        Array = 72,
        Struct = 76,
        Set = 80,
        Bag = 81,
        TimeOfDay = 224,
        Date = 225,
        UTC = 226,
        ClusterId = 232,
        AttributeId = 233,
        BacOID = 234,
        IeeeAddress = 240,
        Key128 = 241,
        //Opaque
        Unknown = 255
    }
}
