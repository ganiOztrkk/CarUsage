using System.ComponentModel.DataAnnotations;

namespace CarUsage.Mvc.Models;

public class CreateVehicleViewModel
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MinLength(6)]
    public string PlateId { get; set; } = string.Empty;
}