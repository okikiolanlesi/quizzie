using System;
using System.Threading.Tasks;
using Quizzie.Models;

namespace Quizzie.Repositories;

public interface IUserRepository
{
    Task<User> GetById(Guid id);
    Task<User> GetByEmail(string email);
    void Add(User user);
    Task<bool> SaveChangesAsync();
    void MarkAsModified(User user);
}
