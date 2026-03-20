using Logic.Tasks.Interfaces;
using Dal.Repositories.Interfaces;  

namespace Api.UseCases.Tasks;

internal sealed class DeleteTasksUseCase : IDeleteTasksUseCase
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTasksUseCase(ITaskRepository taskRepository) 
    {
        _taskRepository = taskRepository;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _taskRepository.DeleteAllAsync(cancellationToken);  
    }
}