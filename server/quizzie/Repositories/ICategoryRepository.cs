using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quizzie.DTOs;
using Quizzie.Models;

namespace Quizzie.Repositories;

public interface ICategoryRepository
{
    Task<Category> GetByTitle(string title);
    Task<CategoryDto> GetById(Guid id);
    Task<List<Category>> GetAll();
    Task<Category> DeleteById(Guid id);
    void Add(Category category);
    Task<bool> SaveChangesAsync();

    void MarkAsModified(Category category);

}
