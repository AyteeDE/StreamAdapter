namespace AyteeDE.StreamAdapter.Core.Communication;

public static class SubscribedEventHandler
{
    public static void InvokeSubscribedEvent<T>(EventHandler<T> eventHandler, object sender, T args)
    {
        if(eventHandler != null)
        {
            eventHandler.Invoke(sender, args);
        }
    }
    public static void InvokeSubscribedEvent(EventHandler eventHandler, object sender)
    {
        if(eventHandler != null)
        {
            eventHandler.Invoke(sender, new EventArgs());
        }
    }
}
