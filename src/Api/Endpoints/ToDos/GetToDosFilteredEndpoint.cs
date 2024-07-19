using FastEndpoints;
using MediatR;
using ToDosFE.Api.Endpoints.Extensions;
using ToDosFE.Business.Queries.GetFiltered;
using ToDosFE.Contracts.Examples;
using ToDosFE.Contracts.Requests;
using ToDosFE.Contracts.Responses;

namespace ToDosFE.Api.Endpoints.ToDos;

public class GetToDosFilteredEndpoint(ISender mediator) : Endpoint<GetToDosFilteredRequest, ToDosResponse>
{
    public override void Configure()
    {
        Get("/todos");
        DontThrowIfValidationFails();
        AllowAnonymous();
        
        Description(x => x
            .Produces<ToDosResponse>(StatusCodes.Status200OK, "application/json")
            .ProducesProblemFE()
            .WithName("GetToDosFiltered"));
        
        Summary(s =>
        {
            s.Summary = "Get To Dos filtered";
            s.Description = "Get To Dos filtered";
            s.RequestParam(x => x.Limit,  "The maximum number of To Dos to return. Must be between 5 and 100. Defaults to 25.");
            s.RequestParam(x => x.OrderBy, "The property to order the filtered To Do results by. Must be either Id or Title. Id also sorts by date created. Defaults to Id.");
            s.RequestParam(x => x.IsDescending, "If true, returns results in descending order. If false, returns results in ascending order. Defaults to false (alphabetic order or in order of creation).");
            s.RequestParam(x => x.Title, "The title of the To Do. Is not case sensitive and supports partial matching. Defaults to null (no filtering).");
            s.RequestParam(x => x.IsCompleted, "Returns only completed To Dos if true, and returns only incomplete To Dos if false. Defaults to null (no filtering).");
            s.RequestParam(x => x.NextPageToken, "The token to retrieve the next batch of To Dos. If not provided, returns the first batch of To Dos.");
            s.RequestExamples.Add(new(ToDoRequestExamples.GetToDosFilteredRequestWithAllProperties));
            s.RequestExamples.Add(new(ToDoRequestExamples.GetToDosFilteredRequestWithoutNullableProperties));
            s.ResponseExamples[200] = ToDoResponseExamples.ToDoResource;
        });
    }
    
    public override async Task HandleAsync(GetToDosFilteredRequest request, CancellationToken cancellationToken)
    {
        if (ValidationFailed)
        {
            await SendErrorsAsync(StatusCodes.Status400BadRequest, cancellationToken);
            return;
        }
        
        var isDesc = request.IsDescending ?? false;

        var query = new GetToDosFilteredQuery(request.Limit ?? 25, request.OrderBy, isDesc,
            request.NextPageToken, request.Title, request.IsCompleted);
        
        var result = await mediator.Send(query, cancellationToken);
        
        await result.Match(
            dto => SendOkAsync(new ToDosResponse(dto.Items.MapToResponse(), dto.TotalCount, dto.NextPageToken), cancellationToken),
            errors => SendResultAsync(errors.ToProblemDetailsResult()));
    }
}