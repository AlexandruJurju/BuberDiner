using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.WebApi.Controllers;

public class ErrorsController : ControllerBase
{
    [HttpGet("error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error;

        return Problem(title: exception.Message);
    }
}