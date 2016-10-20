/**************************************************
* 文 件 名：DictionaryExt.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/4 9:52:13
* 文件说明：字典的扩展
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/
using System.Collections.Generic;

namespace Climb.Utilitys.SystemExt
{
    /// <summary>
    /// Dictionary 字典扩展
    /// </summary>
    public static class DictionaryExt
    {
        /// <summary>
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// dict.TryAdd(1, "A")
        /// </summary>
        /// 
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key) == false) dict.Add(key, value);
            return dict;
        }
        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// .AddOrReplace(3, "C")
        /// </summary>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }

        /// <summary>
        /// 获取与指定的键相关联的值，如果没有则返回输入的默认值
        /// var v1 = dict.GetValue(2);         //不存在则返回 null
        /// var v2 = dict.GetValue(2, "abc");  //不存在返回 ”abc“    
        /// </summary>
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            return dict.ContainsKey(key) ? dict[key] : defaultValue;
        }


        /// <summary>
        /// 向字典中批量添加键值对
        /// </summary>
        /// <param name="dict">当前字典对象</param>
        /// <param name="values">字典对象</param>
        /// <param name="replaceExisted">如果已存在，是否替换</param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
        {
            foreach (var item in values)
            {
                if (dict.ContainsKey(item.Key) == false || replaceExisted)
                    dict[item.Key] = item.Value;
            }
            return dict;
        }
    }
}
