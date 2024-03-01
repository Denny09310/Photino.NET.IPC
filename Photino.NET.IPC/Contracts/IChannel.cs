namespace Photino.NET.IPC;

public interface IChannel
{
    string Name { get; }
}

public delegate void ChannelMessageHandler<T>(Channel<T> channel, Message<T> message);