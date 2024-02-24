namespace Photino.NET.IPC;

public class PhotinoChannel(PhotinoWindow owner, string channelKey)
{
    private readonly string _key = channelKey;
    private readonly PhotinoWindow _owner = owner;

    public string Name => _key;
    public PhotinoWindow Window => _owner;

    public void Emit<T>(T message) where T : class => _owner.SendMessage(_key, new PhotinoPayload<T>(_key, message));
}