namespace Auth.Application.Interfaces;

public interface IAuthDbContext
{

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
