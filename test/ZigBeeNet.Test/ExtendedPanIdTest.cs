using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ZigBeeNet.Test
{
    public class ExtendedPanIdTest
    {
        [Fact]
        public void TestConstructorBigInteger()
        {
            ExtendedPanId address = new ExtendedPanId(Convert.ToInt64("0017880100DC880B", 16));
            Assert.Equal("0017880100DC880B", address.ToString());
        }

        [Fact]
        public void TestConstructorArray()
        {
            ExtendedPanId address = new ExtendedPanId(new byte[] { 0x0b, 0x88, 0xdc, 0x00, 0x01, 0x88, 0x17, 0x00 });
            Assert.Equal("0017880100DC880B", address.ToString());
        }

        [Fact]
        public void TestConstructorArrayShort()
        {
            Assert.Throws<ArgumentException>(() => new ExtendedPanId(new byte[] { 0x0b, 0x88, 0xdc, 0x00, 0x01, 0x88, 0x17 }));
        }

        [Fact]
        public void TestConstructorString()
        {
            ExtendedPanId address = new ExtendedPanId("17880100dc880b");
            Assert.Equal("0017880100DC880B", address.ToString());

            address = new ExtendedPanId("8418260000D9959B");
            Assert.Equal("8418260000D9959B", address.ToString());
        }

        [Fact]
        public void TestValid()
        {
            ExtendedPanId address = new ExtendedPanId("17880100dc880b");
            Assert.True(address.IsValid());

            address = new ExtendedPanId();
            Assert.False(address.IsValid());

            address = new ExtendedPanId("0000000000000000");
            Assert.False(address.IsValid());

            address = new ExtendedPanId("FFFFFFFFFFFFFFFF");
            Assert.False(address.IsValid());
        }

        [Fact]
        public void TestHash()
        {
            ExtendedPanId address1 = new ExtendedPanId("17880100dc880b");
            ExtendedPanId address2 = new ExtendedPanId("17880100dc880b");
            Assert.Equal(address1.GetHashCode(), address2.GetHashCode());
        }

        [Fact]
        public void TestEquals()
        {
            ExtendedPanId address1 = new ExtendedPanId("17880100dc880b");
            ExtendedPanId address2 = new ExtendedPanId("17880100dc880b");
            Assert.Equal(address1, address2);
        }

        [Fact]
        public void TestToString()
        {
            ExtendedPanId address = new ExtendedPanId("17880100dc880b");
            Assert.Equal("0017880100DC880B", address.ToString());
        }
    }
}
