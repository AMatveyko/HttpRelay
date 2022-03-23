using Microsoft.AspNetCore.Mvc;

namespace BGAPI.Workers
{
    internal sealed class FileDownloadResultCreator : IResultCreator
    {

        private readonly IFileGetter _getter;

        public FileDownloadResultCreator(IFileGetter getter) => _getter = getter;

        public IActionResult GetResult() {
            var file = _getter.Get();
            return new FileContentResult(file.Data, file.ContentType) {FileDownloadName = file.Name};
        }
    }
}