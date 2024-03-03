namespace Photino.NET.IPC;

/// <summary>
/// Manages communication channels.
/// </summary>
public class ChannelManager : IChannelManager
{
    /// <summary>
    /// Gets the singleton instance of the ChannelManager.
    /// </summary>
    public static readonly ChannelManager Instance = new();

    private readonly Dictionary<string, IChannel> channels = [];

    /// <inheritdoc />
    public void CreateChannel<T>(string channelName, ChannelMessageHandler<T> handler)
    {
        if (!channels.ContainsKey(channelName))
        {
            var channel = new Channel<T>(channelName);
            channel.RegisterMessageHandler(handler);

            channels.Add(channelName, channel);
        }
    }

    /// <inheritdoc />
    public Channel<T> GetChannel<T>(string channelName)
    {
        var channel = channels.TryGetValue(channelName, out var value) ? value : null;

        if (channel is Channel<T> result)
        {
            return result;
        }

        throw new InvalidCastException($"Cannot cast IChannel to Channel<{typeof(T)}>");
    }
}
