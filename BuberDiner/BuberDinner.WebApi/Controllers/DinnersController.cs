using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.WebApi.Controllers;

[Route("dinners")]
[Authorize]
public class DinnersController : ApiController
{
    [HttpGet]
    public IActionResult ListDinners()
    {
        return Empty;
    }
}