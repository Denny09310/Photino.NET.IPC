﻿namespace Photino.NET.IPC;

public static class PhotinoWindowExtensions
{
    public static PhotinoWindow RegisterChannel<T>(this PhotinoWindow window, string channelName, ChannelMessageHandler<T> handler)
    {
        ChannelManager.Instance.CreateChannel<T>(window, channelName, handler);

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
