using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarUsage.Mvc.Attributes;

public class AuthenticateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var hasAccessToken = context.HttpContext.Request.Cookies.ContainsKey("AccessToken");
        if (!hasAccessToken)
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
        }
    }
}