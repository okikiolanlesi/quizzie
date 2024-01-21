using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzie.DTOs;
using Quizzie.Models;
using Quizzie.Repositories;
using Quizzie.RequestHelpers;

namespace quizzie;

[ApiController]
[Route("api/quiz-session")]
public class QuizSessionController : ControllerBase
{
    private readonly IQuizSessionRepository _quizSessionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IOptionRepository _optionRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;

    public QuizSessionController(IQuizSessionRepository quizSessionRepository, IUserRepository userRepository, IOptionRepository optionRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, IMapper mapper)
    {
        _quizSessionRepository = quizSessionRepository;
        _userRepository = userRepository;
        _optionRepository = optionRepository;
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves quiz sessions for the authenticated user.
    /// </summary>
    /// <param name="searchParams">The search parameters for filtering quiz session.</param>
    /// <returns>
    /// <response code="200">Ok: If the quiz session are successfully retrieved.</response>
    /// </returns>
    [HttpGet]
    [Authorize(Roles = "User")] 
    public async Task<ActionResult> GetQuizSessions([FromQuery] QuizSessionSearchParams searchParams)
    {
        // Get the user ID from the claims
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Retrieve the user based on the user ID
        var user = await _userRepository.GetById(Guid.Parse(userId));

        // Get quiz sessions that need marking for the user
        var unMarkedSessions = await _quizSessionRepository.GetEndedSessionsForAUser(Guid.Parse(userId));

        // If there are unmarked sessions, mark and save changes
        if (unMarkedSessions.Count > 0)
        {
            await MarkQuizSessions(unMarkedSessions);
            await _quizSessionRepository.SaveChangesAsync();
        }

        // Retrieve quiz sessions for the user based on search parameters
        var quizSessionPayload = await _quizSessionRepository.GetAllForAUser(user.Id, searchParams);


        return Ok(new { results = quizSessionPayload });
    }

    /// <summary>
    /// Gets quiz session details by the unique id.
    /// </summary>
    /// <param name="id">The unique identifier of the quiz session.</param>
    /// <returns>
    /// <response code="200">Ok: If the quiz session is successfully retrieved.</response>
    /// <response code="400">Bad Request: If the user is not authorized to view the quiz session.</response>
    /// <response code="404">Not Found: If the user is not authorized to view the quiz session.</response>
    /// </returns>
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    [Route("{id:Guid}")]
    public async Task<ActionResult> GetQuizSessionById(Guid id)
    {
        // Get the user ID from the claims
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Get the user Role from the claims
        var userRole = User.FindFirstValue(ClaimTypes.Role);

        // Retrieve the quiz session based on the provided Id
        var quizSession = await _quizSessionRepository.GetById(id);

        // Check if the quiz session is not valid
        if (quizSession == null)
        {
            return NotFound("Quiz session not found");
        }

        // Check if the user is not an admin and is not the owner of the quiz session
        if (userRole != "Admin" && Guid.Parse(userId) != quizSession.UserId)
        {
            return BadRequest(new
            {
                message = "You cant view this quiz"
            });
        }


        // Return 200 Ok with the retrieved quiz session details
        return Ok(new { results = quizSession });
    }

    /// <summary>
    /// Handles the submission of answers to questions for a specific quiz session.
    /// </summary>
    /// <param name="quizSessionId">The unique identifier of the quiz session.</param>
    /// <param name="answerDto">The data transfer object containing answer details.</param>
    /// <returns>
    /// <response code="200">Ok: If the answer is successfully submitted or updated.</response>
    /// <response code="400">Bad Request: If the quiz session is invalid, completed, or has passed the end time, or if the user is not the owner of the quiz session.</response>
    /// <response code="400">Bad Request: If the question or option is invalid.</response>
    /// </returns>
    [HttpPost]
    [Authorize(Roles = "User")]
    [Route("answer/{quizSessionId:Guid}")]
    public async Task<ActionResult> AnswerQuestion([FromRoute] Guid quizSessionId, [FromBody] AnswerQuestionDto answerDto)
    {
        // Get the user ID from the claims
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Retrieve the user based on the user ID
        var user = await _userRepository.GetById(Guid.Parse(userId));

        // Retrieve the quiz based on the quiz session ID
        var quizSession = await _quizSessionRepository.GetById(quizSessionId);


        if (quizSession == null || quizSession.IsCompleted || quizSession.EndTime <= DateTime.UtcNow)
        {

            return BadRequest(new
            {
                message = "Invalid quiz session"
            });
        }


        if (quizSession.UserId != Guid.Parse(userId))
        {
            return BadRequest(new
            {
                message = "You can't answer questions for this quiz"
            });
        }
        var question = await _questionRepository.GetQuestionForQuizById(answerDto.QuestionId, quizSession.QuizId);

        if (question is null)
        {
            return BadRequest(new
            {
                message = "Invalid question"
            });
        }
        var option = await _optionRepository.GetOptionForQuestionById(answerDto.OptionId, question.Id);

        if (option is null)
        {
            return BadRequest(new
            {
                message = "Invalid option"
            });
        }


        var existingAnswer = await _answerRepository.GetAnswerForQuestion(quizSession.Id, question.Id);

        if (existingAnswer is null)
        {
            var answer = new Answer
            {
                OptionId = answerDto.OptionId,
                QuestionId = answerDto.QuestionId,
                QuizSessionId = quizSession.Id,
                UserId = user.Id
            };
            _answerRepository.Add(answer);
        }
        else
        {
            existingAnswer.OptionId = answerDto.OptionId;
            _answerRepository.MarkAsModified(existingAnswer);
        }

        var result = await _answerRepository.SaveChangesAsync();

        if (!result) return Problem("Unable to answer question");

        var refetchedQuizSession = await _quizSessionRepository.GetById(quizSessionId);


        return Ok(new { result = _mapper.Map<QuizSessionDto>(refetchedQuizSession) });
    }

    /// <summary>
    /// Submits a quiz session for a user.
    /// </summary>
    /// <param name="quizSessionId">The unique quiz session id to be submitted.</param>
    /// <returns> 
    /// <response code="200">Ok: If the quiz session is successfully submitted.</response>
    /// <response code="400">Bad Request: If the quiz session is invalid, completed, or has passed the end time.</response>
    /// <response code="400">Bad Request: If the user attempting to submit the quiz is not the owner of the quiz session.</response>
    /// </returns>
    [HttpPost]
    [Authorize(Roles = "User")]
    [Route("submit/{quizSessionId:Guid}")]
    public async Task<ActionResult> SubmitQuizSession([FromRoute] Guid quizSessionId)
    {
        // Get the user ID from the claims
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Retrieve the quiz session based on the provided quiz session ID
        var quizSession = await _quizSessionRepository.GetById(quizSessionId);

        // Return 400 Bad Request if the quiz session is invalid, completed, or the end time has passed
        if (quizSession == null || quizSession.IsCompleted || quizSession.EndTime <= DateTime.UtcNow)
        {

            return BadRequest(new
            {
                message = "Invalid quiz session"
            });
        }
        // Return 400 Bad Request if the user is not authorized to submit
        if (quizSession.UserId != Guid.Parse(userId))
        {
            return BadRequest(new
            {
                message = "You can't submit this quiz"
            });
        }

        // Mark the quiz session as completed and update its details
        await MarkQuizSession(_mapper.Map<QuizSession>(quizSession));

        // Save changes to answer repository
        var result = await _answerRepository.SaveChangesAsync();

        if (!result) return Problem("Unable to submit");

        return Ok(new { result = _mapper.Map<QuizSessionDto>(quizSession) });
    }
    private async Task MarkQuizSessions(List<QuizSession> quizSessions)
    {
        if (quizSessions.Count < 1) return;

        foreach (QuizSession quizSession in quizSessions)
        {
            var correctAnswersCount = await _quizSessionRepository.CountCorrectAnswers(quizSession.Id);
            var totalQuestions = await _quizSessionRepository.CountTotalQuestions(quizSession.QuizId);

            quizSession.Score = correctAnswersCount;
            quizSession.TotalQuestions = totalQuestions;
            quizSession.IsCompleted = true;
            quizSession.UpdatedAt = DateTime.UtcNow;

            _quizSessionRepository.MarkAsModified(quizSession);

        }
    }
    private async Task MarkQuizSession(QuizSession quizSession)
    {
        var correctAnswersCount = await _quizSessionRepository.CountCorrectAnswers(quizSession.Id);
        var totalQuestions = await _quizSessionRepository.CountTotalQuestions(quizSession.QuizId);

        quizSession.Score = correctAnswersCount;
        quizSession.TotalQuestions = totalQuestions;
        quizSession.IsCompleted = true;
        quizSession.UpdatedAt = DateTime.UtcNow;

        _quizSessionRepository.MarkAsModified(quizSession);


    }

}
