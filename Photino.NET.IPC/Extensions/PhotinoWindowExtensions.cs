namespace Photino.NET.IPC;

/// <summary>
/// Extension methods for PhotinoWindow related to inter-process communication.
/// </summary>
public static class PhotinoWindowExtensions
{
    /// <summary>
    /// Registers a new communication channel for the PhotinoWindow.
    /// </summary>
    /// <typeparam name="T">The type of message data for the channel.</typeparam>
    /// <param name="window">The PhotinoWindow instance.</param>
    /// <param name="channelName">The name of the channel.</param>
    /// <param name="handler">The message handler for the channel.</param>
    /// <returns>The updated PhotinoWindow instance.</returns>
    public static PhotinoWindow RegisterChannel<T>(this PhotinoWindow window, string channelName, ChannelMessageHandler<T> handler)
    {
        ChannelManager.Instance.CreateChannel<T>(channelName, handler);

        // Register the handler for messages from the renderer
        return window.RegisterWebMessageReceivedHandler((sender, message) =>
        {
            var channel = ChannelManager.Instance.GetChannel<T>(channelName);

            if (channel is Channel<T> typedChannel)
            {
                typedChannel.ReceiveMessageFromRenderer(sender, message);
            }
        });
    }
}
