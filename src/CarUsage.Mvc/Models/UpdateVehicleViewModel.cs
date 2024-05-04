namespace CarUsage.Mvc.Models;

public class UpdateVehicleViewModel
{
    public Guid Id { get; set; }
    public decimal? ActiveUsageTime { get; set; }
    public decimal? RepairTime { get; set; }
}