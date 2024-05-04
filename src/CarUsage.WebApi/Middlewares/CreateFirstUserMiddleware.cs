using CarUsage.Domain.Entities;
using CarUsage.Infastructure.Context;
using Microsoft.AspNetCore.Identity;

namespace CarUsage.WebApi.Middlewares;

public static class CreateFirstUserMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using var scoped = app.Services.CreateScope();
        var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        var context = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!userManager.Users.Any(x => x.UserName == "user"))
        {
            var appUser = new AppUser()
            {
                UserName = "user",
                Email = "user@user.com",
                EmailConfirmed = true,
                Name = "user",
                Lastname = "user",
            };
            userManager.CreateAsync(appUser, "1").Wait();
        }

        if (!roleManager.Roles.Any(x => x.Name == "user"))
        {
            var role = new AppRole()
            {
                Name = "user"
            };
            roleManager.CreateAsync(role).Wait();
        }

        var user = userManager.Users.FirstOrDefault(x => x.UserName == "user");
        var userRole = roleManager.Roles.FirstOrDefault(x => x.Name == "user");
        var checkUserRole = context.UserRoles.Any(x => x.UserId == user!.Id && x.RoleId == userRole!.Id);
        if (checkUserRole) return;
        var appUserRole = new AppUserRole
        {
            UserId = user!.Id,
            RoleId = userRole!.Id
        };
        context.UserRoles.Add(appUserRole);
        context.SaveChanges();
    }
}