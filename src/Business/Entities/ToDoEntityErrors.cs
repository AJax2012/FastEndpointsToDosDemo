using ErrorOr;

namespace ToDosFE.Business;

public static class ToDoEntityErrors
{
    internal static Error AlreadyExists = Error.Conflict("ToDo.Exists", "ToDo already exists with that title");
    internal static Error NotFound = Error.NotFound("ToDo.notFound", "ToDo not found");
    internal static Error DatabaseFailure = Error.Failure("ToDo.DatabaseFailure", "Failed to save ToDo to database");
}