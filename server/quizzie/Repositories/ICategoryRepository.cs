﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quizzie.Models;

namespace quizzie;

public interface ICategoryRepository
{
    Task<Category> GetByTitle(string title);
    Task<Category> GetById(Guid id);
    Task<List<Category>> GetAll();

    void Add(Category category);
    Task<bool> SaveChangesAsync();

}
