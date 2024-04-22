namespace Photino.NET.IPC;

public interface IChannelDescriptors
{
    void AddDescriptor(IChannelDescriptor descriptor);
    IReadOnlyCollection<IChannelDescriptor> GetDescriptors();
}
