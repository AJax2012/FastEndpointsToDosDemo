using FastEndpoints;
using MediatR;
using ToDosFE.Api.Endpoints.Extensions;
using ToDosFE.Business.Queries.GetById;
using ToDosFE.Contracts.Examples;
using ToDosFE.Contracts.Requests;
using ToDosFE.Contracts.Responses;

namespace ToDosFE.Api.Endpoints.ToDos;

public class GetToDoByIdEndpoint(ISender mediator) : Endpoint<GetToDoByIdRequest, ToDoResource>
{
    public override void Configure()
    {
        Get("/todos/{Id}");
        DontThrowIfValidationFails();
        AllowAnonymous();
        
        Description(x => x
            .Produces<ToDoResource>(StatusCodes.Status200OK, "application/json")
            .ProducesProblemFE()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithName("GetToDoById"));
        
        Summary(s =>
        {
            s.Summary = "Get a ToDo by id";
            s.Description = "Get a ToDo by id";
            s.ExampleRequest = ToDoRequestExamples.GetToDoByIdRequest;
            s.ResponseExamples[200] = ToDoResponseExamples.ToDoResource;
        });
    }
    
    public override async Task HandleAsync(GetToDoByIdRequest request, CancellationToken cancellationToken)
    {
        if (ValidationFailed)
        {
            await SendErrorsAsync(StatusCodes.Status400BadRequest, cancellationToken);
            return;
        }

        var query = new GetToDoByIdQuery(request.Id);
        var result = await mediator.Send(query, cancellationToken);

        await result.Match(
            entity => SendOkAsync(entity.MapToResponse(), cancellationToken),
            errors => SendResultAsync(errors.ToProblemDetailsResult()));
    }
}