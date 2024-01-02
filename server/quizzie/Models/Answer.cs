using System;

namespace Quizzie.Models
{
    public class Answer
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsCorrect { get; set; }
        //Navigation properties
        public Option Option { get; set; }
        public Guid OptionId { get; set; }
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
        public Guid QuizSessionId { get; set; }
        public QuizSession QuizSession { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
