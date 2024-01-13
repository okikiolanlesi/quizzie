using System;

namespace Quizzie.Models;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; } = Role.User;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string ResetToken { get; set; }
    public DateTime? ResetTokenExpiration { get; set; }
    public bool EmailConfirmed { get; set; } = false;
    public string EmailVerificationToken { get; set; }
    public DateTime? EmailVerificationTokenExpiration { get; set; }
}