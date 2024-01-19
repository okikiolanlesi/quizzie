using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzie.DTOs;
using Quizzie.Models;
using Quizzie.Repositories;
using Swashbuckle.AspNetCore.Filters;

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

    /// <summary>
    /// Creates a category.
    /// </summary>
    /// <param name="categoryDto">The CreateOrUpdateCategoryDto categoryDto object.</param>
    /// <returns></returns>
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

        return CreatedAtAction(nameof(GetById), new { category.Id }, category);
    }

    /// <summary>
    /// Get category by id.
    /// </summary>
    /// <param name="id">The unique category id to get.</param>
    /// <returns></returns>
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        // Retrieve category id from the repository
        var category = await _categoryRepository.GetById(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    /// <summary>
    /// Get all categories.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var AllCategory = await _categoryRepository.GetAll();

        return Ok(AllCategory);

    }

    /// <summary>
    /// Updates a category.
    /// </summary>
    /// <param name="id">The unique category id to be deleted.</param>
    /// <param name="categoryDto">The Dto containing title and description information.</param>
    /// <returns> 
    /// <response code="200">Ok: Category updated successfully.</response>
    /// <response code="404">Not Found: Category not found.</response>
    /// </returns>
    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] CreateOrUpdateCategoryDto categoryDto)
    {
        // Retrieve category id from the repository
        var category = await _categoryRepository.GetById(id);
        if (category == null)
        {
            return NotFound();
        }

        // Updates category details
        category.Title = categoryDto.Title;
        category.Description = categoryDto.Description;

        // Mark as modified in the repository
        _categoryRepository.MarkAsModified(_mapper.Map<Category>(category));
        
        // Save changes to the repository
        await _categoryRepository.SaveChangesAsync();

        return Ok(category);

    }

    /// <summary>
    /// Deletes a category by its unique id.
    /// </summary>
    /// <param name="id">The unique category id to be deleted.</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id)
    {
        // Retrieve category id from the repository
        var category = await _categoryRepository.DeleteById(id);

        // If category is not found, return NotFoundResult
        if (category == null)
        {
            return null;
        }

        return Ok(_mapper.Map<Category>(category));

    }
}
