namespace Repository.TodoTask;
public interface ITodoTaskReadRepository
{
    Task<Domain.TodoTask> GetAsync(Guid id);
    Task<IEnumerable<Domain.TodoTask>> GetAsync();
}