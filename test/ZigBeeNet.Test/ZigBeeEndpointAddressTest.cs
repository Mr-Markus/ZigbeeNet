using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ZigBeeNet.Test
{
    public class ZigBeeEndpointAddressTest
    {
        [Fact]
        public void TestConstructorZdo()
        {
            ZigBeeEndpointAddress address = new ZigBeeEndpointAddress(25000);
            Assert.Equal(25000, address.Address);
            Assert.Equal(0, address.Endpoint);
        }

        [Fact]
        public void TestConstructor()
        {
            ZigBeeEndpointAddress address = new ZigBeeEndpointAddress(25000, 33);
            Assert.Equal(25000, address.Address);
            Assert.Equal(33, address.Endpoint);
        }

        [Fact]
        public void TestStringConstructor()
        {
            ZigBeeEndpointAddress address = new ZigBeeEndpointAddress("25000/33");
            Assert.Equal(25000, address.Address);
            Assert.Equal(33, address.Endpoint);
        }

        [Fact]
        public void TestStringConstructorError()
        {
            Assert.Throws<FormatException>(() => new ZigBeeEndpointAddress(""));
            Assert.Throws<ArgumentException>(() => new ZigBeeEndpointAddress("111/22/33"));
        }

        [Fact]
        public void TestStringConstructorZdo()
        {
            ZigBeeEndpointAddress address = new ZigBeeEndpointAddress("25000");
            Assert.Equal(25000, address.Address);
            Assert.Equal(0, address.Endpoint);
        }

        [Fact]
        public void TestEquals()
        {
            ZigBeeEndpointAddress address1 = new ZigBeeEndpointAddress("25000/33");
            ZigBeeEndpointAddress address2 = new ZigBeeEndpointAddress("25000/33");
            Assert.Equal(address1, address2);
            ZigBeeEndpointAddress address3 = new ZigBeeEndpointAddress("25001/33");
            Assert.NotEqual(address1, address3);
            ZigBeeEndpointAddress address4 = new ZigBeeEndpointAddress(25000, 33);
            Assert.Equal(address1, address4);
        }

        [Fact]
        public void TestCompareTo()
        {
            ZigBeeEndpointAddress address1 = new ZigBeeEndpointAddress("25000/33");
            ZigBeeEndpointAddress address2 = new ZigBeeEndpointAddress("25000/33");
            Assert.Equal(0, address1.CompareTo(address2));
            ZigBeeEndpointAddress address3 = new ZigBeeEndpointAddress("25001/33");
            Assert.True(address1.CompareTo(address3) < 0);
            ZigBeeEndpointAddress address4 = new ZigBeeEndpointAddress("24999/33");
            Assert.True(address1.CompareTo(address4) > 0);
            ZigBeeEndpointAddress address5 = new ZigBeeEndpointAddress("25000/30");
            Assert.True(address1.CompareTo(address5) > 0);
            ZigBeeEndpointAddress address6 = new ZigBeeEndpointAddress("25000/36");
            Assert.True(address1.CompareTo(address6) < 0);
        }

        [Fact]
        public void TestCompareToZdo()
        {
            ZigBeeEndpointAddress address1 = new ZigBeeEndpointAddress(25000);
            ZigBeeEndpointAddress address2 = new ZigBeeEndpointAddress(25000);
            Assert.Equal(0, address1.CompareTo(address2));
            ZigBeeEndpointAddress address3 = new ZigBeeEndpointAddress("25001");
            Assert.Equal(-1, address1.CompareTo(address3));
        }

        [Fact]
        public void TestIsGroup()
        {
            ZigBeeEndpointAddress address = new ZigBeeEndpointAddress("25000/33");
            Assert.False(address.IsGroup);
        }

        [Fact]
        public void TestToString()
        {
            String stringAddress = "25000/44";
            ZigBeeEndpointAddress address = new ZigBeeEndpointAddress(stringAddress);
            Assert.Equal(stringAddress, address.ToString());
        }
    }
}
