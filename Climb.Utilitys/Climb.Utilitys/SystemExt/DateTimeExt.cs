
/**************************************************
* 文 件 名：DateTimeExt.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/1 23:59:30
* 文件说明：时间和日期的扩展方法
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

// ReSharper disable All

namespace Climb.Utilitys.SystemExt
{
    /// <summary>
    /// 时间扩展类
    /// </summary>
    public sealed class DateTimeExt
    {
        #region 返回时间差
        /// <summary>
        /// 获取两个时间 的时间差
        /// </summary>
        /// <param name="datetimebTime">开始时间</param>
        /// <param name="dareruneeTime">结束时间</param>
        /// <returns>返回时间差的字符串 如返回差一个月 1日   几个小时前或者 几分钟前</returns>
        public static string DateDiff(DateTime datetimebTime, DateTime dareruneeTime)
        {
            string dateDiff;
            TimeSpan ts = GetTimeSpan(datetimebTime, dareruneeTime);
            if (ts.Days >= 1)
            {
                dateDiff = datetimebTime.Month.ToString(CultureInfo.InvariantCulture) + "月" +
                           datetimebTime.Day.ToString(CultureInfo.InvariantCulture) + "日";
            }
            else
            {
                if (ts.Hours > 1)
                {
                    dateDiff = ts.Hours.ToString(CultureInfo.InvariantCulture) + "小时前";
                }
                else
                {
                    dateDiff = ts.Minutes.ToString(CultureInfo.InvariantCulture) + "分钟前";
                }
            }
            return dateDiff;
        }
        #endregion

        #region 获得两个日期的间隔
        /// <summary>
        /// 获得两个日期的间隔
        /// </summary>
        /// <param name="dateTimebTime">日期一</param>
        /// <param name="datetimeeTime">日期二</param>
        /// <returns>日期间隔TimeSpan。</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public static TimeSpan GetTimeSpan(DateTime dateTimebTime, DateTime datetimeeTime)
        {
            TimeSpan ts1 = new TimeSpan(dateTimebTime.Ticks);
            TimeSpan ts2 = new TimeSpan(datetimeeTime.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }
        #endregion

        #region 两时间比较
        /// <summary>
        /// 两时间比较
        /// </summary>
        /// <param name="dateTime">开始时间</param>
        /// <param name="compareTime">结束时间</param>
        /// <returns>如果第一个时间大于第二个时间返回true 其他则为false</returns>
        public static bool DiffDateTime(DateTime dateTime, DateTime compareTime)
        {
            return dateTime.CompareTo(compareTime) > 0;
        }
        #endregion

        #region 一天的开始时间
        /// <summary>
        /// 一天的开始时间 日期加上00:00:00.000
        /// </summary>
        /// <returns>返回时间字符串</returns>
        public static string BeginDateTime(DateTime dateTime)
        {
            return string.Concat(dateTime.ToString("yyyy-MM-dd"), " 00:00:00.000");
            
        }
        #endregion

        #region 一天的结束时间
        /// <summary>
        /// 获取 当前的时间一天的结束时间 日期加上23:59:59.999
        /// </summary>
        /// <returns> 返回时间字符串</returns>
        public static string EndDatetime(DateTime dateTime)
        {
            return string.Concat(dateTime.ToString("yyyy-MM-dd"), " 23:59:59.999");
        }
        #endregion

        #region 中国式的时间格式
        ///<summary>
        ///中国式的时间格式
        ///子时：23:00-1:00
        ///丑时：1:00-3:00
        ///寅时：3:00-5:00
        ///卯时：5:00-7:00
        ///辰时：7:00-9:00
        ///巳时：9:00-11:00
        ///午时：11:00-13:00
        ///未时：13:00-15:00
        ///申时：15:00-17:00
        ///酉时：17:00-19:00
        ///戌时：19:00-21:00
        ///亥时：21:00-23:00
        /// </summary>
        /// <param name="date">时间参数</param>
        /// <returns></returns>
        public static string GetChineseDate(DateTime date)
        {
            var cnDate = new ChineseLunisolarCalendar();
            string[] arrMonth = { "", "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "冬月", "腊月" };
            string[] arrDay = { "", "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十" };
            string[] arrYear = { "", "甲子", "乙丑", "丙寅", "丁卯", "戊辰", "己巳", "庚午", "辛未", "壬申", "癸酉", "甲戌", "乙亥", "丙子", "丁丑", "戊寅", "己卯", "庚辰", "辛己", "壬午", "癸未", "甲申", "乙酉", "丙戌", "丁亥", "戊子", "己丑", "庚寅", "辛卯", "壬辰", "癸巳", "甲午", "乙未", "丙申", "丁酉", "戊戌", "己亥", "庚子", "辛丑", "壬寅", "癸丑", "甲辰", "乙巳", "丙午", "丁未", "戊申", "己酉", "庚戌", "辛亥", "壬子", "癸丑", "甲寅", "乙卯", "丙辰", "丁巳", "戊午", "己未", "庚申", "辛酉", "壬戌", "癸亥" };

            var lYear = cnDate.GetYear(date);
            var sYear = arrYear[cnDate.GetSexagenaryYear(date)];
            var lMonth = cnDate.GetMonth(date);
            var lDay = cnDate.GetDayOfMonth(date);

            //获取第几个月是闰月,等于0表示本年无闰月
            var leapMonth = cnDate.GetLeapMonth(lYear);
            var sMonth = arrMonth[lMonth];
            //如果今年有闰月  
            if (leapMonth > 0)
            {
                //闰月数等于当前月份  
                sMonth = lMonth == leapMonth ? string.Format("闰{0}", arrMonth[lMonth - 1]) : sMonth;
                sMonth = lMonth > leapMonth ? arrMonth[lMonth - 1] : sMonth;
            }
            return string.Format("{0}年{1}{2}", sYear, sMonth, arrDay[lDay]);
        }
        #endregion

        #region 最大的天数
        /// <summary>
        /// 获取日期月的最大天数
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns>返回天数</returns>
        public static int DaysInMonth(DateTime dt)
        {
            return DateTime.DaysInMonth(dt.Year, dt.Month);
        }
        #endregion

        #region 获取年龄
        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        /// <returns>返回年龄</returns>
        public static int CalculateAge(DateTime dateOfBirth)
        {
            return CalculateAge(dateOfBirth, DateTime.Today);
        }
        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        /// <param name="referenceDate">比较的时间</param>
        /// <returns>返回年龄</returns>
        public static int CalculateAge(DateTime dateOfBirth, DateTime referenceDate)
        {
            int years = referenceDate.Year - dateOfBirth.Year;
            if (referenceDate.Month < dateOfBirth.Month || (referenceDate.Month == dateOfBirth.Month && referenceDate.Day < dateOfBirth.Day)) --years;
            return years;
        }
        #endregion

        #region 时间是否是今天
        /// <summary>
        /// 时间是否是当天
        /// </summary>
        /// <param name="dt">日期时间</param>
        /// <returns>返回bool 值  是今天 返回true</returns>
        public static bool IsToday(DateTime dt)
        {
            return (dt.Date == DateTime.Today);
        }

        #endregion

        #region 时间是否是工作日
        /// <summary>
        /// 时间是否是工作日
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns>是工作日返回true</returns>
        public static bool IsWeekDay(DateTime date)
        {
            return !IsWeekend(date);
        }
        
        #endregion

        #region 时间是否是周末
        /// <summary>
        /// 时间是否是周末
        /// </summary>
        /// <param name="value">时间日期</param>
        /// <returns>返回bool值  是周末返回true</returns>
        public static bool IsWeekend(DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday;
        }
        #endregion

        #region 得到随机日期
        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="time1">起始日期</param>
        /// <param name="time2">结束日期</param>
        /// <returns></returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime minTime;
            new DateTime();

            var ts = new TimeSpan(time1.Ticks - time2.Ticks);

            // 获取两个时间相隔的秒数
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds;

            if (dTotalSecontds > Int32.MaxValue)
            {
                iTotalSecontds = Int32.MaxValue;
            }
            else if (dTotalSecontds < Int32.MinValue)
            {
                iTotalSecontds = Int32.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }
            if (iTotalSecontds > 0)
            {
                minTime = time2;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
            }
            else
            {
                return time1;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= Int32.MinValue)
                maxValue = Int32.MinValue + 1;

            int i = random.Next(Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }
        #endregion

        #region 将时间转换成int32类型 /默认情况下以1970.01.01为开始时间计算

        /// <summary> 将时间转换成int32类型
        /// </summary>
        public static int DateDiffToInt32(DateTime end, int defaultValue = 0)
        {
            //默认情况下以1970.01.01为开始时间计算
            int result = defaultValue;
            try
            {
                DateTime startdate = new DateTime(1970, 1, 1,0, 0, 0);
                // TimeSpan seconds = end.AddDays(1) - startdate;
                TimeSpan seconds = end - startdate;
                result = Convert.ToInt32(seconds.TotalSeconds);
            }
            catch
            {
                return defaultValue;
            }
            return result;
        }
        #endregion


        #region 获取某个时间的中文星期
        /// <summary>
        /// 获取某个时间的中文星期
        /// </summary>
        public static string GetChineseWeekOfDay(DateTime time)
        {
            int dayOfWeek = (int)time.DayOfWeek;
            return GetWeekDays().FirstOrDefault(x => x.Key == dayOfWeek).Value;
        }

        /// <summary>
        /// 获取星期中的所有天数
        /// </summary>
        private static Dictionary<int, string> GetWeekDays()
        {
            var weekDict = new Dictionary<int, string>
            {
                {0, "星期日"},
                {1, "星期一"},
                {2, "星期二"},
                {3, "星期三"},
                {4, "星期四"},
                {5, "星期五"},
                {6, "星期六"}
            };
            return weekDict;
        }
        #endregion 
    }

}
