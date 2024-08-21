using Code.Kata._9.Api.Responses;
using Code.Kata._9.AppServices.Commands.GetSalesItems;
using Code.Kata._9.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Code.Kata._9.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesItemController(IMediator mediator, ILogger<SalesItemController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ILogger<SalesItemController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    [HttpGet]
    [ProducesResponseType<PaginatedResult<SalesItem>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSalesItems([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = await _mediator.Send(new GetSalesItemsCommand(pageSize, page));
        return Ok(result);
    }
}