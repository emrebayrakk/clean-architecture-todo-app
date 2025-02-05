using CleanArchitectureToDoApp.Domain.Abstractions;
using CleanArchitectureToDoApp.Domain.ToDos;
using MediatR;

namespace CleanArchitectureToDoApp.Application.ToDos.Queries;
public sealed class TodoGetAllQuery() : IRequest<IQueryable<TodoGetAllQueryResponse>>;


internal sealed class TodoGetAllQueryHandler(
    IToDoRepository toDoRepository) : IRequestHandler<TodoGetAllQuery,
        IQueryable<TodoGetAllQueryResponse>>
{
    public Task<IQueryable<TodoGetAllQueryResponse>> Handle(TodoGetAllQuery request, CancellationToken cancellationToken)
    {
        var res = toDoRepository.GetAll()
            .Select(s => new TodoGetAllQueryResponse
            {
                CreatedAt = s.CreatedAt,
                DeletedAt = s.DeletedAt,
                Description = s.Description,
                EndDate = s.EndDate,
                Id = s.Id,
                IsActive = s.IsActive,
                IsDeleted = s.IsDeleted,
                StartDate = s.StartDate,
                Title = s.Title,
                UpdatedAt = s.UpdatedAt
            }).AsQueryable();

        return Task.FromResult(res);
    }
}

public sealed class TodoGetAllQueryResponse : EntityDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}