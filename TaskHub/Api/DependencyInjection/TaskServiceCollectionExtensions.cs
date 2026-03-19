using Dal.Context;
using Dal.Repositories;
using Dal.Repositories.Interfaces;
using Logic.Tasks.Services;
using Logic.Tasks.Services.Interfaces;
using Api.UseCases.Tasks;
using Api.UseCases.Tasks.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.DependencyInjection;

public static class TaskServiceCollectionExtensions
{
    public static IServiceCollection AddTaskServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres")));
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IManageTaskUseCase, ManageTaskUseCase>();

        return services;
    }
}