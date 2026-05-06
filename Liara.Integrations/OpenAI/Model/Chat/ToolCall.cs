using Newtonsoft.Json;

namespace Liara.Integrations.OpenAI.Chat;

public class ToolCall
{
    [JsonProperty("id")]
    public string Id { get; set; } = default!;

    [JsonProperty("type")]
    public string Type { get; set; } = default!;

    [JsonProperty("function")]
    public ToolCallFunction Function { get; set; } = default!;
}
