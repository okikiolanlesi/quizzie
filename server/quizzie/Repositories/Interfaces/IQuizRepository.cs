using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quizzie.DTOs;
using Quizzie.Models;

namespace Quizzie.Repositories;

public interface IQuizRepository
{
    Task<List<Quiz>> GetAll();
    Task<Quiz> GetById(Guid id);
    Task<UserQuizDetailDto> GetByIdWithDetails(Guid id);
    Task<AdminQuizDetailDto> GetByIdWithDetailsAdmin(Guid id);
    void Add(Quiz quiz);
    Task<bool> SaveChangesAsync();
    void MarkAsModified(Quiz quiz);
}
