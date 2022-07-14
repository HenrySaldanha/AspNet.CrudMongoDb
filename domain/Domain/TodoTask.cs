using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Domain;
public class TodoTask
{
    [BsonId(IdGenerator = typeof(GuidGenerator))]
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public IEnumerable<SubTask>? Chields { get; set; }
}
public class SubTask
{
    public string Description { get; set; }
    public IEnumerable<SubTask>? Chields { get; set; }
}