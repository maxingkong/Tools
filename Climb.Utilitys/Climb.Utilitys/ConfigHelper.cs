using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climb.Utilitys
{
    /// <summary>
    /// 配置文件的帮助类
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 获取appconfig 里边的appsetting节点内容
        /// </summary>
        /// <param name="keyName">节点名称</param>
        /// <returns>返回 里边的appsetting节点内容</returns>
        public static string GetAppSetings(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName];
        }

        /// <summary>
        /// 获取appconfig 里边的connection节点内容
        /// </summary>
        /// <param name="keyName">节点名称</param>
        /// <returns>返回connection 节点的conectionstring的值</returns>
        public static string GetConnectionStrings(string keyName)
        {
            return ConfigurationManager.ConnectionStrings[keyName].ConnectionString;
        }
    }
}
