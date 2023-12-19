﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Nover.Video.WebView2
{
    public static partial class Interop
    {
        public static partial class User32
        {
            public enum GW : uint
            {
                HWNDFIRST = 0,
                HWNDLAST = 1,
                HWNDNEXT = 2,
                HWNDPREV = 3,
                OWNER = 4,
                CHILD = 5,
                ENABLEDPOPUP = 6,
                MAX = 6
            }
        }
    }
}