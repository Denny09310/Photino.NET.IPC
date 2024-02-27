
using Microsoft.Extensions.DependencyInjection;

namespace Photino.NET.IPC.Extensions;

public static class PhotinoIPCExtensions
{
    public static IServiceCollection AddIPC(this IServiceCollection services) =>
        services.AddSingleton(PhotinoIPCService.Instance);

    public static PhotinoWindow RegisterChannelHandler<T>(this PhotinoWindow window, string key, PhotinoIPCService.IPCEventHandler<T> handler) where T : class
    {
        var ipc = PhotinoIPCService.Instance;

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
        PhotinoIPCService.Instance.ValidateChannelKey(key);
        window.SendWebMessage(PhotinoPayload<T>.ToJson(message));
    }
}