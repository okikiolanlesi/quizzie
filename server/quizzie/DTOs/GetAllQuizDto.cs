using Quizzie.Models;
using System.Collections.Generic;
using System;

namespace Quizzie.DTOs
{
    public class GetAllQuizDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = false;
        public QuizCategoryDto Category { get; set; }
        public Guid CategoryId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }

    }
}
