using MimeKit.Encodings;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Quizzie.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Quiz> Quizzes { get; set; }

    }
}
