using Application.IService;
using Domain;
using Repository.TodoTask;

namespace Application.Services;
public class TodoTaskService : ITodoTaskService
{
    private readonly ITodoTaskReadRepository _taskReadRepository;
    private readonly ITodoTaskWriteRepository _taskWriteRepository;

    public TodoTaskService(ITodoTaskReadRepository taskRead, ITodoTaskWriteRepository taskWriteRepository)
    {
        _taskReadRepository = taskRead;
        _taskWriteRepository = taskWriteRepository;
    }

    public async Task<TodoTask> CreateAsync(TodoTask task)
    {
        if (task is null)
            return null;

        return await _taskWriteRepository.CreateAsync(task);
    }

    public async Task DeleteAsync(Guid id) =>
        await _taskWriteRepository.DeleteAsync(id);

    public async Task FinishTaskAsync(Guid id)
    {
        var todoTask = await _taskReadRepository.GetAsync(id);
        if (!todoTask.IsDone)
            await _taskWriteRepository.UpdateAsync(id, DateTime.UtcNow, true);
    }

    public async Task<TodoTask> GetAsync(Guid id) =>
        await _taskReadRepository.GetAsync(id);

    public async Task<IEnumerable<TodoTask>> GetAsync() =>
        await _taskReadRepository.GetAsync();

    public async Task UpdateAsync(Guid id, TodoTask task)
    {
        task.Id = id;
        await _taskWriteRepository.UpdateAsync(task);
    }
}
