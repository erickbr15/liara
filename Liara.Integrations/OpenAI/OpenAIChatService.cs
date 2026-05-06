using Liara.Common.Http;
using Liara.Integrations.OpenAI.Chat;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Liara.Integrations.OpenAI;

public class OpenAIChatService : IOpenAIChatService
{
    private readonly IHttpProxy _httpProxy;
    private readonly OpenAIOptions _openAiOptions;

    public OpenAIChatService(IOptions<OpenAIOptions> optionsService, IHttpProxy httpProxy)
    {
        _openAiOptions = optionsService?.Value ?? throw new ArgumentNullException(nameof(optionsService));
        _httpProxy = httpProxy ?? throw new ArgumentNullException(nameof(httpProxy));
    }

    public async Task<ChatCompletionResponse?> CreateChatCompletionAsync(IEnumerable<Message> messages, ChatCompletionCreationModel inputModel, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var headers = new Dictionary<string, string>
        {
            { "Authorization", $"Bearer {_openAiOptions.ApiKey}" }
        };

        inputModel.Model = _openAiOptions.ChatGptModel;

        var chatCompletionBody = new ChatCompletionBodyBuilder().NewWith(messages, inputModel).Build();
        
        string content = JsonConvert.SerializeObject(chatCompletionBody);

        var bodyContent = new StringContent(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(content)), Encoding.UTF8, "application/json");        

        var response = await _httpProxy.PostAsync(new Uri(_openAiOptions.ChatEndpointUrl),
            headers,
            bodyContent,
            cancellationToken);

        var completionResponse = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>(cancellationToken: cancellationToken);

        return completionResponse;
    }    
}
