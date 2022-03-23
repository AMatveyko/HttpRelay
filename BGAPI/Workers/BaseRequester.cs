using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BGAPI.Entities;
using BGAPI.Helpers;

namespace BGAPI.Workers
{
    internal abstract class BaseRequester : IRequester
    {

        public HttpWebRequest GetRequest(string url, RequestInfo info) {
            var request = CreateRequest(url);
            var modifiedRequest = ModifyRequest(request, info);
            SetInfo(modifiedRequest, info);
            return modifiedRequest;
        }

        private static HttpWebRequest CreateRequest(string url) {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            return request;
        }
        
        private static void SetInfo(HttpWebRequest request, RequestInfo info) {
            info.Url = request.RequestUri.ToString();
            info.Method = request.Method;
            info.RequestHeaders = HeadersHelper.GetHeaders(request.Headers);
        }

        protected abstract HttpWebRequest ModifyRequest(HttpWebRequest request, RequestInfo info);
    }
}