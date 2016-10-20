

/**************************************************
* 文 件 名：SyncDictionary.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/9 9:31:15
* 文件说明：实现一个能够在高并发下 异步访问的字典
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/

using System.Collections.Generic;

// ReSharper disable All

namespace Climb.Utilitys.CollectionsExt
{
    /// <summary>
    /// 一个并发下的字典
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public sealed class SyncDictionary<TKey, TValue>
    {
        /// <summary>
        /// 全局锁
        /// </summary>
        private static object LockHelper=new object();

        private readonly Dictionary<TKey, TValue> _Dictionary;
        /// <summary>
        /// 构造函数 创建并发下的字典
        /// </summary>
        /// <param name="capacity">字典初始长度</param>
        public SyncDictionary(int capacity)
        {
            _Dictionary = new Dictionary<TKey, TValue>(capacity);
        }
        /// <summary>
        ///  构造函数 创建并发下的字典
        /// </summary>
        public SyncDictionary()
        {
            _Dictionary = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// 根据key 获取value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (LockHelper)
            {
                return _Dictionary.TryGetValue(key, out value);
            }
            
        }
        /// <summary>
        /// 并发字典的数量
        /// </summary>
        public int Count { get { lock (LockHelper) return _Dictionary.Count; } }
        /// <summary>
        /// 索引器 根据key可以获取 TValue 值
        /// </summary>
        /// <param name="key">key值</param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                lock (LockHelper)
                    return _Dictionary[key];
            }
            set
            {
                lock (LockHelper)
                    _Dictionary[key] = value;
            }
        }
        /// <summary>
        /// 向字典添加内容 如果存在则不添加
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Add(TKey key, TValue value)
        {
            lock (LockHelper)
            {
                if (_Dictionary.ContainsKey(key) == false)
                    _Dictionary.Add(key, value);
            }
        }
        /// <summary>
        /// 从字典删除一个元素
        /// </summary>
        /// <param name="key">字典key</param>
        public void Remove(TKey key)
        {
            lock (LockHelper)
            {
                _Dictionary.Remove(key);
            }
        }
    }
}
