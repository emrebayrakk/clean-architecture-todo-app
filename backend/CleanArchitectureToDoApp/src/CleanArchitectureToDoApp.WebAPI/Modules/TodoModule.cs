using CleanArchitectureToDoApp.Application.ToDos.Commands;
using MediatR;
using TS.Result;

namespace CleanArchitectureToDoApp.WebAPI.Modules;

public static class TodoModule
{
    public static void RegisterTodoRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/todos").WithTags("Todos");

        group.MapPost(string.Empty, async (ISender sender, TodoCreateCommand req, CancellationToken cancellationToken) =>
        {
            var res = await sender.Send(req, cancellationToken);
            return res.IsSuccessful ? Results.Ok(res) : Results.InternalServerError(res);
        })
        .Produces<Result<string>>();

    }
}
