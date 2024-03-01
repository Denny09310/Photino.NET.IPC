using System.Text.Json;

namespace Photino.NET.IPC;

// Channel Class
public class Channel<T>(PhotinoWindow window, string name) : IChannel
{
    public event EventHandler<MessageEventArgs<T>>? MessageReceived;

    public string Name { get; private set; } = name;
    public PhotinoWindow Window { get; set; } = window;

    public void SendMessageToRenderer(Message<T> message)
    {
        var response = JsonSerializer.Serialize(message);
        Window.SendWebMessage(response);
    }

    public void ReceiveMessageFromRenderer(object? sender, string message)
    {
        if (sender is not PhotinoWindow window) return;

        // Parse the incoming message
        var receivedMessage = JsonSerializer.Deserialize<Message<T>>(message);

        // Check if the message type matches the channel name
        if (receivedMessage?.Key == Name)
        {
            // Raise the MessageReceived event
            OnMessageReceived(receivedMessage);
        }
    }

    protected virtual void OnMessageReceived(Message<T> message)
    {
        MessageReceived?.Invoke(this, new MessageEventArgs<T>(message));
    }
}
