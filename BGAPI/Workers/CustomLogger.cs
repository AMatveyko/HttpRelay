using System;
using System.Collections.Generic;
using System.Linq;
using BGAPI.Entities;
using BGAPI.Helpers;
using NLog;

namespace BGAPI.Workers
{
    internal sealed class CustomLogger
    {
        
        private const string ParametersTemplate =
            "|{0}|{1}|{2}|status: {3}\n \nrequest headers^\n{4}\n \nrequest data:\n{5}\n \nresponse headers:\n{6}\n \nresponse data:\n{7}";
        
        private static readonly Logger RequestLogger = LogManager.GetLogger("RequestsLog");
        private static readonly Logger ParametersLogger = LogManager.GetLogger("ParametersLog");
        private static readonly Logger ErrorLogger = LogManager.GetLogger("ErrorsLog");

        private readonly string _clientIp;
        

        public CustomLogger(string clientIp) => _clientIp = clientIp;

        public void Log(RequestInfo info) {
            LogRequest(info.Method, info.Url, info.StatusCode);
            LogParameters(info);
        }

        private void LogRequest(string method, string url, string statusCode) =>
            RequestLogger.Info(LogMessageHelper.GetLogMessage(_clientIp, method, url, statusCode));

        private void LogParameters(RequestInfo info) {
            var requestHeaders = GetFormatedHeaders(info.RequestHeaders);
            var responseHeaders = GetFormatedHeaders(info.ResponseHeaders);
            ParametersLogger.Info(
                string.Format(
                    ParametersTemplate,_clientIp,info.Method,info.Url,info.StatusCode,requestHeaders,info.RequestData,responseHeaders,info.RequestData));
        }
        
        private static string GetFormatedHeaders(Dictionary<string,string> headers) =>
            string.Join(",\n", headers.Select(h => $"{h.Key}: {h.Value}"));
        
        public void LogError(Exception e) =>
            ErrorLogger.Error(e, e.Message);

    }
}