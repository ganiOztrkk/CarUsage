using CarUsage.Core.GenericRepository;
using CarUsage.Domain.Entities;

namespace CarUsage.Domain.Repositories;

public interface IVehicleRepository : IGenericRepository<Vehicle>
{
    public Task<Vehicle?> GetVehicleByNameAsync(string name);
    public Task<Vehicle?> GetVehicleByPlateIdAsync(string plateId);
}