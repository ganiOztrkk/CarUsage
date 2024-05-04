using CarUsage.Core.ResultPattern;
using MediatR;

namespace CarUsage.Application.Features.Auth.Login;

public sealed record LoginCommandRequest(
    string EmailOrUserName,
    string Password) : IRequest<IDataResult<string>>;