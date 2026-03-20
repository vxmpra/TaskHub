using Dal.Entities;  
using Logic.Tasks.Interfaces;
using Logic.Tasks.Models;
using Dal.Repositories.Interfaces;  

namespace Api.UseCases.Tasks;

internal sealed class CreateTaskUseCase : ICreateTaskUseCase
{
    private readonly ITaskRepository _taskRepository;  

    public CreateTaskUseCase(ITaskRepository taskRepository)  
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskModel> ExecuteAsync(string? title, Guid createdByUserId, CancellationToken cancellationToken)
    {
        var entity = new TaskEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            CreatedByUserId = createdByUserId,
            CreatedUtc = DateTimeOffset.UtcNow
        };

        var task = await _taskRepository.CreateAsync(entity, cancellationToken);
        
        return new TaskModel(
            task.Id, 
            task.Title, 
            task.CreatedByUserId, 
            task.CreatedUtc);
    }
}