using System.Text.RegularExpressions;

namespace Climb.Utilitys.SystemExt
{
    /// <summary>
    /// 正则扩展
    /// </summary>
    public static class RegexExt
    {
        #region 常用的正则
        /// <summary>
        /// 匹配数字的正则
        /// </summary>
        public static readonly Regex PatternNumber = new Regex(@"\-?(([1-9]\d*)|(\d*\.\d*)|0)");

        /// <summary>
        /// 邮箱正则
        /// </summary>
        public static readonly Regex PatterngEmail = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 

        /// <summary>
        /// 匹配中文的正则 
        /// </summary>
        public static readonly Regex PatternChina = new Regex("[\u4e00-\u9fa5]");


        /// <summary>
        /// 获取href里的内容
        /// </summary>
        public static readonly string PatternHref = @"(h|H)(r|R)(e|E)(f|F)\s*=\s*('|"")?((\w|\\|\/|\.|:|-|_)+)('|""| *|>)?";

        /// <summary>
        /// 获取src里边的内容
        /// </summary>
        public static readonly string PatternSrc = @"(s|S)(r|R)(c|C)\s*=\s*('|"")?((\w|\\|\/|\.|:|-|_)+)('|""| *|>)?";

        /// <summary>
        /// 获取所有img标签
        /// </summary>
        public static readonly string PatternImg =
            "<img[^>]+src=\\s*(?:'(?<src>[^']+)'|\"(?<src>[^\"]+)\"|(?<src>[^>\\s]+))\\s*[^>]*>";

        /// <summary>
        /// 获取所有页面title 标签
        /// </summary>
        public static readonly string PatternTitle = @"<Title[^>]*>(?<Title>[\s\S]{10,})</Title>";

        /// <summary>
        /// 获取所有页面的
        /// </summary>
        public static readonly string PatternBody = @"<Body[^>]*>(?<Body>[\s\S]{10,})</body>";


        /// <summary>
        /// 获取所有的a标签
        /// </summary>
        public static readonly string PatternAtarg = @"<.?a(.|\n)*?>";

        #endregion

        #region 判断字符串是否匹配

        /// <summary>
        /// 判断字符串是否匹配正则表达式
        /// </summary>
        /// <param name="strValue">输入字符串</param>
        /// <param name="pattern">正则语法</param>
        /// <returns>匹配则返回true</returns>
        public static bool IsMatch(this string strValue, string pattern)
        {
            return strValue != null && Regex.IsMatch(strValue, pattern);
        }

        #endregion

        #region 得到匹配的字符串

        /// <summary>
        /// 得到匹配的字符串
        /// </summary>
        /// <param name="strValue">输入字符串</param>
        /// <param name="pattern">正则语法</param>
        /// <returns>返回正则表达式的字符串</returns>
        public static string Match(this string strValue, string pattern)
        {
            if (strValue == null) return "";
            return Regex.Match(strValue, pattern).Value;
        }

        #endregion

    }
}
