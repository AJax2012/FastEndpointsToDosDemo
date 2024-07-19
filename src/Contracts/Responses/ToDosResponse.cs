namespace ToDosFE.Contracts.Responses;

public record ToDosResponse(IReadOnlyList<ToDoResource> Items, int TotalCount, string NextPageToken);