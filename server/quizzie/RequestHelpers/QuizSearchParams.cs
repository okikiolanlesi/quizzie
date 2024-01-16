using System;

namespace Quizzie.RequestHelpers;
public class QuizSearchParams : SearchParams
{
    public Guid? Category { get; set; } = null;
}
