using Domain;

namespace Api.Models.Response;
public class TodoTaskResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public IEnumerable<TodoTaskResponse>? Chields { get; set; }

    public static implicit operator TodoTaskResponse(TodoTask task)
    {
        if (task is null)
            return null;

        return new TodoTaskResponse
        {
            Id = task.Id,
            Description = task.Description,
            IsDone = task.IsDone,
            CreationDate = task.CreationDate,
            FinishDate = task.FinishDate,
            Chields = task.Chields?.Select(c => (TodoTaskResponse)c)
        };
    }
}
