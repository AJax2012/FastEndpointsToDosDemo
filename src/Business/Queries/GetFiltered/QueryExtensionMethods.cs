using Microsoft.EntityFrameworkCore;
using ToDosFE.Business.Entities;

namespace ToDosFE.Business.Queries.GetFiltered;

internal static class QueryExtensionMethods
{
    internal static IQueryable<ToDo> GetWhere(this IQueryable<ToDo> query, GetToDosFilteredQuery request)
    {
        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            query = query.Where(x => 
                EF.Functions.Like(
                    x.Title,
                    $"%{request.Title}%"));
        }
        
        if (request.IsCompleted.HasValue)
        {
            query = query.Where(x => 
                x.IsCompleted == request.IsCompleted.Value);
        }

        return query;
    }

    internal static IQueryable<ToDo> GetOrderBy(this IQueryable<ToDo> query, ToDosOrderByDto orderByBy, ToDoNextResultToken? token) =>
        orderByBy switch
        {
            { IsDescending: true, OrderBy: ToDosOrderBy.Id } => query.OrderByDescending(x => x.Id)
                .Where(x => token == null || x.Id.Time > token.PreviousLastId.Time),
            { IsDescending: true, OrderBy: ToDosOrderBy.Title } => query.OrderByDescending(x => x.Title)
                .Where(x => token == null || x.Title.CompareTo(token.PreviousLastTitle) > 0),
            { IsDescending: false, OrderBy: ToDosOrderBy.Id } => query.OrderBy(x => x.Id)
                .Where(x => token == null || x.Id.Time > token.PreviousLastId.Time),
            { IsDescending: false, OrderBy: ToDosOrderBy.Title } => query.OrderBy(x => x.Title).ThenBy(x => x.Id)
                .Where(x => token == null || x.Title.CompareTo(token.PreviousLastTitle) > 0),
            _ => throw new ArgumentOutOfRangeException(nameof(orderByBy))
        };
}