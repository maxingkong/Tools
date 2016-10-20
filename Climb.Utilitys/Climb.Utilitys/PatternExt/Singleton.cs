
/**************************************************
* 文 件 名：Singleton.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/3 14:17:41
* 文件说明：对单例模式的抽象封装
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/
namespace Climb.Utilitys.PatternExt
{
    /// <summary>
    /// Singleton泛型类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    // ReSharper disable once ClassCannotBeInstantiated
    public static class Singleton<T> where T : new()
    {
        /// <summary>
        /// 需要创建单例的对象
        /// </summary>
        private static T _instance = new T();

        /// <summary>
        /// 全局锁
        /// </summary>
        // ReSharper disable once StaticMemberInGenericType
        static readonly object LockHelper = new object();

        /// <summary>
        /// 获取实例
        /// </summary>
        public static T GetInstance()
        {
            if (_instance != null) return _instance;
            lock (LockHelper)
            {
                if (_instance == null)
                {
                    // ReSharper disable once PossibleMultipleWriteAccessInDoubleCheckLocking
                    _instance = new T();
                }
            }

            return _instance;
        }
    }
}
