using System.Security.Claims;
using AutoMapper;
using CarUsage.Core.ResultPattern;
using CarUsage.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = CarUsage.Core.ResultPattern.IResult;

namespace CarUsage.Application.Features.Vehicle.UsageUpdate;

public class UsageUpdateCommandHandler(
    IVehicleRepository vehicleRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<UsageUpdateCommandRequest, IResult>
{
    
    public async Task<IResult> Handle(UsageUpdateCommandRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) return new ErrorResult("Access Token bulunamadı.");
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) return new ErrorResult( "Kullanıcı girişi yapın.");
        
        var role = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!role.Contains("user"))
        {
            return new ErrorResult("Sadece user güncelleme yapabilir.");
        }
        
        
        var usageRequestValidator = new UsageUpdateCommandValidator();
        var validationResult = await usageRequestValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }
        
        var vehicle = await vehicleRepository.GetAsync(request.Id);
        if (vehicle is null)
        {
            return new ErrorResult("Araç bulunamadı.");
        }

        if (request.ActiveUsageTime + request.RepairTime > 168)
        {
            return new ErrorResult("Kullanım saatlerini kontrol ediniz.");
        }

        var totalUsageTime = request.ActiveUsageTime + request.RepairTime;
        var idleTime = 168 - totalUsageTime;
        vehicle.ActiveUsageTime = request.ActiveUsageTime;
        vehicle.RepairTime = request.RepairTime;
        vehicle.IdleTime = idleTime;
        vehicle.ActiveTimePercentage =Math.Round((request.ActiveUsageTime / 168) * 100,2);
        vehicle.IdleTimePercentage = Math.Round((idleTime / 168) * 100,2);
        
        await vehicleRepository.UpdateAsync(vehicle);
        return new SuccessResult("Haftalık Saat durumları güncellendi.");
    }
}