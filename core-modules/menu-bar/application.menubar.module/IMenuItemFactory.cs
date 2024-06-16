using System.Windows.Controls;

namespace application.menubar;

internal interface IMenuItemFactory
{
    MenuItem CreateMenuItemFromViewModel(IMenuItemViewModel vm);
    Separator CreateMenuSeparator();
    string GetValidMenuNameFromGuid(string guid);
}