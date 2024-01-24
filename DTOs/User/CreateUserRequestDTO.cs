using System.ComponentModel.DataAnnotations;

namespace finshark.DTOs.User;

public class CreateUserRequestDTO
{
    [Required]
    public string? Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }
}