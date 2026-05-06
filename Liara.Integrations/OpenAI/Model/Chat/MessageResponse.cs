using Newtonsoft.Json;

namespace Liara.Integrations.OpenAI.Chat;

public sealed class MessageResponse : Message
{
    [JsonProperty("tool_calls")]
    public IList<ToolCall> ToolCalls { get; set; } = new List<ToolCall>();
}
