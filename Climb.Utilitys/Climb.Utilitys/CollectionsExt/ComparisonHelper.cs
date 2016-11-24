

/**************************************************
* 文 件 名：ComparisonHelper.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/7/1 23:59:30
* 文件说明：比较类 
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/
using System;
using System.Collections.Generic;

namespace Climb.Utilitys.CollectionsExt
{
    /// <summary> 
    /// 比较辅助类，用于快速创建<see cref="IComparer&lt;T&gt;"/>接口的实例
    /// </summary>
    /// <example>
    /// var comparer1 = Comparison[Person].CreateComparer(p => p.ID); var comparer2 = Comparison[Person].CreateComparer(p => p.Name); var comparer3 = Comparison[Person].CreateComparer(p => p.Birthday.Year)
    /// </example>
    /// <typeparam name="T"> </typeparam>
    // ReSharper disable once UnusedMember.Global
    public static class ComparisonHelper<T>
    {
        /// <summary>
        /// 创建指定对比委托<paramref name="keySelector"/>的实例
        /// </summary>
        public static IComparer<T> CreateComparer<TV>(Func<T, TV> keySelector)
        {
            return new CommonComparer<TV>(keySelector);
        }

        /// <summary>
        /// 创建指定对比委托<paramref name="keySelector"/>与结果二次比较器<paramref name="comparer"/>的实例
        /// </summary>
        public static IComparer<T> CreateComparer<TV>(Func<T, TV> keySelector, IComparer<TV> comparer)
        {
            return new CommonComparer<TV>(keySelector, comparer);
        }

        #region Nested type: CommonComparer

        private class CommonComparer<TV> : IComparer<T>
        {
            private readonly IComparer<TV> _comparer;
            private readonly Func<T, TV> _keySelector;

            public CommonComparer(Func<T, TV> keySelector, IComparer<TV> comparer)
            {
                _keySelector = keySelector;
                _comparer = comparer;
            }

            public CommonComparer(Func<T, TV> keySelector)
                : this(keySelector, Comparer<TV>.Default)
            { }

            #region IComparer<T> Members

            public int Compare(T x, T y)
            {
                return _comparer.Compare(_keySelector(x), _keySelector(y));
            }

            #endregion
        }

        #endregion
    }

}
