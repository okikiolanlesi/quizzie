using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quizzie.Data;
using Quizzie.Models;

namespace Quizzie;

public class UserRepository : IUserRepository
{
    private readonly QuizzieDbContext _context;

    public UserRepository(QuizzieDbContext _context)
    {
        this._context = _context;
    }

    public async Task<User> GetById(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<User> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
    }
    public void Add(User user)
    {
        _context.Users.Add(user);
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
