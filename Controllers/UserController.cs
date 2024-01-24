using finshark.DTOs.User;
using finshark.Interfaces;
using finshark.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finshark.Controllers;

[Route("/api/account")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public UserController(UserManager<User> userManager, ITokenService tokenService)
    {
        this._userManager = userManager;
        _tokenService = tokenService;
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

            return Ok(new CreateUserResponseDTO
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }     
    }
}