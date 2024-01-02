using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzie.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt {  get; set; } = DateTime.UtcNow;
        //Navigation Properties
        public ICollection<Option> Options { get; set; }
        public Quiz Quiz { get; set; }
        public Guid QuizId { get; set; }
    }
}
