﻿using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Climb.Utilitys.SystemExt;

namespace Climb.Utilitys
{
    /// <summary>
    /// 压缩操作类
    /// </summary>
    public static class Compression
    {
        /// <summary>
        /// 对byte数组进行压缩
        /// </summary>
        /// <param name="data">待压缩的byte数组</param>
        /// <returns>压缩后的byte数组</returns>
        public static byte[] Compress(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
                zip.Write(data, 0, data.Length);
                zip.Close();
                byte[] buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// 对byte[]数组进行解压
        /// </summary>
        /// <param name="data">待解压的byte数组</param>
        /// <returns>解压后的byte数组</returns>
        public static byte[] Decompress(byte[] data)
        {
            using (MemoryStream tmpMs = new MemoryStream())
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true);
                    zip.CopyTo(tmpMs);
                    zip.Close();
                }
                return tmpMs.ToArray();
            }
        }

        /// <summary>
        /// 对字符串进行压缩
        /// </summary>
        /// <param name="value">待压缩的字符串</param>
        /// <returns>压缩后的字符串</returns>
        public static string Compress(string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            bytes = Compress(bytes);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 对字符串进行解压
        /// </summary>
        /// <param name="value">待解压的字符串</param>
        /// <returns>解压后的字符串</returns>
        public static string Decompress(string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }
            byte[] bytes = Convert.FromBase64String(value);
            bytes = Decompress(bytes);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
