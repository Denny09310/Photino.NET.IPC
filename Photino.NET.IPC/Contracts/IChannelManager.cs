namespace Photino.NET.IPC;

/// <summary>
/// Manages communication channels.
/// </summary>
public interface IChannelManager
{
    /// <summary>
    /// Creates a new communication channel.
    /// </summary>
    /// <typeparam name="TRequest">The type of request message data.</typeparam>
    /// <param name="channelName">The name of the channel.</param>
    /// <param name="handler">The message handler for the channel.</param>
    void CreateChannel<TRequest>(string channelName, ChannelMessageHandler<TRequest> handler);

    /// <summary>
    /// Gets a communication channel by name.
    /// </summary>
    /// <typeparam name="T">The type of message data.</typeparam>
    /// <param name="channelName">The name of the channel.</param>
    /// <returns>The communication channel.</returns>
    Channel<T> GetChannel<T>(string channelName);
}
