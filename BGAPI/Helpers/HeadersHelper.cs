using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace BGAPI.Helpers
{
    internal static class HeadersHelper
    {

        private static readonly HashSet<string> Set = new() {
            "Content-Type",
            "Accept-Charset",
            "Authorization"
        };

        public static Dictionary<string, string> GetHeaders(WebHeaderCollection headers) =>
            Enumerable.Range(0, headers.Count).ToDictionary(i => headers.GetKey(i), i => string.Join(",",headers.GetValues(i) ?? Array.Empty<string>()));

        public static Dictionary<string, string> GetSuitableHeaders(IHeaderDictionary headers) =>
            headers.Where(IsSuitable).ToDictionary(h => h.Key, h => string.Join(",", h.Value));

        private static bool IsSuitable(KeyValuePair<string,StringValues> header) => Set.Contains(header.Key);
    }
}