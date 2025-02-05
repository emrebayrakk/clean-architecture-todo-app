using CleanArchitectureToDoApp.Domain.Abstractions;
using CleanArchitectureToDoApp.Domain.ToDos;
using GenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CleanArchitectureToDoApp.Infrastructure.Context;
internal sealed class ApplicationDbContext : DbContext,IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<ToDo> ToDos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreatedAt)
                    .CurrentValue = DateTimeOffset.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Property(p => p.IsDeleted).CurrentValue == true)
                {
                    entry.Property(p => p.DeletedAt)
                    .CurrentValue = DateTimeOffset.Now;
                }
                else
                {
                    entry.Property(p => p.UpdatedAt)
                        .CurrentValue = DateTimeOffset.Now;
                }
            }

            if (entry.State == EntityState.Deleted)
            {
                throw new ArgumentException("Db'den direkt silme işlemi yapamazsınız");
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
