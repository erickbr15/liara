namespace Liara.Integrations.Azure;

public class AIServiceConnectionOptions
{
    public string Key { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string Endpoint { get; set; } = default!;
}
