using Liara.Common.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Liara.Common.Extensions;

public static class AppRootExtensions
{
    public static void AddLiaraCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpProxy, HttpProxy>();
    }
}
