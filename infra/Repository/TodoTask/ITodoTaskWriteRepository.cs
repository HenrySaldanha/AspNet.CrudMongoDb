namespace Repository.TodoTask;
public interface ITodoTaskWriteRepository
{
    Task<Domain.TodoTask> CreateAsync(Domain.TodoTask task);
    Task UpdateAsync(Domain.TodoTask task);
    Task DeleteAsync(Guid id);
}
