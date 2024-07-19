using ErrorOr;
using MediatR;

namespace ToDosFE.Business.Queries.GetFiltered;

public record GetToDosFilteredQuery(
    int Limit,
    string OrderBy,
    bool IsDescending,
    string? NextPageToken = null,
    string? Title = null,
    bool? IsCompleted = null) : IRequest<ErrorOr<ToDosDto>>;