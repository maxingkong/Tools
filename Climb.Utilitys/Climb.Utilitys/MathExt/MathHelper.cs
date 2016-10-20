
/**************************************************
* 文 件 名：MathHelper.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/1 23:59:30
* 文件说明：数学公式帮助类  辅助数学计算
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/
using System;

namespace Climb.Utilitys.MathExt
{
    /// <summary>
    /// 数据计算辅助操作类
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// 获取两个坐标的距离
        /// </summary>
        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
        }
    }
}
