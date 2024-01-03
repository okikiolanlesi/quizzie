using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzie.DTOs;

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
}
