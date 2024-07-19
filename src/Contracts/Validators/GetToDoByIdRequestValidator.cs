using FastEndpoints;
using FluentValidation;
using ToDosFE.Contracts.Requests;

namespace ToDosFE.Contracts.Validators;

public class GetToDoByIdRequestValidator : Validator<GetToDoByIdRequest>
{
    public GetToDoByIdRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}