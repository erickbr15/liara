using Liara.Integrations.OpenAI.Chat;

namespace Liara.Integrations.OpenAI;

public interface IOpenAIChatService
{
    Task<ChatCompletionResponse?> CreateChatCompletionAsync(IEnumerable<Message> messages, ChatCompletionCreationModel inputModel, CancellationToken cancellationToken);
}
