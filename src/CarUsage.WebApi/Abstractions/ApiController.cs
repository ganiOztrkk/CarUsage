using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarUsage.WebApi.Abstractions;

[Route("api/[controller]/[action]")]
[ApiController]
public abstract class ApiController : ControllerBase
{
}