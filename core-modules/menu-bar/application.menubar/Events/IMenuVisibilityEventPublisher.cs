namespace application.menubar.Events;

public interface IMenuVisibilityEventPublisher
{
    void SetVisibilityForAllMenuItems(bool isVisible);
    void SetVisibilityForModuleMenuItems(string moduleName, bool isVisible);
    void SetVisibilityForSpecificMenuItem(string menuItemName, bool isVisible);
}