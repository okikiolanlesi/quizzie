﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using quizzie;
using Quizzie.Data;
using Quizzie.DTOs;
using Quizzie.Models;

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

        return Ok(new
        {
            message = "Category created successfully",
            category
        });
    }

}