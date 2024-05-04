using CarUsage.Core.ResultPattern;
using CarUsage.Domain.Entities;

namespace CarUsage.Domain.Services;

public interface IJwtProvider
{
    Task<IDataResult<string>> CreateTokenAsync(AppUser user, List<string> roles);
}