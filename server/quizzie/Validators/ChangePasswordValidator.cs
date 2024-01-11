using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            // Invokes set of rule for ChangePasswordDto
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("NewPassword cannot be empty");
            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage("OldPassword cannot be empty");
        }
    }
}
