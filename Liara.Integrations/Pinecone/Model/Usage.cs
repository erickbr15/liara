using Newtonsoft.Json;

namespace Liara.Integrations.Pinecone;

public class Usage
{
    [JsonProperty("readUnits")]
    public dynamic ReadUnits { get; set; } = default!;
}
