using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quizzie.DTOs;
using Quizzie.Models;
using Quizzie.Repositories;
using Quizzie.Services;

namespace Quizzie.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public AuthController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration, IEmailService emailService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
        _emailService = emailService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
    {

        // Checking if password is equal to passwordConfirm
        var passwordMatch = registerDto.Password == registerDto.ConfirmPassword;

        if (!passwordMatch) return BadRequest(new
        {
            message = "Password mismatch"
        });

        // Checking if user doesn't already exists with that email
        var isAlreadyExists = await _userRepository.GetByEmail(registerDto.Email);

        if (isAlreadyExists is not null)
        {
            return BadRequest(new
            {
                message = "User already exists",
            });
        }

        var user = _mapper.Map<User>(registerDto);

        // Hashing the password before saving in the database
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        // Saving user
        _userRepository.Add(user);

        // Committing changes
        var result = await _userRepository.SaveChangesAsync();

        // Sending welcome mail to newly registered user
        try
        {
            await _emailService.SendHtmlEmailAsync(user.Email, "Welcome to Quizzie", "Welcome", new { Name = user.FirstName + " " + user.LastName });
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
            System.Console.WriteLine("Failed to send welcome email");
        }

        if (!result) return Problem(title: "Something went wrong");

        // Create new JWT token
        string token = CreateToken(user);

        return Ok(new
        {
            message = "User registered successfully",
            user = _mapper.Map<UserDto>(user),
            token
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {

        var user = await _userRepository.GetByEmail(loginDto.Email);

        if (user is null)
        {
            return BadRequest(new
            {
                message = "Invalid credentials"
            });
        }

        // Verifying if password provided matches the saved hashed password
        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return BadRequest(new
            {
                message = "Invalid credentials"
            });
        };

        // Creating JWT
        string token = CreateToken(user);

        var userdto = _mapper.Map<UserDto>(user);

        return Ok(new
        {
            message = "Login successfull"
            ,
            token,
            user = userdto
        });

    }

    private string CreateToken(User user)
    {

        // Declaring claims we would like to write to the JWT
        List<Claim> claims = new List<Claim>{
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())

        };

        // Creating a new SymmetricKey from Token we have saved in appSettings.development.json file
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        // Declaring signing credentials
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Creating new JWT object
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        // Write JWT to a string
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

}
