using Application.Features.Cars.Commands.Create;
using Application.Features.Cars.Commands.Delete;
using Application.Features.Cars.Commands.Update;
using Application.Features.Cars.Queries.GetById;
using Application.Features.Cars.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCarCommand createCarCommand)
    {
        CreatedCarResponse response = await Mediator.Send(createCarCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCarCommand updateCarCommand)
    {
        UpdatedCarResponse response = await Mediator.Send(updateCarCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedCarResponse response = await Mediator.Send(new DeleteCarCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdCarResponse response = await Mediator.Send(new GetByIdCarQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCarQuery getListCarQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCarListItemDto> response = await Mediator.Send(getListCarQuery);
        return Ok(response);
    }
}