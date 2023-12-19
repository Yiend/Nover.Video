﻿using System;
using Microsoft.Web.WebView2.Core;
using Nover.Video.WebView2.Browser;


namespace Nover.Video.ReactApp
{
    public class ReactAppWindow : BrowserWindow
    {
        protected override void OnInitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            base.OnInitializationCompleted(sender, e);
            CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
        }

        private void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
        }
    }
}
