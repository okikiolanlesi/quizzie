using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quizzie.Models;

namespace Quizzie.Repositories;

public interface IQuizSessionRepository
{
    Task<QuizSession> GetById(Guid id);
    Task<QuizSession> GetOngoingQuizForUser(Guid userId, Guid quizId);
    Task<List<QuizSession>> GetAllForAUser(Guid userId);
    void Add(QuizSession quizSession);
    Task<bool> SaveChangesAsync();

    void MarkAsModified(QuizSession quizSession);

}
