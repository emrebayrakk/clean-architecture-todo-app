using CleanArchitectureToDoApp.Domain.Abstractions;

namespace CleanArchitectureToDoApp.Domain.ToDos;
public sealed class ToDo : Entity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}
