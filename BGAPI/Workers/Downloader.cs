using System.IO;
using System.Net;
using System.Net.Mime;
using BGAPI.Entities;

namespace BGAPI.Workers
{
    internal sealed class Downloader : IDownloader
    {

        private readonly IRequester _requester;

        public Downloader(IRequester requester) => _requester = requester;
        
        public FileData Download(string url) => DoDownload(url);

        private FileData DoDownload(string url) {

            var response = (HttpWebResponse)_requester.GetRequest(url, new RequestInfo()).GetResponse();
            var file = ReadFile(response);
            var fileName = GetFileName(response);

            return new FileData(file, fileName, response.ContentType);
        }

                
        private static string GetFileName(HttpWebResponse response) {
            var contentDispositionString = response.GetResponseHeader("content-disposition");
            return new ContentDisposition(contentDispositionString).FileName;
        }
        
        private static byte[] ReadFile(WebResponse response) {
            var dataStream = response.GetResponseStream(); 
            var memoryStream = new MemoryStream();
            dataStream.CopyTo(memoryStream);
            var file = memoryStream.ToArray();
            dataStream.Close();
            memoryStream.Close();
            return file;
        }

    }
}