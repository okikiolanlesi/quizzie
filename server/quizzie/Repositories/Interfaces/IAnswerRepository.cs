using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quizzie.Models;

namespace Quizzie.Repositories;

public interface IAnswerRepository
{
    Task<List<Answer>> GetAllForAQuizSession(Guid quizSessionId);
    Task<Answer> GetAnswerForQuestion(Guid quizSessionId, Guid questionId);

    void Add(Answer answer);
    Task<bool> SaveChangesAsync();

    void MarkAsModified(Answer answer);

    // IDbContextTransaction BeginTransaction();
}
