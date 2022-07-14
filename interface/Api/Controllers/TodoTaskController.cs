using Api.Models.Request;
using Api.Models.Response;
using Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Produces("application/json")]
[Route("v{version:apiVersion}/[controller]")]
public class TodoTaskController : ControllerBase
{
    private readonly ITodoTaskService _taskService;

    public TodoTaskController(ITodoTaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(TodoTaskResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateAsync(CreateTodoTaskRequest task)
    {
        var response = await _taskService.CreateAsync(task);

        if (response is null)
            return BadRequest();

        return Created(nameof(CreateAsync), (TodoTaskResponse)response);
    }

    [HttpPost("{id}/finish")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> FinishTaskAsync(Guid id)
    {
        await _taskService.FinishTaskAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] UpdateTodoTaskRequest task)
    {
        await _taskService.UpdateAsync(id, task);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TodoTaskResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll()
    {
        var response = await _taskService.GetAsync();
        return Ok(response.Select(c=>(TodoTaskResponse)c));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoTaskResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(Guid id)
    {
        var response = await _taskService.GetAsync(id);
        return Ok((TodoTaskResponse)response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _taskService.DeleteAsync(id);
        return NoContent();
    }
}
