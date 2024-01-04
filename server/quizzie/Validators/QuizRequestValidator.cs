using System;
using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie;

public class QuizRequestValidator : AbstractValidator<QuizDto>
{
    public QuizRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Instructions).NotEmpty();
        RuleFor(x => x.Duration).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
    }

}
