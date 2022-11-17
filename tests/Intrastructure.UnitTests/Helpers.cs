using Microsoft.Data.Sqlite;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Intrastructure.UnitTests;

public static class Helpers
{
    public static ApplicationDbContext CreateDbContext()
    {
        SqliteConnection connection = new("DataSource=:memory:");
        connection.Open();

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .EnableSensitiveDataLogging()
            .Options;

        ApplicationDbContext context = new(dbContextOptions);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }
}