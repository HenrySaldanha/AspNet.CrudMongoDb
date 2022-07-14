using Domain;

namespace Application.IService;
public interface ITodoTaskService
{
    Task<TodoTask> GetAsync(Guid id);
    Task<IEnumerable<TodoTask>> GetAsync();
    Task<TodoTask> CreateAsync(TodoTask task);
    Task UpdateAsync(Guid id, TodoTask task);
    Task DeleteAsync(Guid id);
    Task FinishTaskAsync(Guid id);
}