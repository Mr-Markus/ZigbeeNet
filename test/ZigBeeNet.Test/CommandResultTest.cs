using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Clusters.General;

namespace ZigBeeNet.Test
{
    public class CommandResultTest
    {
        [Fact]
        public void testIsError()
        {
            CommandResult result = new CommandResult();
            Assert.True(result.IsError());

            result = new CommandResult(new ZigBeeCommand());
            Assert.False(result.IsError());
            Assert.True(result.IsSuccess());

            DefaultResponse response = new DefaultResponse();
            response.StatusCode = ZclStatus.SUCCESS;
            result = new CommandResult(response);
            Assert.False(result.IsError());
            Assert.True(result.IsSuccess());

            response = new DefaultResponse();
            response.StatusCode = ZclStatus.FAILURE;
            result = new CommandResult(response);
            Assert.True(result.IsError());
            Assert.False(result.IsSuccess());
        }
    }
}
