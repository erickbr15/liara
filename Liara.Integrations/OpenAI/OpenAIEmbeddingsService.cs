using Liara.Common.Http;
using Liara.Integrations.OpenAI.Embeddings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Liara.Integrations.OpenAI;

public class OpenAIEmbeddingsService : IOpenAIEmbeddingsService
{
    private readonly IHttpProxy _httpProxy;
    private readonly OpenAIOptions _openAiOptions;

    public OpenAIEmbeddingsService(IOptions<OpenAIOptions> optionsService, IHttpProxy httpProxy)
    {
        _openAiOptions = optionsService?.Value ?? throw new ArgumentNullException(nameof(optionsService));
        _httpProxy = httpProxy ?? throw new ArgumentNullException(nameof(httpProxy));
    }

    public async Task<CreateEmbeddingResponse?> CreateEmbeddingsAsync(CreateEmbeddingsModel inputModel, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var headers = new Dictionary<string, string>
        {
            { "Authorization", $"Bearer {_openAiOptions.ApiKey}" }
        };

        var requestBody = new CreateEmbeddingsBodyBuilder().NewWithDefaults(_openAiOptions.EmbeddingsModel, inputModel.User, inputModel.Input).Build();

        string content = JsonConvert.SerializeObject(requestBody);

        var bodyContent = new StringContent(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(content)), Encoding.UTF8, "application/json");

        var response = await _httpProxy.PostAsync(new Uri(_openAiOptions.EmbeddingsEndpointUrl),
            headers,
            bodyContent,
            cancellationToken);

        var embeddings = await response.Content.ReadFromJsonAsync<dynamic>(cancellationToken: cancellationToken);

        var embeddingsArrayText = embeddings?.GetProperty("data").ToString();

        var embeddingResponse = new CreateEmbeddingResponse
        {
            Data = JsonConvert.DeserializeObject<List<Embedding>>(embeddingsArrayText)
        };

        return embeddingResponse;
    }
}
