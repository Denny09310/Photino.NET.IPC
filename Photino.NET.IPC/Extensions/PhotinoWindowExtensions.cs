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
    /// <param name="name">The name of the channel.</param>
    /// <param name="handler">The message handler for the channel.</param>
    /// <returns>The updated PhotinoWindow instance.</returns>
    public static PhotinoWindow RegisterChannel<T>(this PhotinoWindow window, string name, ChannelMessageHandler<T> handler)
    {
        ChannelManager.Instance.CreateChannel<T>(name, handler);

        // Register the handler for messages from the renderer
        return window.RegisterWebMessageReceivedHandler((sender, message) =>
        {
            var result = ChannelManager.Instance.GetChannel<T>(name);

            if (result is Channel<T> channel)
            {
                channel.ReceiveMessage(sender, message);
            }
        });
    }
}
