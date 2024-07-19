using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDosFE.Business.Entities;
using ToDosFE.Business.Persistence;

namespace ToDosFE.Business.Commands.Create;

public class CreateToDoCommandHandler(ToDosDbContext dbContext) : IRequestHandler<CreateToDoCommand, ErrorOr<Ulid>>
{
    private readonly ToDosDbContext _dbContext = dbContext;
    
    public async Task<ErrorOr<Ulid>> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
    {
        if (await _dbContext.ToDos
            .AnyAsync(x => x.Title == request.Title, cancellationToken))
        {
            return ToDoEntityErrors.AlreadyExists;
        }

        var entity = new ToDo
        {
            Title = request.Title
        };
        
        await _dbContext.ToDos.AddAsync(entity, cancellationToken);
        var response = await _dbContext.SaveChangesAsync(cancellationToken);

        if (response == 0)
        {
            return ToDoEntityErrors.DatabaseFailure;
        }
        
        return entity.Id;
    }
}