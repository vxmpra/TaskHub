using Logic.Tasks.Interfaces;  
using Logic.Tasks.Models;
using Logic.Tasks.Services.Interfaces;

namespace Logic.Tasks.Services;

public sealed class TaskService : ITaskService
{
    private readonly ICreateTaskUseCase _createTaskUseCase;
    private readonly IGetTasksUseCase _getTasksUseCase;
    private readonly IGetTaskUseCase _getTaskUseCase;
    private readonly ISetTaskTitleUseCase _setTaskTitleUseCase;
    private readonly IDeleteTaskUseCase _deleteTaskUseCase;
    private readonly IDeleteTasksUseCase _deleteTasksUseCase;

    public TaskService(
        ICreateTaskUseCase createTaskUseCase,
        IGetTasksUseCase getTasksUseCase,
        IGetTaskUseCase getTaskUseCase,
        ISetTaskTitleUseCase setTaskTitleUseCase,
        IDeleteTaskUseCase deleteTaskUseCase,
        IDeleteTasksUseCase deleteTasksUseCase)
    {
        _createTaskUseCase = createTaskUseCase;
        _getTasksUseCase = getTasksUseCase;
        _getTaskUseCase = getTaskUseCase;
        _setTaskTitleUseCase = setTaskTitleUseCase;
        _deleteTaskUseCase = deleteTaskUseCase;
        _deleteTasksUseCase = deleteTasksUseCase;
    }

    public async Task<TaskModel> CreateTaskAsync(string? title, Guid createdByUserId, CancellationToken cancellationToken)
    {
        return await _createTaskUseCase.ExecuteAsync(title, createdByUserId, cancellationToken);
    }

    public async Task<IReadOnlyCollection<TaskModel>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        return await _getTasksUseCase.ExecuteAsync(cancellationToken);
    }

    public async Task<TaskModel?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _getTaskUseCase.ExecuteAsync(id, cancellationToken);
    }

    public async Task SetTaskTitleAsync(Guid id, string? title, CancellationToken cancellationToken)
    {
        await _setTaskTitleUseCase.ExecuteAsync(id, title, cancellationToken);
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _deleteTaskUseCase.ExecuteAsync(id, cancellationToken);
    }

    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _deleteTasksUseCase.ExecuteAsync(cancellationToken);
    }
}