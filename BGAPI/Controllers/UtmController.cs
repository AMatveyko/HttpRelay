using BGAPI.Handlers;
using BGAPI.Helpers;
using BGAPI.Workers;
using Microsoft.AspNetCore.Mvc;

namespace BGAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class UtmController : Controller
    {
        [HttpGet]
        [Route("Relay")]
        [ActionName("Relay")]
        public IActionResult GetRelay(string url) {
            var handler = GetHandler(new GetRequester());
            return handler.GetResult(url);
        }
        
        [HttpPost]
        [Route("Relay")]
        [ActionName("Relay")]
        public IActionResult PostRelay(string url, [FromBody] dynamic parameters) {
            var headers = HeadersHelper.GetSuitableHeaders(Request.Headers);
            var data = parameters.ToString();

            var requester = new PostRequester(headers, data);
            var handler = GetHandler(requester);

            return handler.GetResult(url);
        }

        private RequestHandler GetHandler(IRequester requester) => new (requester, GetLogger());
        
        private CustomLogger GetLogger() => new (GetClientIp());
        
        private string GetClientIp() => Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "no ip";
    }
}