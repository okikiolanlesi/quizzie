using Microsoft.EntityFrameworkCore;
using Quizzie.Models;

namespace Quizzie.Data;

public class QuizzieDbContext : DbContext
{
    public QuizzieDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(e => e.Role)
            .HasConversion<string>(); // Convert enum to string

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
}