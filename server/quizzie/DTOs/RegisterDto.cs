using System;
using System.ComponentModel.DataAnnotations;

namespace Quizzie.DTOs;

public class RegisterDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
}
