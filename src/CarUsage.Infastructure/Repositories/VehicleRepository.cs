using CarUsage.Core.GenericRepository;
using CarUsage.Domain.Entities;
using CarUsage.Domain.Repositories;
using CarUsage.Infastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CarUsage.Infastructure.Repositories;

public class VehicleRepository(ApplicationDbContext context) : GenericRepository<Vehicle, ApplicationDbContext>(context), IVehicleRepository
{
    public async Task<Vehicle?> GetVehicleByNameAsync(string name)
    {
        return await context.Vehicles.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Vehicle?> GetVehicleByPlateIdAsync(string plateId)
    {
        return await context.Vehicles.FirstOrDefaultAsync(x => x.PlateId == plateId);
    }
}