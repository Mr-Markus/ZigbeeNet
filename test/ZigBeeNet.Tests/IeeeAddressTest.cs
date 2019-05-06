using System;
using Xunit;

namespace ZigBeeNet.Tests
{
    public class IeeeAddressTest
    {
        [Fact]
        public void IsEqual()
        {
            IeeeAddress address1 = new IeeeAddress("17880100dc880b");
            IeeeAddress address2 = new IeeeAddress("17880100dc880b");
            Assert.True(address1.Equals(address2));
        }
    }
}
