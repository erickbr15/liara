

using Newtonsoft.Json;

namespace Liara.Integrations.Pinecone;

public class UpsertDataModel
{
    [JsonProperty("vectors")]
    public IList<Vector> Vectors { get; set; } = default!;

    [JsonProperty("namespace")]
    public string Namespace { get; set; } = default!;
}
