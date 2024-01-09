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
    [Route("disable/{id:Guid}")]
    public async Task<ActionResult> DisableQuiz(Guid id)
    {
        var quiz = await _quizRepository.GetById(id);

        if (quiz is null)
        {
            return NotFound(new { message = "Quiz does not exist" });
        }
        if (quiz.IsActive == false)
        {
            return BadRequest(new { message = "Quiz is already disabled" });
        }
        quiz.IsActive = false;
        _quizRepository.MarkAsModified(quiz);
        var result = await _quizRepository.SaveChangesAsync();

        if (!result) return Problem("Something went wrong with disabling the quiz");

        return Ok(new { message = "Quiz disabled successfully" });
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("enable/{id:Guid}")]
    public async Task<ActionResult> EnableQuiz(Guid id)
    {
        var quiz = await _quizRepository.GetById(id);

        if(quiz is null)
        {
            return NotFound(new { message = "Quiz does not exist" });
        }
        if (quiz.IsActive)
        {
            return Ok(new { message = "Quiz is already enabled " });
        }
        quiz.IsActive = true;
        _quizRepository.MarkAsModified(quiz);
        var result = await _quizRepository.SaveChangesAsync();

        if (!result) return Problem("Something went wrong with disabling the quiz");

        return Ok(new { message = "Quiz enabled successfully" });
    }

}
