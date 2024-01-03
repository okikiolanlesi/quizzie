using System;
using FluentValidation;
using Quizzie.DTOs;

namespace Quizzie.Validators;

public class CategoryRequestValidator : AbstractValidator<CategoryDto>
{
    public CategoryRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Description).NotEmpty();
    }
}
