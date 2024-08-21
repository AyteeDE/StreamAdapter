namespace AyteeDE.StreamAdapter.Communication;

public static class SubscribedEventHandler
{
    public static void InvokeSubscribedEvent<T>(EventHandler<T> eventHandler, object sender, T args)
    {
        if(eventHandler != null)
        {
            eventHandler.Invoke(sender, args);
        }
    }
}
