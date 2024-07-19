using ErrorOr;
using MediatR;

namespace ToDosFE.Business.Commands.Create;

public record CreateToDoCommand(string Title) : IRequest<ErrorOr<Ulid>>;