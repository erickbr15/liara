namespace Liara.Integrations.Azure;

public class StorageAccountOptions
{
    public IDictionary<string, string> BlobContainers { get; set; } = new Dictionary<string, string>();
    public IDictionary<string, string> QueueNames { get; set; } = new Dictionary<string, string>();
}
