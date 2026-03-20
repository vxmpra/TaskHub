using Logic.Tasks.Interfaces;
using Logic.Tasks.Models;
using Dal.Repositories.Interfaces; 

namespace Api.UseCases.Tasks;

internal sealed class GetTasksUseCase : IGetTasksUseCase
{
    private readonly ITaskRepository _taskRepository;  

    public GetTasksUseCase(ITaskRepository taskRepository)  
    {
        _taskRepository = taskRepository;
    }

    public async Task<IReadOnlyCollection<TaskModel>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync(cancellationToken); 
        
        return tasks
            .Select(x => new TaskModel(x.Id, x.Title, x.CreatedByUserId, x.CreatedUtc))
            .ToList()
            .AsReadOnly();
    }
}