using CarUsage.Core.GenericRepository;

namespace CarUsage.Domain.Entities;

public sealed class Vehicle : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string PlateId { get; set; } = string.Empty;
    public decimal? ActiveUsageTime { get; set; }
    public decimal? RepairTime { get; set; }
    public decimal? IdleTime { get; set; }
    
    public decimal? ActiveTimePercentage { get; set; }
    public decimal? IdleTimePercentage { get; set; }
    
    public bool Status { get; set; } = true;
}