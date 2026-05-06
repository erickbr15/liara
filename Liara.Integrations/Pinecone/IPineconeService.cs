namespace Liara.Integrations.Pinecone;

public interface IPineconeService
{
    Task UpsertAsync(UpsertDataModel inputModel, CancellationToken cancellationToken);

    Task<QueryVectorsResult?> QueryVectorsAsync(QueryVectorsModel inputModel, CancellationToken cancellationToken);
}
