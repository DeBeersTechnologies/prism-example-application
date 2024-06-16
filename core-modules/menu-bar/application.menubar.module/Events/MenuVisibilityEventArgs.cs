using System;
using Prism.Events;

namespace application.menubar.Events;

internal class MenuVisibilityEventArgs : IMenuVisibilityEventArgs, IMenuVisibilityEventPublisher, IMenuVisibilityEventSubscriber

{
    public string AllMenuItems => "_1ED3C8AB-18ED-47BB-B3C7-5D208F78FA89";

    public MenuVisibilityEventArgs(IEventAggregator eventAggregator)
        => SetMenuVisibilityEvent = eventAggregator.GetEvent<SetMenuItemVisibilityEvent>();

    private MenuVisibilityEventArgs(string menuName, bool isVisible)
    {
        MenuName = menuName;
        IsMenuVisible = isVisible;
    }

    private MenuVisibilityEventArgs(bool isVisible)
        => IsMenuVisible = isVisible;

    public SetMenuItemVisibilityEvent SetMenuVisibilityEvent { get; }

    public string OwningModuleName { get; set; }

    public void SetVisibilityForModuleMenuItems(string moduleName, bool isVisible)
    {
        if (string.IsNullOrWhiteSpace(moduleName)) return;
        SetMenuVisibilityEvent?.Publish(new MenuVisibilityEventArgs(isVisible) { OwningModuleName = moduleName });
    }

    public void SetVisibilityForSpecificMenuItem(string menuItemName, bool isVisible)
    {
        if (string.IsNullOrWhiteSpace(menuItemName)) return;
        SetMenuVisibilityEvent?.Publish(new MenuVisibilityEventArgs(menuItemName, isVisible));
    }

    public void SetVisibilityForAllMenuItems(bool isVisible)
        => SetMenuVisibilityEvent?.Publish(new MenuVisibilityEventArgs(AllMenuItems, isVisible));

    public bool IsMenuVisible { get; }
    public string MenuName { get; }
    public void SetEventSubscription(Action<IMenuVisibilityEventArgs> eventArgs, Func<IMenuVisibilityEventArgs, bool> subscriptionFilter)
        => SetMenuVisibilityEvent?.Subscribe(eventArgs, ThreadOption.UIThread, true, subscriptionFilter.Invoke);
}