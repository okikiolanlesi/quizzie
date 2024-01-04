using System;
using System.Collections.Generic;

namespace Quizzie.DTOs;

public class QuestionDto
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    //Navigation Properties
    public ICollection<UserOptionDto> Options { get; set; }
}
