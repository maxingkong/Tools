
/**************************************************
* 文 件 名：ToolsExt.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/4 9:50:21
* 文件说明：
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/

using System;

namespace Climb.Utilitys
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Tools
    {

        /// <summary>
        /// 根据身份证获取出生年月
        /// </summary>
        /// <param name="id">身份证号码</param>
        /// <returns>出生年月</returns>
        public static DateTime GetCardBirthday(string id)
        {
            string result;
            switch (id.Length)
            {
                case 15:
                    result = "19" + id.Substring(6, 2) + "-" + id.Substring(8, 2) + "-" + id.Substring(10, 2);
                    break;
                case 18:
                    result = id.Substring(6, 4) + "-" + id.Substring(10, 2) + "-" + id.Substring(12, 2);
                    break;
                default:
                    throw new ArgumentException("身份证号码不是15或者18位！");
            }
            return Convert.ToDateTime(result);
        }


        /// <summary>
        /// 扩展方法:手机号转化成189****6547形式
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns>'Error': 号码格式出错</returns>
        public static string GetHidePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 11)
            {
                return phoneNumber.Substring(0, 3) + "****" + phoneNumber.Substring(7, 4);
            }
            return "Error";
        }

        #region 敏感字符字符转换（450821198506010034转450821********0034）
        /// <summary>
        /// 敏感字符字符转换（450821198506010034转450821********0034）
        /// </summary>
        /// <param name="number">身份证号码</param>
        /// <param name="start">从哪位开始</param>
        /// <param name="length">隐藏的长度</param>
        /// <returns>返回隐藏的身份证号码</returns>
        public static string GetHideCardString(string number, int start, int length)
        {
            bool isDo = false;
            char[] s = number.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                if (i == start)
                {
                    isDo = true;
                }

                if (isDo)
                {
                    s[i] = '*';
                    if (length <= 1)
                    {
                        isDo = false;
                    }
                    length--;
                }
            }
            return new string(s);
        }
        #endregion




     
    }
}
