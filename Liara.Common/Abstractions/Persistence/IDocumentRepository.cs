namespace Liara.Common.Abstractions.Persistence;

public interface IDocumentRepository<TDocument> where TDocument : class
{
    Task<TDocument> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<TDocument> GetByIdAsync(string id, string partitionKeyValue, CancellationToken cancellationToken);
    Task<IEnumerable<TDocument>> GetAsync(string textQuery, string? continuationToken, CancellationToken cancellationToken);
    Task CreateAsync(TDocument item, string partitionKeyValue, CancellationToken cancellationToken);
    Task UpsertAsync(TDocument item, string partitionKeyValue, CancellationToken cancellationToken);
    Task DeleteAsync(string id, string partitionKeyValue, CancellationToken cancellationToken);
}
