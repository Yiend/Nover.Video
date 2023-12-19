﻿using System;
using Nover.Video.WebView2.Infrastructure;

namespace Microsoft.Extensions.Logging
{
    public static class LoggerExtensions
    {
        public static void LogError(this ILogger logger, Exception exception)
        {
            logger?.LogError(exception, exception?.Message);
        }
    }
}
