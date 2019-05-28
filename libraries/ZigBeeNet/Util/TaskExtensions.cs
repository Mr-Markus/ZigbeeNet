using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZigBeeNet
{
    public static class TaskExtensions
    {
        public static async Task TimeoutAfter(this Task task, int millisecondsTimeout, Action timeoutAction = null)
        {
            if (task == await Task.WhenAny(task, Task.Delay(millisecondsTimeout)))
                await task;
            else if (timeoutAction != null)
                timeoutAction();
            else
                throw new TimeoutException();
        }
    }
}
