using ErrorOr;
using FastEndpoints;
using FluentValidation.Results;

namespace ToDosFE.Api.Endpoints.Extensions;

public static class ProblemDetailsFactory
{
    public static IResult ToProblemDetailsResult(this IReadOnlyList<Error> errors)
    {
        if (errors.Count is 0)
        {
            return TypedResults.NoContent();
        }

        return errors.All(error => error.Type == ErrorType.Validation) ?
            ValidationProblem(errors) : Problem(errors[0]);
    }

    private static IResult Problem(Error error) =>
        error.Type switch
        {
            ErrorType.Conflict => TypedResults.Conflict(error.Description),
            ErrorType.Validation => ValidationProblem(new[] { error }),
            ErrorType.NotFound => TypedResults.NotFound(error.Description),
            _ => TypedResults.Problem(error.Description)
        };

    private static IResult ValidationProblem(IReadOnlyList<Error> errors)
    {
        var validationErrors = errors
            .Select(error => new ValidationFailure(error.Code, error.Description))
            .ToList()
            .AsReadOnly();

        return new ProblemDetails(validationErrors);
    }
}