using System;

namespace application.menubar.Services;

internal class MenuRegistrar : IMenuRegistrar
{
    public event Action<IMenuInfo> MenuItemAdded;

    public void AddMenuItem(IMenuInfo newMenu)
        => MenuItemAdded?.Invoke(newMenu);
}