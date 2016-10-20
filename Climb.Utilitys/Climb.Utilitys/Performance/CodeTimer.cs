﻿

/**************************************************
* 文 件 名：Singleton.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/3 14:17:41
* 文件说明：程序执行时间
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Climb.Utilitys.Performance
{
    /// <summary>
    /// 代码性能测试计时器（来自博客园-老赵）
    /// odeTimer.Time("Thread Sleep", 1, () => { Thread.Sleep(3000); });
    /// CodeTimer.Time("Empty Method", 10000000, () => { });
    /// </summary>
    public static class CodeTimer
    {
        #region 私有方法

        /// <summary>
        /// 获取当前CPU循环次数
        /// </summary>
        /// <returns> </returns>
        private static ulong GetCycleCount()
        {
            ulong cycleCount = 0;
            NativeMethods.QueryThreadCycleTime(NativeMethods.GetCurrentThread(), ref cycleCount);
            return cycleCount;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 计时器初始化 对计时器进行初始化操作，同时对后续操作进行预热，以避免初次操作带来的性能影响
        /// </summary>
        public static void Initialize()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Time(string.Empty, 1, () => { });
        }

        /// <summary>
        /// 计时器，传入操作标识名，重复次数，操作过程获取操作的性能数据
        /// </summary>
        /// <param name="name"> 操作标识名 </param>
        /// <param name="iteration"> 重复次数 </param>
        /// <param name="action"> 操作过程的Action </param>
        // ReSharper disable once MemberCanBePrivate.Global
        public static void Time(string name, int iteration, Action action)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i < GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(1);
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            ulong cycleCount = GetCycleCount();
            for (int i = 0; i < iteration; i++)
            {
                action();
            }
            ulong cpuCycles = GetCycleCount() - cycleCount;
            watch.Stop();

            Console.ForegroundColor = currentForeColor;
            Console.WriteLine("\tTime Elapsed:\t" + watch.Elapsed.TotalMilliseconds + "ms");
            Console.WriteLine("\tCPU Cycles:\t" + cpuCycles.ToString("N0"));

            for (int i = 0; i < GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                Console.WriteLine("\tGen" + i + "\t\t" + count);
            }

            Console.WriteLine();
        }

        #endregion


        #region
        /// <summary>
        /// 获取方法执行的时间
        /// </summary>
        /// <param name="action">匿名方法</param>
        /// <returns>返回代码执行时间</returns>
        public static TimeSpan DoWorkTime(this Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action", "action is null.");

            var watch = Stopwatch.StartNew();
            action();
            watch.Stop();
            return watch.Elapsed;
        }

        #endregion
    }

    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetCurrentThread();

    }
}
