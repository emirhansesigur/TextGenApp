using Auth.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Interfaces;

public interface IAuthDbContext
{
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
