using Dal.Repositories.Interfaces;
using Logic.Tasks.Models;
using Logic.Tasks.Services.Interfaces;

namespace Logic.Tasks.Services;

public sealed class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskModel> CreateTaskAsync(string? title, Guid createdByUserId, CancellationToken cancellationToken)
    {
        var entity = new Dal.Entities.TaskEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            CreatedByUserId = createdByUserId,
            CreatedUtc = DateTimeOffset.UtcNow
        };

        var task = await _taskRepository.CreateAsync(entity, cancellationToken);
        
        return new TaskModel(task.Id, task.Title, task.CreatedByUserId, task.CreatedUtc);
    }

    public async Task<IReadOnlyCollection<TaskModel>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync(cancellationToken);

        var result = tasks
            .Select(x => new TaskModel(x.Id, x.Title, x.CreatedByUserId, x.CreatedUtc))
            .ToList()
            .AsReadOnly();

        return result;
    }

    public async Task<TaskModel?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(id, cancellationToken);

        if (task == null)
        {
            return null;
        }
        
        return new TaskModel(task.Id, task.Title, task.CreatedByUserId, task.CreatedUtc);
    }

    public async Task SetTaskTitleAsync(Guid id, string? title, CancellationToken cancellationToken)
    {
        await _taskRepository.UpdateTitleAsync(id, title, cancellationToken);
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _taskRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskRepository.DeleteAllAsync(cancellationToken);
    }
}