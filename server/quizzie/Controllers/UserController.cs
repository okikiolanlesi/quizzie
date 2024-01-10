using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Tls;
using Quizzie.DTOs;
using Quizzie.Repositories;

namespace Quizzie.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserDto>> GetUserProfile(Guid id)
    {
        var user = await _userRepository.GetById(id);

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetLoggedInUserProfile()
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userRepository.GetById(Guid.Parse(userId));

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPut]
    [Authorize(Roles = "Admin, User")]
    [Route("{userId:Guid}")]
    public async Task<ActionResult> UpdateProfile(Guid userId, [FromBody] UserDto userDto)
    {
        var user = await _userRepository.GetById(userId);

        if(user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;

        _userRepository.MarkAsModified(user);
        var result = await _userRepository.SaveChangesAsync();
        if(result == false)
        {
            return Problem("Something went wrong while updating user name");
        }
        return Ok(new
        {
            message="Profile updated successfully",
            user
        });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,User")]
    [Route("change-password")]

    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userRepository.GetById(Guid.Parse(userId));

        if(user == null)
        {
            return NotFound(new { message = "User not found" });
        }
         if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, user.PasswordHash))
        {
            return BadRequest(new
            {
                message = "Invalid credentials"
            });
        };
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);

        user.PasswordHash = hashedPassword;
        _userRepository.MarkAsModified(user);
        var result = await _userRepository.SaveChangesAsync();
        if(result == false)
        {
            return Problem("Something went wrong while changing password");
        }
        return Ok(new
        {
            message = "Password successfully changed"
        });
    }
}
