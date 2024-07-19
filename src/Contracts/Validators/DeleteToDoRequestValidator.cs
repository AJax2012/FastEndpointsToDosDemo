using FastEndpoints;
using FluentValidation;
using ToDosFE.Contracts.Constants;
using ToDosFE.Contracts.Requests;

namespace ToDosFE.Contracts.Validators;

public class DeleteToDoRequestValidator : Validator<DeleteToDoRequest>
{
    public DeleteToDoRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}