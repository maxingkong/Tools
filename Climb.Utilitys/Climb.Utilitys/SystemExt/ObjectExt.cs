﻿
/**************************************************
* 文 件 名：ObjectExr.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/9/4 9:19:07
* 文件说明：
* 修 改 人：
* 修改日期：
* 备注描述：
*           
*************************************************/

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;

// ReSharper disable All

namespace Climb.Utilitys.SystemExt
{
    /// <summary>
    /// object 扩展
    /// </summary>
    public static class ObjectExt
    {
        #region 将Object 转换为字符串
        /// <summary>
        /// 返回对象obj的String值,obj为null时返回空值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>字符串。</returns>
        public static string ObjectToString(object obj)
        {
            return null == obj ? string.Empty : obj.ToString();
        }
        #endregion

        #region 将对象序列化成byte数组 把数组转换object
        /// <summary>
        /// 将object 对象转换byte数组
        /// </summary>
        /// <param name="serObject">需要转换的对象</param>
        /// <returns>返回byte数组</returns>
        public static byte[] ObjectToBytes(object serObject)
        {
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, serObject);
                ms.Position = 0;
                bytes = ms.ToArray();
            }
            return bytes;
        }
        /// <summary>
        /// 将byte 数组转换二进制
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns></returns>
        public static object BytesToObject(byte[] bytes)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return formatter.Deserialize(ms);
            }
        }
        #endregion

        #region 将对象转换指定类型
        /// <summary>
        /// 把对象类型转换为指定类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object CastTo(this object value, Type conversionType)
        {
            if (value == null)
            {
                return null;
            }
            if (conversionType.IsNullableType())
            {
                conversionType = conversionType.GetUnNullableType();
            }
            if (conversionType.IsEnum)
            {
                return Enum.Parse(conversionType, value.ToString());
            }
            if (conversionType == typeof(Guid))
            {
                return Guid.Parse(value.ToString());
            }
            return Convert.ChangeType(value, conversionType);
        }

        /// <summary>
        /// 把对象类型转化为指定类型
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <returns> 转化后的指定类型的对象，转化失败引发异常。 </returns>
        public static T CastTo<T>(this object value)
        {
            object result = CastTo(value, typeof(T));
            return (T)result;
        }

        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
        /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            try
            {
                return CastTo<T>(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        #endregion

        #region object类型的扩展,转换成json字符串
        /// <summary>object类型的扩展,转换成json字符串
        /// </summary>
        public static string ToJson<T>(this object obj) where T : class
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string resultStr = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return resultStr;
        }
        #endregion

        #region 将json的字符串转换成对象
        /// <summary> 将json的字符串转换成对象
        /// </summary>
        public static T FromJson<T>(this string json) where T : class
        {
            try
            {
                //json 必须为 {name:"value",name:"value"} 的格式(要符合JSON格式要求)
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json.ToCharArray()));
                T obj = (T)serializer.ReadObject(ms);
                ms.Close();
                return obj;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 将对象序列化成xml
        /// <summary>将对象序列化成xml
        /// </summary>
        public static string ToXml<T>(this object obj) where T : class
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string resultStr = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return resultStr;
        }
        #endregion

        #region 将xml转换成对象
        /// <summary> 将xml转换成对象
        /// </summary>
        public static T FromXml<T>(this string xml) where T : class
        {
            try
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml.ToCharArray()));
                T obj = (T)serializer.ReadObject(ms);
                ms.Close();
                return obj;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
