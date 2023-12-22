using BBDown.Core.Util;
using Nover.Video.Application.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.EventBus;

namespace Nover.Video.Application.EventHandler
{
    public class BilibiliEventHandler : ILocalEventHandler<BilibiliLoginEvent>
    {
        /// <summary>
        /// 获取登录状态
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(BilibiliLoginEvent eventData)
        {
            if (eventData.Type == 1)
            {
                await GetWebLoginStatusAsync(eventData);
            }
            else 
            {
                await GetTvLoginStatusAsync(eventData);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task GetWebLoginStatusAsync(BilibiliLoginEvent eventData)
        {
            bool flag = false;
            while (true)
            {
                await Task.Delay(1000);
                string w = await HTTPUtil.GetWebSourceAsync(string.Format(BilibiliApiConst.LoginStatusUrl, eventData.QrcodeKey));
                int code = JsonDocument.Parse(w).RootElement.GetProperty("data").GetProperty("code").GetInt32();
                switch (code)
                {
                    case 86038://二维码过期
                        throw new Exception("二维码已过期, 请重新执行登录指令");
                    case 86101://等待扫码
                        continue;
                    case 86090://等待确认
                        if (!flag)
                        {
                            flag = !flag;
                        }
                        break;
                    default:
                        string cc = JsonDocument.Parse(w).RootElement.GetProperty("data").GetProperty("url").ToString();
                        //导出cookie
                        File.WriteAllText(Path.Combine(Path.GetDirectoryName(Environment.ProcessPath), "BBDown.data"), cc[(cc.IndexOf('?') + 1)..].Replace("&", ";"));
                        return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        private async Task GetTvLoginStatusAsync(BilibiliLoginEvent eventData) 
        {
            while (true)
            {
                await Task.Delay(1000);
                byte[] responseArray = await (await HTTPUtil.AppHttpClient.PostAsync(BilibiliApiConst.PollUrl, new FormUrlEncodedContent(eventData.TvParams))).Content.ReadAsByteArrayAsync();
                string web = Encoding.UTF8.GetString(responseArray);
                string code = JsonDocument.Parse(web).RootElement.GetProperty("code").ToString();
                if (code == "86038")
                {
                    throw new Exception("二维码已过期, 请重新执行登录指令");
                }
                else if (code == "86039") //等待扫码
                {
                    continue;
                }
                else
                {
                    string cc = JsonDocument.Parse(web).RootElement.GetProperty("data").GetProperty("access_token").ToString();
                    //导出cookie
                    File.WriteAllText(Path.Combine(Path.GetDirectoryName(Environment.ProcessPath), "BBDownTV.data"), "access_token=" + cc);
                    break;
                }
            }
        }
    }
}
