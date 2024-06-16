namespace application.menubar;

public interface IMenuServiceInterpreter
{
    event Action DisplayAllMenus;
    event Action<string> DisplayModuleMenus;
    event Action<string> DisplayThisMenu;
    event Action HideAllMenus;
    event Action<string> HideModuleMenus;
    event Action<string> HideThisMenu;
}