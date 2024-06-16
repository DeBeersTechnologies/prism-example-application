using System;

namespace application.menubar.Events;

internal class MenuVisibilityEventFacade : IMenuVisibilityEventFacade
{
    public MenuVisibilityEventFacade(IMenuVisibilityEventPublisher publisher, IMenuVisibilityEventSubscriber subscriber)
    {
        SetEventSubscription = subscriber.SetEventSubscription;
        SetVisibilityForAllMenuItems = publisher.SetVisibilityForAllMenuItems;
        SetVisibilityForSpecificMenuItem = publisher.SetVisibilityForSpecificMenuItem;
        SetVisibilityForModuleMenuItems = publisher.SetVisibilityForModuleMenuItems;
    }

    public Action<Action<IMenuVisibilityEventArgs>, Func<IMenuVisibilityEventArgs, bool>> SetEventSubscription { get; }

    public Action<bool> SetVisibilityForAllMenuItems { get; }

    public Action<string, bool> SetVisibilityForModuleMenuItems { get; }

    public Action<string, bool> SetVisibilityForSpecificMenuItem { get; }
}