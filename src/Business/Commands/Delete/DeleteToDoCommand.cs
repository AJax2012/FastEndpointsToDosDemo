using ErrorOr;
using MediatR;

namespace ToDosFE.Business.Commands.Delete;

public record DeleteToDoCommand(Ulid Id) : IRequest<ErrorOr<Success>>;