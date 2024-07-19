using ErrorOr;
using MediatR;
using ToDosFE.Business.Entities;

namespace ToDosFE.Business.Queries.GetById;

public record GetToDoByIdQuery(Ulid Id) : IRequest<ErrorOr<ToDo>>;