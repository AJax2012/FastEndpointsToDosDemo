using System.Collections.Immutable;
using ToDosFE.Business.Entities;

namespace ToDosFE.Contracts.Responses;

public static class ResponseMappers
{
    public static ToDoResource MapToResponse(this ToDo e) => new(e.Id, e.Title, e.IsCompleted);
    public static IReadOnlyList<ToDoResource> MapToResponse(this IReadOnlyList<ToDo> e) => e.Select(MapToResponse).ToImmutableList();
}