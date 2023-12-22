using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Nover.Video.Application
{
    public static class BilibiliUtil
    {
        /// <summary>
        /// 获取url字符串参数, 返回参数值字符串
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="url">url字符串</param>
        /// <returns></returns>
        public static string GetQueryString(string name, string url)
        {
            Regex re = RegexUtil.QueryRegex();
            MatchCollection mc = re.Matches(url);
            foreach (Match m in mc.Cast<Match>())
            {
                if (m.Result("$2").Equals(name))
                {
                    return m.Result("$3");
                }
            }
            return "";
        }

        public static string GetSign(string parms)
        {
            string toEncode = parms + "59b43e04ad6965f34319062b478f83dd";
            return string.Concat(MD5.HashData(Encoding.UTF8.GetBytes(toEncode)).Select(i => i.ToString("x2")));
        }

        public static string GetTimeStamp(bool bflag)
        {
            DateTimeOffset ts = DateTimeOffset.Now;
            return (bflag ? ts.ToUnixTimeSeconds() : ts.ToUnixTimeMilliseconds()).ToString();
        }

        //https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
        private static readonly Random random = new();
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //https://stackoverflow.com/a/45088333
        public static string ToQueryString(NameValueCollection nameValueCollection)
        {
            NameValueCollection httpValueCollection = HttpUtility.ParseQueryString(string.Empty);
            httpValueCollection.Add(nameValueCollection);
            return httpValueCollection.ToString()!;
        }
    }
}
