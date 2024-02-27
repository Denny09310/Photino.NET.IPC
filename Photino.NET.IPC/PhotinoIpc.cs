using System.Collections.Concurrent;

namespace Photino.NET.IPC;

public class PhotinoIPCService
{
    public static readonly PhotinoIPCService Instance = new();

    public ConcurrentDictionary<string, PhotinoChannel> Channels { get; } = new();

    public delegate void IPCEventHandler<TEventArgs>(PhotinoChannel sender, TEventArgs? args);

    public void ValidateChannelKey(string key)
    {
        if (!Channels.ContainsKey(key))
        {
            throw new InvalidOperationException($"Channel with key {key} does not exist");
        }
    }
}