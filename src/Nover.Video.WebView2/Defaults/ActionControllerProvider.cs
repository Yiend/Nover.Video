﻿using System.Threading.Tasks;
using Nover.Video.WebView2.Configuration;
using Nover.Video.WebView2.Infrastructure;
using Nover.Video.WebView2.Network;

namespace Nover.Video.WebView2.Defaults
{
    /// <summary>
    /// The default implementation of <see cref="IActionControllerProvider"/>.
    /// </summary>
    public class ActionControllerProvider : IActionControllerProvider
    {
        protected readonly IConfiguration _config;
        protected readonly IActionRouteProvider _routeProvider;
        protected readonly IErrorHandler _errorHandler;
        protected readonly IDataTransferOptions _dataTransferOptions;

        /// <summary>
        /// Initializes a new instance of <see cref="ActionControllerProvider"/>.
        /// </summary>
        /// <param name="config">EdgeSharp <see cref="IConfiguration"/> instance.</param>
        /// <param name="routeProvider">The <see cref="IActionRouteProvider"/> instance.</param>
        /// <param name="errorHandler">The <see cref="IErrorHandler"/> instance.</param>
        /// <param name="dataTransferOptions">The <see cref="IDataTransferOptions"/> instance.</param>
        public ActionControllerProvider(IConfiguration config, IActionRouteProvider routeProvider, IErrorHandler errorHandler, IDataTransferOptions dataTransferOptions)
        {
            _config = config;
            _routeProvider = routeProvider;
            _errorHandler = errorHandler;
            _dataTransferOptions = dataTransferOptions;
        }

        /// <inheritdoc />
        public virtual IActionResponse Execute(IActionRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.RoutePath))
            {
                return _errorHandler.HandleRouteNotFound(request.RoutePath);
            }

            if (request.RoutePath.ToLower().Equals("/info"))
            {
                return GetInfo();
            }

            var route = _routeProvider.GetRoute(request.RoutePath);
            if (route == null)
            {
                return _errorHandler.HandleRouteNotFound(request.RoutePath);
            }

            return ExecuteRoute(route, request);
        }

        private IActionResponse ExecuteRoute(Route route, IActionRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.RoutePath))
            {
                return _errorHandler.HandleRouteNotFound(request.RoutePath);
            }

            if (request.RoutePath.ToLower().Equals("/info"))
            {
                return GetInfo();
            }

            IActionResponse response = null;
            if (route.IsAsync)
            {
                if (!route.HasReturnValue)
                {
                    Task.Run(() => route.InvokeAsync(request));
                    response = new ActionResponse
                    {
                        HasRouteResponse = false
                    };
                }
                else
                {
                    var asyncTask = Task.Run(async () => await route.InvokeAsync(request));
                    asyncTask.Wait();
                    response = asyncTask?.Result;
                }
            }
            else
            {
                response = route.Invoke(request);
            }

            return response;
        }

        private IActionResponse GetInfo()
        {
            dynamic info = new System.Dynamic.ExpandoObject();
            info.sdk = _config?.SdkVersion ?? VersionInfo.SdkVersion;
            info.runtime = _config?.RuntimeVersion;

            return new ActionResponse
            {
                HasRouteResponse = true,
                Content = info
            };
        }
    }
}