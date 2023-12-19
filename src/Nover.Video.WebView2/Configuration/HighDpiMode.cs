﻿
namespace Nover.Video.WebView2.Configuration
{
    /// <summary>
    ///  Specifies the different high DPI modes that can be applied to an application.
    /// </summary>
    /// <remarks>
    ///  <para>
    ///  Specifying the high DPI mode is dependent on the OS version of the machine you're running your application on.
    ///  Setting the high DPI mode will work on machines running Windows 10 Creators Update (version 1703) or later versions.
    ///  </para>
    ///  <para>
    ///  Changing the DPI mode while the application is running doesn't impact scaling if you're using the `PerMonitor` value.
    ///  If there is more than one monitor attached and their DPI settings are different, the DPI may change when the window
    ///  is moved from one monitor to the other.
    ///  In this case, the application rescales according to the new monitor's DPI settings.
    ///  Alternatively, the DPI of a window can be changed when the OS scaling setting is changed for the monitor the window is on.
    ///  </para>
    /// </remarks>
    public enum HighDpiMode
    {
        INVALID = -1,

        /// <summary>
        ///  The window does not scale for DPI changes and always assumes a scale factor of 100%.
        /// </summary>
        UNAWARE = 0,

        /// <summary>
        ///  The window queries for the DPI of the primary monitor once and uses this for the application on all monitors.
        /// </summary>
        SYSTEM_AWARE = 1,

        /// <summary>
        ///  The window checks for DPI when it's created and adjusts scale factor when the DPI changes.
        /// </summary>
        PER_MONITOR_AWARE = 2,

        /// <summary>
        ///  Similar to <see cref="PerMonitor"/>, but enables child window DPI change notification, improved scaling of comctl32 controls and dialog scaling.
        /// </summary>
        PER_MONITOR_AWARE2 = 3,

        /// <summary>
        ///  Similar to <see cref="DpiUnaware"/>, but improves the quality of GDI/GDI+ based content.
        /// </summary>
        UNAWARE_GDI_SCALED = 4
    }
}
