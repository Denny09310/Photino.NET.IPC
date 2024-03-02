using System.Text.Json.Serialization;

namespace Photino.NET.IPC;

/// <summary>
/// Represents a message with a key and data.
/// </summary>
/// <typeparam name="T">The type of message data.</typeparam>
public partial class Message<T>
{
    /// <summary>
    /// Gets or sets the key of the message.
    /// </summary>
    [JsonPropertyName("key")]
    public string Key { get; set; } = null!;

    /// <summary>
    /// Gets or sets the data of the message.
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; set; }
}

/// <summary>
/// Event arguments for passing messages.
/// </summary>
/// <typeparam name="T">The type of message data.</typeparam>
/// <remarks>
/// Initializes a new instance of the MessageEventArgs class.
/// </remarks>
/// <param name="message">The message associated with the event.</param>
public class MessageEventArgs<T>(Message<T> message) : EventArgs
{
    /// <summary>
    /// Gets the message associated with the event.
    /// </summary>
    public Message<T> Message { get; } = message;
}
