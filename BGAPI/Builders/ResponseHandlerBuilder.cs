using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BGAPI.Entities;
using BGAPI.Handlers;

namespace BGAPI.Builders
{
    internal sealed class ResponseHandlerBuilder : IResponseHandlerBuilder
    {

        private readonly List<(string, Func<IResponseHandler>)> Set;

        private readonly HttpWebResponse _response;
        private readonly RequestInfo _info;

        public ResponseHandlerBuilder(HttpWebResponse response, RequestInfo info) {
            (_response, _info) = (response, info);
            Set = new () {
                ("application/json", GetContentHandler),
                ("text/plain", GetContentHandler),
                ("application/octet-stream", GetFileHandler)
            };
        }

        public IResponseHandler Create() {
            var action =
                Set.FirstOrDefault(s => Contains(s.Item1)).Item2
                ?? ThrowException;
            return action();
        }

        private bool Contains(string value) => _response.ContentType.Contains(value);
        
        private IResponseHandler GetFileHandler() =>
            new FileResponseHandler(_response, _info);
        
        private IResponseHandler GetContentHandler() =>
            new ContentResponseHandler(_response, _info);
        
        private IResponseHandler ThrowException() =>
            throw new ArgumentOutOfRangeException($"not supported content type: {_response.ContentType}"); 
    }
}