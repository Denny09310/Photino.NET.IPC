
using Microsoft.Extensions.DependencyInjection;
using Photino.NET.IPC;

namespace Photino.NET.Extensions;

public static class PhotinoExtensions
{
    public static PhotinoWindow RegisterChannelHandler<T>(this PhotinoWindow window, string key, IPCEventHandler<T> handler) where T : class
    {
        var ipc = PhotinoInterProcessCommunication.Instance;

        ipc.Channels.TryAdd(key, new PhotinoChannel(window, key));

        return window.RegisterWebMessageReceivedHandler((sender, data) =>
        {
            if (!PhotinoPayload<T>.TryFromJson(data, out var payload)) return;

            if (payload!.Key != key) return;

            if (!ipc.Channels.TryGetValue(key, out var channel)) return;

            handler?.Invoke(channel, payload.Data);
        });
    }

    public static void SendMessage<T>(this PhotinoWindow window, string key, T message) where T : class
    {
        PhotinoInterProcessCommunication.Instance.ValidateChannelKey(key);
        window.SendWebMessage(PhotinoPayload<T>.ToJson(message));
    }
}