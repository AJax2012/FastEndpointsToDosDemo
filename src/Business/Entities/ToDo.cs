namespace ToDosFE.Business.Entities;

public class ToDo
{
    public Ulid Id { get; init; } = Ulid.NewUlid();
    public required string Title { get; set; }
    public bool IsCompleted { get; set; }
}