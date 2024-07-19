using FastEndpoints;
using MediatR;
using ToDosFE.Api.Endpoints.Extensions;
using ToDosFE.Business.Commands.Delete;
using ToDosFE.Contracts.Examples;
using ToDosFE.Contracts.Requests;

namespace ToDosFE.Api.Endpoints.ToDos;

public class DeleteToDoEndpoint(ISender mediator) : Endpoint<DeleteToDoRequest>
{
    public override void Configure()
    {
        Delete("/todos/{Id}");
        DontThrowIfValidationFails();
        AllowAnonymous();
        
        Description(x => x
            .ProducesProblemFE()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithName("DeleteToDo"));
        
        Summary(s =>
        {
            s.Summary = "Delete a ToDo by id";
            s.Description = "Delete a ToDo by id";
            s.ExampleRequest = ToDoRequestExamples.DeleteToDoRequest;
        });
    }
    
    public override async Task HandleAsync(DeleteToDoRequest request, CancellationToken cancellationToken)
    {
        if (ValidationFailed)
        {
            await SendErrorsAsync(StatusCodes.Status400BadRequest, cancellationToken);
            return;
        }

        var command = new DeleteToDoCommand(request.Id);
        var result = await mediator.Send(command, cancellationToken);
        
        await result.Match(
            _ => SendNoContentAsync(cancellationToken),
            errors => SendResultAsync(errors.ToProblemDetailsResult()));
    }
}