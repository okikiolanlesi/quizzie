using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizzie.DTOs;
using Quizzie.Models;
using Quizzie.Repositories;
using System;
using System.Threading.Tasks;

namespace Quizzie.Controllers;

[Route("api/option")]
[ApiController]
public class OptionController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IOptionRepository _optionRepository;
    private readonly IQuestionRepository _questionRepository;

    public OptionController(IMapper mapper, IOptionRepository optionRepository, IQuestionRepository questionRepository)
    {
        _mapper = mapper;
        _optionRepository = optionRepository;
        _questionRepository = questionRepository;
    }

    /// <summary>
    /// Deletes an Option.
    /// </summary>
    /// <param name="id">The unique option id to delete.</param>
    /// <returns>
    /// <response code="200"> Option is successfully deleted.</response>
    /// <response code="404">Not Found: Option does not exist.</response>
    /// <response code="200">Ok: Option has already been deleted.</response>
    /// </returns>
    [HttpPatch]
    [Authorize(Roles = "Admin")]
    [Route("{id:Guid}")]
    public async Task<ActionResult> DeleteOption(Guid id)
    {
        var option = await _optionRepository.GetById(id);
        if (option == null)
        {
            return NotFound(new { message = "Option does not exist" });
        }
        if (option.IsDeleted == true)
        {
            return Ok("This option has already been deleted");
        }
        option.IsDeleted = true;
        _optionRepository.MarkAsModified(option);
        var result = await _optionRepository.SaveChangesAsync();
        if (!result)
        {
            return Problem("Something went wrong with deleting the option");
        }
        return Ok(new { message = "Option deleted successfully" });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("{questionId:Guid}")]
    public async Task<IActionResult> AddOption([FromRoute] Guid questionId, [FromBody] CreateOrUpdateOptionDto createOrUpdateOptionDto)
    {

        var question = await _questionRepository.GetById(questionId);

        if (question is null)
        {
            return BadRequest(new
            {
                message = "Invalid question"
            });
        }
        var newOption = _mapper.Map<Option>(createOrUpdateOptionDto);

        newOption.QuestionId = questionId;

        var option = await _optionRepository.AddOption(newOption);


        return Ok(option);

    }

    [HttpPut]
    [Route("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateOrUpdateOptionDto createOrUpdateOptionDto)
    {
        var option = await _optionRepository.GetById(id);

        if (option == null)
        {
            return NotFound();
        }
        option.OptionText = createOrUpdateOptionDto.OptionText;
        option.IsCorrect = createOrUpdateOptionDto.isCorrect;

        _optionRepository.MarkAsModified(option);

        var result = await _optionRepository.SaveChangesAsync();

        if (!result)
        {
            return Problem("Something went wrong with updating the option");
        }

        return Ok(_mapper.Map<OptionDto>(option));


    }
}
