using System;

namespace Quizzie.DTOs;

public class QuizDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Instructions { get; set; }
    public int Duration { get; set; }
    public Guid? CategoryId { get; set; }
}
