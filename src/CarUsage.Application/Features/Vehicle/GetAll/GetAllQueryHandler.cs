using System.Security.Claims;
using AutoMapper;
using CarUsage.Core.ResultPattern;
using CarUsage.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarUsage.Application.Features.Vehicle.GetAll;

public class GetAllQueryHandler(
    IVehicleRepository vehicleRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
    ): IRequestHandler<GetAllQueryRequest, IDataResult<List<GetAllQueryResponse>>>
{
    public async Task<IDataResult<List<GetAllQueryResponse>>> Handle(GetAllQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) return new ErrorDataResult<List<GetAllQueryResponse>>("Access Token bulunamadı.");
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) return new ErrorDataResult<List<GetAllQueryResponse>>( "Kullanıcı girişi yapın.");
        
        var role = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        
        
        var vehicles = await vehicleRepository.GetAllAsync();

        if (!vehicles.Any())
        {
            return new ErrorDataResult<List<GetAllQueryResponse>>("Araç bulunamadı.");
        }
        
        var response = mapper.Map<List<GetAllQueryResponse>>(vehicles);

        return new SuccessDataResult<List<GetAllQueryResponse>>(response);
    }
}