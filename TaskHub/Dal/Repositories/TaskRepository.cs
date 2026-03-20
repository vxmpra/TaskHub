using Dal.Context;
using Dal.Entities;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public sealed class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _dbContext;

    public TaskRepository(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskEntity> CreateAsync(TaskEntity task, CancellationToken cancellationToken)
    {
        _dbContext.Tasks.Add(task);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return task;
    }

    public async Task<IReadOnlyCollection<TaskEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateTitleAsync(Guid id, string? title, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.FindAsync(new object[] { id }, cancellationToken);
        if (task is not null)
        {
            task.Title = title;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.FindAsync(new object[] { id }, cancellationToken);
        if (task is null)
        {
            return false;
        }

        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task DeleteAllAsync(CancellationToken cancellationToken)
    {
        await _dbContext.Tasks.ExecuteDeleteAsync(cancellationToken);
    }
}