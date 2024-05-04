namespace CarUsage.Mvc.Models;

public class VehicleViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string PlateId { get; set; } = string.Empty;
    public decimal? ActiveUsageTime { get; set; }
    public decimal? RepairTime { get; set; }
    public decimal? IdleTime { get; set; }
    
    public decimal? ActiveTimePercentage { get; set; }
    public decimal? IdleTimePercentage { get; set; }
}