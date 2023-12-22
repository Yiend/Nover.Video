using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nover.Video.Application.Events
{
    public class BilibiliLoginEvent
    {
        /// <summary>
        /// 类型，1,Web ，2,,TV
        /// </summary>
        public int Type {  get; set; }

        /// <summary>
        /// Web 登录参数
        /// </summary>
        public string QrcodeKey { get; set; }

        /// <summary>
        /// TV 登录参数
        /// </summary>
        public Dictionary<string, string> TvParams { get; set; }
    }
}
