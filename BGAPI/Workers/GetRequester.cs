using System.Net;
using BGAPI.Entities;

namespace BGAPI.Workers
{
    internal sealed class GetRequester : BaseRequester
    {
        protected override HttpWebRequest ModifyRequest(HttpWebRequest request, RequestInfo info) =>
            request;
    }
}