using ErrorOr;
using MediatR;
using ToDosFE.Business.Entities;

namespace ToDosFE.Business.Commands.Update;

public record UpdateToDoCommand(Ulid Id, string? Title, bool? IsCompleted) : IRequest<ErrorOr<ToDo>>;