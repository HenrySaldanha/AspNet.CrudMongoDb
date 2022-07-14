using Domain;

namespace Api.Models.Request;
public class CreateSubTaskRequest
{
    public string Description { get; set; }
    public IEnumerable<CreateSubTaskRequest>? Chields { get; set; }

    public static implicit operator SubTask(CreateSubTaskRequest request)
    {
        if (request is null)
            return null;

        return new SubTask
        {
            Description = request.Description,
            Chields = request.Chields?.Select(c => (SubTask)c)
        };
    }
}