namespace MeteorTools
{
    /// <summary>
    /// 关于字符串操作类
    /// </summary>
    public static class String
    {
        /// <summary>
        /// 通过前后字符串截取中间
        /// </summary>
        /// <param name="Original">源字符串</param>
        /// <param name="Before">截取前字符</param>
        /// <param name="After">截取后字符</param>
        /// <returns>截取完毕字符串</returns>
        public static string InterceptionString(string Original, string Before, string After)
        {
            Original = Original.Remove(Original.LastIndexOf(After));
            int i = Original.IndexOf(Before) + 1;
            return Original.Substring(i, Original.Length - i);
        }
    }
}
