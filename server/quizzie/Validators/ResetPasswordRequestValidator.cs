using System;
using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie.Validators;

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordRequestValidator()
    {
        // Invokes set of rule for RegisterDto
        RuleFor(x => x.Token).NotEmpty().WithMessage("Invalid token");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New Password must not be empty");

    }
}
