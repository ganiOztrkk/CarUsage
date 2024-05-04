using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarUsage.Mvc.Attributes;

public class RoleAuthorizeAttribute : ActionFilterAttribute
{
    private readonly string _role;

    public RoleAuthorizeAttribute(string role)
    {
        _role = role;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userRole = context.HttpContext.Request.Cookies["Role"];
        if (userRole != _role)
        {
            if (_role == "admin")
            {
                context.Result = new RedirectToActionResult("VehicleTable", "Home", null);
            }
            else
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}