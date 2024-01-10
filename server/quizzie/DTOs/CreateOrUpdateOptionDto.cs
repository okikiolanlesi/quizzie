using System;

namespace Quizzie.DTOs;

public class CreateOrUpdateOptionDto
{
    public string OptionText { get; set; }
    public bool isCorrect { get; set; }
}
