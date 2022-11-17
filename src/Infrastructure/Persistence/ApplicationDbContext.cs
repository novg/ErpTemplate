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
    public DbSet<BookOrder> BookOrders => Set<BookOrder>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new BookConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new BookOrderConfiguration());

        base.OnModelCreating(builder);
    }
}