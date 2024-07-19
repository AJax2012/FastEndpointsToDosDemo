using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDosFE.Business.Persistence;

namespace ToDosFE.Business.Queries.GetFiltered;

public class GetToDosFilteredQueryHandler(ToDosDbContext dbContext) : IRequestHandler<GetToDosFilteredQuery, ErrorOr<ToDosDto>>
{
    private readonly ToDosDbContext _dbContext = dbContext;

    public Task<ErrorOr<ToDosDto>> Handle(GetToDosFilteredQuery request, CancellationToken cancellationToken)
    {
        var orderBy = ToDosOrderByExtensions.Parse(request.OrderBy, true);
        var decodeToken = ToDoNextResultToken.DecodeToken(request.NextPageToken);
        
        var query =  _dbContext.ToDos.AsQueryable()
            .GetWhere(request)
            .GetOrderBy(new(orderBy, request.IsDescending), decodeToken);

        var result =  query
            .Take(request.Limit)
            .ToListAsync(cancellationToken);
        
        var count = query
            .CountAsync(cancellationToken);

        Task.WaitAll([result, count], cancellationToken: cancellationToken);
        
        if (result.IsFaulted || count.IsFaulted)
        {
            return Task.FromResult<ErrorOr<ToDosDto>>(ToDoEntityErrors.DatabaseFailure);
        }
        
        var lastItem = result.Result.LastOrDefault();
        var nextPageToken = lastItem is null ? null : new ToDoNextResultToken(lastItem.Id, lastItem.Title);
        
        return Task.FromResult<ErrorOr<ToDosDto>>(
            new ToDosDto(
                Items: result.Result,
                TotalCount: count.Result,
                NextPageToken: ToDoNextResultToken.EncodeToken(nextPageToken)));
    }
}