using System.Collections.Generic;
using System.Net;
using System.Text;
using BGAPI.Entities;

namespace BGAPI.Workers
{
    internal sealed class PostRequester : BaseRequester
    {

        private readonly Dictionary<string, string> _headers;
        private readonly string _data;

        public PostRequester(Dictionary<string, string> headers, string data ) {
            (_headers, _data ) = (headers, data);
        }
            

        
        protected override HttpWebRequest ModifyRequest(HttpWebRequest request, RequestInfo info) {
            info.RequestData = _data;
            request.Method = "POST";
            AddHeaders(request, _headers);
            AddData(request, _data);
            return request;
        }
        
        private static void AddHeaders(HttpWebRequest request, Dictionary<string, string> headers) {
            foreach (var header in headers) {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        private static void AddData(HttpWebRequest request, string data) {
            var payLoad = Encoding.ASCII.GetBytes(data);
            request.ContentLength = payLoad.Length;
            using var stream = request.GetRequestStream();
            stream.Write(payLoad, 0, payLoad.Length);
        }
    }
}