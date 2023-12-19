﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Nover.Video.WebView2
{
    public static partial class Interop
    {
        public static partial class SHCore
        {
            [DllImport(Libraries.SHCore, ExactSpelling = true)]
            public static extern HRESULT GetProcessDpiAwareness(IntPtr hprocess, out PROCESS_DPI_AWARENESS value);
        }
    }
}
