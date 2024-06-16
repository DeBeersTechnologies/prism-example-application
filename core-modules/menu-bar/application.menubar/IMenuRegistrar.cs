namespace application.menubar;

public interface IMenuRegistrar
{
    event Action<IMenuInfo> MenuItemAdded;
    void AddMenuItem(IMenuInfo newMenu);
}