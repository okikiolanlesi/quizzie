using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quizzie.Models;

namespace Quizzie.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<QuizzieDbContext>());
    }

    private static void SeedData(QuizzieDbContext context)
    {
        context.Database.Migrate();

        if (context.Users.Any())
        {
            Console.WriteLine("Already have data - no need to seed");
            return;
        }

        var admin = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Admin",
            LastName = "Oga",
            Email = "admin@admin.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
            Role = Role.Admin
        };

        context.Users.Add(admin);

        context.SaveChanges();
    }
}