using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch.Operations;
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

    /// <summary>
    /// Adds a new question to a quiz with the provided question details.
    /// </summary>
    /// <param name="questionDto"> The data transfer object containing question details.</param>
    /// <param name="quizId"> The unique identifier of the quiz to which the question will be added.</param>
    /// <returns>
    /// <response code="200"> Ok: If the question is added successfully.</response>
    /// <response code="400"> Bad Request: If the quiz with the specified ID does not exist.</response>
    /// <response code="500"> Internal Server Error: If there is an issue while saving the question or options, or if an exception occurs during the process.</response>
    /// </returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("{quizId:Guid}")]
    public async Task<ActionResult> AddQuestion([FromBody] CreateQuestionDto questionDto, [FromRoute] Guid quizId)
    {
        using (var transaction = _questionRepository.BeginTransaction())
        {
            try
            {
                // Retrieve the quiz based on the provided quiz ID
                var quiz = await _quizRepository.GetById(quizId);

                // Return 400 Bad Request if the quiz does not exist
                if (quiz is null)
                {
                    return BadRequest(new
                    {
                        message = "Quiz does not exist"
                    });
                }

                // Create a new question entity
                var newQuestion = new Question
                {
                    QuestionText = questionDto.QuestionText,
                    Id = new Guid(),
                    Quiz = quiz
                };

                // Add the question to the repository and save changes
                _questionRepository.Add(newQuestion);
                await _questionRepository.SaveChangesAsync();

                // Create a list to hold the options
                var options = new List<Option>();

                // Map each option DTO to an Option entity and associate it with the new question
                foreach (var optionDto in questionDto.Options)
                {
                    var option = _mapper.Map<Option>(optionDto);
                    option.Question = newQuestion;
                    option.IsCorrect = optionDto.isCorrect;
                    options.Add(option);
                }

                // Add the options to the repository and save changes
                _optionRepository.AddRange(options);

                var result = await _optionRepository.SaveChangesAsync();

                if (!result)
                {
                    return Problem(title: "Something went wrong");
                };

                // Commit the transaction as all operations were successful
                await transaction.CommitAsync();

                // Return 200 OK with the success message
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
