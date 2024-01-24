using finshark.DTOs.User;
using finshark.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finshark.Controllers;

[Route("/api/account")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager)
    {
        this._userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] CreateUserRequestDTO userDTO)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User
            {
                UserName = userDTO.Username,
                Email = userDTO.Email
            };

            var createdUser = await _userManager.CreateAsync(user, userDTO.Password);

            if (!createdUser.Succeeded) return BadRequest(createdUser.Errors);
            
            var roleUser = await _userManager.AddToRoleAsync(user, "User");

            if (!roleUser.Succeeded) return BadRequest(roleUser.Errors);

            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }     
    }
}