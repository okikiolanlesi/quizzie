using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Quizzie.Data;
using Quizzie.DTOs;
using Quizzie.Models;

namespace Quizzie.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly QuizzieDbContext _context;
    private readonly IMapper _mapper;

    public QuestionRepository(QuizzieDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public void Add(Question question)
    {
        _context.Questions.Add(question);
    }

    public Task<List<Question>> GetAllForAQuiz(Guid quizId)
    {
        throw new NotImplementedException();
    }

    public async Task<QuestionDto> GetById(Guid id)
    {
        return await _context.Questions.ProjectTo<QuestionDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
    }

    public void MarkAsModified(Question question)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public Task<Question> GetQuestionForQuizById(Guid questionId, Guid quizId)
    {
        return _context.Questions.FirstOrDefaultAsync(x => x.QuizId == quizId && x.Id == questionId);
    }
}
