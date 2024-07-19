namespace ToDosFE.Contracts.Responses;

public record ToDoResource(Ulid Id, string Title, bool IsCompleted);