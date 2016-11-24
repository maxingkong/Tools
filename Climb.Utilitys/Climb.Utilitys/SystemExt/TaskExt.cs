using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Climb.Utilitys.SystemExt
{
    /// <summary>
    /// 对于task 的扩展
    /// </summary>
    public static  class TaskExt
    {
        /// <summary>
        /// 等待某个时间之后获取task的值
        /// </summary>
        public static TResult WaitResult<TResult>(this Task<TResult> task, int timeoutMillis)
        {
            return task.Wait(timeoutMillis) ? task.Result : default(TResult);
        }

        /// <summary>设置Task过期时间
        /// </summary>
        public static async Task TimeoutAfter(this Task task, int millisecondsDelay)
        {
            var timeoutCancellationTokenSource = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(millisecondsDelay, timeoutCancellationTokenSource.Token));
            if (completedTask == task)
            {
                timeoutCancellationTokenSource.Cancel();
            }
            else
            {
                throw new TimeoutException("The operation has timed out.");
            }
        }

        /// <summary>设置Task过期时间
        /// </summary>
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, int millisecondsDelay)
        {
            var timeoutCancellationTokenSource = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(millisecondsDelay, timeoutCancellationTokenSource.Token));
            if (completedTask == task)
            {
                timeoutCancellationTokenSource.Cancel();
                return task.Result;
            }
            throw new TimeoutException("The operation has timed out.");
        }
    }
}
