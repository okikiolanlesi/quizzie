using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quizzie.Models;

namespace Quizzie.Repositories;

public interface IOptionRepository
{
    Task<Option> GetById(Guid id);
    Task<Option> GetOptionForQuestionById(Guid optionId, Guid questionId);
    Task<List<Option>> GetAllForAQuestion(Guid questionId);

    void Add(Option Option);
    void AddRange(List<Option> Option);
    Task<bool> SaveChangesAsync();

    void MarkAsModified(Option Option);
}
