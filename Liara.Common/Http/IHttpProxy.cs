namespace Liara.Common.Http;

public interface IHttpProxy
{
    Task<HttpResponseMessage> GetAsync(Uri uri, IDictionary<string, string> headers, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PostAsync(Uri uri, IDictionary<string, string> headers, HttpContent? content, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PutAsync(Uri uri, IDictionary<string, string> headers, HttpContent? content, CancellationToken cancellationToken);
    Task<HttpResponseMessage> DeleteAsync(Uri uri, IDictionary<string, string> headers, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PatchAsync(Uri uri, IDictionary<string, string> headers, HttpContent? content, CancellationToken cancellationToken);
}
