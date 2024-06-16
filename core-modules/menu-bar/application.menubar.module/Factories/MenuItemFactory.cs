using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace application.menubar.Factories;

internal class MenuItemFactory : IMenuItemFactory
{
    private readonly Func<MenuItem> _newMenuItem;
    private readonly Func<Separator> _newMenuSeparator;

    public MenuItemFactory(Func<MenuItem> menuItemFactoryMethod, Func<Separator> separatorFactory)
    {
        _newMenuItem = menuItemFactoryMethod;
        _newMenuSeparator = separatorFactory;
    }

    public MenuItem CreateMenuItemFromViewModel(IMenuItemViewModel vm)
    {
        var menuItem = _newMenuItem();

        menuItem.DataContext = vm;
        menuItem.Command = vm.Command;
        menuItem.CommandParameter = vm.CommandParameter;
        menuItem.Header = vm.Header;
        menuItem.Name = GetValidMenuNameFromGuid(vm.Name);
        menuItem.ItemsSource = vm.MenuItems;
        menuItem.Icon = vm.Icon;
        menuItem.ToolTip = vm.ToolTip;

        BindingOperations.SetBinding(menuItem, UIElement.VisibilityProperty, new Binding("Visibility") { Source = vm });

        return menuItem;
    }

    public Separator CreateMenuSeparator()
        => _newMenuSeparator();


    public string GetValidMenuNameFromGuid(string guid)
        => $"_{guid.Replace("}", "").Replace("{", "").Replace("-", "_")}";
}