using System;

namespace Quizzie.DTOs;

public class QuizSessionAnswerDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    //Navigation properties
    public QuizSessionOptionDto Option { get; set; }
    public Guid OptionId { get; set; }
    public QuizSessionQuestionDto Question { get; set; }
    public Guid QuestionId { get; set; }

}
