
/**************************************************
* 文 件 名：ConvertHelper.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/3 16:52:52
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
    /// 转换帮助类
    /// </summary>
    public class ConvertHelper
    {

        #region 各进制数间转换
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, @from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {

                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return "0";
            }
        }
        #endregion

        #region  转换成Int32类型
        /// <summary>
        /// 转换成Int32类型
        /// </summary>
        /// <param name="obj">需要转换对象</param>
        /// <param name="defaultValue">如果转换失败则返回默认值</param>
        /// <returns>返回转换Int32值</returns>
        public static int ToInt32(object obj, int defaultValue = 0)
        {
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out defaultValue);
            }
            return defaultValue;
        }
        #endregion

        #region 转换成Int64类型
        /// <summary>
        /// 转换成Int64类型
        /// </summary>
        /// <param name="obj">需要转换对象</param>
        /// <param name="defaultValue">转换失败默认值</param>
        /// <returns>返回转换Int64值</returns>
        public static long ToInt64(object obj, long defaultValue)
        {
            if (obj != null)
            {
                Int64.TryParse(obj.ToString(), out defaultValue);
            }
            return defaultValue;
        }
        #endregion

        #region 转换成Double类型
        /// <summary>
        /// 转换成Double类型
        /// </summary>
        /// <param name="obj">需要转换对象</param>
        /// <param name="defaultValue">转换失败默认值</param>
        /// <returns>返回转换double值</returns>
        public static double ToDouble(object obj, double defaultValue)
        {
            if (obj != null)
            {
                double.TryParse(obj.ToString(), out defaultValue);
            }
            return defaultValue;
        }
        #endregion

        #region  转换成double类型,并保留有效的位数
        /// <summary>
        ///  转换成double类型,并保留有效的位数
        /// </summary>
        /// <param name="obj">需要转换对象</param>
        /// <param name="defaultValue">转换失败默认值</param>
        /// <param name="digit">保留小数点最后的位数</param>
        /// <returns>返回转换double值</returns>
        public static double ToDouble(object obj, double defaultValue, int digit)
        {
            if (obj != null)
            {
                double.TryParse(obj.ToString(), out defaultValue);
                defaultValue = Math.Round(defaultValue, digit);
            }
            return defaultValue;
        }
        #endregion

        #region 转换成Datetime
        /// <summary>
        /// 转换成Datetime
        /// </summary>
        /// <param name="obj">需要转换对象</param>
        /// <param name="defaultValue">转换失败默认值</param>
        /// <returns>返回转换DateTime值</returns>
        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            DateTime d1 = defaultValue;
            if (obj != null)
            {
                bool r = DateTime.TryParse(obj.ToString(), out d1);
                if (!r)
                {
                    return defaultValue;
                }
            }
            return d1;
        }
        #endregion

        #region  转换成Bool类型
        /// <summary> 
        /// 转换成Bool类型
        /// </summary>
        /// <param name="obj">需要转换对象</param>
        /// <param name="defaultValue">转换失败默认值</param>
        /// <returns>返回转换bool值</returns>
        public static bool ToBool(object obj, bool defaultValue)
        {
            if (obj != null)
            {
                bool.TryParse(obj.ToString(), out defaultValue);
            }
            return defaultValue;
        }
        #endregion

        #region 将string类型字符串转换成对应的guid
        /// <summary>
        /// 将string类型字符串转换成对应的guid
        /// </summary>
        /// <param name="str">输入要转换字符串</param>
        /// <returns>返回 guid对象</returns>
        public static Guid ToGuid(string str)
        {
            return new Guid(str);
        }
        #endregion
    }
}
