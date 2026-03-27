using Api.Controllers.Tasks.Request;
using Api.Controllers.Tasks.Response;
using Logic.Tasks.Services.Interfaces;
using Api.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Tasks;

[ApiController]
[Route("tasks")]
public sealed class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> CreateTaskAsync(
        [FromBody] CreateTaskRequest? request,
        CancellationToken cancellationToken)
    {
        var task = await _taskService.CreateTaskAsync(request!.Title, request!.CreatedByUserId, cancellationToken);
        return CreatedAtAction("GetTaskById", new { id = task.Id }, task);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<TaskResponse>>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskService.GetAllTasksAsync(cancellationToken);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskResponse>> GetTaskByIdAsync([FromRouteTaskId] Guid id, CancellationToken cancellationToken)
    {
        var taskResponse = await _taskService.GetTaskByIdAsync(id, cancellationToken);
        
        if (taskResponse is null)
        {
            return NotFound();
        }

        return Ok(taskResponse);
    }

    [HttpPut("{id}/title")]
    public async Task<IActionResult> SetTaskTitleAsync(
        [FromRouteTaskId] Guid id,
        [FromBody] SetTaskTitleRequest? request,
        CancellationToken cancellationToken)
    {
        await _taskService.SetTaskTitleAsync(id, request!.Title, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskByIdAsync([FromRouteTaskId] Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _taskService.DeleteTaskByIdAsync(id, cancellationToken);
        if (deleted == false)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskService.DeleteAllTasksAsync(cancellationToken);
        return NoContent();
    }
}