﻿using System;
using System.Threading.Tasks;
using Quizzie.Models;

namespace quizzie;

public interface ICategoryRepository
{
    Task<Category> GetByTitle(string title);
    Task<Category> GetById(Guid id);

    void Add(Category category);
    Task<bool> SaveChangesAsync();

}