using System;
using System.ComponentModel.DataAnnotations;
using Nicosia.Assessment.Security.Authorization;

namespace Nicosia.Assessment.WebApi.Controllers.Authenticate.V1.Models;

public class UserDto
{
    public Guid Id { get; set; }

    [Required] 
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;
    
    [Required]
    public string Email { get; set; } 
    
    public string Phone { get; set; }
    
    public Role Role { get; set; }
}