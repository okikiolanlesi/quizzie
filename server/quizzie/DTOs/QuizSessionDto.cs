using System;
using System.Collections.Generic;
using Quizzie.Models;

namespace Quizzie.DTOs;

public class QuizSessionDto
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsCompleted { get; set; } = false;
    public int? TotalQuestions { get; set; }
    public double? Score { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    //Navigation properties
    public UserQuizDetailDto Quiz { get; set; }
    public Guid QuizId { get; set; }
    public UserDto User { get; set; }
    public Guid UserId { get; set; }
    public ICollection<QuizSessionAnswerDto> UserAnswers { get; set; }
}
