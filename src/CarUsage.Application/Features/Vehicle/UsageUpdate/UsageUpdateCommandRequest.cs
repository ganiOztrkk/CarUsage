using CarUsage.Core.ResultPattern;
using MediatR;

namespace CarUsage.Application.Features.Vehicle.UsageUpdate;

public sealed record UsageUpdateCommandRequest(
    Guid Id,
    decimal ActiveUsageTime,
    decimal RepairTime
    ): IRequest<IResult>;