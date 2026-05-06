using Newtonsoft.Json;

namespace Liara.Integrations.OpenAI.Chat;

public class ToolChoiceFunction
{
    [JsonProperty("name")]
    public string Name { get; set; } = default!;
}
