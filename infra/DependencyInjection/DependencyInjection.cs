using Application.IService;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.MongoDb.Context;
using Repository.MongoDb.Options;
using Repository.MongoDb.TodoTask;
using Repository.TodoTask;

namespace DependencyInjection;
public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ITodoTaskService, TodoTaskService>();
    }

    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IMongoContext, MongoContext>();

        services.AddScoped<ITodoTaskReadRepository, TodoTaskReadRepository>();
        services.AddScoped<ITodoTaskWriteRepository, TodoTaskWriteRepository>();
    }

    public static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoOptions>(configuration.GetSection("MongoConnection"));
    }
}