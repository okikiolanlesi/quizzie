using FluentValidation;
using FluentValidation.Validators;
using Quizzie.DTOs;

namespace Quizzie.Validators
{
    public class CreateOrUpdateOptionValidator: AbstractValidator<CreateOrUpdateOptionDto>
    {
        public CreateOrUpdateOptionValidator()
        {
            // Invokes set of rule for CreateorUpdateOptionDto
            RuleFor(x => x.OptionText)
                .NotEmpty()
                .WithMessage("OptionText must not be empty");
           
        }
    }
}
