
/**************************************************
* 文 件 名：RandomExt.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/2 0:10:15
* 文件说明：随机数扩展类
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/

using System;
using System.Globalization;
using System.Text;
using Climb.Utility;

namespace Climb.Utilitys.SystemExt
{
    /// <summary>
    /// 随机类扩展
    /// </summary>
    public static class RandomExt
    {
        #region 生成一个指定范围的随机整数
        /// <summary>
        /// 生成一个指定范围的随机整数，该随机数范围包括最小值，但不包括最大值
        /// </summary>
        /// <param name="minNum">最小值</param>
        /// <param name="maxNum">最大值</param>
        public static int GetRandomInt(int minNum, int maxNum)
        {
            Random randRandom = new Random(Guid.NewGuid().GetHashCode());
            return randRandom.Next(minNum, maxNum);
        }
        #endregion

        #region 1-int.MaxValue 随机数字
        /// <summary>
        /// 得到一个随机数值 1-int Max
        /// </summary>
        /// <returns></returns>
        public static int GetRandomInt()
        {
            return GetRandomInt(1, int.MaxValue);
        }
        #endregion

        #region 生成一个0.0到1.0的随机小数
        /// <summary>
        /// 生成一个0.0到1.0的随机小数
        /// </summary>
        public static double GetRandomDouble()
        {
            Random randRandom = new Random(Guid.NewGuid().GetHashCode());
            return randRandom.NextDouble();
        }

        #endregion

        #region 对一个数组进行随机排序，数组会改变 不会产生的新的数组
        /// <summary>
        /// 对一个数组进行随机排序
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">需要随机排序的数组</param>
        public static void RandomArray<T>(T[] arr)
        {
            //对数组进行随机排序的算法:随机选择两个位置，将两个位置上的值交换
            //交换的次数,这里使用数组的长度作为交换次数
            int count = arr.Length;
            //开始交换
            for (int i = 0; i < count; i++)
            {
                //生成两个随机数位置
                int randomNum1 = GetRandomInt(0, arr.Length);
                int randomNum2 = GetRandomInt(0, arr.Length);

                //定义临时变量
                //交换两个随机数位置的值
                T temp = arr[randomNum1];
                arr[randomNum1] = arr[randomNum2];
                arr[randomNum2] = temp;
            }
        }
        #endregion

        #region 随机bool值
        /// <summary>
        /// 随机返回true 或者false
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static bool NextBool(this Random random)
        {
            return GetRandomDouble()> 0.5;
        }
        #endregion

        #region  生成不重复的随机字符串
        /// <summary>
        /// 生成不重复的随机字符串
        /// </summary>
        /// <param name="codeCount">穗子字符串位数</param>
        /// <returns>返回随机的字符串</returns>
        public static  string CreateCheckCodeNum(int codeCount)
        {
            int rep = 0;
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                int num = random.Next();
                str = str + ((char)(0x30 + ((ushort)(num % 10))));
            }
            return str;
        }
        /// <summary>
        /// 随机生成字符串（数字和字母混和）
        /// </summary>
        /// <param name="codeCount">随机位数</param>
        /// <returns>返回随机的字符串（数字和字母混和）</returns>
        public static string CreateCheckCode(int codeCount)
        {
            int rep = 0;
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString(CultureInfo.InvariantCulture);
            }
            return str;
        }
        #endregion

        #region 生成纯数字的验证码
        /// <summary>
        /// 生成纯数字的验证码
        /// </summary>
        /// <param name="codeCount">随机位数</param>
        /// <returns>返回随机的字符串</returns>
        public static string CreateNumberCode(int codeCount)
        {
            StringBuilder sbBuilder = new StringBuilder();
            for (int i = 0; i < codeCount; i++)
            {
                sbBuilder.Append(GetRandomInt(0, 9));
            }
            return sbBuilder.ToString();
        }
        #endregion

        #region 生成随机数的种子
        /// <summary>生成随机数的种子
        /// </summary>
        public static int GetRandomSeed(int len = 8)
        {
            byte[] bytes = new byte[len];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        #endregion
    }
}
