namespace application.menubar.Events;

public interface IMenuVisibilityEventFacade
{
    Action<Action<IMenuVisibilityEventArgs>, Func<IMenuVisibilityEventArgs, bool>> SetEventSubscription { get; }
    Action<bool> SetVisibilityForAllMenuItems { get; }
    Action<string, bool> SetVisibilityForModuleMenuItems { get; }
    Action<string, bool> SetVisibilityForSpecificMenuItem { get; }
}