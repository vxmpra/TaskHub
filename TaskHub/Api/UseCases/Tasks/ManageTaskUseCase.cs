using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Logic.Tasks.Services.Interfaces;

namespace Api.UseCases.Tasks;

internal sealed class ManageTaskUseCase : IManageTaskUseCase
{
    private readonly ITaskService _taskService;

    public ManageTaskUseCase(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskResponse> CreateTaskAsync(string? title, Guid createdByUserId, CancellationToken cancellationToken)
    {
        var task = await _taskService.CreateTaskAsync(title, createdByUserId, cancellationToken);
        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            CreatedByUserId = task.CreatedByUserId,
            CreatedUtc = task.CreatedUtc
        };
    }

    public async Task<IReadOnlyCollection<TaskResponse>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskService.GetAllTasksAsync(cancellationToken);
        return tasks.Select(x => new TaskResponse
        {
            Id = x.Id,
            Title = x.Title,
            CreatedByUserId = x.CreatedByUserId,
            CreatedUtc = x.CreatedUtc
        }).ToList().AsReadOnly();
    }

    public async Task<TaskResponse?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskService.GetTaskByIdAsync(id, cancellationToken);
        if (task is null)
        {
            return null;
        }

        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            CreatedByUserId = task.CreatedByUserId,
            CreatedUtc = task.CreatedUtc
        };
    }

    public async Task SetTaskTitleAsync(Guid id, string? title, CancellationToken cancellationToken)
    {
        await _taskService.SetTaskTitleAsync(id, title, cancellationToken);
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _taskService.DeleteTaskByIdAsync(id, cancellationToken);
    }

    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskService.DeleteAllTasksAsync(cancellationToken);
    }
}