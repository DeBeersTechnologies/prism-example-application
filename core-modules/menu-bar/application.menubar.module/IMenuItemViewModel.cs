using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace application.menubar;

internal interface IMenuItemViewModel
{
    string Header { get; }
    string Name { get; }

    string ParentName { get; }

    dynamic Icon { get; }

    string ToolTip { get; }

    Visibility Visibility { get; }

    ObservableCollection<object> MenuItems { get; }

    ICommand Command { get; }
    object CommandParameter { get; }
}