using Microsoft.Extensions.DependencyInjection;

namespace Photino.NET.IPC;

public interface IChannelDescriptor
{
    public void RegisterChannel(PhotinoWindow window);
    public void RegisterChannelServices(IServiceCollection services);
}
