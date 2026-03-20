using Logic.Tasks.Interfaces;
using Dal.Repositories.Interfaces;  

namespace Api.UseCases.Tasks;

internal sealed class DeleteTaskUseCase : IDeleteTaskUseCase
{
    private readonly ITaskRepository _taskRepository; 

    public DeleteTaskUseCase(ITaskRepository taskRepository) 
    {
        _taskRepository = taskRepository;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _taskRepository.DeleteByIdAsync(id, cancellationToken);  
    }
}