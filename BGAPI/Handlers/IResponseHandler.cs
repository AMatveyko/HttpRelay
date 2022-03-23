using Microsoft.AspNetCore.Mvc;

namespace BGAPI.Handlers
{
    internal interface IResponseHandler
    {
        IActionResult GetResult();
    }
}