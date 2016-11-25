/**************************************************
* 文 件 名：OperationResult.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/7/1 23:59:30
* 文件说明：返回信息 通用对象 包含操作结果  操作结果的枚举 操作结果信息
* 修 改 人：
* 修改日期：
* 备注描述：
*
**************************************************/
using System;
using System.Text;


namespace Climb.Core
{
    /// <summary>
    /// 返回结果类
    /// </summary>
    [Serializable]
    public class OperationResult
    {
        /// <summary>
        /// 错误类型
        /// </summary>
        public OperateTypeEnum ErrorOptionEnum { get; set; }

        /// <summary>
        /// 存储错误消息
        /// </summary>
        private readonly StringBuilder _messageInfo = new StringBuilder();


        /// <summary>
        /// 设置消息
        /// </summary>
        /// <param name="msg">如果需要设置错误消息 可以从此字段设置</param>
        public void SetMessage(string msg)
        {
            _messageInfo.AppendLine(msg);
        }

        /// <summary>
        /// 获得消息
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            return _messageInfo.ToString();
        }
    }

    /// <summary>
    /// 对result 的对象扩展类 可以实现得到一个类型
    /// </summary>
    /// <typeparam name="T">返回clinet结果 但是必须是支持序列化的类型</typeparam>
    [Serializable]
    public class OperationResult<T> : OperationResult
    { 
        public T ObjectResult { get; set; }
    }
}
