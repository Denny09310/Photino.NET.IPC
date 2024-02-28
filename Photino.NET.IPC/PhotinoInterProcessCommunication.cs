using System.Collections.Concurrent;

namespace Photino.NET.IPC;

public delegate void IPCEventHandler<TEventArgs>(PhotinoChannel sender, TEventArgs? args);

public class PhotinoInterProcessCommunication
{
    public static readonly PhotinoInterProcessCommunication Instance = new();

    public ConcurrentDictionary<string, PhotinoChannel> Channels { get; } = new();

    public void Subscribe(string key, Action<object> handler)
    {
        ValidateChannelKey(key);
        Channels[key].Subscribe(handler);
    }

    public void Unsubscribe(string key, Action<object> handler)
    {
        ValidateChannelKey(key);
        Channels[key].Unsubscribe(handler);
    }

    public void ValidateChannelKey(string key)
    {
        if (!Channels.ContainsKey(key))
        {
            throw new InvalidOperationException($"Channel with key {key} does not exist");
        }
    }
}