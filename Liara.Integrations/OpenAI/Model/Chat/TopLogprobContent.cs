using Newtonsoft.Json;

namespace Liara.Integrations.OpenAI.Chat;

public class TopLogprobContent
{
    [JsonProperty("token")]
    public string Token { get; set; } = default!;

    [JsonProperty("logprob")]
    public int Logprob { get; set; }

    [JsonProperty("bytes")]
    public int[] Bytes { get; set; } = default!;
}
