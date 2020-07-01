using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;

namespace MeteorTools
{
    /// <summary>
    /// 有关腾讯的接口
    /// </summary>
    public class Tencent
    {
        /// <summary>
        /// 通过SKEY获取G_Tk
        /// </summary>
        /// <param name="skey">skey</param>
        /// <returns>方法字符串的g_tk</returns>
        public static string GetGToken(string skey) 
        {
            int hash = 5381;
            for (int i = 0, len = skey.Length; i < len; ++i)
            {
                hash += (hash << 5) + System.Text.Encoding.Unicode.GetBytes(skey[i].ToString())[0];
            }
            return (hash & 0x7fffffff).ToString();
        }

        /// <summary>
        /// 使用QQ号获取QQ头像
        /// </summary>
        /// <param name="QQUin">QQ号</param>
        /// <param name="ServerNum">请求服务器，传入2则使用QQ空间，传入1或不传入则使用QQ</param>
        /// <returns>返回Bitmap图片</returns>
        public static Bitmap GetUinImage(string QQUin,int ServerNum=1) {
            string URL = "";
            if (ServerNum == 2)
            {
                URL = "http://qlogo4.store.qq.com/qzone/" + QQUin + "/" + QQUin + "/640?" + (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            }
            else 
            {
                URL = "https://q.qlogo.cn/headimg_dl?dst_uin=" + QQUin + "&spec=640";
            }
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            return new Bitmap(resp.GetResponseStream());
        }

        /// <summary>
        /// 使用群号获取群头像
        /// </summary>
        /// <param name="QQGroup">群号</param>
        /// <returns>返回Bitmap图片</returns>
        public static Bitmap GetGroupImage(string QQGroup) {
            string URL = "http://p.qlogo.cn/gh/" + QQGroup + "/" + QQGroup + "/640/";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            return new Bitmap(resp.GetResponseStream());
        }

        /// <summary>
        /// 获取QQ空间访客数量
        /// </summary>
        /// <param name="QQUin">QQ号</param>
        /// <param name="g_tk">该QQ号对应的令牌</param>
        /// <param name="p_uin">该QQ号对应的令牌</param>
        /// <param name="skey">该QQ号对应的令牌</param>
        /// <param name="pt4_token">该QQ号对应的令牌</param>
        /// <param name="p_skey">该QQ号对应的令牌</param>
        /// <returns></returns>
        public static Dictionary<string,string> GetVisitors(string QQUin, string g_tk, string p_uin, string skey, string pt4_token, string p_skey)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://user.qzone.qq.com/proxy/domain/g.qzone.qq.com/cgi-bin/friendshow/cgi_get_visitor_simple?uin=" + QQUin + "&mask=1&g_tk=" + g_tk);
            request.Accept = "*/*";
            request.Referer = "https://user.qzone.qq.com/" + QQUin + "/infocenter";
            request.Method = "GET";
            request.Headers.Add("authority", "user.qzone.qq.com");
            request.Headers.Add("method", "GET");
            request.Headers.Add("path", "/proxy/domain/g.qzone.qq.com/cgi-bin/friendshow/cgi_get_visitor_simple?uin=" + QQUin + "&mask=1&g_tk=" + g_tk);
            request.Headers.Add("scheme", "https");
            request.Headers.Add("Cookie", "p_uin=" + p_uin + ";skey=" + skey + "; pt4_token=" + pt4_token + "; p_skey=" + p_skey + ";");
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(resp.GetResponseStream());
            JObject ResponseJson = JObject.Parse(String.InterceptionString(reader.ReadToEnd(), @"(", @")"));
            if (ResponseJson["error"] != null) {
                return null;
            }
            JArray DataJson = JArray.Parse(JObject.Parse(ResponseJson["data"].ToString())["modvisitcount"].ToString());
            Dictionary<string, string> Json = new Dictionary<string, string>();
            Json.Add("TotalCount", JObject.Parse(DataJson[0].ToString())["totalcount"].ToString());
            Json.Add("TodayCount", JObject.Parse(DataJson[0].ToString())["todaycount"].ToString());
            Json.Add("BlockCount", JObject.Parse(DataJson[2].ToString())["totalcount"].ToString());
            Json.Add("BlockTodayCount", JObject.Parse(DataJson[2].ToString())["todaycount"].ToString());
            return Json;
        }

        /// <summary>
        /// 获取详细的访客信息，以JArray返回
        /// </summary>
        /// <param name="QQUin">QQ号</param>
        /// <param name="g_tk">该QQ号对应的令牌</param>
        /// <param name="p_uin">该QQ号对应的令牌</param>
        /// <param name="skey">该QQ号对应的令牌</param>
        /// <param name="pt4_token">该QQ号对应的令牌</param>
        /// <param name="p_skey">该QQ号对应的令牌</param>
        /// <returns>返回一个JArray类型</returns>
        public static JArray GetVisitorsInfo(string QQUin, string g_tk, string p_uin, string skey, string pt4_token, string p_skey) 
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://user.qzone.qq.com/proxy/domain/r.qzone.qq.com/cgi-bin/right_frame.cgi?uin=" + QQUin + "&param=3_" + QQUin + "_0%7C14_" + QQUin + "%7C8_8_" + QQUin + "_1_1_0_0_1%7C10%7C11%7C12%7C13_0%7C17%7C20%7C9_0_8_1%7C18&g_tk=" + g_tk);
            request.Accept = "*/*";
            request.Referer = "https://user.qzone.qq.com/" + QQUin + "/infocenter";
            request.Method = "GET";
            request.Headers.Add("authority", "user.qzone.qq.com");
            request.Headers.Add("method", "GET");
            request.Headers.Add("path", "/proxy/domain/r.qzone.qq.com/cgi-bin/right_frame.cgi?uin=" + QQUin + "&param=3_" + QQUin + "_0%7C14_" + QQUin + "%7C8_8_" + QQUin + "_1_1_0_0_1%7C10%7C11%7C12%7C13_0%7C17%7C20%7C9_0_8_1%7C18&g_tk=" + g_tk);
            request.Headers.Add("scheme", "https");
            request.Headers.Add("Cookie", "p_uin=" + p_uin + ";skey=" + skey + "; pt4_token=" + pt4_token + "; p_skey=" + p_skey + ";");
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            if (resp.StatusCode == HttpStatusCode.InternalServerError) 
            {
                return null; 
            }
            StreamReader reader = new StreamReader(resp.GetResponseStream());
            JObject ResponseJson = JObject.Parse(String.InterceptionString(reader.ReadToEnd(), @"(", @")").Replace("\'", "\""));
            return (JArray)JArray.Parse(JObject.Parse(JObject.Parse(JObject.Parse(ResponseJson["data"].ToString())["module_3"].ToString())["data"].ToString())["items"].ToString());
        }
    }
}
