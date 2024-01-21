using System;
using Quizzie.Models;

namespace quizzie;

public class AnswerQuestionDto
{
    public Guid QuestionId { get; set; }
    public Guid OptionId { get; set; }

}
