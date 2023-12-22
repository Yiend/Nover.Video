using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nover.Video.Application
{
    public class BilibiliApiConst
    {
        /// <summary>
        /// 主域名
        /// </summary>
        public const string BaseUrl = "https://passport.bilibili.com";

        /// <summary>
        /// Web登录地址
        /// </summary>
        public const string WebLoginUrl = $"{BaseUrl}/x/passport-login/web/qrcode/generate?source=main-fe-header";

        /// <summary>
        /// 获取登录状态地址
        /// </summary>
        public const string LoginStatusUrl = $"{BaseUrl}/x/passport-login/web/qrcode/poll?qrcode_key={{0}}&source=main-fe-header";

        /// <summary>
        /// TV登录地址
        /// </summary>
        public const string TvLoginUrl = "https://passport.snm0516.aisee.tv/x/passport-tv-login/qrcode/auth_code";
       
        /// <summary>
        /// 
        /// </summary>
        public const string PollUrl = "https://passport.bilibili.com/x/passport-tv-login/qrcode/poll";
    }
}
