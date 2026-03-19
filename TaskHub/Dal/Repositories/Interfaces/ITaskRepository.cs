using Dal.Entities;

namespace Dal.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<TaskEntity> CreateAsync(TaskEntity task, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<TaskEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateTitleAsync(Guid id, string? title, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAllAsync(CancellationToken cancellationToken = default);
}