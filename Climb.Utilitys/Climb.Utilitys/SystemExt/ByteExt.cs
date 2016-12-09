
/**************************************************
* 文 件 名：ByteExt.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/4 9:52:13
* 文件说明：byte 数组的常用扩展
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Climb.Utilitys.SystemExt
{
    /// <summary>
    /// byte 操作的扩展
    /// </summary>
    public static class ByteExt
    {
        #region bytes 和二进制互相转换

        /// <summary>
        /// 将二进制的数据转成Hex格式
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToHex(byte[] bytes)
        {
            if (bytes == null)
                return "";
            return bytes.Aggregate("", (current, t) => current + t.ToString("X2"));
        }

        /// <summary>
        /// Hex进制转二进制
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(string input)
        {
            int i = input.Length % 2;
            if (i != 0)
            {
                throw new Exception("字符串的长度必需是偶数");
            }
            List<byte> ls = new List<byte>();
            for (int j = 0; j < input.Length; j += 2)
            {
                byte b = Convert.ToByte(input.Substring(j, 2), 16);
                ls.Add(b);
            }
            return ls.ToArray();
        }
        #endregion

        #region 指定字符集  byte数组和 string字符串互相转换

        /// <summary>
        /// 使用指定字符集将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }

        /// <summary>
        /// 使用指定字符集将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        #endregion

        #region 将byte[]转换成int  int 转化 bytes

        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数
            int num = 0;

            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区
                byte[] tempBuffer = new byte[4];

                //将传入的字节数组的前4个字节复制到临时缓冲区
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //将临时缓冲区的值转换成整数，并赋给num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数
            return num;
        }

        /// <summary>
        /// int 转换byte数组
        /// </summary>
        /// <param name="number">int 值</param>
        /// <returns>返回byte数组</returns>
        public static byte[] Int32ToBytes(int number)
        {
            byte[] bytes = BitConverter.GetBytes(number);
            return bytes;
        }

        #endregion

        #region byte数组转换base64字符串
        /// <summary>
        /// byte数组转换base64字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
        #endregion



    }
}
