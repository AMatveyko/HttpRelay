using System.Collections.Generic;

namespace BGAPI.Entities
{
    internal sealed class RequestInfo
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public Dictionary<string,string> RequestHeaders { get; set; }
        public string RequestData { get; set; }
        public Dictionary<string,string> ResponseHeaders { get; set; }
        public string ResponseData { get; set; }
        public string StatusCode { get; set; }
    }
}