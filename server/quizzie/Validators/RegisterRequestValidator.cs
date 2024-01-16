using System;
using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterDto>
{
    public RegisterRequestValidator()
    {
        // Invokes set of rule for RegisterDto
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName must not be empty");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName must not be empty");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword cannot be empty");
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
