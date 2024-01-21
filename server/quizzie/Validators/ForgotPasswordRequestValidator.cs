using System;
using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie.Validators;

public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordDto>
{
    public ForgotPasswordRequestValidator()
    {
        // Invokes set of rule for RegisterDto
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid Email");

    }
}
