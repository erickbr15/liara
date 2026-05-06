using Newtonsoft.Json;

namespace Liara.Integrations.OpenAI.Chat;

public class Message
{
    [JsonProperty("role")]
    public string Role { get; set; } = default!;

    [JsonProperty("content")]
    public string Content { get; set; } = default!;
}
