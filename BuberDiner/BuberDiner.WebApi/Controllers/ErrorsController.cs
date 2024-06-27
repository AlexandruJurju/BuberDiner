﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDiner.WebApi.Controllers;

public class ErrorsController : ControllerBase
{
    [HttpGet("error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error;

        return Problem(title: exception.Message);
    }
}