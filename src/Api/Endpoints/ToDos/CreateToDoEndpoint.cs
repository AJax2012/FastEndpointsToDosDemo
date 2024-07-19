using FastEndpoints;
using MediatR;
using ToDosFE.Api.Endpoints.Extensions;
using ToDosFE.Business.Commands.Create;
using ToDosFE.Contracts.Examples;
using ToDosFE.Contracts.Requests;
using ToDosFE.Contracts.Responses;

namespace ToDosFE.Api.Endpoints.ToDos;

public class CreateToDoEndpoint(ISender mediator) : Endpoint<CreateToDoRequest, CreateToDoResponse>
{
    public override void Configure()
    {
        Post("/todos");
        DontThrowIfValidationFails();
        AllowAnonymous();
        
        Description(x => x
            .Accepts<CreateToDoRequest>("application/json")
            .Produces<CreateToDoResponse>(StatusCodes.Status201Created, "application/json")
            .ProducesProblemFE()
            .ProducesProblem(StatusCodes.Status409Conflict)
            .WithName("CreateToDo")
        , clearDefaults: true);
        
        Summary(s =>
        {
            s.Summary = "Creates a new To Do";
            s.Description = "Creates a new To Do";
            s.ExampleRequest = ToDoRequestExamples.CreateToDoRequest;
            s.ResponseExamples[201] = ToDoResponseExamples.CreateToDoResponse;
            s.Responses[409] = "To Do already exists with that title";
            s.Responses[400] = "Title is invalid.";
        });
    }
    
    public override async Task HandleAsync(CreateToDoRequest request, CancellationToken cancellationToken)
    {
        if (ValidationFailed)
        {
            await SendErrorsAsync(StatusCodes.Status400BadRequest, cancellationToken);
            return;
        }
        
        var command = new CreateToDoCommand(request.Title);
        var result = await mediator.Send(command, cancellationToken);
        
        await result.Match(
            id => SendCreatedAtAsync<GetToDoByIdEndpoint>(new GetToDoByIdRequest(id), new(id), cancellation: cancellationToken),
            errors => SendResultAsync(errors.ToProblemDetailsResult()));
    }
}