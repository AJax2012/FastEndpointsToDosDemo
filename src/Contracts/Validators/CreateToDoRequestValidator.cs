using FastEndpoints;
using FluentValidation;
using ToDosFE.Contracts.Constants;
using ToDosFE.Contracts.Requests;

namespace ToDosFE.Contracts.Validators;

public class CreateToDoRequestValidator : Validator<CreateToDoRequest>
{
    public CreateToDoRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(ToDoConstants.TitleMaxLength);
    }
}