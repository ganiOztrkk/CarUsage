using CarUsage.Application.Features.Vehicle.Create;
using CarUsage.Application.Features.Vehicle.GetAll;
using CarUsage.Application.Features.Vehicle.UsageUpdate;
using CarUsage.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarUsage.WebApi.Controllers;

[AllowAnonymous]
public class VehiclesController(
    IMediator mediator): ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(UsageUpdateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllQueryRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }

}