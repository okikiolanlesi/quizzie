using System;
using System.Collections;
using System.Collections.Generic;

namespace Quizzie.Models
{
    public class QuizSession
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
        public Quiz Quiz { get; set; }
        public Guid QuizId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Answer> UserAnswers { get; set; }
    }
}
