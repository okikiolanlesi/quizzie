using System;
using System.Collections;
using System.Collections.Generic;

namespace Quizzie.Models;

public class Quiz
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Instructions { get; set; }
    public int Duration { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    //Navigation properties (relationships)
    public Category Category { get; set; }
    public Guid CategoryId { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public ICollection<Question> Questions { get; set; }
}
