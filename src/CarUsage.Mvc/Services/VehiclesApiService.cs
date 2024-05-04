using System.Text;
using CarUsage.Mvc.Models;

namespace CarUsage.Mvc.Services;

public class VehiclesApiService(
    IHttpClientFactory httpClientFactory, 
    IHttpContextAccessor httpContextAccessor,
    HttpClient httpClient)
{
    
    public async Task<CreateVehicleApiResponse?> CreateVehicleAsync(CreateVehicleViewModel request)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
        {
            return new CreateVehicleApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        }
        
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7021/api/Vehicles/Create")
            {
                Content = JsonContent.Create(request),
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };

            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<CreateVehicleApiResponse>();
                return responseBody;
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new CreateVehicleApiResponse { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new CreateVehicleApiResponse { Success = false, Message = "Exception in CreateVehicleAsync: " + ex.Message };
        }
    }
    
    public class CreateVehicleApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }


    public async Task<GetAllVehicleApiResponse?> GetAllVehicleAsync()
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
        {
            return new GetAllVehicleApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        }
        
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7021/api/Vehicles/GetAll")
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json"),
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };

            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<GetAllVehicleApiResponse>();
                return responseBody;
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new GetAllVehicleApiResponse { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new GetAllVehicleApiResponse { Success = false, Message = "Exception in CreateVehicleAsync: " + ex.Message };
        }
        
    }
    
    public class GetAllVehicleApiResponse
    {
        public IList<VehicleViewModel>? Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
    
    public async Task<UpdateVehicleApiResponse?> UpdateVehicleAsync(UpdateVehicleViewModel request)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
        {
            return new UpdateVehicleApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        }
        
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7021/api/Vehicles/Update")
            {
                Content = JsonContent.Create(request),
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };

            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<UpdateVehicleApiResponse>();
                return responseBody;
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new UpdateVehicleApiResponse { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new UpdateVehicleApiResponse { Success = false, Message = "Exception in CreateVehicleAsync: " + ex.Message };
        }
        
    }
    
    public class UpdateVehicleApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
    
}