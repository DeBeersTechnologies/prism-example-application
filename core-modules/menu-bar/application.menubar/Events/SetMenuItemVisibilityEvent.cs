using Prism.Events;

namespace application.menubar.Events;

public class SetMenuItemVisibilityEvent : PubSubEvent<IMenuVisibilityEventArgs> { }