using Application.Interfaces;
using Application.Interfaces.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IFileReaderFactory, FileReaderFactory>();

        return services;
    }
}