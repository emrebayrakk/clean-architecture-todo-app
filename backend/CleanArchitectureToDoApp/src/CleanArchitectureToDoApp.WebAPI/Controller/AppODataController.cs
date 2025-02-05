using CleanArchitectureToDoApp.Application.ToDos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace CleanArchitectureToDoApp.WebAPI.Controller;

[Route("odata")]
[ApiController]
[EnableQuery]
public class AppODataController(
    ISender sender) : ODataController
{

    public static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder = new();
        builder.EnableLowerCamelCase();
        builder.EntitySet<TodoGetAllQueryResponse>("todos");
        return builder.GetEdmModel();
    }

    [HttpGet("todos")]
    [ProducesResponseType(typeof(IQueryable<TodoGetAllQueryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTodos(CancellationToken cancellationToken)
    {
        var todos = await sender.Send(new TodoGetAllQuery(), cancellationToken);
        return Ok(todos);
    }
}
