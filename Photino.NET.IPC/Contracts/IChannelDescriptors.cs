namespace Photino.NET.IPC;

public interface IChannelDescriptorCollection
{
    void AddDescriptor(IChannelDescriptor descriptor);
    IReadOnlyCollection<IChannelDescriptor> GetDescriptors();
}
