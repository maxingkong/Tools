
/**************************************************
* 文 件 名：CookieHelper.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/5 15:33:48
* 文件说明：cookie的常用操作
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/
using System;
using System.Web;

namespace Climb.WebUtility
{
    /// <summary>
    /// 网站cookie的操作
    /// </summary>
    public sealed class CookieHelper
    {
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            WriteCookie(strName, strValue, 0, string.Empty);
        }

        /// <summary>
        ///  写cookie 
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="day">过期时间</param>
        /// <param name="doMain">cookie域</param>
        public static void WriteCookie(string strName, string strValue, int day, string doMain)
        {
            if (string.IsNullOrEmpty(strName))
            {
                return;
            }
            HttpCookie cookie = ContextExt .Request.Cookies[strName] ?? new HttpCookie(strName);
            cookie.Value = strValue;

            if (!string.IsNullOrEmpty(doMain))
            {
                ContextExt.Response.AddHeader("p3p", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
                cookie.Domain = doMain;
            }
            if (day != 0)
            {
                cookie.Expires = DateTime.Now.AddDays(day);
            }
            ContextExt.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 设置cookie的域
        /// </summary>
        /// <param name="strName">cookie名称</param>
        /// <param name="domainValue">cookie域</param>
        public static void SetCookieDomain(string strName, string domainValue)
        {
            if (string.IsNullOrEmpty(strName))
            {
                return;
            }
            if (string.IsNullOrEmpty(domainValue))
            {
                return;
            }
            HttpCookie cookie = ContextExt.Request.Cookies[strName];
            if (cookie == null) return;
            cookie.Domain = domainValue;
            ContextExt.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 删除cookie的方法
        /// </summary>
        /// <param name="strName">cookie名称</param>
        /// <param name="domainValue">cookie域</param>
        public static void RemoveCookie(string strName, string domainValue)
        {
            if (string.IsNullOrEmpty(strName))
            {
                return;
            }

            HttpCookie cookie = new HttpCookie(strName);
            if (!string.IsNullOrEmpty(domainValue))
            {
                cookie.Domain = domainValue;
            }
            // 删除Cookie
            cookie.Expires = new DateTime(1900, 1, 1);
            ContextExt.Response.Cookies.Add(cookie);

        }

        /// <summary>
        /// 读取cookie 的值
        /// </summary>
        /// <param name="key">cookie名称</param>
        /// <returns>获取cookie的值</returns>
        public static string GetCookie(string key)
        {
            string strValue = string.Empty;
            HttpCookie cookie =ContextExt.Request.Cookies[key];
            if (cookie != null)
            {
                strValue = cookie.Value;
            }
            return strValue;
        }
 
    }
}
