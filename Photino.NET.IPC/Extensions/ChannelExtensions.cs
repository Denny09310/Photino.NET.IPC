namespace Photino.NET.IPC;

public static class ChannelExtensions
{
    public static Channel<T> Parse<T>(this IChannel? channel)
    {
        if (channel is Channel<T> result)
        {
            return result;
        }

        throw new InvalidCastException($"Cannot cast IChannel to Channel<{typeof(T)}>");
    }
}
