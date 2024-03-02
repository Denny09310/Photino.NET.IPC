namespace Photino.NET.IPC;

public interface IChannelManager
{
    void CreateChannel<T>(PhotinoWindow window, string channelName, ChannelMessageHandler<T> handler);
    IChannel? GetChannel<T>(string channelName);
}
