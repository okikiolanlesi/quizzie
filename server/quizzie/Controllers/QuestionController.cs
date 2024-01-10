using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzie.DTOs;
using Quizzie.Models;
using Quizzie.Repositories;

namespace Quizzie.Controllers;

[ApiController]
[Route("api/question")]
public class QuestionController : ControllerBase
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IMapper _mapper;
    private readonly IOptionRepository _optionRepository;

    public QuestionController(IQuestionRepository questionRepository, IQuizRepository quizRepository, IMapper mapper, IOptionRepository optionRepository)
    {
        _questionRepository = questionRepository;
        _quizRepository = quizRepository;
        _mapper = mapper;
        _optionRepository = optionRepository;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("{quizId:Guid}")]
    public async Task<ActionResult> AddQuestion([FromBody] CreateQuestionDto questionDto, [FromRoute] Guid quizId)
    {
        using (var transaction = _questionRepository.BeginTransaction())
        {
            try
            {

                var quiz = await _quizRepository.GetById(quizId);
                if (quiz is null)
                {
                    return BadRequest(new
                    {
                        message = "Quiz does not exist"
                    });
                }
                var newQuestion = new Question
                {
                    QuestionText = questionDto.QuestionText,
                    Id = new Guid(),
                    Quiz = quiz
                };

                _questionRepository.Add(newQuestion);
                await _questionRepository.SaveChangesAsync();


                var options = new List<Option>();

                foreach (var optionDto in questionDto.Options)
                {
                    var option = _mapper.Map<Option>(optionDto);
                    option.Question = newQuestion;
                    option.IsCorrect = optionDto.isCorrect;
                    options.Add(option);
                }

                _optionRepository.AddRange(options);

                var result = await _optionRepository.SaveChangesAsync();

                if (!result)
                {
                    return Problem(title: "Something went wrong");
                };
                await transaction.CommitAsync();

                return Ok(new
                {
                    message = "Question added successfully"
                });

            }
            catch (Exception)
            {
                // If an exception occurs, roll back the transaction
                transaction.Rollback();
                return StatusCode(500, "An error occurred while saving the data.");
            }
        }




    }
}
