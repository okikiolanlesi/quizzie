using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzie.DTOs;
using Quizzie.Models;
using Quizzie.Repositories;

namespace Quizzie.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    [HttpPost]
     [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateCategory([FromBody] CreateOrUpdateCategoryDto categoryDto)
    {
        var existingCategory = await _categoryRepository.GetByTitle(categoryDto.Title);

        if (existingCategory != null)
        {
            return BadRequest(new
            {
                message = "Category already exists with that title"
            });
        }
        
        var category = _mapper.Map<Category>(categoryDto);

        _categoryRepository.Add(category);

        var result = await _categoryRepository.SaveChangesAsync();

        if (!result) return Problem(title: "Something went wrong");

        return CreatedAtAction(nameof(GetBy_Id), new { category.Id }, category);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetBy_Id([FromRoute] Guid id)
    {
        var category = await _categoryRepository.GetById(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpGet]
    public async Task<IActionResult> Get_All()
    {
        var AllCategory = await _categoryRepository.GetAll();

        return Ok(AllCategory);

    }

    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] CreateOrUpdateCategoryDto categoryDto)
    {
        var category = await _categoryRepository.GetById(id);
        if (category == null)
        {
            return NotFound();
        }

        category.Title = categoryDto.Title;
        category.Description = categoryDto.Description;

        _categoryRepository.MarkAsModified(_mapper.Map<Category>(category));

        await _categoryRepository.SaveChangesAsync();

        return Ok(category);

    }
    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBy_Id([FromRoute] Guid id)
    {
        var category = await _categoryRepository.DeleteById(id);

        if(category == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<Category>(category));

    }
}
