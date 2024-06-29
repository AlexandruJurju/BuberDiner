using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Contracts.Menus;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.WebApi.Controllers;

[Route("menus")]
public class MenusController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public MenusController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("hosts/{hostId}/menus")]
    [ProducesResponseType(200, Type = typeof(MenuResponse))]
    public async Task<IActionResult> CreateMenu(CreateMenuRequest request, Guid hostId)
    {
        var command = _mapper.Map<CreateMenuCommand>((request, hostId));

        var createMenuResult = await _mediator.Send(command);

        return createMenuResult.Match(
            menu => Ok(_mapper.Map<MenuResponse>(menu)),
            errors => Problem(errors));
    }
}