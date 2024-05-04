namespace CarUsage.Mvc.Models;

public class ChartValues
{
    public List<string>? VehiclePlate { get; set; }
    public List<decimal>? ActiveTimePercentages { get; set; }
    public List<decimal>? IdleTimePercentages { get; set; }
}