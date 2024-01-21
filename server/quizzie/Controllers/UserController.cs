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

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="userId">The ID of the user to update.</param>
    /// <param name="userDto">The updated userDto object.</param>
    /// <returns>
    /// <response code="200"> Profile updated successfully.</response>
    /// <response code="404">Not Found: User not found.</response>
    /// </returns>
    [HttpPut]
    [Authorize(Roles = "Admin, User")]
    [Route("{userId:Guid}")]
    public async Task<ActionResult> UpdateProfile(Guid userId, [FromBody] UpdateUserDto userDto)
    {
        // Retrieve user id from the repository
        var user = await _userRepository.GetById(userId);

        // Check if the user exists
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        // Updates user details
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;

        // Mark as modified in the respository

        _userRepository.MarkAsModified(user);
        // Save changes to the repository
        var result = await _userRepository.SaveChangesAsync();

        // Check if changes were saved successfully
        if (result == false)
        {
            return Problem("Something went wrong while updating user name");
        }
        return Ok(new
        {
            message = "Profile updated successfully",
            user = _mapper.Map<UserDto>(user)
        });
    }

    /// <summary>
    /// Change the password for the currently authenticated user.
    /// </summary>
    /// <remarks>
    /// This endpoint allows the currently authenticated user (identified by their JWT token) to change their password.
    /// </remarks>
    /// <param name="changePasswordDto">Dto containing old and new password information.</param>
    /// <returns>
    /// <response code="200">OK: Password successfully changed.</response>
    /// <response code="400">Bad Request: Invalid credentials.</response>
    /// <response code="404">Not Found: The user is not found.</response>
    /// </returns>
    [HttpPut]
    [Authorize(Roles = "Admin,User")]
    [Route("change-password")]

    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        // // Retrieve user ID from the JWT token
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Retrieve the user from the repository
        var user = await _userRepository.GetById(Guid.Parse(userId));

        // Check if the user exists
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        // Verify the old password
        if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, user.PasswordHash))
        {
            return BadRequest(new
            {
                message = "Invalid credentials"
            });
        };

        // Hash the new password
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);

        // Update the user's password
        user.PasswordHash = hashedPassword;

        // Mark user as modified in the repository
        _userRepository.MarkAsModified(user);

        // Save changes to the repository
        var result = await _userRepository.SaveChangesAsync();

        // Check if changes were saved successfully
        if (result == false)
        {
            return Problem("Something went wrong while changing password");
        }
        return Ok(new
        {
            message = "Password successfully changed"
        });
    }
}
