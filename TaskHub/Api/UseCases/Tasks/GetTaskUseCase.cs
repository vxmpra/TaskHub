using Logic.Tasks.Interfaces;
using Logic.Tasks.Models;
using Dal.Repositories.Interfaces; 

namespace Api.UseCases.Tasks;

internal sealed class GetTaskUseCase : IGetTaskUseCase
{
    private readonly ITaskRepository _taskRepository; 

    public GetTaskUseCase(ITaskRepository taskRepository)  
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskModel?> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(id, cancellationToken); 
        
        if (task is null)
            return null;
            
        return new TaskModel(task.Id, task.Title, task.CreatedByUserId, task.CreatedUtc);
    }
}