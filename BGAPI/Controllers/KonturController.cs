using BGAPI.Entities;
using BGAPI.Workers;
using Microsoft.AspNetCore.Mvc;

namespace BGAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KonturController : Controller
    {

        [HttpGet]
        [Route("Download")]
        public IActionResult Download(string fileName) => GetResult(fileName);

        private static IActionResult GetResult(string fileName) {
            var downloader = new Downloader(new GetRequester());
            var getter = new FileGetterByName(downloader, fileName);
            var creator = new FileDownloadResultCreator(getter);
            var handler = new ResultHandler(creator);
            return handler.GetResult();
        }


    }
}