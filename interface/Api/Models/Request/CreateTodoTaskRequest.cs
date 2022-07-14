using Domain;

namespace Api.Models.Request;
public class CreateOrUpdateTodoTaskRequest
{
    public string Description { get; set; }
    public IEnumerable<CreateOrUpdateTodoTaskRequest>? Chields { get; set; }

    public static implicit operator TodoTask(CreateOrUpdateTodoTaskRequest request)
    {
        if (request is null)
            return null;

        return new TodoTask
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            FinishDate = null,
            IsDone = false,
            Description = request.Description,
            Chields = request.Chields?.Select(c => (TodoTask)c)
        };
    }
}