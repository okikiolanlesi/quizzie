using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzie.DTOs;
using Quizzie.Models;
using Quizzie.Repositories;

namespace Quizzie.Controllers;

[ApiController]
[Route("api/quiz")]
public class QuizController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IQuizRepository _quizRepository;
    private readonly IUserRepository _userRepository;

    public QuizController(IMapper mapper, IQuizRepository quizRepository, IUserRepository userRepository)
    {
        _mapper = mapper;
        _quizRepository = quizRepository;
        _userRepository = userRepository;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateQuiz(QuizDto quizDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userRepository.GetById(Guid.Parse(userId));
        var quiz = _mapper.Map<Quiz>(quizDto);
        Console.WriteLine(user);

        quiz.User = user;
        _quizRepository.Add(quiz);

        var result = await _quizRepository.SaveChangesAsync();
        if (!result) return Problem("Something went wrong");

        var newQuiz = await _quizRepository.GetByIdWithDetailsAdmin(quiz.Id);

        return CreatedAtAction(nameof(GetAQuizAdmin),
                    new { quiz.Id }, newQuiz);
    }


    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult> GetAllQuizzes()
    {
        return Ok(new
        {
            results = await _quizRepository.GetAll()
        });

    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    [Route("{id:Guid}")]
    public async Task<ActionResult> GetAQuiz(Guid id)
    {
        var quiz = await _quizRepository.GetByIdWithDetails(id);

        if (quiz is null)
        {
            return NotFound(new { message = "Quiz does not exist" });
        }
        return Ok(quiz);

    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("admin/{id:Guid}")]
    public async Task<ActionResult> GetAQuizAdmin(Guid id)
    {
        var quiz = await _quizRepository.GetByIdWithDetailsAdmin(id);

        if (quiz is null)
        {
            return NotFound(new { message = "Quiz does not exist" });
        }
        return Ok(quiz);

    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("{id:Guid}")]
    public async Task<ActionResult> UpdateQuiz(Guid id, QuizDto quizDto)
    {
        var quiz = await _quizRepository.GetById(id);
        if (quiz == null)
        {
            return NotFound();
        }

        quiz.Title = quizDto.Title;
        quiz.Description = quizDto.Description;
        quiz.Instructions = quizDto.Instructions;
        quiz.Duration = quizDto.Duration;
        quiz.CategoryId = quizDto?.CategoryId;

        _quizRepository.MarkAsModified(quiz);

        await _quizRepository.SaveChangesAsync();

        return Ok(quiz);

    }

}
