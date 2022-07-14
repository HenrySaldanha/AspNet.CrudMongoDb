using MongoDB.Bson;
using MongoDB.Driver;
using Repository.MongoDb.Context;
using Repository.TodoTask;

namespace Repository.MongoDb.TodoTask;
public class TodoTaskWriteRepository : ITodoTaskWriteRepository
{
    private readonly IMongoCollection<Domain.TodoTask> _taskRepository;

    public TodoTaskWriteRepository(IMongoContext context)
    {
        _taskRepository = context.GetDatabase().GetCollection<Domain.TodoTask>("Tasks");
    }

    public async Task<Domain.TodoTask> CreateAsync(Domain.TodoTask task)
    {
        await _taskRepository.InsertOneAsync(task);
        return task;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _taskRepository.DeleteOneAsync(a => a.Id == id);
    }

    public async Task UpdateAsync(Domain.TodoTask task)
    {
        await _taskRepository.ReplaceOneAsync(a => a.Id == task.Id, task);
    }
    public async Task UpdateAsync(Guid id, DateTime finishTask, bool isDone)
    {
        var filter = Builders<Domain.TodoTask>.Filter.Eq(nameof(Domain.TodoTask.Id), id);
        var update = Builders<Domain.TodoTask>.Update
            .Set(nameof(Domain.TodoTask.IsDone), isDone)
            .Set(nameof(Domain.TodoTask.FinishDate), finishTask);

        await _taskRepository.UpdateOneAsync(filter, update);
    }
}
