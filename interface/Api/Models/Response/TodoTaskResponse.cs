using Domain;

namespace Api.Models.Response;
public class TodoTaskResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public IEnumerable<SubTaskResponse>? Chields { get; set; }

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
            Chields = task.Chields?.Select(c => (SubTaskResponse)c)
        };
    }
}

public class SubTaskResponse
{
    public string Description { get; set; }
    public IEnumerable<SubTaskResponse>? Chields { get; set; }

    public static implicit operator SubTaskResponse(SubTask task)
    {
        if (task is null)
            return null;

        return new SubTaskResponse
        {
            Description = task.Description,
            Chields = task.Chields?.Select(c => (SubTaskResponse)c)
        };
    }
}