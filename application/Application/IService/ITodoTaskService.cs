using Domain;

namespace Application.IService;
public interface ITodoTaskService
{
    public Task<TodoTask> GetAsync(Guid id);
    public Task<IEnumerable<TodoTask>> GetAsync();
    public Task<TodoTask> CreateAsync(TodoTask task);
    public Task<TodoTask> UpdateAsync(TodoTask task);
    public Task DeleteAsync(Guid id);
}