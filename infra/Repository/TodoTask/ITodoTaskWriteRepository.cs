namespace Repository.TodoTask;
public interface ITodoTaskWriteRepository
{
    Task<Domain.TodoTask> CreateAsync(Domain.TodoTask task);
    Task UpdateAsync(Domain.TodoTask task);
    Task UpdateAsync(Guid id, DateTime finishTask, bool isDone);
    Task DeleteAsync(Guid id);
}
