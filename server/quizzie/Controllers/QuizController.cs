using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizzie.DTOs;
using Quizzie.Models;
using Quizzie.Repositories;
using Quizzie.RequestHelpers;

namespace Quizzie.Controllers;

[ApiController]
[Route("api/quiz")]
public class QuizController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IQuizRepository _quizRepository;
    private readonly IUserRepository _userRepository;
    private readonly IQuizSessionRepository _quizSessionRepository;

    public QuizController(IMapper mapper, IQuizRepository quizRepository, IUserRepository userRepository, IQuizSessionRepository quizSessionRepository)
    {
        _mapper = mapper;
        _quizRepository = quizRepository;
        _userRepository = userRepository;
        _quizSessionRepository = quizSessionRepository;
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
    public async Task<ActionResult> GetAllQuizzes([FromQuery] QuizSearchParams searchParams)
    {
        var results = await _quizRepository.GetAllForUsers(searchParams);
        return Ok(results);

    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("admin")]
    public async Task<ActionResult> GetAllQuizzesAdmin([FromQuery] QuizSearchParams searchParams)
    {
        var results = await _quizRepository.GetAll(searchParams);
        return Ok(results);

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

    /// <summary>
    /// Disables a Quiz.
    /// </summary>
    /// <param name="id">The unique Quiz ID to disable.</param>
    /// <returns>
    /// <response code="200">Quiz successfully disabled.</response>
    /// <response code="400"> Bad Request: The quiz is already disabled.</response>
    /// <response code="404">Not Found: The specified quiz does not exist.</response>
    /// </returns>
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

    /// <summary>
    /// Enables a Quiz.
    /// </summary>
    /// <param name="id">The unique Quiz ID to enable.</param>
    /// <returns>
    /// <response code="200">The quiz is already enabled.</response>
    /// <response code="404">Not Found: The specified quiz does not exist.</response>
    /// <response code="200">OK: Quiz successfully enabled.</response>
    /// </returns>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("enable/{id:Guid}")]
    public async Task<ActionResult> EnableQuiz(Guid id)
    {
        // Retrieve the quiz by its unique identifier
        var quiz = await _quizRepository.GetById(id);

        // Check if the quiz exists
        if (quiz is null)
        {
            return NotFound(new { message = "Quiz does not exist" });
        }

        // Check if the quiz is already enabled
        if (quiz.IsActive)
        {
            return Ok(new { message = "Quiz is already enabled " });
        }

        // Enable the quiz
        quiz.IsActive = true;

        // Mark the quiz as modified in the repository
        _quizRepository.MarkAsModified(quiz);

        // Save changes to the repository
        var result = await _quizRepository.SaveChangesAsync();

        // Check if changes were saved successfully
        if (!result) return Problem("Something went wrong with disabling the quiz");

        // Return success response
        return Ok(new { message = "Quiz enabled successfully" });
    }

    /// <summary>
    /// Starts a quiz session for the authenticated user with the specified quiz ID.
    /// </summary>
    /// <param name="id"> The unique identifier of the quiz to start.</param>
    /// <returns>
    /// <response code="200"> Ok: If the quiz session is started successfully, along with the details of the newly started quiz session.</response>
    /// <response code="400"> Bad Request: If the quiz is invalid, not active, or the user already has an ongoing session for the same quiz.</response>
    /// <response code="500"> Internal Server Error: If there is an issue while starting the quiz session or saving changes.</response>
    /// </returns>
    [HttpPost]
    [Authorize(Roles = "User")]
    [Route("start/{id:Guid}")]
    public async Task<ActionResult> StartQuiz(Guid id)
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userRepository.GetById(Guid.Parse(userId));

        var quiz = await _quizRepository.GetById(id);

        if (quiz is null || !quiz.IsActive)
        {
            return BadRequest(new { message = "Invalid quiz" });
        }

        var ongoingSession = await _quizSessionRepository.GetOngoingQuizForUser(user.Id, id);

        if (ongoingSession is not null)
        {
            System.Console.WriteLine(ongoingSession.Id);
            return BadRequest(new
            {
                message = "You're already taking this quiz, please finish that one before starting anotehr one"
            });
        }

        var newQuizSession = new QuizSession
        {
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddMinutes(quiz.Duration),
            User = user,
            Quiz = quiz
        };

        _quizSessionRepository.Add(newQuizSession);

        var result = await _quizSessionRepository.SaveChangesAsync();

        if (!result) return Problem("Something went wrong with disabling the quiz");

        return Ok(new { message = "Quiz started successfully", result = _mapper.Map<QuizSessionDto>(newQuizSession) });
    }

}

