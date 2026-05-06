namespace Liara.Common.Abstractions.Persistence;

public interface IUnitOfWork
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
