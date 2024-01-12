using System;

namespace Quizzie.Models
{
    public class Option
    {
        public Guid Id { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsDeleted { get; set; } = false;
        //Navigation Properties
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
