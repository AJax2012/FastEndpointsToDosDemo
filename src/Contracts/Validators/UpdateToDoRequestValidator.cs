using FastEndpoints;
using FluentValidation;
using ToDosFE.Contracts.Constants;
using ToDosFE.Contracts.Requests;

namespace ToDosFE.Contracts.Validators;

public class UpdateToDoRequestValidator : Validator<UpdateToDoRequest>
{
    public UpdateToDoRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.IsCompleted).NotNull();
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(ToDoConstants.TitleMaxLength);
    }
}