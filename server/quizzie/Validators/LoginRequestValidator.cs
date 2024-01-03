using System;
using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie.Validators;

public class LoginRequestValidator : AbstractValidator<LoginDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
