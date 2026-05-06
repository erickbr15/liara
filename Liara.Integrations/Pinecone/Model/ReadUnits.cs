using Newtonsoft.Json;

namespace Liara.Integrations.Pinecone;

public class ReadUnits
{
    [JsonProperty("read_units")]
    public long ReadUnitsValue { get; set; }
}
