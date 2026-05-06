using Newtonsoft.Json;

namespace Liara.Integrations.OpenAI.Chat;

public class Logprob
{
    [JsonProperty("content")]
    public IList<LogprobContent>? Content { get; set; }
}
