using Logic.Tasks.Interfaces;
using Dal.Repositories.Interfaces;  

namespace Api.UseCases.Tasks;

internal sealed class SetTaskTitleUseCase : ISetTaskTitleUseCase
{
    private readonly ITaskRepository _taskRepository; 

    public SetTaskTitleUseCase(ITaskRepository taskRepository)  
    {
        _taskRepository = taskRepository;
    }

    public async Task ExecuteAsync(Guid id, string? title, CancellationToken cancellationToken)
    {
        await _taskRepository.UpdateTitleAsync(id, title, cancellationToken); 
    }
}