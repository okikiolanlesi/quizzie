using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie.Validators
{
    public class CreateOrUpdateCategoryValidator : AbstractValidator<CreateOrUpdateCategoryDto>
    {
        public CreateOrUpdateCategoryValidator() 
        {
            // Invokes set of rule for CreateOrUpdateCategoryDto
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title cannot be empty");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty");
        }
    }
}
