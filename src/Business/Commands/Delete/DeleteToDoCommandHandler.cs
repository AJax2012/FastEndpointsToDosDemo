using ErrorOr;
using MediatR;
using ToDosFE.Business.Persistence;

namespace ToDosFE.Business.Commands.Delete;

public class DeleteToDoCommandHandler(ToDosDbContext dbContext) : IRequestHandler<DeleteToDoCommand, ErrorOr<Success>>
{
    private readonly ToDosDbContext _dbContext = dbContext;
    
    public async Task<ErrorOr<Success>> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.ToDos
            .FindAsync(request.Id, cancellationToken);
        
        if (entity is null)
        {
            return ToDoEntityErrors.NotFound;
        }
        
        _dbContext.ToDos.Remove(entity);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        if (result == 0)
        {
            return ToDoEntityErrors.DatabaseFailure;
        }

        return Result.Success;
    }
}