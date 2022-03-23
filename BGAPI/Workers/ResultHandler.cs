using System;
using BGAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BGAPI.Workers
{
    internal sealed class ResultHandler
    {
        private readonly IResultCreator _creator;

        public ResultHandler(IResultCreator creator) => _creator = creator;

        public IActionResult GetResult() {
            try {
                return _creator.GetResult();
            }
            catch (Exception e) {
                return new ObjectResult(CreateErrorAnswer(e));
            }
        }

        private static ErrorAnswer CreateErrorAnswer(Exception e) => new() {
            IsError = true,
            ErrorMessage = e.Message,
            ErrorType = e.GetType().ToString()
        };

    }
}