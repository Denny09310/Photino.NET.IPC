using Microsoft.Extensions.DependencyInjection;

namespace Photino.NET.IPC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInterProcessCommunication(this IServiceCollection services) =>
        services.AddSingleton(PhotinoInterProcessCommunication.Instance);
}
