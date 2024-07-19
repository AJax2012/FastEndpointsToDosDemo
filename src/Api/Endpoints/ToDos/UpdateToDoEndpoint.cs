using FastEndpoints;
using MediatR;
using ToDosFE.Api.Endpoints.Extensions;
using ToDosFE.Business.Commands.Update;
using ToDosFE.Contracts.Examples;
using ToDosFE.Contracts.Requests;
using ToDosFE.Contracts.Responses;

namespace ToDosFE.Api.Endpoints.ToDos;

public class UpdateToDoEndpoint(ISender mediator) : Endpoint<UpdateToDoRequest, ToDoResource>
{
    public override void Configure()
    {
        Put("/todos/{Id}");
        DontThrowIfValidationFails();
        AllowAnonymous();
        
        Description(x => x
            .Accepts<UpdateToDoRequest>("application/json")
            .Produces<ToDoResource>(StatusCodes.Status200OK, "application/json")
            .ProducesProblemFE()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithName("UpdateToDo"));
        
        Summary(s =>
        {
            s.Summary = "Update a ToDo by id";
            s.Description = "Update a ToDo by id";
            s.ExampleRequest = ToDoRequestExamples.UpdateToDoRequest;
            s.ResponseExamples[200] = ToDoRequestExamples.UpdateToDoRequest;
        });
    }
    
    public override async Task HandleAsync(UpdateToDoRequest request, CancellationToken cancellationToken)
    {
        if (ValidationFailed)
        {
            await SendErrorsAsync(StatusCodes.Status400BadRequest, cancellationToken);
            return;
        }
        
        var command = new UpdateToDoCommand(request.Id, request.Title, request.IsCompleted);
        var result = await mediator.Send(command, cancellationToken);
        
        await result.Match(
            entity => SendOkAsync(entity.MapToResponse(), cancellationToken),
            errors => SendResultAsync(errors.ToProblemDetailsResult()));
    }
}