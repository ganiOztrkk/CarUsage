using System.IdentityModel.Tokens.Jwt;
using CarUsage.Mvc.Models;
using CarUsage.Mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarUsage.Mvc.Controllers;

[AllowAnonymous]
public class AuthController(AuthApiService authApiService) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var response = await authApiService.LoginAsync(request);
        if (response!.Data is null)
        {
            ModelState.AddModelError(string.Empty, response.Message);
            return View();
        }
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.Now.AddHours(1)
        };
        HttpContext.Response.Cookies.Append("AccessToken", response.Data, cookieOptions);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(response.Data) as JwtSecurityToken;
        if (jsonToken != null)
        {
            var userRole = jsonToken.Claims.FirstOrDefault(c => c.Type == "roles")?.Value;
            HttpContext.Response.Cookies.Append("Role", userRole ?? "", cookieOptions);
        }
        
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    public IActionResult Logout()
    {
        var cookieOptions = new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(-1)
        };
        HttpContext.Response.Cookies.Append("AccessToken", "", cookieOptions);
        HttpContext.Response.Cookies.Append("Role", "", cookieOptions);

        return RedirectToAction("Login");
    }
}