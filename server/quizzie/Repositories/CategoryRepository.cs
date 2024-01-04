using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Quizzie.Data;
using Quizzie.DTOs;
using Quizzie.Models;

namespace Quizzie.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly QuizzieDbContext _context;
    private readonly IMapper _mapper;

    public CategoryRepository(QuizzieDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Add(Category category)
    {
        _context.QuizCategories.Add(category);
    }

    public async Task<CategoryDto> GetById(Guid id)
    {
        return await _context.QuizCategories.Include(x => x.Quizzes).ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
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
}
