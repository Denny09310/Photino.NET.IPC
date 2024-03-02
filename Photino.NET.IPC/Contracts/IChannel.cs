namespace Photino.NET.IPC;

/// <summary>
/// Represents a communication channel.
/// </summary>
public interface IChannel
{
    /// <summary>
    /// Gets the name of the channel.
    /// </summary>
    string Name { get; }
}

/// <summary>
/// Represents a delegate for handling messages in a channel.
/// </summary>
/// <typeparam name="T">The type of message data.</typeparam>
public delegate void ChannelMessageHandler<T>(Channel<T> channel, Message<T> message);
