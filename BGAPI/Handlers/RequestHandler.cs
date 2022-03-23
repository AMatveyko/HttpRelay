using System;
using System.Net;
using BGAPI.Builders;
using BGAPI.Entities;
using BGAPI.Workers;
using Microsoft.AspNetCore.Mvc;

namespace BGAPI.Handlers
{
    internal sealed class RequestHandler
    {
        private readonly IRequester _requester;
        private readonly CustomLogger _logger;
        private readonly RequestInfo _info = new ();

        public RequestHandler(IRequester requester, CustomLogger logger ) =>
            (_requester, _logger ) = (requester, logger );

        public IActionResult GetResult(string url) => TryGetAndLogResult(url);

        private IActionResult TryGetAndLogResult(string url) {
            try {
                var result = DoGetResult(url);
                Log();
                return result;
            }
            catch (Exception e) {
                _logger.LogError(e);
                return new ContentResult {Content = $"error: {e.GetType()} {e.Message}"};
            }
        }

        private IActionResult DoGetResult(string url) {
            var response = GetResponse(url);
            return IsOk(response) ? HandleResponse(response) : HandleByCode(response);
        }

        private HttpWebResponse GetResponse(string url) {
            var request = _requester.GetRequest(url, _info);
            var response = GetResponse(request);
            return response;
        }

        private void Log() => _logger.Log(_info);

        private HttpWebResponse GetResponse(HttpWebRequest request) {
            var response = (HttpWebResponse) request.GetResponse();
            SetResponseInfo(response);
            return response;
        }

        private void SetResponseInfo(HttpWebResponse response) {
            _info.StatusCode = response.StatusCode.ToString();
        }
        
        private static bool IsOk(HttpWebResponse response) => response.StatusCode == HttpStatusCode.OK;

        private static IActionResult HandleByCode(HttpWebResponse response) =>
            response.StatusCode switch {
                HttpStatusCode.RedirectKeepVerb => new RedirectResult(response.Headers.Get("Location"), false),
                HttpStatusCode.TooManyRequests => new ContentResult {Content = "TooManyRequests"},
                _ => new ContentResult {Content = response.StatusCode.ToString()}
            };
        
        private IActionResult HandleResponse(HttpWebResponse response) {
            var handler = GetHandler(response);
            return handler.GetResult();
        }
        
        private IResponseHandler GetHandler(HttpWebResponse response) =>
            new ResponseHandlerBuilder(response, _info).Create();
    }
}