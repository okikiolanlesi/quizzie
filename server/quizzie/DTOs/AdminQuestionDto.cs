using System;
using System.Collections.Generic;

namespace Quizzie.DTOs;

public class AdminQuestionDto
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<AdminOptionDto> Options { get; set; }
}
