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
    public async Task<ActionResult> Create(Domain.TodoTask task)
    {
        await _taskService.CreateAsync(task);
        return Ok(task);
    }

    [HttpPatch]
    public async Task<ActionResult> Update(Domain.TodoTask task)
    {
        await _taskService.UpdateAsync(task);
        return Ok(task);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await _taskService.GetAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        return Ok(await _taskService.GetAsync(id));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _taskService.DeleteAsync(id);
        return Ok();
    }
}
