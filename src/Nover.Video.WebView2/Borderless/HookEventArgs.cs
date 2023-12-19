﻿using System;

namespace Nover.Video.WebView2.Borderless
{
    public class HookEventArgs : EventArgs
    {
        public int HookCode;    // Hook code
        public IntPtr wParam;   // WPARAM argument
        public IntPtr lParam;   // LPARAM argument
    }
}