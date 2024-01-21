using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie.Validators
{
    public class CreateQuestionValidator: AbstractValidator<CreateQuestionDto>
    {
        public CreateQuestionValidator()
        {
            // Invokes set of rule for CreateQuestionDto
            RuleFor(x => x.QuestionText)
                .NotEmpty()
                .WithMessage("QuestionText must not be empty")
                .MaximumLength(300);
            // Create a rule for each item in the collection
            RuleForEach(x => x.Options)
                .SetValidator(new CreateOrUpdateOptionValidator());
        }
    }
}
