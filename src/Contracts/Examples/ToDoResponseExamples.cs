using ToDosFE.Contracts.Responses;

namespace ToDosFE.Contracts.Examples;

public static class ToDoResponseExamples
{
    public static ToDoResource ToDoResource =
        new ToDoResource(ExampleConstants.Id, ExampleConstants.Title, false);

    public static ToDosResponse ToDosResponse = 
        new ToDosResponse([ToDoResource], 1, ExampleConstants.NextPageToken);
    
    public static CreateToDoResponse CreateToDoResponse = new CreateToDoResponse(ExampleConstants.Id);
    
}