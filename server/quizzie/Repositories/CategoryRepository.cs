using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quizzie.Data;
using Quizzie.Models;

namespace Quizzie.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly QuizzieDbContext _context;

    public CategoryRepository(QuizzieDbContext context)
    {
        _context = context;
    }

    public void Add(Category category)
    {
        _context.QuizCategories.Add(category);
    }

    public async Task<Category> GetById(Guid id)
    {
        return await _context.QuizCategories.Include(x => x.Quizzes).FirstOrDefaultAsync(x => x.Id == id);

    }

    public async Task<Category> GetByTitle(string title)
    {
        return await _context.QuizCategories.FirstOrDefaultAsync(x => x.Title == title);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
