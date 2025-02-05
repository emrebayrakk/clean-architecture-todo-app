using CleanArchitectureToDoApp.Domain.ToDos;
using FluentValidation;
using GenericRepository;
using Mapster;
using MediatR;
using TS.Result;

namespace CleanArchitectureToDoApp.Application.ToDos.Commands;
public sealed record TodoCreateCommand(
    string Title,
    string Description,
    DateOnly StartDate,
    DateOnly? EndDate) : IRequest<Result<string>>;

internal sealed class TodoCreateCommandHandler(
    IToDoRepository toDoRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<TodoCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(TodoCreateCommand request, CancellationToken cancellationToken)
    {
        ToDo toDo = request.Adapt<ToDo>();
        toDoRepository.Add(toDo);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return toDo.Id.ToString();

    }
}
public sealed class TodoCreateCommandValidator : AbstractValidator<TodoCreateCommand>
{
    public TodoCreateCommandValidator()
    {
        RuleFor(s => s.Title).NotEmpty().MaximumLength(100);
        RuleFor(s => s.Description).NotEmpty().MaximumLength(500);
        RuleFor(s => s.StartDate).NotEmpty();
    }
}
