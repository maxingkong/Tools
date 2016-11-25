/**************************************************
* 文 件 名：OperateTypeEnum.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/7/1 23:59:30
* 文件说明：常用的错误的枚举
* 修 改 人：
* 修改日期：
* 备注描述：
*
**************************************************/


using System.ComponentModel;

namespace Climb.Core
{
    /// <summary>
    /// 列举错误类型
    /// </summary>
    [Description("错误类型的枚举")]
    public enum OperateTypeEnum
    {
        /// <summary>
        /// 用户没有登录
        /// </summary>
        [Description("没有登录")]
        NoLogin,
        /// <summary>
        /// 登录失败
        /// </summary>
        [Description("登录失败")]
        LoginFail,
        /// <summary>
        /// 登录成功
        /// </summary>
        [Description("登录成功")]
        LoginSucess,
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        OperateOk,
        /// <summary>
        /// 操作失败
        /// </summary>
        [Description("操作失败")]
        OperateFail,
        /// <summary>
        /// 输入验证码有误
        /// </summary>
        [Description("验证码失败")]
        CehckCodeError,
        /// <summary>
        /// 输入错误 或者输入参数有误
        /// </summary>
        [Description("参数错误")]
        ParamsError,
        /// <summary>
        /// 用户没有权限访问
        /// </summary>
        [Description("没有权限")]
        NoRule,
        /// <summary>
        /// 返回结果没有相应
        /// </summary>
        [Description("没有结果响应")]
        NoResult,
        /// <summary>
        /// 程序错误 
        /// </summary>
        [Description("程序错误")]
        ServiceError,

        /// <summary>
        /// 字段重复 
        /// </summary>
        [Description("字段重复")]
        FiledRename
    }

}
