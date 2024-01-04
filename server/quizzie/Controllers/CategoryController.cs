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
    public async Task<ActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
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

        return CreatedAtAction(nameof(GetById), new { category.Id }, category);
    }

    [HttpGet("{id:Guid}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var category = await _categoryRepository.GetById(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }
    [HttpGet]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> GetAll()
    {
        var AllCategory = await _categoryRepository.GetAll();

        return Ok(AllCategory);

    }
}
