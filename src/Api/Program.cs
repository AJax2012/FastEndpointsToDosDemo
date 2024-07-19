using FastEndpoints;
using FastEndpoints.Swagger;
using ToDosFE.Business;
using ToDosFE.Business.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddFastEndpoints(opt =>
        {
            opt.SourceGeneratorDiscoveredTypes.AddRange(ToDosFE.Api.DiscoveredTypes.All);
            opt.SourceGeneratorDiscoveredTypes.AddRange(ToDosFE.Contracts.DiscoveredTypes.All);
        })
        .SwaggerDocument()
        .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IToDoBusinessMarker>())
        .AddDbContext<ToDosDbContext>();
}

var app = builder.Build();
{
    app.UseFastEndpoints(c => c.Errors.UseProblemDetails())
        .UseSwaggerGen();
}

app.Run();
