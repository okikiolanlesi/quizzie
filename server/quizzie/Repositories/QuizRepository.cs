using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Quizzie.Data;
using Quizzie.DTOs;
using Quizzie.Models;

namespace Quizzie.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly QuizzieDbContext _context;
    private readonly IMapper _mapper;

    public QuizRepository(QuizzieDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public void Add(Quiz quiz)
    {
        _context.Quizzes.Add(quiz);
    }

    public async Task<List<Quiz>> GetAll()
    {
        return await _context.Quizzes.ToListAsync();
    }

    public async Task<Quiz> GetById(Guid id)
    {
        return await _context.Quizzes.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<UserQuizDetailDto> GetByIdWithDetails(Guid id)
    {
        return await _context.Quizzes.Include(x => x.User).Include(x => x.Category).Include(x => x.Questions).ThenInclude(c => c.Options).ProjectTo<UserQuizDetailDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AdminQuizDetailDto> GetByIdWithDetailsAdmin(Guid id)
    {
        return await _context.Quizzes.Include(x => x.User).Include(x => x.Category).Include(x => x.Questions).ThenInclude(c => c.Options).ProjectTo<AdminQuizDetailDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void MarkAsModified(Quiz quiz)
    {
        _context.Entry(quiz).State = EntityState.Modified;
    }

}
