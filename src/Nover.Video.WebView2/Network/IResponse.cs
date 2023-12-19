using System.Collections.Generic;
using System.Net;

namespace Nover.Video.WebView2.Network
{
    public interface IResponse
    {
        object Content { get; set; }
        IDictionary<string, string[]> Headers { get; }
        HttpStatusCode StatusCode { get; set; }
        string ReasonPhrase { get; set; }
        bool HasRouteResponse { get; set; }
    }
}
