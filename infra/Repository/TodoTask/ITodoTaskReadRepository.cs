namespace Repository.TodoTask;
public interface ITodoTaskReadRepository
{
    public Task<Domain.TodoTask> GetAsync(Guid id);
    public Task<IEnumerable<Domain.TodoTask>> GetAsync();
}