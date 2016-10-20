
/**************************************************
* 文 件 名：ArraryExt.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/1 23:59:30
* 文件说明：集合类的扩展方法 提供常量数组 判断数组是否为null 或者为空   合并数组  随机数组一项
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable All


namespace Climb.Utilitys.SystemExt
{
    /// <summary>
    /// 数组扩展
    /// </summary>
    public static class ArraryExt
    {
        #region 判断数组是否为空 或者为null 
        /// <summary>
        /// 判断数组是否为空
        /// </summary>
        /// <param name="array">当前数组</param>
        /// <returns>如果为空或者为null则返回true 否则返回false</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public static bool IsNullEmpty(this Array array)
        {
            return array == null || array.Length == 0;
        }

        #endregion

        #region 合并数组
        /// <summary>
        /// 合并两条数组
        /// </summary>
        /// <param name="t1">数组t1</param>
        /// <param name="t2">数组t2</param>
        /// <typeparam name="T">数组类型</typeparam>
        /// <returns>返回合并后的数组</returns>
        public static T[] CombineArrary<T>(T[] t1, T[] t2)
        {
            var tAry = new T[t1.Length + t2.Length];
            Array.Copy(t1, 0, tAry, 0, t1.Length);
            Array.Copy(t2, 0, tAry, t1.Length, t2.Length);
            return tAry;
        }
        #endregion

        #region 随机数组里的一项

        /// <summary>
        /// 获取数组中的随机一项
        /// </summary>
        /// <param name="t1">数组t1</param>
        /// <typeparam name="T">对象t类型</typeparam>
        /// <returns>返回某个对象</returns>
        public static T ArryRandom<T>(T[] t1)
        {
            T newObj = default(T);
            if (t1.IsNullEmpty()) return newObj;
            if (t1.Length == 1)
            {
                newObj = t1[0];
            }
            else
            {
                var randLen = RandomExt.GetRandomInt(0, t1.Length - 1);
                newObj = t1[randLen];
            }
            return newObj;
        }

        #endregion

        #region 循环执行某个方法
        /// <summary>
        /// 循环执行某个 方法
        /// </summary>
        /// <param name="enum">集合</param>
        /// <param name="mapFunction">执行操作的泛型</param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> @enum, Action<T> mapFunction)
        {
            foreach (var item in @enum) mapFunction(item);
        }
        #endregion

        #region substring 截取数组的方法
        /// <summary>
        /// 截取数组 获取数组的一部分
        /// </summary>
        /// <typeparam name="T">t类型的数组</typeparam>
        /// <param name="t1">数组长度</param>
        /// <param name="start">开始截取</param>
        /// <param name="len">截取的长度</param>
        /// <returns></returns>
        public static T[] SubArrys<T>(T[] t1, int start, int len)
        {
            if (t1.Length < start + len)
            {
                throw new ArgumentOutOfRangeException("参数已经超出数组的长度");
            }
            var reArrys = new T[len];
            for (int i = 0; i < len; i++)
            {
                reArrys[i] = t1[i + start];
            }
            return reArrys;
        }
        #endregion

        #region 判断两个数组的内容是否相等
        /// <summary>
        /// 确定两个数组是相等的。
        /// </summary>
        /// <param name="t1">第一个数组进行比较。</param>
        /// <param name="t2">数组的第一个比较。</param>
        /// <returns><see t1="true"/> 如果两个数组是相等的，否则 <see langword="false"/>.</returns>
        public static bool Compare<T>(T[] t1, T[] t2)
        {
            if (t1.Length != t2.Length)
            {
                return false;
            }
            return !t1.Where((t, i) => t.Equals(t2[i])).Any();
        }
        #endregion

        #region 判断对象是否在集合中
        /// <summary>
        /// 判断对象是否在集合中
        /// </summary>
        /// <param name="o">对象</param>
        /// <param name="c">集合</param>
        /// <returns>存在返回true 不存在返回flase</returns>
        public static bool In(this object o, IEnumerable c)
        {
            foreach (object i in c)
            {
                if (i.Equals(o)) return true;
            }
            return false;
        }
        #endregion
    }
}
