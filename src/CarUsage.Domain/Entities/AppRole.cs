using CarUsage.Core.GenericRepository;
using Microsoft.AspNetCore.Identity;

namespace CarUsage.Domain.Entities;

public sealed class AppRole : IdentityRole<Guid>, IEntity
{
}