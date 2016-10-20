﻿
/**************************************************
* 文 件 名：IntExt.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/1 23:59:30
* 文件说明：整形的扩展
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/

using System;
using System.Globalization;
using System.Linq;

namespace Climb.Utilitys.SystemExt
{
    /// <summary>
    /// int 类型的扩展
    /// </summary>
    public static class IntExt
    {

        #region 判断输入的数字是在边界内
        /// <summary>
        /// 判断输入的数字是在边界内
        /// </summary>
        /// <param name="number">数字</param>
        /// <param name="minnumber">最小的边界值</param>
        /// <param name="maxnumber">最大的边界值</param>
        /// <returns>如果超出编辑 则返回边界值</returns>
        public static int Limit(this int number, int minnumber, int maxnumber)
        {
            if (number > maxnumber)
            {
                return maxnumber;
            }
            return number < minnumber ? minnumber : number;
        }

        #endregion

        #region 精确小数点位数
        /// <summary>
        /// 把价格精确至小数点第多少位置
        /// </summary>
        /// <param name="dPrice"></param>
        /// <param name="formatCt"></param>
        /// <returns>返回小数的值</returns>
        public static string FormatPrice(double dPrice, int formatCt)
        {
            double d = dPrice;
            NumberFormatInfo myNfi = new NumberFormatInfo { NumberNegativePattern = formatCt };
            return d.ToString("N", myNfi);
        }
        #endregion

        #region 多个数字转为中文形式
        /// <summary>
        /// 多个数字转为中文形式
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string IntToChinese(int number)
        {
            string numStr = number.ToString();
            return numStr.Select(t => Convert.ToInt16(numStr[0])).Aggregate<short, string>(null, (current, sh) => current + ShortToChinese(sh));
        }
        #endregion

        #region 一个数字转为中文形式
        /// <summary>
        /// 一个数字转为中文形式
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public static string ShortToChinese(short number)
        {
            if (number.ToString().Length >= 2)
            {
                throw new Exception("IntToChinese的参数不能大于10");
            }

            switch (number)
            {
                case 0:
                    return "零";
                case 1:
                    return "一";
                case 2:
                    return "二";
                case 3:
                    return "三";
                case 4:
                    return "四";
                case 5:
                    return "五";
                case 6:
                    return "六";
                case 7:
                    return "七";
                case 8:
                    return "八";
                case 9:
                    return "九";
                default:
                    throw new Exception("IntToChinese的参数不正确");
            }
        }
        #endregion

        #region 获取三个数最小的值
        /// <summary>
        /// 获取三个数最小的数
        /// </summary>
        /// <param name="first">数字1</param>
        /// <param name="second">数字2</param>
        /// <param name="third">数字3</param>
        /// <returns>返回最小的数</returns>
        public static int Minimum(int first, int second, int third)
        {
            var intMin = first;
            if (second < intMin)
            {
                intMin = second;
            }
            if (third < intMin)
            {
                intMin = third;
            }
            return intMin;
        }
        #endregion

    }
}
