using System.Text.Json.Serialization;

namespace Photino.NET.IPC;

// Message Class
public partial class Message<T>
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = null!;

    [JsonPropertyName("data")]
    public T? Data { get; set; }
}


// Custom EventArgs for passing messages
public class MessageEventArgs<T>(Message<T> message) : EventArgs
{
    public Message<T> Message { get; } = message;
}
