using System;
using System.Collections.Generic;
using System.Text;

namespace MeteorTools
{
    /// <summary>
    /// 关于字符串操作类
    /// </summary>
    public class String
    {
        /// <summary>
        /// 通过前后字符串截取中间
        /// </summary>
        /// <param name="Original">源字符串</param>
        /// <param name="Before">截取前字符</param>
        /// <param name="After">截取后字符</param>
        /// <returns>截取完毕字符串</returns>
        public static string InterceptionString(string Original,string Before,string After) 
        {
            int i, ii;
            i = Original.IndexOf(Before, 0) + Before.Length;
            ii = Original.IndexOf(After, i);
            return Original.Substring(i, ii - i);
        }
    }
}
