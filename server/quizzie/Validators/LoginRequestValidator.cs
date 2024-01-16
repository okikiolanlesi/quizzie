using System;
using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie.Validators;

public class LoginRequestValidator : AbstractValidator<LoginDto>
{
    public LoginRequestValidator()
    {
        // Invokes set of rule for LoginDto
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
