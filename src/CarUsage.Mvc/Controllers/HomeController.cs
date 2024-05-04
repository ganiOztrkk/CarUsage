using System.Text.Json;
using System.Text.Json;
using CarUsage.Mvc.Attributes;
using CarUsage.Mvc.Models;
using CarUsage.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace CarUsage.Mvc.Controllers;

[Authenticate]
public class HomeController(VehiclesApiService vehiclesApiService) : Controller
{
    
    [RoleAuthorize("admin")]
    public async Task<IActionResult> Index()
    {
        var vehicles = await vehiclesApiService.GetAllVehicleAsync();
        var vehicleData = vehicles!.Data;
        var plates = new List<string>();
        var actimeTimes = new List<decimal>();
        var idleTimes = new List<decimal>();
        if (vehicleData is not null)
        {
            foreach (var item in vehicleData)
            {
                plates.Add(item.PlateId);
                actimeTimes.Add(item.ActiveTimePercentage ?? 0);
                idleTimes.Add(item.IdleTimePercentage ?? 0);
            }
            var chartValues = new ChartValues
            {
                VehiclePlate = plates,
                ActiveTimePercentages = actimeTimes,
                IdleTimePercentages = idleTimes
            };
            return View(chartValues);
        }

        return View();

    }
    
    [Microsoft.AspNetCore.Mvc.HttpGet]
    [RoleAuthorize("admin")]
    public IActionResult CreateVehicle()
    {
        return View();
    }
    
    [Microsoft.AspNetCore.Mvc.HttpPost]
    [RoleAuthorize("admin")]
    public async Task<IActionResult> CreateVehicle(CreateVehicleViewModel request)
    {
        var response = await vehiclesApiService.CreateVehicleAsync(request);
        if (!response!.Success)
        {
            ModelState.AddModelError("", response.Message!);
            return View();
        }
        return RedirectToAction("Index");
    }
    
    
    [Microsoft.AspNetCore.Mvc.HttpGet]
    [RoleAuthorize("user")]
    public async Task<IActionResult> VehicleTable()
    {
        var vehicles = await vehiclesApiService.GetAllVehicleAsync();
        return View(vehicles!.Data);
    }
    
    [Microsoft.AspNetCore.Mvc.HttpGet]
    [RoleAuthorize("user")]
    public IActionResult UpdateVehicle(Guid id)
    {
        return View(new UpdateVehicleViewModel(){Id = id});
    }
    
    [Microsoft.AspNetCore.Mvc.HttpPost]
    [RoleAuthorize("user")]
    public async Task<IActionResult> UpdateVehicle(UpdateVehicleViewModel request)
    {
        var response = await vehiclesApiService.UpdateVehicleAsync(request);
        if (!response!.Success)
        {
            ModelState.AddModelError("", response.Message!);
            return View();
        }
        return RedirectToAction("VehicleTable");
    }
}