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

    [HttpGet]
    [Authorize(Roles = "User")]
    public async Task<ActionResult> GetQuizSessions([FromQuery] QuizSessionSearchParams searchParams)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userRepository.GetById(Guid.Parse(userId));

        var unMarkedSessions = await _quizSessionRepository.GetEndedSessionsForAUser(Guid.Parse(userId));

        if (unMarkedSessions.Count > 0)
        {
            await MarkQuizSessions(unMarkedSessions);
            await _quizSessionRepository.SaveChangesAsync();
        }

        var quizSessionPayload = await _quizSessionRepository.GetAllForAUser(user.Id, searchParams);


        return Ok(new { results = quizSessionPayload });
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    [Route("{id:Guid}")]
    public async Task<ActionResult> GetQuizSessionById(Guid id)
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userRole = User.FindFirstValue(ClaimTypes.Role);


        var quizSession = await _quizSessionRepository.GetById(id);

        if (userRole != "Admin" && Guid.Parse(userId) != quizSession.UserId)
        {
            return BadRequest(new
            {
                message = "You cant view this quiz"
            });
        }

        return Ok(new { results = quizSession });
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    [Route("answer/{quizSessionId:Guid}")]
    public async Task<ActionResult> AnswerQuestion([FromRoute] Guid quizSessionId, [FromBody] AnswerQuestionDto answerDto)
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userRepository.GetById(Guid.Parse(userId));

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

    [HttpPost]
    [Authorize(Roles = "User")]
    [Route("submit/{quizSessionId:Guid}")]
    public async Task<ActionResult> SubmitQuizSession([FromRoute] Guid quizSessionId)
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
                message = "You can't submit this quiz"
            });
        }

        await MarkQuizSession(_mapper.Map<QuizSession>(quizSession));

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
