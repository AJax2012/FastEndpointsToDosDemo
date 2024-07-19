using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDosFE.Business.Entities;
using ToDosFE.Business.Persistence;

namespace ToDosFE.Business.Commands.Update;

public class UpdateToDoCommandHandler(ToDosDbContext dbContext) : IRequestHandler<UpdateToDoCommand, ErrorOr<ToDo>>
{
    private readonly ToDosDbContext _dbContext = dbContext;
    
    public async Task<ErrorOr<ToDo>> Handle(UpdateToDoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.ToDos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (entity is null)
        {
            return ToDoEntityErrors.NotFound;
        }
        
        entity.Title = string.IsNullOrWhiteSpace(request.Title) ? entity.Title : request.Title;
        entity.IsCompleted = request.IsCompleted.HasValue ? request.IsCompleted.Value : entity.IsCompleted;
        
        _dbContext.ToDos.Update(entity);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        if (result == 0)
        {
            return ToDoEntityErrors.DatabaseFailure;
        }
        
        return entity;
    }
}