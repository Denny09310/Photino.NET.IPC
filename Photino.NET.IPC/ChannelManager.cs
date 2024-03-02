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
            var newChannel = new Channel<T>(channelName);
            newChannel.MessageReceived += (s, e) => HandleMessage(e, handler);
            channels.Add(channelName, newChannel);
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

    private void HandleMessage<T>(MessageEventArgs<T> e, ChannelMessageHandler<T> handler)
    {
        if (e.Message.Key is string channelName)
        {
            var channel = GetChannel<T>(channelName);

            if (channel is Channel<T> typedChannel)
            {
                handler.Invoke(typedChannel, e.Message);
            }
        }
    }
}
