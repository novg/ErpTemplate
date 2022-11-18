using Domain.Models;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new BookConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());

        builder.Entity<Book>().HasData(
            new Book { Id = 1, Name = "SICP", Description = "Structure and Interpretation of Computer Programs MIT cource", Price = 100 },
            new Book { Id = 2, Name = "Getting Clojure", Description = "Describe Clojure programming language", Price = 200 },
            new Book { Id = 3, Name = "Web Development with Clojure", Description = "Describe Web Development with Clojure programming language", Price = 300 }
        );

        base.OnModelCreating(builder);
    }
}