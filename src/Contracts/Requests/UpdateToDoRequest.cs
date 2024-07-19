namespace ToDosFE.Contracts.Requests;

public record UpdateToDoRequest(Ulid Id, string Title, bool IsCompleted);
