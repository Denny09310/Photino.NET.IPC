namespace Photino.NET.IPC;

// Channel Manager Class
public class InterProcessCommunication
{
    public static readonly InterProcessCommunication Instance = new();

    private readonly Dictionary<string, IChannel> channels = [];

    public void CreateChannel<T>(PhotinoWindow window, string channelName, ChannelMessageHandler<T> handler)
    {
        if (!channels.ContainsKey(channelName))
        {
            var newChannel = new Channel<T>(window, channelName);
            newChannel.MessageReceived += (s, e) => HandleMessage(s, e, handler); // Subscribe to the channel's messages
            channels.Add(channelName, newChannel);
        }
    }

    public IChannel? GetChannel<T>(string channelName)
    {
        return channels.TryGetValue(channelName, out var channel) ? channel : null;
    }

    private void HandleMessage<T>(object? sender, MessageEventArgs<T> e, ChannelMessageHandler<T> handler)
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
