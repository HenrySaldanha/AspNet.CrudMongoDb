using Domain;

namespace Api.Models.Request;
public class UpdateTodoTaskRequest
{
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public IEnumerable<CreateSubTaskRequest>? Chields { get; set; }

    public static implicit operator TodoTask(UpdateTodoTaskRequest request)
    {
        if (request is null)
            return null;

        return new TodoTask
        {
            CreationDate = request.CreationDate,
            FinishDate = request.FinishDate,
            IsDone = request.IsDone,
            Description = request.Description,
            Chields = request.Chields?.Select(c => (SubTask)c)
        };
    }
}