using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie.Validators
{
    public class UpdateUserValidator: AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator() 
        {
            // Invokes set of rule for UpdateUserDto
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName cannot be empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName cannot be empty");
        }
    }
}
