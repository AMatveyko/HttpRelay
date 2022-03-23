namespace BGAPI.Helpers
{
    internal static class LogMessageHelper
    {
        private static readonly string Template = "{0}|{1}|{2}|{3}";

        public static string GetLogMessage(string ip, string method, string url, string statusCode) =>
            string.Format(Template, ip, method, url, statusCode);
    }
}