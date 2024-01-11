using System;
using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie.Validators;

public class CategoryRequestValidator : AbstractValidator<CreateOrUpdateCategoryDto>
{
    // Invokes set of rule for CreateorUpdateCategoryDto
    public CategoryRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title must not be empty")
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description must not be empty");
    }
}
