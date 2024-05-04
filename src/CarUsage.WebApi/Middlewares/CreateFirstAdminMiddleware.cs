using CarUsage.Domain.Entities;
using CarUsage.Infastructure.Context;
using Microsoft.AspNetCore.Identity;

namespace CarUsage.WebApi.Middlewares;

public static class CreateFirstAdminMiddleware
{
    public static void CreateFirstAdmin(WebApplication app)
    {
        using var scoped = app.Services.CreateScope();
        var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        var context = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!userManager.Users.Any(x => x.UserName == "admin"))
        {
            var user = new AppUser()
            {
                UserName = "admin",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                Name = "admin",
                Lastname = "admin",
            };
            userManager.CreateAsync(user, "1").Wait();
        }

        if (!roleManager.Roles.Any())
        {
            var role = new AppRole()
            {
                Name = "admin"
            };
            roleManager.CreateAsync(role).Wait();
        }

        var admin = userManager.Users.FirstOrDefault(x => x.UserName == "admin");
        var adminRole = roleManager.Roles.FirstOrDefault(x => x.Name == "admin");
        var checkAdminUserRole = context.UserRoles.Any(x => x.UserId == admin!.Id && x.RoleId == adminRole!.Id);
        if (checkAdminUserRole) return;
        var appUserRole = new AppUserRole
        {
            UserId = admin!.Id,
            RoleId = adminRole!.Id
        };
        context.UserRoles.Add(appUserRole);
        context.SaveChanges();
    }
}