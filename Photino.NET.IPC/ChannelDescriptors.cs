
namespace Photino.NET.IPC;

public class ChannelDescriptors : IChannelDescriptors
{
    public static readonly ChannelDescriptors Instance = new();

    private readonly List<IChannelDescriptor> _descriptors = [];

    public void AddDescriptor(IChannelDescriptor descriptor) => _descriptors.Add(descriptor);

    public IReadOnlyCollection<IChannelDescriptor> GetDescriptors() => _descriptors;
}
