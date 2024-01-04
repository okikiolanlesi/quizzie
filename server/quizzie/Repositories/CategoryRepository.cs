using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quizzie.Data;
using Quizzie.Models;

namespace quizzie;

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
    public async Task<List<Category>> GetAll()
    {
        return await _context.QuizCategories.ToListAsync();
    }

    public async Task<Category> GetByTitle(string title)
    {
        return await _context.QuizCategories.FirstOrDefaultAsync(x => x.Title == title);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Category> UpdateCategory(Guid id, Category category)
    {
        var existingCategory= await _context.QuizCategories.FirstOrDefaultAsync(x=>x.Id == id);
        if (existingCategory == null)
        {
            return null;
        }
        existingCategory.Title = category.Title;
        existingCategory.Description = category.Description;
        await _context.SaveChangesAsync();
        return existingCategory;
    }

    public async Task<Category> DeleteById(Guid id)
    {
       var existingRegion =  await _context.QuizCategories.FirstOrDefaultAsync(x => x.Id == id);
        if(existingRegion == null)
        {
            return null;
        }
        _context.QuizCategories.Remove(existingRegion);
        await _context.SaveChangesAsync();
        return existingRegion;
    }
}
