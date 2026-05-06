using Newtonsoft.Json;

namespace Liara.Integrations.Pinecone;

public class Match
{
    [JsonProperty("id")]
    public string Id { get; set; } = default!;

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("values")]
    public IList<float> Values { get; set; } = default!;

    [JsonProperty("metadata")]
    public IDictionary<string, object> Metadata { get; set; } = default!;
 
}
