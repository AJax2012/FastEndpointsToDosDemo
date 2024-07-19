using ToDosFE.Business.Queries.GetFiltered;
using ToDosFE.Contracts.Requests;

namespace ToDosFE.Contracts.Examples;

public static class ToDoRequestExamples
{
    public static CreateToDoRequest CreateToDoRequest = new CreateToDoRequest(ExampleConstants.Title);
    public static UpdateToDoRequest UpdateToDoRequest = new UpdateToDoRequest(ExampleConstants.Id, ExampleConstants.Title, false);
    public static DeleteToDoRequest DeleteToDoRequest = new DeleteToDoRequest(ExampleConstants.Id);
    public static GetToDoByIdRequest GetToDoByIdRequest = new GetToDoByIdRequest(ExampleConstants.Id);

    public static GetToDosFilteredRequest GetToDosFilteredRequestWithAllProperties = new GetToDosFilteredRequest(
        ToDosOrderBy.Title.ToStringFast(),
        25,
        true,
        ExampleConstants.NextPageToken,
        ExampleConstants.Title,
        false);

    public static GetToDosFilteredRequest GetToDosFilteredRequestWithoutNullableProperties =
        new GetToDosFilteredRequest(ToDosOrderBy.Id.ToStringFast());
}