using Newtonsoft.Json;

namespace Liara.Integrations.OpenAI.Chat;

public class ResponseFormat
{
    [JsonProperty("type")]
    public string Type { get; set; } = default!;
}
