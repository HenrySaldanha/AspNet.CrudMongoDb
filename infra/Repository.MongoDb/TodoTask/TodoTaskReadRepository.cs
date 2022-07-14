using MongoDB.Driver;
using Repository.MongoDb.Context;
using Repository.TodoTask;

namespace Repository.MongoDb.TodoTask;
public class TodoTaskReadRepository : ITodoTaskReadRepository
{
    private readonly IMongoCollection<Domain.TodoTask> _taskRepository;

    public TodoTaskReadRepository(IMongoContext context)
    {
        _taskRepository = context.GetDatabase().GetCollection<Domain.TodoTask>("Tasks");
    }

    public async Task<Domain.TodoTask> GetAsync(Guid id) =>
        await _taskRepository.Find(a => a.Id == id).SingleOrDefaultAsync();


    public async Task<IEnumerable<Domain.TodoTask>> GetAsync() =>
        await _taskRepository.Find(a => true).ToListAsync();

}
