using System.ComponentModel.DataAnnotations;

namespace finshark.DTOs.User;

public class LoginRequestDTO
{
   [Required] 
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}