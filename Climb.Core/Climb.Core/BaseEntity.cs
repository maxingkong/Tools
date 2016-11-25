/**************************************************
* 文 件 名：BaseEntity.cs
* 文件版本：1.0
* 创 建 人：mxk
* 联系方式：QQ:84664969   Email:84664969@qq.com   Phone:18513950591
* 创建日期：2014/7/1 23:59:30
* 文件说明：基类 实现了基础类。
* 修 改 人：
* 修改日期：
* 备注描述：
*
**************************************************/

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Climb.Core
{
    /// <summary>
    /// 所有实体的基类  支持可序列化
    /// </summary>
    [Serializable]
    public abstract class BaseEntity : ICloneable, IDisposable
    {
        /// <summary>
        /// 基类的主键
        /// </summary>
        public int  PkId { get; }


        /// <summary>
        /// 构造函数
        /// </summary>
        protected BaseEntity(int pkId)
        {
            PkId = pkId;
        }

        #region 方法
         
        /// <summary>
        /// 判断两个实体是否是同一数据记录的实体
        /// </summary>
        /// <param name="obj">要比较的实体信息中的主键id</param>
        /// <returns>如果是同一个对象那么返回ture 如果不是同一个对象返回false</returns>
        public override bool Equals(object obj)
        {
            BaseEntity entity = obj as BaseEntity;
            return entity != null && PkId.Equals(entity.PkId);
        }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>
        /// 当前对象ID的哈希代码。
        /// </returns>
        public override int GetHashCode()
        {
            return PkId.GetHashCode();
        }

        /// <summary>
        /// 创建当前业务实体对象的一个副本。
        /// </summary>
        /// <returns>返回当前的对象副本信息 深度克隆</returns>
        public object Clone()
        {
            using (MemoryStream buffer = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(buffer, this);
                buffer.Position = 0;
                BaseEntity obj = (BaseEntity)formatter.Deserialize(buffer);
                return obj;
            }
        }

        /// <summary>
        /// 通过属性名称获取该属性值。
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <returns>获取属性的值</returns>
        public object GetPropertyValue(string name)
        {
            PropertyInfo pi = GetType().GetProperty(name);
            return pi.GetValue(this, null);
        }

        /// <summary>
        /// 通过属性名称设置该属性值。
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="value">要设置的值</param>
        public void SetPropertyValue(string name, object value)
        {
            PropertyInfo pi = this.GetType().GetProperty(name);
            pi.SetValue(this, value, null);
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        { 
            this.Dispose(true);
            // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
            GC.SuppressFinalize(true);
        }

        /// <summary>
        /// 释放资源。
        /// </summary>
        /// <param name="disposing">是否释放资源</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //do something..
            }
        }

        #endregion

    }
}
