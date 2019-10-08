using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ZigBeeNet.Test
{
    public class ZigBeeNodeTest
    {
        [Fact]
        public void TestSetIeeeAddress()
        {
            ZigBeeNode node = new ZigBeeNode();
            node.IeeeAddress = new IeeeAddress("17880100dc880b");

            Assert.Equal(new IeeeAddress("17880100dc880b"), node.IeeeAddress);
        }
    }
}
