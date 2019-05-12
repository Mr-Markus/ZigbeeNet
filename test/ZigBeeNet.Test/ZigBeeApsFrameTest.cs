using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace ZigBeeNet.Test
{
    public class ZigBeeApsFrameTest
    {
        private readonly ITestOutputHelper _output;

        public ZigBeeApsFrameTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestToString()
        {
            ZigBeeApsFrame frame = new ZigBeeApsFrame();

            Assert.NotNull(frame.ToString());
        }

        [Fact]
        public void TestSecurityEnable()
        {
            ZigBeeApsFrame frame = new ZigBeeApsFrame();

            frame.SecurityEnabled = true;
            Assert.True(frame.SecurityEnabled);

            frame.SecurityEnabled = false;
            Assert.False(frame.SecurityEnabled);

            _output.WriteLine(frame.ToString());
        }

        [Fact]
        public void TestRadius()
        {
            ZigBeeApsFrame frame = new ZigBeeApsFrame();

            frame.Radius = 4;
            Assert.Equal(4, frame.Radius);
        }

        [Fact]
        public void TestNonMemberRadius()
        {
            ZigBeeApsFrame frame = new ZigBeeApsFrame();

            frame.NonMemberRadius = 4;
            Assert.Equal(4, frame.NonMemberRadius);
        }

        [Fact]
        public void TestGroupAddress()
        {
            ZigBeeApsFrame frame = new ZigBeeApsFrame();
            _output.WriteLine(frame.ToString());

            frame.GroupAddress = 1;
            Assert.Equal(1, frame.GroupAddress);
        }
    }
}
