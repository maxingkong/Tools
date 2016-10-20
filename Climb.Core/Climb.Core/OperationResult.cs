﻿using System;
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
        public OperateType ErrorOptionEnum { get; set; }

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
