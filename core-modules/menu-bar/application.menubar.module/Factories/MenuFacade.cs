using System;
using System.Windows.Controls;

namespace application.menubar.Factories;

internal class MenuFacade : IMenuFacade
{
    public MenuFacade(IMenuItemFactory menuItemFactory, IMenuItemViewModelFactory viewModelFactory)
    {
        CreateMenuItemFromViewModel = menuItemFactory.CreateMenuItemFromViewModel;
        CreateMenuSeparator = menuItemFactory.CreateMenuSeparator;
        CreateMenuViewModel = viewModelFactory.CreateMenuViewModel;
        GetValidMenuNameFromGuid = menuItemFactory.GetValidMenuNameFromGuid;
    }

    public Func<IMenuItemViewModel, MenuItem> CreateMenuItemFromViewModel { get; }

    public Func<Separator> CreateMenuSeparator { get; }

    public Func<IMenuInfo, IMenuItemViewModel> CreateMenuViewModel { get; }

    public Func<string, string> GetValidMenuNameFromGuid { get; }
}