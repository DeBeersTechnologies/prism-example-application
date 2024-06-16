using System;

namespace application.menubar.Services;

public class MenuService : IMenuServiceInterpreter, IMenuService
{
    public event Action DisplayAllMenus;
    public event Action<string> DisplayModuleMenus;
    public event Action<string> DisplayThisMenu;
    public event Action HideAllMenus;
    public event Action<string> HideModuleMenus;
    public event Action<string> HideThisMenu;

    public void DisplayAllMenuItems()
        => DisplayAllMenus?.Invoke();

    public void DisplayMenuItemsForModule(string moduleName)
        => DisplayModuleMenus?.Invoke(moduleName);

    public void DisplayThisMenuItem(string menuName)
        => DisplayThisMenu?.Invoke(menuName);

    public void HideAllMenuItems()
        => HideAllMenus?.Invoke();

    public void HideMenuItemsForModule(string moduleName)
        => HideModuleMenus?.Invoke(moduleName);

    public void HideThisMenuItem(string menuName)
        => HideThisMenu?.Invoke(menuName);
}