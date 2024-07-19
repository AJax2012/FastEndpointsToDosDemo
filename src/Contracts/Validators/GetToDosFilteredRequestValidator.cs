using FastEndpoints;
using FluentValidation;
using ToDosFE.Business.Queries.GetFiltered;
using ToDosFE.Contracts.Requests;

namespace ToDosFE.Contracts.Validators;

public class GetToDosFilteredRequestValidator : Validator<GetToDosFilteredRequest>
{
    public GetToDosFilteredRequestValidator()
    {
        RuleFor(x => x.OrderBy)
            .IsEnumName(typeof(ToDosOrderBy), false);
        
        RuleFor(x => x.Limit)
            .NotNull()
            .GreaterThanOrEqualTo(5)
            .LessThanOrEqualTo(100);
    }
}