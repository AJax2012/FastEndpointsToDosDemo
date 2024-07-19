using ToDosFE.Business.Entities;

namespace ToDosFE.Business.Queries.GetFiltered;

public record ToDosDto(IReadOnlyList<ToDo> Items, int TotalCount, string NextPageToken);