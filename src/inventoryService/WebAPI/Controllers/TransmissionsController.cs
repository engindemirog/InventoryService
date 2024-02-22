using Application.Features.Transmissions.Commands.Create;
using Application.Features.Transmissions.Commands.Delete;
using Application.Features.Transmissions.Commands.Update;
using Application.Features.Transmissions.Queries.GetById;
using Application.Features.Transmissions.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransmissionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTransmissionCommand createTransmissionCommand)
    {
        CreatedTransmissionResponse response = await Mediator.Send(createTransmissionCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTransmissionCommand updateTransmissionCommand)
    {
        UpdatedTransmissionResponse response = await Mediator.Send(updateTransmissionCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedTransmissionResponse response = await Mediator.Send(new DeleteTransmissionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdTransmissionResponse response = await Mediator.Send(new GetByIdTransmissionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTransmissionQuery getListTransmissionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTransmissionListItemDto> response = await Mediator.Send(getListTransmissionQuery);
        return Ok(response);
    }
}