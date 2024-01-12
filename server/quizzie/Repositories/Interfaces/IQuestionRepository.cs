using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Quizzie.Models;

namespace Quizzie.Repositories;

public interface IQuestionRepository
{
    Task<Question> GetById(Guid id);
    Task<Question> GetQuestionForQuizById(Guid questionId, Guid quizId);
    Task<List<Question>> GetAllForAQuiz(Guid quizId);

    void Add(Question question);
    Task<bool> SaveChangesAsync();

    void MarkAsModified(Question question);

    IDbContextTransaction BeginTransaction();
}
