using Microsoft.AspNetCore.Mvc;

namespace BGAPI.Workers
{
    internal interface IResultCreator
    {
        IActionResult GetResult();
    }
}