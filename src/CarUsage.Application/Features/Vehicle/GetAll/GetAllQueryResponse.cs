namespace CarUsage.Application.Features.Vehicle.GetAll;

public sealed record GetAllQueryResponse(
    Guid Id,
    string Name,
    string PlateId,
    decimal? ActiveUsageTime,
    decimal? RepairTime,
    decimal? IdleTime,
    decimal? ActiveTimePercentage,
    decimal? IdleTimePercentage
    );