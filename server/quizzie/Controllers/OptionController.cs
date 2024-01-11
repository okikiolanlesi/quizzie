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

[Route("api/[controller]")]
[ApiController]
public class OptionController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IOptionRepository _optionRepository;

    public OptionController(IMapper mapper, IOptionRepository optionRepository)
    {
        _mapper = mapper;
        _optionRepository = optionRepository;
    }

  /*  [HttpPatch]
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
    }*/
    [HttpPost]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> AddOptions([FromBody] CreateOrUpdateOptionDto createOrUpdateOptionDto)
    {

        var existingOption = _mapper.Map<Option>(createOrUpdateOptionDto);
        var option = await _optionRepository.AddOption(existingOption);

        if (option != null)
        {
            return BadRequest(new
            {
                message = "Option already exists"
            });
        }
        return Ok(_mapper.Map<OptionDto>(option));

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
        return Ok(_mapper.Map<OptionDto>(option));


    }
}
