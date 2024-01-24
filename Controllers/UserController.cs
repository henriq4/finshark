using finshark.DTOs.User;
using finshark.Interfaces;
using finshark.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finshark.Controllers;

[Route("/api/account")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<User> _signInManager;

    public UserController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
    {
        this._userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO userDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userDTO.Username);

        if (user == null) return Unauthorized("Invalid username or password");

        var result = await _signInManager.CheckPasswordSignInAsync(user, userDTO.Password, false);

        if (!result.Succeeded) return Unauthorized("Invalid username or password");

        return Ok(new LoginResponseDTO
        {
            Email = user.Email,
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        });
    }
}