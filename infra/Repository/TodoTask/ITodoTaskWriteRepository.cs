namespace Repository.TodoTask;
public interface ITodoTaskWriteRepository
{
    public Task<Domain.TodoTask> CreateAsync(Domain.TodoTask task);
    public Task<Domain.TodoTask> UpdateAsync(Domain.TodoTask task);
    public Task DeleteAsync(Guid id);
}
