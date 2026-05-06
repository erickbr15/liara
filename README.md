# Liara

`Liara` is a set of **.NET class libraries** that provide:

- **Common building blocks** (`Liara.Common`): lightweight result types and an `HttpProxy` abstraction.
- **Integration clients** (`Liara.Integrations`): opinionated, DI-friendly clients for **OpenAI**, **Pinecone**, and a small **Azure** integration surface (Document Intelligence + Storage clients).

This repository is a **library repo** (no executable app). You consume it from your own application via dependency injection and configuration.

## Projects

- `Liara.Common`
  - Target framework: `net10.0`
  - Key pieces:
    - `Liara.Common.Extensions.AppRootExtensions.AddLiaraCommonServices()`
    - `Liara.Common.Http.IHttpProxy` + `Liara.Common.Http.HttpProxy`
    - `Liara.Common.Result` / `Liara.Common.GenericResult<T>` (result + validation patterns)
- `Liara.Integrations`
  - Target framework: `net10.0`
  - Key pieces:
    - `Liara.Integrations.Extensions.AppRootExtensions` for wiring integrations into DI
    - `OpenAIChatService` / `OpenAIEmbeddingsService`
    - `PineconeService`
    - Azure client wiring for `DocumentAnalysisClient`, `BlobServiceClient`, and `QueueServiceClient`

## Requirements

- **.NET SDK** capable of building `net10.0` (a .NET 10 preview SDK may be required).

## Build

From the repo root:

```bash
dotnet restore
dotnet build -c Release
```

### Troubleshooting restore failures (private feeds)

If `dotnet restore` fails with a `401 (Unauthorized)` against an Azure DevOps NuGet feed (for example `NU1301`), it usually means you have a **global NuGet source** configured on your machine that requires authentication.

Options:

- Restore using only public NuGet.org:

```bash
dotnet restore --source "https://api.nuget.org/v3/index.json"
```

- Or ignore failing sources (useful if a private feed is temporarily unavailable):

```bash
dotnet restore --ignore-failed-sources
```

## Pack (NuGet)

Both projects are configured as packable libraries. To create NuGet packages locally:

```bash
dotnet pack -c Release
```

Packages will be placed under each project’s `bin/Release/` folder.

## Usage

### Register services (DI)

In your application startup (e.g. `Program.cs` in an ASP.NET Core app):

```csharp
using Liara.Common.Extensions;
using Liara.Integrations.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLiaraCommonServices();
builder.Services.AddLiaraOpenAIServices();
builder.Services.AddLiaraPineconeServices();
builder.Services.AddLiaraAzureServices(builder.Configuration);

var app = builder.Build();
app.Run();
```

### Configuration

Liara uses the options pattern with these configuration sections:

- `OpenAI` → `OpenAIOptions`
- `Pinecone` → `PineconeOptions`
- `AzureAI` → `AIServiceConnectionOptions`
- `ConnectionStrings` → used to register Azure Storage clients (filtered by keys that start with `AzureStorage`)

Example `appsettings.json`:

```json
{
  "OpenAI": {
    "ApiKey": "<your-openai-api-key>",
    "ChatEndpointUrl": "https://api.openai.com/v1/chat/completions",
    "EmbeddingsEndpointUrl": "https://api.openai.com/v1/embeddings",
    "ChatGptModel": "gpt-4.1-mini",
    "EmbeddingsModel": "text-embedding-3-small"
  },
  "Pinecone": {
    "ApiKey": "<your-pinecone-api-key>",
    "IndexHostUrl": "https://<index-host>.svc.<region>.pinecone.io"
  },
  "AzureAI": {
    "Key": "<your-azure-ai-key>",
    "Location": "<region>",
    "Endpoint": "https://<resource-name>.cognitiveservices.azure.com/"
  },
  "ConnectionStrings": {
    "AzureStoragePrimary": "<storage-connection-string>",
    "AzureStorageSecondary": "<storage-connection-string>"
  }
}
```

Notes:

- **Azure Storage clients**: `AddLiaraAzureServices()` registers blob + queue service clients for every `ConnectionStrings` entry whose key starts with `AzureStorage` (case-insensitive). Each is registered with `.WithName(connectionStringKey)`.
- **Azure Document Intelligence**: `AddLiaraAzureServices()` registers a `DocumentAnalysisClient` using `AzureAI:Endpoint` and `AzureAI:Key`.

## OpenAI

### Chat completions

`IOpenAIChatService.CreateChatCompletionAsync(...)` posts to `OpenAI:ChatEndpointUrl` using bearer auth and the configured `OpenAI:ChatGptModel`.

Minimal example:

```csharp
using Liara.Integrations.OpenAI;
using Liara.Integrations.OpenAI.Chat;

var chat = serviceProvider.GetRequiredService<IOpenAIChatService>();

var messages = new[]
{
    new Message { Role = Types.User, Content = "Say hello from Liara." }
};

var input = new ChatCompletionCreationModel
{
    // Additional request parameters live here (temperature, tools, etc.)
};

var result = await chat.CreateChatCompletionAsync(messages, input, CancellationToken.None);
```

### Embeddings

`IOpenAIEmbeddingsService.CreateEmbeddingsAsync(...)` posts to `OpenAI:EmbeddingsEndpointUrl` using bearer auth and the configured `OpenAI:EmbeddingsModel`.

## Pinecone

`IPineconeService` provides:

- `UpsertAsync(...)` → `POST {Pinecone:IndexHostUrl}/vectors/upsert`
- `QueryVectorsAsync(...)` → `POST {Pinecone:IndexHostUrl}/query`

API key is sent as header `Api-Key`.

## Azure

`AddLiaraAzureServices(...)` wires up:

- `Azure.AI.FormRecognizer.DocumentAnalysisClient` (Document Intelligence)
- `Azure.Storage.Blobs.BlobServiceClient` (named clients)
- `Azure.Storage.Queues.QueueServiceClient` (named clients)

## Development notes

- **HTTP behavior**: `Liara.Common.Http.HttpProxy` creates a new `HttpClient` per request and calls `EnsureSuccessStatusCode()`. If you need resiliency, retries, logging, or a shared client factory, consider providing an alternative `IHttpProxy` implementation in your application.
- **Nullable reference types**: enabled across the projects.

## Repository structure

```text
.
├─ liara.slnx
├─ Liara.Common/
└─ Liara.Integrations/
```

