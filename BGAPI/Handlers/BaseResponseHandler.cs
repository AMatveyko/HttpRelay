using System.Net;
using BGAPI.Entities;
using BGAPI.Helpers;

namespace BGAPI.Handlers
{
    internal abstract class BaseResponseHandler
    {
        protected readonly HttpWebResponse Response;
        protected readonly RequestInfo Info;

        protected BaseResponseHandler(HttpWebResponse response, RequestInfo info) => (Response, Info) = (response, info);

        protected void SetInfo(string data) {
            Info.ResponseData = data;
            Info.ResponseHeaders = HeadersHelper.GetHeaders(Response.Headers);
        }
    }
}