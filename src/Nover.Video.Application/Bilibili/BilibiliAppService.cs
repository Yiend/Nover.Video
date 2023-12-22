using BBDown;
using BBDown.Core.Util;
using Microsoft.Extensions.Logging;
using Nover.Video.Application.Events;
using QRCoder;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Local;


namespace Nover.Video.Application;

public class BilibiliAppService : NoverAppService
{
    private readonly ILocalEventBus _localEventBus;


    /// <summary>
    /// 获取登录二维码
    /// </summary>
    /// <returns></returns>
    public Task<byte[]> GetLoginQRCodeAsync(int type)
    {
        if (type == 1)
        {
            return WebLoginAsync();
        }

        return TVLoginAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private async Task<byte[]> WebLoginAsync()
    {
        try
        {
            string loginData = await HTTPUtil.GetWebSourceAsync(BilibiliApiConst.WebLoginUrl);
            string url = JsonDocument.Parse(loginData).RootElement.GetProperty("data").GetProperty("url").ToString();
            string qrcodeKey = BilibiliUtil.GetQueryString("qrcode_key", url);

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode pngByteCode = new(qrCodeData);
            // 发布事件
            _ = _localEventBus.PublishAsync(new BilibiliLoginEvent { Type = 1, QrcodeKey = qrcodeKey });
            // 返回二维码图片
            return pngByteCode.GetGraphic(7);
        }
        catch (Exception e)
        {
            Logger.LogException(e);
            throw e;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private async Task<byte[]> TVLoginAsync()
    {
        try
        {
            var parms = GetTVLoginParms();
            var loginData = await HTTPUtil.AppHttpClient.PostAsync(BilibiliApiConst.TvLoginUrl, new FormUrlEncodedContent(parms.ToDictionary()));
            byte[] responseArray = await loginData.Content.ReadAsByteArrayAsync();
            string web = Encoding.UTF8.GetString(responseArray);
            string url = JsonDocument.Parse(web).RootElement.GetProperty("data").GetProperty("url").ToString();
            string authCode = JsonDocument.Parse(web).RootElement.GetProperty("data").GetProperty("auth_code").ToString();


            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode pngByteCode = new(qrCodeData);

            var consoleQRCode = new ConsoleQRCode(qrCodeData);
            consoleQRCode.GetGraphic();
            parms.Set("auth_code", authCode);
            parms.Set("ts", BilibiliUtil.GetTimeStamp(true));
            parms.Remove("sign");
            parms.Add("sign", BilibiliUtil.GetSign(BilibiliUtil.ToQueryString(parms)));

            // 发布事件
            _ = _localEventBus.PublishAsync(new BilibiliLoginEvent { Type = 1, TvParams = parms.ToDictionary() });
            // 返回二维码图片
            return pngByteCode.GetGraphic(7);
        }
        catch (Exception e)
        {
            Logger.LogException(e);
            throw e;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public NameValueCollection GetTVLoginParms()
    {
        NameValueCollection sb = new();
        DateTime now = DateTime.Now;
        string deviceId = BilibiliUtil.GetRandomString(20);
        string buvid = BilibiliUtil.GetRandomString(37);
        string fingerprint = $"{now:yyyyMMddHHmmssfff}{BilibiliUtil.GetRandomString(45)}";
        sb.Add("appkey", "4409e2ce8ffd12b8");
        sb.Add("auth_code", "");
        sb.Add("bili_local_id", deviceId);
        sb.Add("build", "102801");
        sb.Add("buvid", buvid);
        sb.Add("channel", "master");
        sb.Add("device", "OnePlus");
        sb.Add($"device_id", deviceId);
        sb.Add("device_name", "OnePlus7TPro");
        sb.Add("device_platform", "Android10OnePlusHD1910");
        sb.Add($"fingerprint", fingerprint);
        sb.Add($"guid", buvid);
        sb.Add($"local_fingerprint", fingerprint);
        sb.Add($"local_id", buvid);
        sb.Add("mobi_app", "android_tv_yst");
        sb.Add("networkstate", "wifi");
        sb.Add("platform", "android");
        sb.Add("sys_ver", "29");
        sb.Add($"ts", BilibiliUtil.GetTimeStamp(true));
        sb.Add($"sign", BilibiliUtil.GetSign(BilibiliUtil.ToQueryString(sb)));

        return sb;
    }


}
