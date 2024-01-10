using System;

namespace Quizzie.DTOs;

public class AdminOptionDto
{
    public Guid Id { get; set; }
    public string OptionText { get; set; }
    public bool isCorrect { get; set; }
}
