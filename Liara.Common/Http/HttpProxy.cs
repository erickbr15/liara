namespace Liara.Common.Http;

public class HttpProxy : IHttpProxy
{
    public Task<HttpResponseMessage> DeleteAsync(Uri uri, IDictionary<string, string> headers, CancellationToken cancellationToken)
    {
        var response = SendAsync(HttpMethod.Delete, uri, headers, content: null, cancellationToken);
        return response;
    }

    public Task<HttpResponseMessage> GetAsync(Uri uri, IDictionary<string, string> headers, CancellationToken cancellationToken)
    {
        var response = SendAsync(HttpMethod.Get, uri, headers, content: null, cancellationToken);
        return response;
    }

    public Task<HttpResponseMessage> PatchAsync(Uri uri, IDictionary<string, string> headers, HttpContent? content, CancellationToken cancellationToken)
    {
        var response = SendAsync(HttpMethod.Patch, uri, headers, content, cancellationToken);
        return response;
    }

    public async Task<HttpResponseMessage> PostAsync(Uri uri, IDictionary<string, string> headers, HttpContent? content, CancellationToken cancellationToken)
    {
        var response = await SendAsync(HttpMethod.Post, uri, headers, content, cancellationToken);
        return response;
    }

    public Task<HttpResponseMessage> PutAsync(Uri uri, IDictionary<string, string> headers, HttpContent? content, CancellationToken cancellationToken)
    {
        var response = SendAsync(HttpMethod.Put, uri, headers, content, cancellationToken);
        return response;
    }

    private async Task<HttpResponseMessage> SendAsync(HttpMethod httpMethod, Uri uri, IDictionary<string, string> headers, HttpContent? content, CancellationToken cancellationToken)
    {
        HttpResponseMessage? response = null;

        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage())
        {
            request.Method = httpMethod;
            request.RequestUri = uri;

            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            if (content != null)
            {
                request.Content = content;
            }

            response = await client.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        return response;
    }        
}
