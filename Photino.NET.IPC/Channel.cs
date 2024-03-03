using System.Text.Json;

namespace Photino.NET.IPC;

/// <summary>
/// Represents a communication channel with specific message data type.
/// </summary>
/// <typeparam name="TRequest">The type of request message data.</typeparam>
/// <remarks>
/// Initializes a new instance of the Channel class.
/// </remarks>
/// <param name="name">The name of the channel.</param>
public class Channel<TRequest>(string name) : IChannel
{
    private PhotinoWindow? window;

    /// <inheritdoc />
    public string Name { get; private set; } = name;

    /// <summary>
    /// Event triggered when a message is received on the channel.
    /// </summary>
    public event EventHandler<MessageEventArgs<TRequest>>? MessageReceived;

    /// <inheritdoc />
    public void Emit<TResponse>(TResponse response)
    {
        var message = new Message<TResponse> { Key = Name, Data = response };
        SendMessage(message);
    }

    /// <inheritdoc />
    public void ReceiveMessage(object? sender, string message)
    {
        if (sender is not PhotinoWindow) return;

        window = (PhotinoWindow)sender;

        // Parse the incoming message
        var receivedMessage = JsonSerializer.Deserialize<Message<TRequest>>(message);

        // Check if the message type matches the channel name
        if (receivedMessage?.Key == Name)
        {
            // Raise the MessageReceived event
            OnMessageReceived(receivedMessage);
        }
    }

    /// <summary>
    /// Raises the MessageReceived event.
    /// </summary>
    /// <param name="message">The received message.</param>
    protected virtual void OnMessageReceived(Message<TRequest> message)
    {
        MessageReceived?.Invoke(this, new MessageEventArgs<TRequest>(message));
    }

    /// <summary>
    /// Registers a message handler for the channel.
    /// </summary>
    /// <param name="handler">The message handler to register.</param>
    public void RegisterMessageHandler(ChannelMessageHandler<TRequest> handler)
    {
        MessageReceived += (s, e) => HandleMessage(e, handler);
    }

    private void HandleMessage(MessageEventArgs<TRequest> e, ChannelMessageHandler<TRequest> handler)
    {
        handler.Invoke(this, e.Message);
    }

    private void SendMessage<TResponse>(Message<TResponse> message)
    {
        var response = JsonSerializer.Serialize(message);
        window?.SendWebMessage(response);
    }
}
