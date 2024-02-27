using Photino.NET.IPC.Extensions;

namespace Photino.NET.IPC;

public class PhotinoChannel(PhotinoWindow owner, string channelKey)
{
    private readonly string _key = channelKey;
    private readonly PhotinoWindow _owner = owner;

    /*  
        TODO: Make it typesafe, problem relies on the ConcurrentDictionary, 
        because we don't know what is the type of the payload at compile time 
    */
    private readonly List<Action<object>> _subscribers = [];

    public string Name => _key;
    public PhotinoWindow Window => _owner;

    public void Subscribe(Action<object> handler)
    {
        _subscribers.Add(handler);
    }

    public void Unsubscribe(Action<object> handler)
    {
        _subscribers.Remove(handler);
    }

    public void Emit<T>(T message) where T : class
    {
        _owner.SendMessage(_key, new PhotinoPayload<T>(_key, message));

        foreach (var subscriber in _subscribers)
        {
            subscriber.Invoke(message);
        }
    }
}
