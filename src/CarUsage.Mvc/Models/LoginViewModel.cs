using System.ComponentModel.DataAnnotations;

namespace CarUsage.Mvc.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez")]
    [MinLength(4)]
    public string EmailOrUsername { get; set; } = string.Empty;
    [Required(ErrorMessage = "Şifre Boş Geçilemez")]
    [MinLength(1)]
    public string Password { get; set; } = string.Empty;
}