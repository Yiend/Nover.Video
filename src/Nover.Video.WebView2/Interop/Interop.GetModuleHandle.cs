using System;
using System.Runtime.InteropServices;
using static Nover.Video.WebView2.Interop;

namespace Nover.Video.WebView2
{
    internal partial class Interops
    {
        internal partial class Kernel32
        {
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern IntPtr GetModuleHandleW(string moduleName);
        }
    }
}
