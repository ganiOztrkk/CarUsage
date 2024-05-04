using CarUsage.Core.ResultPattern;
using MediatR;

namespace CarUsage.Application.Features.Vehicle.Create;

public sealed record CreateCommandRequest(
    string Name,
    string PlateId) : IRequest<IResult>;