using System.Net;
using BGAPI.Entities;

namespace BGAPI.Workers
{
    internal interface IRequester
    {
        HttpWebRequest GetRequest(string url, RequestInfo info);
    }
}