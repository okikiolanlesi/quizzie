using Quizzie.Models;
using System;

namespace Quizzie.DTOs
{
    public class OptionDto
    {
        public Guid Id { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
