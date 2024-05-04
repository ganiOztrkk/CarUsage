using System.Security.Claims;
using AutoMapper;
using CarUsage.Core.ResultPattern;
using CarUsage.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = CarUsage.Core.ResultPattern.IResult;

namespace CarUsage.Application.Features.Vehicle.Create;

public class CreateCommandHandler(
    IVehicleRepository vehicleRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateCommandRequest, IResult>
{

    public async Task<IResult> Handle(CreateCommandRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) return new ErrorResult("Access Token bulunamadı.");
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) return new ErrorResult( "Kullanıcı girişi yapın.");
        
        var role = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!role.Contains("admin"))
        {
            return new ErrorResult("Sadece admin araç kaydı yapabilir.");
        }
        
        
        var createVehicleValidator = new CreateCommandValidator();
        var validationResult = await createVehicleValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }
        
        
        if (await vehicleRepository.GetVehicleByNameAsync(request.Name) is not null)
        {
            return new ErrorResult("Araç adı zaten kayıtlı.");
        }
        
        if (await vehicleRepository.GetVehicleByPlateIdAsync(request.PlateId) is not null)
        {
            return new ErrorResult("Plaka zaten kayıtlı.");
        }
        
        var vehicle = mapper.Map<Domain.Entities.Vehicle>(request);
        await vehicleRepository.InsertAsync(vehicle);
        return new SuccessResult("Araç kayıt edildi.");
    }
}