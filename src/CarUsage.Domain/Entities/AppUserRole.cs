using CarUsage.Core.GenericRepository;
using Microsoft.AspNetCore.Identity;

namespace CarUsage.Domain.Entities;

public class AppUserRole : IdentityUserRole<Guid>, IEntity
{
}