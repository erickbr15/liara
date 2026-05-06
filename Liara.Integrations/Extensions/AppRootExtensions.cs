using Azure;
using Liara.Integrations.Azure;
using Liara.Integrations.OpenAI;
using Liara.Integrations.Pinecone;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Liara.Integrations.Extensions;

public static class AppRootExtensions
{
    public static void AddLiaraOpenAIServices(this IServiceCollection services)
    {
        services.AddOptions<OpenAIOptions>().BindConfiguration("OpenAI");

        services.AddSingleton<IOpenAIEmbeddingsService, OpenAIEmbeddingsService>();
        services.AddSingleton<IOpenAIChatService, OpenAIChatService>();
    }

    public static void AddLiaraPineconeServices(this IServiceCollection services)
    {
        services.AddOptions<PineconeOptions>().BindConfiguration("Pinecone");

        services.AddSingleton<IPineconeService, PineconeService>();
    }

    public static void AddLiaraAzureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var aiServiceConnectionOptions = new AIServiceConnectionOptions();
        configuration.Bind("AzureAI", aiServiceConnectionOptions);

        services.AddAzureClients(builder =>
        {
            builder.AddDocumentAnalysisClient(new Uri(aiServiceConnectionOptions.Endpoint), new AzureKeyCredential(aiServiceConnectionOptions.Key));
        });

        services.AddBlobServiceClients(configuration);
        services.AddQueueServiceClients(configuration);
    }

    private static void AddBlobServiceClients(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = configuration.GetSection("ConnectionStrings")
            .Get<Dictionary<string, string>>()!
            .Where(x => x.Key.StartsWith("AzureStorage", StringComparison.OrdinalIgnoreCase));

        services.AddAzureClients(builder =>
        {
            foreach (var connectionString in connectionStrings)
            {
                builder.AddBlobServiceClient(connectionString.Value)
                    .WithName(connectionString.Key);
            }
        });
    }

    private static void AddQueueServiceClients(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = configuration.GetSection("ConnectionStrings")
            .Get<Dictionary<string, string>>()!
            .Where(x => x.Key.StartsWith("AzureStorage", StringComparison.OrdinalIgnoreCase));

        services.AddAzureClients(builder =>
        {
            foreach (var connectionString in connectionStrings)
            {
                builder.AddQueueServiceClient(connectionString.Value)
                    .WithName(connectionString.Key);
            }
        });
    }
}
