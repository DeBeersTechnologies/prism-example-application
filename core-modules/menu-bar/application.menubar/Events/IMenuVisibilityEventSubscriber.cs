namespace application.menubar.Events;

public interface IMenuVisibilityEventSubscriber
{
    void SetEventSubscription(Action<IMenuVisibilityEventArgs> eventArgs,
        Func<IMenuVisibilityEventArgs, bool> subscriptionFilter);
}