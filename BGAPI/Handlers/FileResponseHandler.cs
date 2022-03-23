using System.IO;
using System.Net;
using System.Net.Mime;
using BGAPI.Entities;
using BGAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BGAPI.Handlers
{
    internal sealed class FileResponseHandler : BaseResponseHandler, IResponseHandler
    {

        public FileResponseHandler(HttpWebResponse response, RequestInfo info) : base(response, info) { }
        
        public IActionResult GetResult() {

            SetInfo("File");
            
            var file = GetFile();
            var fileName = GetFileName();
            return new FileContentResult(file, Response.ContentType) {FileDownloadName = fileName};
        }

        private byte[] GetFile() {
            using var dataStream = Response.GetResponseStream();
            using var memoryStream = new MemoryStream();
            dataStream.CopyTo(memoryStream);
            var file = memoryStream.ToArray();
            dataStream.Close();
            memoryStream.Close();
            return file;
        }

        private string GetFileName() {
            var contentDispositionString = Response.GetResponseHeader("content-disposition");
            return new ContentDisposition(contentDispositionString).FileName;
        } 
    }
}