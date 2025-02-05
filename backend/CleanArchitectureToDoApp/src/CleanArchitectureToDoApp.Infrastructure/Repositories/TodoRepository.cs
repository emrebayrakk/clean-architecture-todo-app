using CleanArchitectureToDoApp.Domain.ToDos;
using CleanArchitectureToDoApp.Infrastructure.Context;
using GenericRepository;

namespace CleanArchitectureToDoApp.Infrastructure.Repositories;
internal sealed class TodoRepository : Repository<ToDo,ApplicationDbContext> , IToDoRepository
{
    public TodoRepository(ApplicationDbContext context) : base(context)
    {
    }
}
