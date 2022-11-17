
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingModelsProfile));

        services
            .AddScoped<IBookService, BookService>()
            .AddScoped<IOrderService, OrderService>();

        return services;
    }
}