namespace ToDosFE.Contracts.Requests;

public record GetToDosFilteredRequest(
    string OrderBy = "Id",
    int? Limit = 25,
    bool? IsDescending = false,
    string? NextPageToken = null,
    string? Title = null,
    bool? IsCompleted = null);