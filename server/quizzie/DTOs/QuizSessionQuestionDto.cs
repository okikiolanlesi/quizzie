using System;

namespace Quizzie.DTOs;

public class QuizSessionQuestionDto
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
