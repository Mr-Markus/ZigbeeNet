using System;
using Xunit;

namespace ZigBeeNet.Test
{
    public class IeeeAddressTest
    {
        [Fact]
        public void IsEqual()
        {
            // byte[] order is little endian
            byte[] baAddress = new byte[] {0x0b,0x88,0xdc,0x00,0x01,0x88,0x17,0x00};
            IeeeAddress address1 = new IeeeAddress(0x17880100dc880bul);
            IeeeAddress address2 = new IeeeAddress("17880100dc880b");
            IeeeAddress address3 = new IeeeAddress(baAddress);
            Assert.True(address1.Equals(address2));
            Assert.True(address1.Equals(address3));
            Assert.Equal(address1.GetAddress(),baAddress);
        }

    }
}
