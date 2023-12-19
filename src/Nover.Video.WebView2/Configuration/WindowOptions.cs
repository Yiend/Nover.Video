﻿using System.Drawing;

namespace Nover.Video.WebView2.Configuration
{
    /// <summary>
    /// Default implementation of <see cref="IWindowOptions"/>.
    /// </summary>
    public class WindowOptions : IWindowOptions
    {
        private WindowState _windowState;

        /// <summary>
        /// Initializes a new instance of <see cref="WindowOptions"/>
        /// </summary>
        public WindowOptions()
        {
            DisableResizing = false;
            DisableMinMaximizeControls = false;
            Borderless = false;
            StartCentered = true;
            KioskMode = false;
            Fullscreen = false;
            NoResize = false;
            WindowState = WindowState.Normal;
            Title = "DB For Nover";
            RelativePathToIconFile = "edgesharp.ico";

            MinimumSize = Size.Empty;
            MaximumSize = Size.Empty;
            BorderlessOption = new BorderlessOption();
        }

        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public string RelativePathToIconFile { get; set; }

        /// <inheritdoc />
        public bool DisableResizing { get; set; }

        /// <inheritdoc />
        public bool DisableMinMaximizeControls { get; set; }

        /// <inheritdoc />
        public bool Fullscreen { get; set; }

        /// <inheritdoc />
        public bool KioskMode { get; set; }

        /// <inheritdoc />
        public bool StartCentered { get; set; }

        /// <inheritdoc />
        public bool Borderless { get; set; }

        /// <inheritdoc />
        public bool UseCustomStyle { get; set; }
        
        /// <inheritdoc />
        public bool NoResize { get; set; }

        /// <inheritdoc />
        public Size MinimumSize { get; set; }

        /// <inheritdoc />
        public Size MaximumSize { get; set; }

        /// <inheritdoc />
        public HighDpiMode HighDpiMode { get; set; }

        /// <inheritdoc />
        public WindowState WindowState
        {
            get
            {
                if (Fullscreen || KioskMode) return WindowState.Fullscreen;
                return _windowState;
            }
            set
            {
                _windowState = value;
            }
        }

        /// <inheritdoc />
        public BorderlessOption BorderlessOption { get; set; }
    }
}