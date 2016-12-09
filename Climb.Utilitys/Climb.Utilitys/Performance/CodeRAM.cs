
/**************************************************
* 文 件 名：Singleton.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/3 14:17:41
* 文件说明：内存的性能读取工具
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/

using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Climb.Utilitys.Performance
{
    /// <summary>
    /// 代码性能测试内存计算工具
    /// </summary>
    public static class CodeRam
    {
        /// <summary>
        /// 内存计算初始化，同时后续操作进行预热，以避免初次操作带来的性能影响
        /// </summary>
        public static void Initialize()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Ram(string.Empty, () => { });
        }

        /// <summary>
        /// 内存计算，传入操作标识名，重复次数，操作过程获取操作的性能数据
        /// </summary>
        /// <param name="name"> 操作标识名 </param>
        /// <param name="action"> 操作过程的Action </param>
        // ReSharper disable once MemberCanBePrivate.Global
        public static string  Ram(string name, Action action)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "";
            }
            StringBuilder sbBuilder =new StringBuilder();
            sbBuilder.AppendLine(name);

            GC.Collect();
            long start = GC.GetTotalMemory(true);
            action();
            GC.Collect();
            GC.WaitForFullGCComplete();
            long end = GC.GetTotalMemory(true);
            long result = end - start;
            sbBuilder.AppendLine("\tRam:\t" + result + " B");
            sbBuilder.AppendLine("\tRam:\t" + result / 1024 + " KB");
            sbBuilder.AppendLine("\tRam:\t" + result / 1024 / 1024 + " MB");
            sbBuilder.AppendLine();
            return sbBuilder.ToString();
        }
    }
}
