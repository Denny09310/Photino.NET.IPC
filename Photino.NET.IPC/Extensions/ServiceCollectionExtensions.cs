using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Photino.NET.IPC;

/// <summary>
/// Extension methods for configuring dependency injection related to inter-process communication.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds inter-process communication services to the dependency injection container.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The updated IServiceCollection instance.</returns>
    public static IServiceCollection AddInterProcessCommunication(this IServiceCollection services, params Assembly[] assemblies)
    {
        var descriptors = assemblies.SelectMany(assembly => assembly.DefinedTypes)
            .Where(type => !type.IsInterface && typeof(IChannelDescriptor).IsAssignableFrom(type))
            .Select(Activator.CreateInstance)
            .Cast<IChannelDescriptor>();

        foreach (var descriptor in descriptors)
        {
            descriptor.RegisterChannelServices(services);
            ChannelDescriptors.Instance.AddDescriptor(descriptor);
        }

        return services.AddSingleton<IChannelManager>(ChannelManager.Instance)
                       .AddSingleton<IChannelDescriptors>(ChannelDescriptors.Instance);
    }
}
