﻿using Nover.Video.WebView2.Defaults;

namespace Nover.Video.WebView2.Infrastructure
{
    public class CurrentAppSettings : AppUser
    {
        private IAppSettings appSettings;

        public override IAppSettings Properties
        {
            get
            {
                if (appSettings == null)
                {
                    appSettings = new AppSettings();
                }
                return appSettings;
            }
            set
            {
                appSettings = value;
            }
        }

    }
}
