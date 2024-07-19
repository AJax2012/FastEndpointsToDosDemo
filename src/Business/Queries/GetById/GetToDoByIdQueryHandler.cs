using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDosFE.Business.Entities;
using ToDosFE.Business.Persistence;

namespace ToDosFE.Business.Queries.GetById;

public class GetToDoByIdQueryHandler(ToDosDbContext dbContext) : IRequestHandler<GetToDoByIdQuery, ErrorOr<ToDo>>
{
    private readonly ToDosDbContext _dbContext = dbContext;
    
    public async Task<ErrorOr<ToDo>> Handle(GetToDoByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.ToDos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (entity is null)
        {
            return ToDoEntityErrors.NotFound;
        }
        
        return entity;
    }
}