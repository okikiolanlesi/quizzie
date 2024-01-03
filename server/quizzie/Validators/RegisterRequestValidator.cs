using System;
using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterDto>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.ConfirmPassword).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
