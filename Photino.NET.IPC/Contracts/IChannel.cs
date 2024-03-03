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

    /// <summary>
    /// Sends a response to the renderer.
    /// </summary>
    /// <typeparam name="TResponse">The type of response message data.</typeparam>
    /// <param name="response">The response data.</param>
    void Emit<TResponse>(TResponse response);
}

/// <summary>
/// Represents a delegate for handling messages in a channel.
/// </summary>
/// <typeparam name="T">The type of message data.</typeparam>
public delegate void ChannelMessageHandler<T>(IChannel channel, Message<T> message);
