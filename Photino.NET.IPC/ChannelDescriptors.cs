
namespace Photino.NET.IPC;

public class ChannelDescriptorCollection : IChannelDescriptorCollection
{
    public static readonly ChannelDescriptorCollection Instance = new();

    private readonly List<IChannelDescriptor> _descriptors = [];

    public void AddDescriptor(IChannelDescriptor descriptor) => _descriptors.Add(descriptor);

    public IReadOnlyCollection<IChannelDescriptor> GetDescriptors() => _descriptors;
}
