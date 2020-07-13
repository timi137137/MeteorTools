using System;

namespace MeteorTools
{
    /// <summary>
    /// 时间相关类
    /// </summary>
    public static class Time
    {
        /// <summary>    
        /// 时间戳转为DateTime格式时间    
        /// </summary>    
        /// <param name="timeStamp">时间戳</param>    
        /// <returns>DateTime格式</returns>    
        public static DateTime ConvertStringToDateTime(string timeStamp)
        {
            Int64 begtime = Convert.ToInt64(timeStamp) * 10000000;//100毫微秒为单位,textBox1.text需要转化的int日期
            DateTime dt_1970 = new DateTime(1970, 1, 1, 8, 0, 0);
            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
            long time_tricks = tricks_1970 + begtime;//日志日期刻度
            DateTime dt = new DateTime(time_tricks);//转化为DateTim
            return dt;
        }
    }
}
