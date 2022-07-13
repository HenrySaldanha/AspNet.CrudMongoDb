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
        task.Id = Guid.NewGuid();
        return await _taskWriteRepository.CreateAsync(task);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _taskWriteRepository.DeleteAsync(id);
    }

    public async Task<TodoTask> GetAsync(Guid id)
    {
        return await _taskReadRepository.GetAsync(id);
    }

    public async Task<IEnumerable<TodoTask>> GetAsync()
    {
        return await _taskReadRepository.GetAsync();
    }

    public async Task<TodoTask> UpdateAsync(TodoTask task)
    {
        return await _taskWriteRepository.UpdateAsync(task);
    }
}
