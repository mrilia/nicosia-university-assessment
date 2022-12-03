using System.ComponentModel.DataAnnotations;

namespace Nicosia.Assessment.WebApi.Controllers.Authenticate.V1.Models;

public class AuthenticateRequest
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(4)]
    public string Password { get; set; } = null!;
}