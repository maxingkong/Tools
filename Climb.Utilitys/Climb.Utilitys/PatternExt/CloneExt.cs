using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Climb.Utilitys
{
    /// <summary>
    /// 克隆对象
    /// </summary>
    public  class CloneExt
    {
        #region 复制对象
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <returns></returns>
        public static T Clone<T>(T serializableObject)
        {
            object objCopy = null;
            MemoryStream stream = new MemoryStream();
            BinaryFormatter binFormatter = new BinaryFormatter();
            binFormatter.Serialize(stream, serializableObject);
            stream.Position = 0;
            objCopy = (T)binFormatter.Deserialize(stream);
            stream.Close();
            return (T)objCopy;
        }
        #endregion
    }
}
