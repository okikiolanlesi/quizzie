using System;
using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie;

public class QuizRequestValidator : AbstractValidator<QuizDto>
{
    public QuizRequestValidator()
    {
        // Invokes set of rule for QuizDto
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty");
        RuleFor(x => x.Instructions).NotEmpty().WithMessage("Instructions must not be empty");
        RuleFor(x => x.Duration).NotEmpty().WithMessage("Quiz must have duration");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description must not be empty");
        RuleFor(x => x.CategoryId).NotEmpty();
    }

}
