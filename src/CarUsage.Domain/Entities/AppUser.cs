using CarUsage.Core.GenericRepository;using Microsoft.AspNetCore.Identity;

namespace CarUsage.Domain.Entities;

public sealed class AppUser : IdentityUser<Guid>, IEntity
{
    public string Name { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
}