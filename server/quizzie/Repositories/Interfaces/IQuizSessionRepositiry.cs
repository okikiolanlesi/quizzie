using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quizzie.DTOs;
using Quizzie.Models;

namespace Quizzie.Repositories;

public interface IQuizSessionRepository
{
    Task<QuizSessionDto> GetById(Guid id);
    Task<QuizSession> GetOngoingQuizForUser(Guid userId, Guid quizId);
    Task<List<QuizSession>> GetEndedSessionsForAUser(Guid userId);
    Task<List<QuizSession>> GetAllForAUser(Guid userId);
    Task<int> CountCorrectAnswers(Guid quizSessionId);
    Task<int> CountTotalQuestions(Guid quizId);
    void Add(QuizSession quizSession);
    Task<bool> SaveChangesAsync();

    void MarkAsModified(QuizSession quizSession);

}
