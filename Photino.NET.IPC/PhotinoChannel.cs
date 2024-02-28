using Photino.NET.Extensions;

namespace Photino.NET.IPC;

public class PhotinoChannel(PhotinoWindow owner, string key)
{
    /*  
        TODO: Make it typesafe, problem relies on the ConcurrentDictionary, 
        because we don't know what is the type of the payload at compile time 
    */
    private readonly List<Action<object>> _subscribers = [];

    public string Name => key;
    public PhotinoWindow Window => owner;

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
        owner.SendMessage(key, new PhotinoPayload<T>(key, message));

        foreach (var subscriber in _subscribers)
        {
            subscriber.Invoke(message);
        }
    }
}
