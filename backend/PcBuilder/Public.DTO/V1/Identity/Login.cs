using System.ComponentModel.DataAnnotations;

namespace Public.DTO.V1.Identity;

public class Login
{
    [MaxLength(128)]
    public string Email { get; set; } = default!;
    
    [MaxLength(64)]
    public string Password { get; set; } = default!;
}