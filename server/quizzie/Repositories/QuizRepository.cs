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
using Quizzie.RequestHelpers;

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

    public async Task<PagedResponse<List<GetAllQuizDto>>> GetAll(QuizSearchParams searchParams)
    {
        var query = _context.Quizzes.AsQueryable();
        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query = query.Where(quiz => quiz.Title.Contains(searchParams.SearchTerm) || quiz.Description.Contains(searchParams.SearchTerm));
        }

        if (!string.IsNullOrEmpty(searchParams.Category.ToString()))
        {
            query = query.Where(quiz => quiz.CategoryId == searchParams.Category);

        }

        query = query.Skip((searchParams.PageNumber - 1) * searchParams.PageSize).Take(searchParams.PageSize);
        var result = await query.Include(x => x.Category).OrderByDescending(x => x.UpdatedAt).ProjectTo<GetAllQuizDto>(_mapper.ConfigurationProvider).ToListAsync();
        var payload = new PagedResponse<List<GetAllQuizDto>>
        {
            results = result,
            totalCount = query.Count(),
            page = searchParams.PageNumber,
            pageSize = searchParams.PageSize,
        };
        return payload;
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
