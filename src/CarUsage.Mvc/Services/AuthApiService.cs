using CarUsage.Mvc.Models;

namespace CarUsage.Mvc.Services;

public class AuthApiService(IHttpClientFactory httpClientFactory, 
    IHttpContextAccessor httpContextAccessor,
    HttpClient httpClient)
{
    public async Task<AuthApiResponse?> LoginAsync(LoginViewModel request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("https://localhost:7021/api/Auth/Login", request);
            
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<AuthApiResponse>();
                
                return responseBody;
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Error: " + errorResponse);
                return new AuthApiResponse { Success = false, Message = "API tarafından hata mesajı alındı." };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception in LoginApiService: " + ex.Message);
            return new AuthApiResponse { Success = false, Message = "Login işlemi sırasında bir hata oluştu: " + ex.Message };
        }
    }
    
    public CookieValues GetAccessTokenAndRole()
    {
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"] ?? "";
        var userRole = httpContextAccessor.HttpContext.Request.Cookies["Role"] ?? "";
        return new CookieValues() { AccessToken = accessToken, Role = userRole };
    }
    
    
}
public class AuthApiResponse
{
    public string? Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}
