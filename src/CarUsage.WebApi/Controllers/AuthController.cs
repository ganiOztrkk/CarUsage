using CarUsage.Application.Features.Auth.Login;
using CarUsage.Infastructure.Context;
using CarUsage.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarUsage.WebApi.Controllers;

[AllowAnonymous]
public class AuthController(
    IMediator mediator,
    ApplicationDbContext context) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpGet]
    public IActionResult Test()
    {
        var user = context.Users.ToList();
        return Ok(user);
    }
}