using System;

namespace Quizzie.RequestHelpers;

public class QuizSessionSearchParams : SearchParams
{
    public QuizSessionStatus? Status { get; set; } = null;
}

public enum QuizSessionStatus
{
    ongoing, completed
}
