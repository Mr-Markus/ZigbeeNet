using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.Test.Serialization
{
    public class DefaultDeserializerTest
    {

        [Fact]
        public void TestDeserialize_DATA_8_BIT()
        {
            byte[] valIn = { 0x9 };
            byte valOut = 0x9;
            TestDeserialize(valIn, valOut, DataType.DATA_8_BIT);
        }

        // TODO: Support RAW_OCTET
        // see: https://github.com/zsmartsystems/com.zsmartsystems.zigbee/commit/720fabdc2333e3da1153b79265d15921b191b1ef
        // and  https://github.com/zsmartsystems/com.zsmartsystems.zigbee/commit/0c582d3355ed640cc1ef854a0dbedbed0b2cec34
        //[Fact]
        //public void TestDeserialize_RAW_OCTET()
        //{
        //    int[] valIn = { 0x00, 0x11, 0x22, 0x44, 0x88, 0xCC, 0xFF };
        //    ByteArray valOut = new ByteArray(new byte[] { 0x00, 0x11, 0x22, 0x44, 0x88, 0xCC, 0xFF });
        //    TestDeserialize(valIn, valOut, DataType.RAW_OCTET);
        //}

        [Fact]
        public void TestDeserialize_SIGNED_16_BIT_INTEGER()
        {
            byte[] valIn = { 0x97, 0x03 };
            Int16 valOut = 0x397;
            TestDeserialize(valIn, valOut, DataType.SIGNED_16_BIT_INTEGER);
        }

        [Fact]
        public void TestDeserialize_UNSIGNED_32_BIT_INTEGER()
        {
            byte[] valIn = { 0x97, 0x03, 0x12, 0x65 };
            uint valOut = 1695679383;
            TestDeserialize(valIn, valOut, DataType.UNSIGNED_32_BIT_INTEGER);
        }

        [Fact]
        public void TestDeserialize_IEEE_ADDRESS()
        {
            byte[] valIn = { 0x56, 0x34, 0x12, 0x90, 0x78, 0x56, 0x34, 0x12 };
            IeeeAddress valOut = new IeeeAddress("1234567890123456");
            TestDeserialize(valIn, valOut, DataType.IEEE_ADDRESS);
        }

        [Fact]
        public void TestDeserialize_EXTENDED_PANID()
        {
            byte[] valIn = { 0x56, 0x34, 0x12, 0x90, 0x78, 0x56, 0x34, 0x12 };
            ExtendedPanId valOut = new ExtendedPanId("1234567890123456");
            TestDeserialize(valIn, valOut, DataType.EXTENDED_PANID);
        }

        [Fact]
        public void TestDeserialize_ZIGBEE_DATA_TYPE()
        {
            byte[] valIn = { 33 };
            ZclDataType valOut = ZclDataType.Get(valIn[0]);
            TestDeserialize(valIn, valOut, DataType.ZIGBEE_DATA_TYPE);
        }

        [Fact]
        public void TestDeserialize_CHARACTER_STRING()
        {
            TestDeserialize<string>(new byte[] { 0xFF }, null, DataType.CHARACTER_STRING);
            TestDeserialize(new byte[] { 0x00 }, "", DataType.CHARACTER_STRING);
            TestDeserialize(new byte[] { 0x01, 0x49 }, "I", DataType.CHARACTER_STRING);
            TestDeserialize(
                    new byte[] { 0x0D, 0x49, 0x6E, 0x74, 0x65, 0x72, 0x6E, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x61, 0x6C },
                    "International", DataType.CHARACTER_STRING);
            TestDeserialize(
                    new byte[] { 0x0E, 0x49, 0x6E, 0x74, 0x65, 0x72, 0x6E, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x61, 0x6C, 0x00 },
                    "International", DataType.CHARACTER_STRING);
            TestDeserialize(new byte[] { 0x1F, 0x4D, 0x61, 0x65, 0x73, 0x74, 0x72, 0x6F, 0x53, 0x74, 0x61, 0x74, 0x00, 0x00,
                0x00, 0xBB, 0xEF, 0x00, 0x00, 0x00, 0x00, 0xA7, 0x43, 0x00, 0xA4, 0x29, 0x02, 0x01, 0x3A, 0x02, 0x00,
                0x00 }, "MaestroStat", DataType.CHARACTER_STRING);
        }

        private void TestDeserialize<T>(byte[] input, T objectIn, DataType type)
        {
            DefaultDeserializer deserializer = new DefaultDeserializer(input);
            var objectOut = deserializer.ReadZigBeeType<T>(type);
            Assert.Equal(objectIn, objectOut);
        }
    }
}
