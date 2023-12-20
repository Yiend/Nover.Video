using Nover.Video.WebView2.Configuration;
using Nover.Video.WebView2.Defaults;
using Nover.Video.WebView2.Infrastructure;
using System;

namespace Nover.Video.ReactApp
{
    public class ReactAppConfig : Configuration
    {
        public ReactAppConfig() : base()
        {
            UrlSchemes.Add(new UrlScheme("http", "dist", "dist", UrlSchemeType.HostToFolder));
            UrlSchemes.Add(new UrlScheme("https://github.com/edgeSharp/EdgeSharp", true, UrlSchemeType.ExternalBrowser));

            //启动地址
            StartUrl = "http://dist/index.html"; 

            //最小宽高
            WindowOptions.MinimumSize = new System.Drawing.Size(800,600);
            //最大宽高
            WindowOptions.MaximumSize = new System.Drawing.Size(1366, 768);
            //无边框
            WindowOptions.Borderless = false;

            //启用右键菜单
            WebView2CreationOptions.AreDefaultContextMenusEnabled = false;
            WebView2CreationOptions.AreDefaultScriptDialogsEnabled = false;
            WebView2CreationOptions.AreBrowserAcceleratorKeysEnabled = false;
            //启用缩放
            WebView2CreationOptions.IsZoomControlEnabled = false;
            //启用状态栏
            WebView2CreationOptions.IsStatusBarEnabled = false;
            //启用F12开发者工具
            WebView2CreationOptions.AreDevToolsEnabled = false;
            //启用错误页面
            WebView2CreationOptions.IsBuiltInErrorPageEnabled = false;

            CommandLineOptions.Add("disable-web-security");
        }
    }
}