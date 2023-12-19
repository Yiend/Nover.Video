﻿using Microsoft.Web.WebView2.Core;

namespace Nover.Video.WebView2.Configuration
{
    public class WebView2CreationOptions
    {
        private bool _isDebugging;

        public WebView2CreationOptions(bool isDebugging = true)
        {
            _isDebugging = isDebugging;

            IsScriptEnabled = true;
            IsWebMessageEnabled = true;
            AreHostObjectsAllowed = true;
            AllowSingleSignOnUsingOSPrimaryAccount = false;

            IsZoomControlEnabled = _isDebugging ? true : false;
            AreDefaultScriptDialogsEnabled = _isDebugging ? true : false;
            IsStatusBarEnabled = _isDebugging ? true : false;
            AreDevToolsEnabled = _isDebugging ? true : false;
            AreDefaultContextMenusEnabled = _isDebugging ? true : false;
            IsBuiltInErrorPageEnabled = _isDebugging ? true : false;
            AreBrowserAcceleratorKeysEnabled = _isDebugging ? true : false;
            AdditionalBrowserArguments = null;
            Language = null;
            TargetCompatibleBrowserVersion = null;
            BrowserExecutableFolder = null;
            UserDataFolder = null;
            UserAgent = null;
            UriFilter = "*";
            ResourceContext = CoreWebView2WebResourceContext.All;
        }

        public bool IsScriptEnabled { get; set; }
        public bool IsWebMessageEnabled { get; set; }
        public bool AreDefaultScriptDialogsEnabled { get; set; }
        public bool IsStatusBarEnabled { get; set; }
        public bool AreDevToolsEnabled { get; set; }
        public bool AreDefaultContextMenusEnabled { get; set; }
        public bool AreHostObjectsAllowed { get; set; }
        public bool IsZoomControlEnabled { get; set; }
        public bool IsBuiltInErrorPageEnabled { get; set; }
        public string UserAgent { get; set; }
        public bool AreBrowserAcceleratorKeysEnabled { get; set; }
        public string AdditionalBrowserArguments { get; set; }
        public string Language { get; set; }
        public string TargetCompatibleBrowserVersion { get; set; }
        public bool AllowSingleSignOnUsingOSPrimaryAccount { get; set; }
        public string BrowserExecutableFolder { get; set; }
        public string UserDataFolder { get; set; }
        public string UriFilter { get; set; }
        public CoreWebView2WebResourceContext ResourceContext { get; set; }
    }
}
