using System.IO;
using System.Net;
using BGAPI.Entities;
using BGAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BGAPI.Handlers
{
    internal sealed class ContentResponseHandler : BaseResponseHandler, IResponseHandler
    {
        public ContentResponseHandler(HttpWebResponse response, RequestInfo info) : base(response, info) { }
        
        public IActionResult GetResult() {
            using var dataStream = Response.GetResponseStream();
            using var reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();

            SetInfo(responseFromServer);
            
            return new ContentResult {Content = responseFromServer, ContentType = Response.ContentType};
        }
    }
}