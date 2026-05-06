using Liara.Integrations.OpenAI.Embeddings;

namespace Liara.Integrations.OpenAI;

public interface IOpenAIEmbeddingsService
{
    Task<CreateEmbeddingResponse?> CreateEmbeddingsAsync(CreateEmbeddingsModel inputModel, CancellationToken cancellationToken);
}
